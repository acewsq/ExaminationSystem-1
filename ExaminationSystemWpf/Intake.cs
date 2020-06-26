using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.DynamicData;

namespace ExaminationSystemWpf
{
    [TableName("Intake")]
    public class Intake
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public byte ID { get; set; }
        public byte Number { get; set; }
        [Column(TypeName = "date")]
        public DateTime StartDate { get; set; }

        public virtual ICollection<BranchIntackTrack> BranchIntackTracks { get; set; }
    }
}

