using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.DynamicData;

namespace ExaminationSystemWpf
{
    [TableName("Branch")]
    public class Branch
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public byte ID { get; set; }
        [MaxLength(50)]
        public string Location { get; set; }
        [MaxLength(30)]
        public string Name { get; set; }

        public virtual ICollection<BranchIntackTrack> BranchIntackTracks { get; set; }
        public virtual ICollection<Instructor> Instructors { get; set; }
    }
}

