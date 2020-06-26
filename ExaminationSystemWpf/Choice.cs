

namespace ExaminationSystemWpf
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Web.DynamicData;

    [TableName("Choice")]
    public class Choice
    {
        public int ID { get; set; }
        [MaxLength(50)]
        public string Body { get; set; }
        public bool Answer { get; set; }
        public int QuestionPoolID { get; set; }
        [ForeignKey("QuestionPoolID")]
        public virtual McqQuestion McqQuestion { get; set; }
        //public virtual QuestionPool QuestionPool { get; set; }

        //  public virtual ICollection<Answer> Answers { get; set; } 
        public virtual ICollection<MCQAnswer> MCQAnswers { get; set; }

    }
}
