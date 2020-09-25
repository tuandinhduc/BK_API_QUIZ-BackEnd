using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BK_API_QUIZ_01.Models
{
    public class Quiz
    {
        public int Id { get; set; }
        public string QType { get; set; }
        public string QName { get; set; }
        public string QuestionText { get; set; }
        public string QSkill { get; set; }
        public decimal DefaultMask { get; set; }
        public DateTime TimeCreate { get; set; }
        public DateTime TimeUpdate { get; set; }
        public string QLevel { get; set; }
        public string Image { get; set; }

        public ICollection<Exam> Exams { get; set; }
        public virtual ICollection<TrueFalse> TrueFalses { get; set; }
        public virtual ICollection<MultipleChoice> MultipleChoices { get; set; }
        public virtual ICollection<MultipleResponse> MultipleResponses { get; set; }
        public virtual ICollection<Matching> Matchings { get; set; }
        public virtual ICollection<FillinBlank> FillinBlanks { get; set; }
        public virtual ICollection<DragDrop> DragDrops { get; set; }
        public virtual ICollection<ShortAnswer> ShortAnswers { get; set; }
        public virtual ICollection<Numeric> Numerics { get; set; }
        public virtual ICollection<SelectFromList> SelectFromLists { get; set; }
        public virtual ICollection<Sequence> Sequences { get; set; }

    }
}