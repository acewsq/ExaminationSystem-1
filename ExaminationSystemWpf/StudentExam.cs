
namespace ExaminationSystemWpf
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Web.DynamicData;

    [TableName("StudentExam")]
    public class StudentExam
    {
        [Key, Column(Order = 0)]
        public int StudentID { get; set; }
        [Key, Column(Order = 1)]
        public int ExamID { get; set; }
        public Nullable<byte> FinalMark { get; set; }
        [ForeignKey("ExamID")]

        public virtual Exam Exam { get; set; }
        [ForeignKey("StudentID")]
        public virtual Student Student { get; set; }
    }
}
