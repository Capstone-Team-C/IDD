using System;
using Newtonsoft.Json.Linq;
using Common.Models;
using Common.Data;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Collections;
using Appserver.Data;
using Microsoft.EntityFrameworkCore.SqlServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace IDD
{
    public class FormToDbUtil
    {
        private SubmissionContext _scontext;
        private SubmissionStagingContext _sscontext;

        public FormToDbUtil(SubmissionContext context, SubmissionStagingContext sscontext)
        {
            _scontext = context;
            _sscontext = sscontext;
        }


        // Use EF core to send data to DB
        public int TimesheetEFtoDB(Timesheet ts)
        {
            _scontext.Add(ts);
            _scontext.SaveChanges();
            _sscontext.Remove(ts.Id);
            _sscontext.SaveChanges();
            return ts.Id;
        }


        // Given a timesheet and a timesheetid referencing an existing timesheet
        // submission in the DB, insert the TimeEntry(s) in the TimeSheet. Get
        // back the number of records inserted.
        public int TimesheetEntriesToDB(Timesheet ts, int tsid)
        {
            string conn_str = Environment.GetEnvironmentVariable("test-db-conn-str");
            int totalInserts = 0;
            // Create connection for each entry
            // TODO bulk insert option? executemany?
            // FIXME add try/catch
            foreach (TimeEntry te in ts.TimeEntries)
            {

                string query = "INSERT INTO [dbo].[TimeEntry] ([Date],[Group],[Status],[In],[Out],[Hours],[TimesheetId]) ";
                query += "VALUES(@date, @group, @status, @in, @out, @hours, @timesheetid) SELECT SCOPE_IDENTITY();";

                SqlConnection conn = new SqlConnection(conn_str);
                SqlCommand command = null;
                SqlDataReader reader = null;

                System.Console.WriteLine("ENTRY SQL: " + query);
                command = new SqlCommand(query, conn);
                command.Parameters.Add("@date", System.Data.SqlDbType.DateTime2);
                command.Parameters["@date"].Value = te.Date;

                command.Parameters.Add("@group", System.Data.SqlDbType.Bit);
                command.Parameters["@group"].Value = 1;

                command.Parameters.Add("@status", System.Data.SqlDbType.NVarChar, 25);
                command.Parameters["@status"].Value = te.Status;

                command.Parameters.Add("@in", System.Data.SqlDbType.DateTime2);
                command.Parameters["@in"].Value = te.In;

                command.Parameters.Add("@out", System.Data.SqlDbType.DateTime2);
                command.Parameters["@out"].Value = te.Out;

                command.Parameters.Add("@hours", System.Data.SqlDbType.Float);
                command.Parameters["@hours"].Value = te.Hours;

                command.Parameters.Add("@timesheetid", System.Data.SqlDbType.Int);
                command.Parameters["@timesheetid"].Value = tsid;

                conn.Open();

                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    decimal id = reader.GetDecimal(0);
                    if (id > 0)
                    {
                        totalInserts += 1;
                    }
                }
                reader.Close();
            }
            return totalInserts;
        }

        // Give a timesheetform obj, get back a partially populated timesheet obj.
        // TODO fix UriString, confirm vals for // marks
        public Timesheet PopulateTimesheet(TimesheetForm tsf, Timesheet tsheet=null)
        {
            if(tsheet == null){tsheet = new Timesheet();}

            tsheet.ClientName = tsf.clientName;
            tsheet.ClientPrime = tsf.prime;
            tsheet.ProviderName = tsf.providerName;
            tsheet.ProviderId = tsf.providerNum;
            tsheet.ServiceGoal = tsf.serviceGoal;
            tsheet.ProgressNotes = tsf.progressNotes;
            tsheet.FormType = tsf.serviceAuthorized;
            tsheet.RejectionReason = ""; //
            tsheet.Submitted = DateTime.Now; //
            tsheet.LockInfo = null;
            tsheet.UserActivity = ""; //
            
            tsheet.UriString = _sscontext.Stagings.Find(tsf.id).UriString;

            return tsheet;
        }

        // PWA to TimesheetForm converter
        public TimesheetForm PWAtoTimesheetFormConverter(PWAsubmission pwasub)
        {
            var convUtil = new FormConversionUtils();
            TimesheetForm tsf = new TimesheetForm();
            List<TimesheetRowItem> tsl = new List<TimesheetRowItem>();
            tsf.clientName = pwasub.clientName.value;
            tsf.prime = pwasub.prime.value;
            tsf.providerName = pwasub.providerName.value;
            tsf.providerNum = pwasub.providerNum.value;
            tsf.brokerage = pwasub.brokerage.value;
            tsf.scpaName = pwasub.scpaName.value;
            tsf.serviceAuthorized = pwasub.serviceAuthorized.value;
            tsf.serviceGoal = pwasub.serviceGoal.value;
            tsf.progressNotes = pwasub.progressNotes.value;
            tsf.employerSignature = convUtil.PWABoolConverter(pwasub.employerSignature.value);
            tsf.employerSignDate = pwasub.employerSignDate.value;
            tsf.providerSignature = convUtil.PWABoolConverter(pwasub.providerSignature.value);
            tsf.providerSignDate = pwasub.providerSignDate.value;
            tsf.authorization = convUtil.PWABoolConverter(pwasub.authorization.value);
            tsf.id = pwasub.id;


            foreach(PWAtimesheetVals lsv in pwasub.timesheet.value)
            {
                string s = lsv.totalHours.Replace(':', '.');
                tsf.addTimeRow(lsv.date, lsv.starttime, lsv.endtime, s, "true");
            }

            return tsf;
        }

        // Convert the timesheet form row items into timesheet time entries. Makes
        // certain assumptions about start times, end times, and group. 
        public void PopulateTimesheetEntries(TimesheetForm tsf, Timesheet tsheet)
        {
            double totalHours = 0;
            var tl = new List<TimeEntry>();
            var convUtil = new FormConversionUtils();

            foreach (TimesheetRowItem tsri in tsf.Times)
            {
                var x = new TimeEntry();
                try
                {
                    x.Date = Convert.ToDateTime(tsri.date);
                }
                catch (FormatException)
                {
                    x.Date = DateTime.Now;
                }
                try
                {
                    x.Hours = float.Parse(tsri.totalHours);
                }
                catch (FormatException)
                {
                    x.Hours = 0;
                }
                totalHours += x.Hours;

                // Assume Group field is 'N'
                x.Group = true;

                // Assume starttime is AM, pad with leading zero if necessary
                string sdf = convUtil.TimeFormatterPadding(tsri.starttime);
                string sd;
                if (!sdf.Contains("AM"))
                {
                    sd = tsri.date + " " + sdf + " AM";
                }
                else
                {
                    sd = tsri.date + " " + sdf;

                }
                try
                {
                    x.In = DateTime.ParseExact(sd, "yyyy-MM-dd HH:mm tt", null);
                }
                catch ( FormatException) 
                {
                    x.In = DateTime.Now;
                }

                // Assume endtime is PM, convert to 24hr.
                string edf = convUtil.TimeFormatter24(tsri.endtime);
                string ed;
                if (!sdf.Contains("AM"))
                {
                    ed = tsri.date + " " + edf + " PM";
                }
                else
                {
                    ed = tsri.date + " " + edf;

                }
                try
                {
                    x.Out = DateTime.ParseExact(ed, "yyyy-MM-dd HH:mm tt", null);
                }
                catch (FormatException)
                {
                    x.Out = DateTime.Now;
                }

                tl.Add(x);
            }

            tsheet.TotalHours = totalHours;
            tsheet.TimeEntries = tl;   
        }

    }
}
