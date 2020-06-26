using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.DynamicData;

namespace ExaminationSystemWpf
{
    [TableName("Course")]
  public class Course
    {
        public Course()
        {
            IsDeleted = false;
        }
        
        public short ID { get; set; }
        [MaxLength(30)]
        public string Name { get; set; }
        public Nullable<byte> Hours { get; set; }
        [MaxLength(150)]
        public string Discription { get; set; }
        public Nullable<byte> MinDegree { get; set; }
        public Nullable<byte> MaxDegree { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
    
        public virtual ICollection<InstructorCourseStudent> InstructorCourseStudents { get; set; }
        public virtual ICollection<TrackCourse> trackCourse { get; set; }
        public virtual ICollection<Exam> Exams { get; set; }

         public virtual ICollection<QuestionPool> QuestionPools { get; set; }
        // public virtual ICollection<Track> Tracks { get; set; }
    }
}


