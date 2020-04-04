class TimesheetRowItem : AbstractFormObject
{
    public int date=1;
    public int starttime=100;
    public int endtime=200;
    public bool am=true;
    
    public TimesheetRowItem( int date, int start, int end, bool am)
    {
        this.date = date;
        this.starttime = start;
        this.endtime = end;
        this.am = am;
    }

    public override string ToJSON(){
        return "";
    }
}