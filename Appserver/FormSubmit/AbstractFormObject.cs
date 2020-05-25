using Appserver.FormSubmit;
using Appserver.TextractDocument;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;

public abstract class AbstractFormObject{

    /*******************************************************************************
    /// Enums
    *******************************************************************************/
    public enum FormType
    {
        OR004_MILEAGE = 1,
        OR507_RELIEF = 2,
        OR526_ATTENDANT = 3,
    }

    /*******************************************************************************
    /// Fields
    *******************************************************************************/
    /// Front Fields
    public int id { get; set; }
    public string clientName { get; set; }
    public string prime { get; set; }
    public string providerName { get; set; }
    public string providerNum { get; set; }
    public string brokerage { get; set; }
    public string scpaName { get; set; }
    public string serviceAuthorized { get; set; }
    /// Back Fields
    public string serviceGoal { get; set; }
    public string progressNotes { get; set; }
    public bool employerSignature { get; set; }
    public string employerSignDate { get; set; }
    public bool providerSignature { get; set; }
    public string providerSignDate { get; set; }
    public bool authorization { get; set; }
    public bool approval { get; set; }
    public string review_status { get; set; } = "Pending";

    /*******************************************************************************
    /// Static Methods
    *******************************************************************************/

    public static AbstractFormObject FromTextract(TextractDocument doc, FormType formType)
    {
        // Here we'll Determine the type of object (timesheet or mileage form) and then
        // return the correct type.

        // Grab the first page and make sure it is the front
        if (doc.PageCount() < 2)
        {
            throw new ArgumentException();
        }

        AbstractFormObject form;
        switch (formType)
        {
            case FormType.OR526_ATTENDANT:
            case FormType.OR507_RELIEF:
                form = new TimesheetForm();
                break;
            case FormType.OR004_MILEAGE:
                form = new MileageForm();
                break;
            default:
                throw new ArgumentException();
        }
        // Do a silly assignment because C# won't let me assign the variable in the foreach loop instead
        // and there is no default constructor
        Page frontpage = doc.GetPage(0);
        bool frontfound = false;
        List<Page> backpages = new List<Page>();

        // Improve front page detection
        foreach (var page in doc.Pages)
        {
            if (!frontfound)
            {
                // Search for Service Delivered On:
                foreach (var line in page.GetLines())
                {
                    // Ever form has "Service Delivered On:" on the front page, so we use
                    // this to determine if this is the front or back.
                    frontfound = line.ToString().Contains("vice Delivered O");
                    if (frontfound)
                        break;
                }
                if (frontfound)
                {
                    frontpage = page;
                }
                else
                {
                    backpages.Add(page);
                }
            }
            else
            {
                backpages.Add(page);
            }
        }

        if (!frontfound)
        {
            throw new ArgumentException();
        }
        var formitems = frontpage.GetFormItems();

        // Top Form Information

        form.clientName = formitems[0].Value.ToString().Trim(); // Customer Name 
        form.prime = formitems[1].Value.ToString().Trim(); // Prime 
        form.providerName = formitems[2].Value.ToString().Trim(); // Provider Name 
        form.providerNum = formitems[3].Value.ToString().Trim(); // Provider Num 
        form.brokerage = formitems[4].Value.ToString().Trim(); // CM Organization
        form.scpaName = formitems[5].Value.ToString().Trim(); // SC/PA Name
        form.serviceAuthorized = formitems[6].Value.ToString().Trim(); // Service
        
        // Table
        var tables = frontpage.GetTables();
        if (tables.Count == 0)
        {
            Console.WriteLine("No Table Information");
            return form;
        }
        form.AddTables(tables);
        // Populate back form objects
        form.AddBackForm(backpages[0]);

        return form;
    }
    public static string FixHours( string s )
    {
        return s.Replace(".", ":");
    }

    public static int ConvertInt( string s)
    {
        s = s.ToLower();
        if (s == "y" || s == "yes" )
            return 1;
        return 0;
    }

    public static string ConvertDate(string s)
    {
        string date;
        try
        {
            return DateTime.Parse(s).ToString("yyyy-MM-dd");
        }
        catch (FormatException)
        {
            return s;
        }
    }

    public static int minimum(params int[] rest)
    {
        int min = int.MaxValue;
        foreach( var y in rest)
        {
            if (y < min)
                min = y;
        }
        return min;
    }
    public static int LevenshteinDistance(string s, string t)
    {
        //
        
        // Initialize rows
        List<int> v0 = new List<int>(Enumerable.Range(0, t.Length+1).ToList<int>());
        List<int> v1 = new List<int>(Enumerable.Repeat(0,t.Length+1).ToList<int>());

        for ( int i = 0; i < s.Length; ++i)
        {
            // Calculate distances to v1, first column is number of deletions compared
            // to an empty string

            v1[0] = i + 1;

            for( int j = 0; j < t.Length; ++j)
            {
                int sub = s[i] == t[j] ? v0[j] : v0[j] + 1;
                v1[j + 1] = minimum( v0[j + 1] + 1, v1[j] + 1, s[i] == t[j] ? v0[j] : v0[j] + 1);
            }

            // Copy current row to previous row
            // No need to swap as v1 is invalidated
            v0 = new List<int>(v1);
        }

        return v0[t.Length];
    }

    public static double NGLD(string s, string t)
    {
        double gld = LevenshteinDistance(s, t);
        return (2.0 * gld/((double)(s.Length + t.Length + gld)));
    }
    public static List<KeyValuePair<string,string>> MatchKeyValuePairs(List<string> keys, List<string> values)
    {
        var matches = new List<KeyValuePair<string, string>>(keys.Count);

        // NGLD forms a metric on the space which means that NGLD(X,Y)==NGLD(Y,X)

        // For each key we want to find minimum along the row
        // First create distance matrix
        List<List<double>> matrix = new List<List<double>>(keys.Count);
        for(int i = 0; i < keys.Count; ++i)
        {
            matrix.Add(new List<double>(Enumerable.Repeat(double.MaxValue, values.Count)));
        }

        // Now calculate distances
        for( int i = 0; i < keys.Count; ++i)
        {
            // Track minimum index
            int minIndex = 0;
            for( int j = 0; j < values.Count; ++j)
            {
                matrix[i][j] = NGLD(keys[i], values[j]);
                if (matrix[i][j] < matrix[i][minIndex])
                    minIndex = j;
            }

            // Add to matches
            matches.Add(new KeyValuePair<string, string>(keys[i], values[minIndex]));
        }

        return matches;
    }

    protected abstract void AddTables(List<Table> tables);
    protected abstract void AddBackForm(Page page);
}
