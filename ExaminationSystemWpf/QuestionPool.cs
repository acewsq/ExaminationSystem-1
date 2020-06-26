
namespace ExaminationSystemWpf
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class QuestionPool
    {
        public int ID { get; set; }
        public short CourseID { get; set; }
        [MaxLength(10)]
        public string Type { get; set; }
        [MaxLength(200)]
        public string Body { get; set; }
        [ForeignKey("CourseID")]
        public virtual Course Course { get; set; }

        public virtual ICollection<ExamQuestionPool> ExamQuestionPools { get; set; }
        public virtual ICollection<McqQuestion> McqQuestion { get; set; }
        public virtual ICollection<TextQuestion> TextQuestions { get; set; }
        public virtual ICollection<TrueFalseQuestion> TrueFalseQuestion { get; set; }
    }
}
