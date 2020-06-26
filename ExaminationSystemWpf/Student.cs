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
    [TableName("Student")]
    public class Student
    {

        public Student() 
        {
            IsDeleted = false;
        }
        public int ID { get; set; }
        [MaxLength(30)]

        [Required]
        public string Name { get; set; }
        [MaxLength(30)]
        public string Email { get; set; }
        [MaxLength(50)]
        public string College { get; set; }
        [Index(IsUnique = true)]
        public int UserID { get; set; }
        public Nullable<bool> IsDeleted { get; set; }

        public int BranchIntackTrackID { get; set; }
        [ForeignKey("BranchIntackTrackID")]
        public virtual BranchIntackTrack BranchIntackTrack { get; set; }

        [ForeignKey("UserID")]
        public virtual User User { get; set; }
       public virtual ICollection<StudentExam> StudentExams { get; set; }
        public virtual ICollection<Answer> Answers { get; set; }

    }
}


