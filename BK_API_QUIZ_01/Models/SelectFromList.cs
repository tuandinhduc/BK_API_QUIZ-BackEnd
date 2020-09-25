﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BK_API_QUIZ_01.Models
{
    public class SelectFromList
    {
        public int Id { get; set; }
        public bool IsCorrect { get; set; }
        public bool ShuffleAns { get; set; }
        public string Content { get; set; }
        public int QuizId { get; set; }

        public Quiz Quiz { get; set; }
    }
}