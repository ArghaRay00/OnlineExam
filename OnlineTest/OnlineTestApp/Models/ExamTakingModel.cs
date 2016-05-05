using OnlineTestEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineTestApp.Models
{
    public class ExamTakingModel
    {
        public Question Question { get; set; }
        public int Choice { get; set; }
    }
}