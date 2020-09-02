using Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DL
{
    public interface IDonorsDL
    {
        Boolean PostDonor(Donors Donor);
        List<DonorsList> GetAllDonors();
    }
}
