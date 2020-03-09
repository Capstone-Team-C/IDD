using Newtonsoft.Json;
using System.Collections.Generic;
class TimesheetForm: AbstractFormObject{
    private const string title = "Something Here";
    private List<TimesheetRowItem> times = new List<TimesheetRowItem>();

    public TimesheetForm(){
        this.Title = title;
    }

    public string Title {get; set;}
    
    [JsonProperty("timesheet")]
    [JsonConverter(typeof(TimesheetRowConverter))]
    internal List<TimesheetRowItem> Times { get => times; set => times = value; }

    public void addTimeRow(int date, int start, int end, bool am) => 
        this.Times.Add(new TimesheetRowItem(date, start, end, am));

    public override string ToJSON(){
        return JsonConvert.SerializeObject(this);
    }
}