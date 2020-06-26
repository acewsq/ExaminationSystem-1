
namespace ExaminationSystemWpf
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class TextAnswer
    {
        [Key]
        public int AnswerID { get; set; }
        [MaxLength(200)]
        public string Answer { get; set; }
        [ForeignKey("AnswerID")]
        public virtual Answer answer { get; set; }
    }
}
