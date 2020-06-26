
namespace ExaminationSystemWpf
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Web.DynamicData;

    [TableName("TrueFalseQuestion")]
    public class TrueFalseQuestion
    {
        [Key]
        public int QuestionPoolID { get; set; }
        [MaxLength(50)]
        public string Header { get; set; }
        public bool Answer { get; set; }
        [ForeignKey("QuestionPoolID")]
        public virtual QuestionPool QuestionPool { get; set; }
    }
}
