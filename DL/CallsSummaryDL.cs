using Entities;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace DL
{
    public class CallsSummaryDL : ICallsSummaryDL
    {
        DataAccess _Connection;

        public CallsSummaryDL(DataAccess Connection)
        {
            _Connection = Connection;
            _Connection.Sheet = "CallsSummary";
            _Connection.Connect();
        }
        public void PostCallsSummary(CallsSummary cs)
        {
            _Connection.Sheet = "CallsSummary";
            var range = $"{_Connection.Sheet}!A:E";
            var valueRange = new ValueRange();
            Members CurrentMember = GetCurrentMember(cs.FID);
            Donors SelectedDonor = GetSelectedDonor(cs.DID);
            var oblist = new List<object>() {
                cs.PhoneCallID,
                cs.TimeDate,
                cs.Charity,
                cs.Location,
                cs.DID,
                cs.DFullName,
                cs.FID,
                cs.FFullName,
                cs.Donation,
                cs.CallRating,
                cs.Other,
                SelectedDonor.Gender,
                CurrentMember.Gender,
                SelectedDonor.AgeGroup,
                CurrentMember.AgeGroup,
                SelectedDonor.Country,
                CurrentMember.Country,
                SelectedDonor.City,
                CurrentMember.City,
                SelectedDonor.TotalDonation,
                CurrentMember.TotalDonations,
                SelectedDonor.LastDonation,
                CurrentMember.TotalCallsMade,
                SelectedDonor.VIP,
                SelectedDonor.NativeLanguage,
                CurrentMember.FluentHebrew,
                CurrentMember.FluentRussian,
                CurrentMember.FluentEnglish,
                SelectedDonor.AnashIsrael,
                CurrentMember.AnashIsrael,
                SelectedDonor.AnashUSA,
                CurrentMember.AnashUSA,
                SelectedDonor.PinskSchoolGraduate,
                CurrentMember.PinskSchoolGraduate,
                SelectedDonor.KievSchoolGraduate,
                CurrentMember.KievSchoolGraduate,
                SelectedDonor.YeshivaGraduate,
                CurrentMember.YeshivaGraduate,
                SelectedDonor.InPinsk,
                CurrentMember.InPinsk,
                SelectedDonor.BusinessAssociate,
                CurrentMember.BusinessAssociate,
                SelectedDonor.BoysCounselor,
                CurrentMember.BoysCounselor,
                SelectedDonor.GirlsCounselor,
                CurrentMember.GirlsCounselor,
                SelectedDonor.HelpedByPinsk,
                CurrentMember.HelpedByPinsk,
                SelectedDonor.GeneralSupporter,
                CurrentMember.GeneralSupporter,
                SelectedDonor.MHSG,
                CurrentMember.MHSG,
                SelectedDonor.BelarusAnsectors,
                CurrentMember.BelarusAnsectors,
                SelectedDonor.BelarusTourism,
                CurrentMember.BelarusTourism,
                SelectedDonor.YYFundraiser,
                CurrentMember.YYFundraiser,
                SelectedDonor.YYFamily,
                CurrentMember.YYFamily,
                SelectedDonor.YYStaff,
                CurrentMember.YYStaff,
                SelectedDonor.RShteiermanFamily,
                CurrentMember.RShteiermanFamily,
                SelectedDonor.RFimaFamily,
                CurrentMember.RFimaFamily,
                SelectedDonor.MarriedAYYGraduate,
                CurrentMember.MarriedAYYGraduate,
                SelectedDonor.YearsInYadYisroel,
                CurrentMember.YearsInYadYisroel,
                CurrentMember.YearsIAsFundraiser
            };
            valueRange.Values = new List<IList<object>> { oblist };
            var appendRequest = _Connection.Service.Spreadsheets.Values.Append(valueRange, _Connection.SpreadsheetId, range);
            appendRequest.ValueInputOption = SpreadsheetsResource.ValuesResource.AppendRequest.ValueInputOptionEnum.USERENTERED;
            var appendReponse = appendRequest.Execute();
            IncreaseTotalDonationAndCallsMade(cs.FID, cs.Donation);
            IncreaseTotalDonation(cs.DID, cs.Donation);
        }
        public Boolean IncreaseTotalDonation(string ID, int Donation)
        {

            _Connection.Sheet = "Donors";
            var rangeTD = $"{_Connection.Sheet}!M" + (int.Parse(ID) + 1).ToString();
            int PrevTotalDonation = int.Parse(_Connection.Service.Spreadsheets.Values.Get(_Connection.SpreadsheetId, rangeTD).Execute().Values[0][0].ToString());
            var oblist = new List<object>(){
                               (PrevTotalDonation + Donation),
                               (Donation),
            };
            var valueRange = new ValueRange();
            valueRange.Values = new List<IList<object>> { oblist };
            var updateRequest = _Connection.Service.Spreadsheets.Values.Update(valueRange, _Connection.SpreadsheetId, rangeTD);
            updateRequest.ValueInputOption = SpreadsheetsResource.ValuesResource.UpdateRequest.ValueInputOptionEnum.USERENTERED;
            updateRequest.Execute();
            return true;
        }
        public Boolean IncreaseTotalDonationAndCallsMade(string ID, int Donation)
        {
            _Connection.Sheet = "Fundraisers";
            var rangeTD = $"{_Connection.Sheet}!M" + (int.Parse(ID) + 1).ToString();
            int PrevTotalDonation = int.Parse(_Connection.Service.Spreadsheets.Values.Get(_Connection.SpreadsheetId, rangeTD).Execute().Values[0][0].ToString());
            var rangeTC = $"{_Connection.Sheet}!N" + (int.Parse(ID) + 1).ToString();
            int TotalCalls = int.Parse(_Connection.Service.Spreadsheets.Values.Get(_Connection.SpreadsheetId, rangeTC).Execute().Values[0][0].ToString());
            var oblist = new List<object>(){
                               (PrevTotalDonation + Donation),
                               (TotalCalls+1),
            };
            var valueRange = new ValueRange();
            valueRange.Values = new List<IList<object>> { oblist };
            var updateRequest = _Connection.Service.Spreadsheets.Values.Update(valueRange, _Connection.SpreadsheetId, rangeTD);
            updateRequest.ValueInputOption = SpreadsheetsResource.ValuesResource.UpdateRequest.ValueInputOptionEnum.USERENTERED;
            updateRequest.Execute();
            return true;
        }
        public Members GetCurrentMember(string ID)
        {
            _Connection.Sheet = "Fundraisers";
            var CurrentMemberRange = $"{_Connection.Sheet}!A" + (int.Parse(ID) + 1).ToString() + ":AO" + (int.Parse(ID) + 1).ToString();
            SpreadsheetsResource.ValuesResource.GetRequest request =
                _Connection.Service.Spreadsheets.Values.Get(_Connection.SpreadsheetId, CurrentMemberRange);
            var response = request.Execute();
            IList<IList<object>> values = response.Values;
            Members CurrentMember = new Members();
            CurrentMember.ID = values[0][0].ToString();
            CurrentMember.FullName = values[0][1].ToString();
            CurrentMember.Password = values[0][2].ToString();
            CurrentMember.FirstName = values[0][3].ToString();
            CurrentMember.LastName = values[0][4].ToString();
            CurrentMember.Confirmed = bool.Parse(values[0][5].ToString());
            CurrentMember.Gender = values[0][6].ToString();
            CurrentMember.AgeGroup = values[0][7].ToString();
            CurrentMember.Email = values[0][8].ToString();
            CurrentMember.PhoneNumber = values[0][9].ToString();
            CurrentMember.Country = values[0][10].ToString();
            CurrentMember.City = values[0][11].ToString();
            CurrentMember.TotalDonations = int.Parse(values[0][12].ToString());
            CurrentMember.TotalCallsMade = int.Parse(values[0][13].ToString());
            CurrentMember.FluentHebrew = bool.Parse(values[0][14].ToString());
            CurrentMember.FluentRussian = bool.Parse(values[0][15].ToString());
            CurrentMember.FluentEnglish = bool.Parse(values[0][16].ToString());
            CurrentMember.AnashIsrael = bool.Parse(values[0][17].ToString());
            CurrentMember.AnashUSA = bool.Parse(values[0][18].ToString());
            CurrentMember.PinskSchoolGraduate = bool.Parse(values[0][19].ToString());
            CurrentMember.KievSchoolGraduate = bool.Parse(values[0][20].ToString());
            CurrentMember.YeshivaGraduate = bool.Parse(values[0][21].ToString());
            CurrentMember.InPinsk = bool.Parse(values[0][22].ToString());
            CurrentMember.BusinessAssociate = bool.Parse(values[0][23].ToString());
            CurrentMember.BoysCounselor = bool.Parse(values[0][24].ToString());
            CurrentMember.GirlsCounselor = bool.Parse(values[0][25].ToString());
            CurrentMember.HelpedByPinsk = bool.Parse(values[0][26].ToString());
            CurrentMember.GeneralSupporter = bool.Parse(values[0][27].ToString());
            CurrentMember.MHSG = bool.Parse(values[0][28].ToString());
            CurrentMember.BelarusAnsectors = bool.Parse(values[0][29].ToString());
            CurrentMember.BelarusTourism = bool.Parse(values[0][30].ToString());
            CurrentMember.YYFundraiser = bool.Parse(values[0][31].ToString());
            CurrentMember.YYFamily = bool.Parse(values[0][32].ToString());
            CurrentMember.YYStaff = bool.Parse(values[0][33].ToString());
            CurrentMember.RShteiermanFamily = bool.Parse(values[0][34].ToString());
            CurrentMember.RFimaFamily = bool.Parse(values[0][35].ToString());
            CurrentMember.MarriedAYYGraduate = bool.Parse(values[0][36].ToString());
            CurrentMember.YearsIAsFundraiser = Int32.Parse(values[0][37].ToString());
            CurrentMember.YearsInYadYisroel = Int32.Parse(values[0][38].ToString());
            return CurrentMember;
        }
        public Donors GetSelectedDonor(string ID)
        {
            _Connection.Sheet = "Donors";
            var CurrentDonorRange = $"{_Connection.Sheet}!A" + (int.Parse(ID) + 1).ToString() + ":AY" + (int.Parse(ID) + 1).ToString();
            SpreadsheetsResource.ValuesResource.GetRequest request =
                _Connection.Service.Spreadsheets.Values.Get(_Connection.SpreadsheetId, CurrentDonorRange);
            var response = request.Execute();
            IList<IList<object>> values = response.Values;
            Donors SelectedDonor = new Donors();
            SelectedDonor.ID = values[0][0].ToString();
            SelectedDonor.FullName = values[0][1].ToString();
            SelectedDonor.FirstName = values[0][2].ToString();
            SelectedDonor.LastName = values[0][3].ToString();
            SelectedDonor.ConnectionID = values[0][4].ToString();
            SelectedDonor.Gender = values[0][5].ToString();
            SelectedDonor.AgeGroup = values[0][6].ToString();
            SelectedDonor.Email = values[0][7].ToString();
            SelectedDonor.PhoneNumber = values[0][8].ToString();
            SelectedDonor.Country = values[0][9].ToString();
            SelectedDonor.City = values[0][10].ToString();
            SelectedDonor.NativeLanguage = values[0][11].ToString();
            SelectedDonor.TotalDonation = int.Parse(values[0][12].ToString());
            SelectedDonor.LastDonation = int.Parse(values[0][13].ToString());
            SelectedDonor.VIP = bool.Parse(values[0][14].ToString());
            SelectedDonor.AnashIsrael = bool.Parse(values[0][15].ToString());
            SelectedDonor.AnashUSA = bool.Parse(values[0][16].ToString());
            SelectedDonor.PinskSchoolGraduate = bool.Parse(values[0][17].ToString());
            SelectedDonor.KievSchoolGraduate = bool.Parse(values[0][18].ToString());
            SelectedDonor.YeshivaGraduate = bool.Parse(values[0][19].ToString());
            SelectedDonor.InPinsk = bool.Parse(values[0][20].ToString());
            SelectedDonor.BusinessAssociate = bool.Parse(values[0][21].ToString());
            SelectedDonor.BoysCounselor = bool.Parse(values[0][22].ToString());
            SelectedDonor.GirlsCounselor = bool.Parse(values[0][23].ToString());
            SelectedDonor.HelpedByPinsk = bool.Parse(values[0][24].ToString());
            SelectedDonor.GeneralSupporter = bool.Parse(values[0][25].ToString());
            SelectedDonor.MHSG = bool.Parse(values[0][26].ToString());
            SelectedDonor.BelarusAnsectors = bool.Parse(values[0][27].ToString());
            SelectedDonor.BelarusTourism = bool.Parse(values[0][28].ToString());
            SelectedDonor.YYFundraiser = bool.Parse(values[0][29].ToString());
            SelectedDonor.YYFamily = bool.Parse(values[0][30].ToString());
            SelectedDonor.YYStaff = bool.Parse(values[0][31].ToString());
            SelectedDonor.RShteiermanFamily = bool.Parse(values[0][32].ToString());
            SelectedDonor.RFimaFamily = bool.Parse(values[0][33].ToString());
            SelectedDonor.MarriedAYYGraduate = bool.Parse(values[0][34].ToString());
            SelectedDonor.YearsInYadYisroel = Int32.Parse(values[0][35].ToString());
            SelectedDonor.Other = values[0][36].ToString();
            return SelectedDonor;

        }
    }
}