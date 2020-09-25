using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BK_API_QUIZ_01.Models
{
    public class Sequence
    {
        public int Id { get; set; }
        public int order { get; set; }
        public string Content { get; set; }
        public string Image { get; set; }
        public int QuizId { get; set; }

        public Quiz Quiz { get; set; }
    }
}