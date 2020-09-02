using Entities;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace DL
{
    public class DonorsDL : IDonorsDL
    {
        DataAccess _Connection;

        public DonorsDL(DataAccess Connection)
        {
            _Connection = Connection;
            _Connection.Sheet = "Donors";
            _Connection.Connect();
        }
        public List<DonorsList> GetAllDonors()
        {
            _Connection.Sheet = "Donors";
            var range = $"{_Connection.Sheet}!A2:B";
            SpreadsheetsResource.ValuesResource.GetRequest request =
                _Connection.Service.Spreadsheets.Values.Get(_Connection.SpreadsheetId, range);
            var response = request.Execute();
            IList<IList<object>> values = response.Values;
            List<DonorsList> AllDonors = new List<DonorsList>();
            if (values != null && values.Count > 0)
            {
                foreach (var row in values)
                {
                    if (row.Count != 0)
                    {
                        DonorsList donor = new DonorsList();
                        if (row[0].ToString() != "")
                            donor.DonorsID = Int32.Parse(row[0].ToString());
                        if (row.Count > 1)
                            donor.DonorsFullName = row[1].ToString();
                        AllDonors.Add(donor);
                    }
                }
            }
            return AllDonors;
        }
        public Boolean PostDonor(Donors Donor)
        {
            _Connection.Sheet = "Donors";
            var range = $"{_Connection.Sheet}!A:E";
            var valueRange = new ValueRange();
            var oblist = new List<object>() {
                Donor.ID,
                Donor.FullName,
                Donor.FirstName,
                Donor.LastName,
                Donor.ConnectionID,
                Donor.Gender,
                Donor.AgeGroup,
                Donor.Email,
                Donor.PhoneNumber,
                Donor.Country,
                Donor.City,
                Donor.NativeLanguage,
                Donor.TotalDonation,
                Donor.LastDonation,
                Donor.VIP,
                Donor.AnashIsrael,
                Donor.AnashUSA,
                Donor.PinskSchoolGraduate,
                Donor.KievSchoolGraduate,
                Donor.YeshivaGraduate,
                Donor.InPinsk,
                Donor.BusinessAssociate,
                Donor.BoysCounselor,
                Donor.GirlsCounselor,
                Donor.HelpedByPinsk,
                Donor.GeneralSupporter,
                Donor.MHSG,
                Donor.BelarusAnsectors,
                Donor.BelarusTourism,
                Donor.YYFundraiser,
                Donor.YYFamily,
                Donor.YYStaff,
                Donor.RShteiermanFamily,
                Donor.RFimaFamily,
                Donor.MarriedAYYGraduate,
                Donor.YearsInYadYisroel,
                Donor.Other };
            valueRange.Values = new List<IList<object>> { oblist };
            var appendRequest = _Connection.Service.Spreadsheets.Values.Append(valueRange, _Connection.SpreadsheetId, range);
            appendRequest.ValueInputOption = SpreadsheetsResource.ValuesResource.AppendRequest.ValueInputOptionEnum.USERENTERED;
            var appendReponse = appendRequest.Execute();
            return true;
        }

        
    }
}
