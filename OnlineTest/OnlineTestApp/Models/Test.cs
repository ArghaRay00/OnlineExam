using System;
using System.Collections.Generic;
using OnlineTestEntities;

namespace OnlineTestApp.Models
{
    public class Test
    {
        public int QuizId { get; set; }
        public virtual List<Question> Questions { get; set; }
        public DateTime? StartTime { get; set; }
        public TimeSpan? Duration { get; set; }
        public DateTime? EndTime { get; set; }
        public int Score { get; set; }
    }
}