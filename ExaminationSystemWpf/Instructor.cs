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
    [TableName("Instructor")]
    public class Instructor
    {
        public Instructor() 
        {
            IsDeleted = false;
        }
        public short ID { get; set; }
        [MaxLength(30)]

        [Required]
        public string Name { get; set; }
        [MaxLength(30)]
        public string Email { get; set; }
        [Index(IsUnique = true)]
        public int UserID { get; set; }
        public byte BranchID { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        [ForeignKey("BranchID")]
        public virtual Branch Branch { get; set; }

        public virtual ICollection<InstructorCourseStudent> InstructorCourseStudents { get; set; }

        public Nullable<short> TrainingMangerID { get; set; }
        [ForeignKey("TrainingMangerID")]
        public virtual ICollection<Instructor> TrainingManager { get; set; }

        [ForeignKey("UserID")]
        public virtual User User { get; set; }
    }
}

