class TimesheetRowItem : AbstractFormObject
{
    public string date="2000-01-01";
    public string starttime="9:00";
    public string endtime="10:00";
    public float totalHours=12;
    public int numClient;
    
    public TimesheetRowItem( string date, string start, string end, float totalHours, int numClient)
    {
        this.date = date;
        this.starttime = start;
        this.endtime = end;
        this.totalHours = totalHours;
        this.numClient = numClient;
    }
}