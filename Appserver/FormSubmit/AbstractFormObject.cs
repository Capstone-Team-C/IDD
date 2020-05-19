using Appserver.TextractDocument;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

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
        // For now, assume TimesheetForm

        AbstractFormObject form;
        switch (formType)
        {
            case FormType.OR507_RELIEF:
                form = new TimesheetForm();
                break;
            default:
                throw new ArgumentException();
        }
        return form.FromTextract(doc);
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

    protected abstract AbstractFormObject FromTextract(TextractDocument doc);
}