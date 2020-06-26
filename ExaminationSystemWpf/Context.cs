using System.Data.Entity;

namespace ExaminationSystemWpf
{
    public class Context : DbContext
    {
        public Context() :
              base($@"Data Source=.;Initial Catalog=ExaminationSystemFinal;Integrated Security = true")
        {

        }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<InstructorCourseStudent> InstructorCourseStudent { get; set; }
        public virtual DbSet<Instructor> Instructors { get; set; }
        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<Course> Courses { get; set; }
        public virtual DbSet<Track> Tracks { get; set; }
        public virtual DbSet<Branch> Branches { get; set; }
        public virtual DbSet<Intake> Intakes { get; set; }
        public virtual DbSet<BranchIntackTrack> BranchIntackTracks { get; set; }
        public virtual DbSet<TrackCourse> TrackCourses { get; set; }
        public virtual DbSet<TextQuestion> TextQuestions { get; set; }
        public virtual DbSet<TextAnswer> TextAnswers { get; set; }
        public virtual DbSet<TrueFalseQuestion> TrueFalseQuestions { get; set; }
        public virtual DbSet<TrueFalseAnswer> TrueFalseAnswers { get; set; }
        public virtual DbSet<McqQuestion> McqQuestions { get; set; }
        public virtual DbSet<MCQAnswer> MCQAnswers { get; set; }
        public virtual DbSet<Exam> Exams { get; set; }
        public virtual DbSet<StudentExam> StudentExams { get; set; }
        public virtual DbSet<ExamQuestionPool> ExamQuestionPools { get; set; }
        public virtual DbSet<QuestionPool> QuestionPools { get; set; }
        public virtual DbSet<Choice> Choices { get; set; }
        public virtual DbSet<Answer> Answers { get; set; }















    }
}