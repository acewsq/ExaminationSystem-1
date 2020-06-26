
namespace ExaminationSystemWpf
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class McqQuestion
    {
        [Key]
        public int QuestionPoolID { get; set; }
        [MaxLength(50)]
        public string Header { get; set; }
    
        public virtual ICollection<Choice> Choices { get; set; }

       [ForeignKey("QuestionPoolID")]
        public virtual QuestionPool QuestionPool { get; set; }
    }
}
