using Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BL
{
    public interface IMembersBL
    {
        Members GetMember(string Email, string Password);
        ConfirmationCode SendPassword(string Email, SendPassword Source);
        Members PostMember(Members Member);
        Boolean PutMember(Members MemberToUpdate);
        Boolean DeleteMember(int ID);

    }
}
