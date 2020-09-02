using DL;
using Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BL
{
    public class CallsSummaryBL:ICallsSummaryBL
    {
        ICallsSummaryDL _ICallsSummaryDL;
        public CallsSummaryBL(ICallsSummaryDL ICallsSummaryDL)
        {
            _ICallsSummaryDL = ICallsSummaryDL;
        }
        public void PostCallsSummary(CallsSummary cs)
        {
            cs.TimeDate = DateTime.Now.ToString();
            _ICallsSummaryDL.PostCallsSummary(cs);
        }

    }
}
