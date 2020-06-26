
namespace ExaminationSystemWpf
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Web.DynamicData;

    [TableName("TextQuestion")]
    public class TextQuestion
    {
        [Key]
        public int QuestionPoolID { get; set; }
        [MaxLength(50)]
        public string Header { get; set; }
        [MaxLength(200)]
        public string Answer { get; set; }
        [ForeignKey("QuestionPoolID")]
        public virtual QuestionPool questionPool { get; set; }
    }
}
