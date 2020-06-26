using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.DynamicData;

namespace ExaminationSystemWpf
{   [TableName("MCQAnswer")]
    public class MCQAnswer
    {
        [Key, Column(Order = 0)]

        public int AnswerID { get; set; }
       
        [ForeignKey("AnswerID")]
        public virtual Answer answer { get; set; }
        [Key, Column(Order = 1)]

        public int ChoiceID { get; set; }

        [ForeignKey("ChoiceID")]
        public virtual Choice Choice { get; set; }

    }
}
