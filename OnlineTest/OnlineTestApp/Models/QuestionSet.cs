using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineTestApp.Models
{
    public class QuestionSet
    {
       public int QuestionSetId { get; set; }
        public string QuestionSetCode { get; set; }
        public bool IsAlreadyUsed { get; set; }

    }
}