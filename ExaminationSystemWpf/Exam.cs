
namespace ExaminationSystemWpf
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Web.DynamicData;

    [TableName("Exam")]
    public  class Exam
    {
        public int ID { get; set; }
        [MaxLength(10)]
        public string Type { get; set; }
        public Nullable<System.TimeSpan> StartTime { get; set; }
        public Nullable<System.TimeSpan> EndTime { get; set; }
        public Nullable<byte> Degree { get; set; }

        public short CourseID { get; set; }
       [ForeignKey("CourseID")]
        public virtual Course Course { get; set; }
       public virtual ICollection<ExamQuestionPool> ExamQuestionPools { get; set; }
        public virtual ICollection<StudentExam> StudentExams { get; set; }
    }
}
