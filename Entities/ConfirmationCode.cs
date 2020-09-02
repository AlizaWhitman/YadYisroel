using System;
using System.Collections.Generic;
using System.Text;

namespace Entities
{
    public class ConfirmationCode
    {
        public Status EmailStatus { get; set; }
        public string Code { get; set; }
        
    }

    public enum Status
    {
        EmailSent = 1,
        ProblemSending = 2,
        EmailNotFound = 3,
        IncorrectFormat = 4,
    }
}
