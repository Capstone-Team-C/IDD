using Newtonsoft.Json;

class TimesheetForm: AbstractFormObject{
    private const string V = "Something Here";
    public string json= V;
    public override void print(){
        System.Console.Write("I'm a timesheet");
    }

    public override string ToJSON(){
        return JsonConvert.SerializeObject(this);
    }
}