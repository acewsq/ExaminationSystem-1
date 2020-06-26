
namespace ExaminationSystemWpf
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Answer
    {
        public int ID { get; set; }
        public int StudentID { get; set; }
        public int ExamQuestionPoolID { get; set; }
        [ForeignKey("ExamQuestionPoolID")]
        public virtual ExamQuestionPool ExamQuestionPool { get; set; }
        [ForeignKey("StudentID")]
        public virtual Student Student { get; set; }
        public virtual ICollection<TextAnswer> TextAnswer { get; set; }
        public virtual ICollection<TrueFalseAnswer> TrueFalseAnswer { get; set; }
        public virtual ICollection<MCQAnswer> MCQAnswer { get; set; }
    }
}
