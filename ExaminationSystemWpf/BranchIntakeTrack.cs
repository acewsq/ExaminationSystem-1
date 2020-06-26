using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.DynamicData;

namespace ExaminationSystemWpf
{
    [TableName("BranchIntackTrack")]
    public class BranchIntackTrack
    {
        public int ID { get; set; }
        public byte BranchID { get; set; }
        public byte IntakeID { get; set; }
        public byte TrackID { get; set; }
        [ForeignKey("BranchID")]
        public virtual Branch Branch { get; set; }
        [ForeignKey("IntakeID")]
        public virtual Intake Intake { get; set; }
        [ForeignKey("TrackID")]
        public virtual Track Track { get; set; }
        public virtual ICollection<Student> Students { get; set; }
    }
}


