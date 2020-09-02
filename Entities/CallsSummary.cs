using System;
using System.Collections.Generic;
using System.Text;

namespace Entities
{
    public class CallsSummary
    {
        public string PhoneCallID { get; set; }
        public string TimeDate { get; set; }
        public string Charity { get; set; }
        public string Location { get; set; }
        public string DID { get; set; }
        public string DFullName { get; set; }
        public string FID { get; set; }
        public string FFullName { get; set; }
        public string CallRating { get; set; }
        public int Donation { get; set; }
        public string Other { get; set; }
    }
}
