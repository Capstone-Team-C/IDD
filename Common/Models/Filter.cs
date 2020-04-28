﻿using System;

namespace Common.Models
{
    public class Filter
    {
        public int Id { get; set; }
        public string FilterName { get; set; }
        public string ProviderName { get; set; }
        public string ProviderId { get; set; }
        public string ClientName { get; set; }
        public string ClientPrime { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public string Status { get; set; }
        public bool? Locked { get; set; }
    }
}
