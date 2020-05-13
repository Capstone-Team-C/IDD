﻿using System.Collections.Generic;
using Common.Models;
namespace AdminUI.Models
{
    public class HomeModel
    {
        public IList<Submission> Submissions;
        public int TotalPages;
        public int TotalSubmissions;
        public int PerPage;
        public int Page;

        //keep track of the current sort order
        public string SortOrder;

        //keep track of current filters
        public string PName; 
        public string CName;
        public string Prime;
        public string DateFrom;
        public string DateTo;
        public string ProviderId;
        public string Status;
        public string FormType;
    }
}
