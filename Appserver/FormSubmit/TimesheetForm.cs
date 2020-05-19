using Newtonsoft.Json;
using System.Collections.Generic;
using Appserver.TextractDocument;
using System;
using System.Linq;

public class TimesheetForm: AbstractFormObject{
    
    private List<TimesheetRowItem> times = new List<TimesheetRowItem>();

    public TimesheetForm(){
    }

    public int units { get; set; }
    public string type { get; set; }
    public string frequency { get; set; }
    
    [JsonProperty("timesheet")]
    [JsonConverter(typeof(TimesheetRowConverter))]
    internal List<TimesheetRowItem> Times { get => times; set => times = value; }
    public string totalHours { get; set; }
    
    public void addTimeRow(string date, string start, string end, string total, string group) => 
        this.Times.Add(new TimesheetRowItem(date, start, end, total, group));

    protected override AbstractFormObject FromTextract(TextractDocument doc)
    {
        // Grab the first page and make sure it is the front
        if (doc.PageCount() < 2)
        {
            throw new ArgumentException();
        }

        // Do a silly assignment because C# won't let me assign the variable in the foreach loop instead
        // and there is no default constructor
        Page frontpage = doc.GetPage(0);
        bool frontfound = false;
        List<Page> backpages = new List<Page>();

        foreach (var page in doc.Pages)
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

        if (!frontfound)
        {
            throw new ArgumentException();
        }

        var formitems = frontpage.GetFormItems();

        // Top Form Information

        clientName        = formitems[0].Value.ToString().Trim(); // Customer Name 
        prime             = formitems[1].Value.ToString().Trim(); // Prime 
        providerName      = formitems[2].Value.ToString().Trim(); // Provider Name 
        providerNum       = formitems[3].Value.ToString().Trim(); // Provider Num 
        brokerage         = formitems[4].Value.ToString().Trim(); // CM Organization
        scpaName          = formitems[5].Value.ToString().Trim(); // SC/PA Name
        serviceAuthorized = formitems[6].Value.ToString().Trim(); // Service

        // Table
        var tables = frontpage.GetTables();
        if (tables.Count == 0)
        {
            Console.WriteLine("No Table Information");
            return this;
        }

        var table = tables[0].GetTable();
        // Remove first row
        table.RemoveAt(0);

        // Grab last row for total
        var lastrow = table.Last();
        // Now remove it
        table.RemoveAt(table.Count - 1);

        foreach (var row in table)
        {
            addTimeRow(row[0].ToString().Trim(),
              FixHours(row[1].ToString()).ToString().Trim(),
              FixHours(row[2].ToString()).ToString().Trim(),
              FixHours(row[3].ToString()).ToString().Trim(),
              ConvertInt(row[4].ToString()).ToString().Trim());
        }

        if (lastrow.Count > 3)
        {
            try
            {
                totalHours = FixHours(lastrow[2].ToString()).Trim();
            }
            catch (FormatException)
            {
                totalHours = lastrow[2].ToString();
            }
        }

        // Populate back form objects
        formitems = backpages[0].GetFormItems();

        serviceGoal       = formitems[6].Value.ToString().Trim();
        progressNotes     = formitems[7].Value.ToString().Trim();
        employerSignDate  = formitems[8].Value.ToString().Trim();
        employerSignature = !string.IsNullOrEmpty(employerSignDate);
        providerSignDate  = formitems[10].Value.ToString().Trim();
        providerSignature = !string.IsNullOrEmpty(providerSignDate);

        return this;
    }
}