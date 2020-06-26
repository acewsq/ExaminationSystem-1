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
    [TableName("InstructorCourseStudent")]
    public class InstructorCourseStudent
    {
        public short ID { get; set; }
        public short InstructorID { get; set; }
        
        public short CourseID { get; set; }
        
        public int StudentID { get; set; }

        [ForeignKey("CourseID")]
        public virtual Course Course { get; set; }
        [ForeignKey("InstructorID")]
        public virtual Instructor Instructor { get; set; }
        [ForeignKey("StudentID")]
        public virtual Student Student { get; set; }
    }
}


