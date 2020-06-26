
namespace ExaminationSystemWpf
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Web.DynamicData;

    [TableName("TrueFalseAnswer")]
    public class TrueFalseAnswer
    {
        [Key]
        public int AnswerID { get; set; }
        public bool Answer { get; set; }
    
        [ForeignKey("AnswerID")]
        public virtual Answer answer { get; set; }
    }
}
