using Appserver.TextractDocument;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

public abstract class AbstractFormObject{
    public static AbstractFormObject FromTextract(TextractDocument doc)
    {
        // Here we'll Determine the type of object (timesheet or mileage form) and then
        // return the correct type.
        // For now, assume TimesheetForm

        TimesheetForm t = new TimesheetForm();

        // Grab the first page and make sure it is the front
        if( doc.PageCount() < 2)
        {
            throw new ArgumentException();
        }

        // Do a silly assignment because C# won't let me assign the variable in the foreach loop instead
        // and there is no default constructor
        Page frontpage = doc.GetPage(0);
        bool frontfound = false;
        List<Page> backpages = new List<Page>();

        foreach( var page in doc.Pages)
        {
            if (page.GetTables().Count >= 1)
            {
                frontpage = page;
                frontfound = true;
            }
            else
            {
                backpages.Add(page);
            }
        }

        if( !frontfound )
        {
            throw new ArgumentException();
        }

        var formitems = frontpage.GetFormItems();

        // Top Form Information
        
        t.clientName        = formitems[0].Value.ToString(); // Customer Name 
        t.prime             = formitems[1].Value.ToString(); // Prime 
        t.providerName      = formitems[2].Value.ToString(); // Provider Name 
        t.providerNum       = formitems[3].Value.ToString(); // Provider Num 
        t.brokerage         = formitems[4].Value.ToString(); // CM Organization
        t.scpaName          = formitems[5].Value.ToString(); // SC/PA Name
        t.serviceAuthorized = formitems[6].Value.ToString(); // Service

        // Table
        var tables = frontpage.GetTables();
        if(tables.Count == 0)
        {
            Console.WriteLine("No Table Information");
            return t;
        }

        var table = tables[0].GetTable();
        // Remove first row
        table.RemoveAt(0);

        // Grab last row for total
        var lastrow = table.Last();
        // Now remove it
        table.RemoveAt(table.Count - 1);

        foreach( var row in table)
        {
            t.addTimeRow(row[0].ToString(), row[1].ToString(), row[2].ToString(), ConvertHours(row[3].ToString()).ToString(), ConvertInt(row[4].ToString()).ToString());
        }

        if (lastrow.Count > 3)
        {
            try
            {
                t.totalHours = lastrow[2].ToString();
            }catch(FormatException)
            {
                t.totalHours = "0";
            }
        }

        // Populate back form objects
        formitems = backpages[0].GetFormItems();

        t.serviceGoal = formitems[6].Value.ToString();
        t.progressNotes = formitems[7].Value.ToString();
        t.employerSignDate = formitems[8].Value.ToString();
        t.employerSignature = !string.IsNullOrEmpty(t.employerSignDate);
        t.providerSignDate = formitems[10].Value.ToString();
        t.providerSignature = !string.IsNullOrEmpty(t.providerSignDate);
        // t.authorization

        return t;
    }
    public static DateTime ConvertDate( string s )
    {
        return DateTime.Now;
    }
    public static float ConvertHours( string s )
    {
        // Try to read in only the number portion
        string num = "";
        bool encounteredDecimal = false;
        bool invalid = false;
        foreach( var c in s.ToCharArray())
        {
            switch (c)
            {
                case '0':
                case '1':
                case '2':
                case '3':
                case '4':
                case '5':
                case '6':
                case '7':
                case '8':
                case '9':
                    num += c;
                    break;
                case '.':
                    if (encounteredDecimal)
                    {
                        invalid = true;
                        break;
                    }
                    encounteredDecimal = true;
                    num += c;
                    break;
                default:
                    invalid = true;
                    break;
            }
            // If we've encountered a second decimal then we break out of loop
            if (invalid) break;
        }
        if (num.Length == 0)
            return 0;
        return int.Parse(num);
    }

    public static int ConvertInt( string s)
    {
        s = s.ToLower();
        if (s == "y" || s == "yes" )
            return 1;
        return 0;
    }
}