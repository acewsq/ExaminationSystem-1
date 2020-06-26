
namespace ExaminationSystemWpf
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Web.DynamicData;

    [TableName("ExamQuestionPool")]
    public class ExamQuestionPool
    {
        public int ID { get; set; }
        public int ExamID { get; set; }
        public int QuestionPoolID { get; set; }
        public byte Mark { get; set; }
    
        public virtual ICollection<Answer> Answers { get; set; }

       [ForeignKey("ExamID")]
        public virtual Exam Exam { get; set; }
        [ForeignKey("QuestionPoolID")]
        public virtual QuestionPool QuestionPool { get; set; }
    }
}
