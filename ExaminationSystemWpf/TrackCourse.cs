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
    [TableName("TrackCourse")]
    public class TrackCourse
    {
        [Key, Column(Order = 0)]
        public byte TrackID { get; set; }
        [Key, Column(Order = 1)]
        public short CourseID { get; set; }

        [ForeignKey("TrackID")]
        public virtual Track track { get; set; }
        [ForeignKey("CourseID")]
        public virtual Course course { get; set; }
        
    }
}
