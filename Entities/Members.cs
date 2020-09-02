using System;
using System.Collections.Generic;
using System.Text;

namespace Entities
{
    public class Members
    {
        public string ID { get; set; }
        public string FullName { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool Confirmed { get; set; }
        public string Gender { get; set; }
        public string AgeGroup { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Country { get; set; }
        public string City { get; set; }

        public int TotalDonations { get; set; }
        public int TotalCallsMade { get; set; }

        public bool FluentHebrew { get; set; }
        public bool FluentRussian { get; set; }
        public bool FluentEnglish { get; set; }
        public bool AnashIsrael { get; set; }
        public bool AnashUSA { get; set; }
        public bool PinskSchoolGraduate { get; set; }
        public bool KievSchoolGraduate { get; set; }
        public bool YeshivaGraduate { get; set; }
        public bool InPinsk { get; set; }
        public bool BusinessAssociate { get; set; }
        public bool BoysCounselor { get; set; }
        public bool GirlsCounselor { get; set; }
        public bool HelpedByPinsk { get; set; }
        public bool GeneralSupporter { get; set; }
        public bool MHSG { get; set; }
        public bool BelarusAnsectors { get; set; }
        public bool BelarusTourism { get; set; }
        public bool YYFundraiser { get; set; }
        public bool YYFamily { get; set; }
        public bool YYStaff { get; set; }
        public bool RShteiermanFamily { get; set; }
        public bool RFimaFamily { get; set; }
        public bool MarriedAYYGraduate { get; set; }
        public int YearsInYadYisroel { get; set; }
        public int YearsIAsFundraiser { get; set; }
        public string OperationRoom { get; set; }
        public string Other { get; set; }

    }
}
