using DL;
using Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BL
{
    public class MembersBL : IMembersBL
    {
        IMembersDL _IMemberDL;
        public MembersBL(IMembersDL MemberDl)
        {
            _IMemberDL = MemberDl;
        }
        public Members GetMember(string Email, string Password)
        {
            return _IMemberDL.GetMember(Email, Password);
        }
        public Members PostMember(Members Member)
        {
            return _IMemberDL.PostMember(Member);
        }
        public Boolean PutMember(Members MemberToUpdate)
        {
            return _IMemberDL.PutMember(MemberToUpdate);
        }
        public Boolean DeleteMember(int ID)
        {
            return _IMemberDL.DeleteMember(ID);
        }

        public ConfirmationCode SendPassword(string Email, SendPassword Source)
        {
            return _IMemberDL.SendPassword(Email, Source);
        }
    }
}
