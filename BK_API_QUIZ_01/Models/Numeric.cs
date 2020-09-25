using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BK_API_QUIZ_01.Models
{
    public class Numeric
    {
        public int Id { get; set; }
        public int QuizId { get; set; }
        public string KeyContent { get; set; }

        public Quiz Quiz { get; set; }
    }
}