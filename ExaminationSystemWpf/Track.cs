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
    [TableName("Track")]
    public  class Track
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public byte ID { get; set; }
        [MaxLength(30)]
        public string Name { get; set; }
        [MaxLength(30)]

        public string Department { get; set; }

        public virtual ICollection<BranchIntackTrack> BranchIntackTracks { get; set; }
        public virtual ICollection<TrackCourse> trackCourse { get; set; }
       // public virtual ICollection<Course> Courses { get; set; }
    }
}


