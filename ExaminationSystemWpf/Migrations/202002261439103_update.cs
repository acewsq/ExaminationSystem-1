namespace ExaminationSystemWpf.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Answers",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        StudentID = c.Int(nullable: false),
                        ExamQuestionPoolID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.ExamQuestionPools", t => t.ExamQuestionPoolID, cascadeDelete: true)
                .ForeignKey("dbo.Students", t => t.StudentID, cascadeDelete: true)
                .Index(t => t.StudentID)
                .Index(t => t.ExamQuestionPoolID);
            
            CreateTable(
                "dbo.ExamQuestionPools",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ExamID = c.Int(nullable: false),
                        QuestionPoolID = c.Int(nullable: false),
                        Mark = c.Byte(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.QuestionPools", t => t.QuestionPoolID, cascadeDelete: true)
                .ForeignKey("dbo.Exams", t => t.ExamID, cascadeDelete: true)
                .Index(t => t.ExamID)
                .Index(t => t.QuestionPoolID);
            
            CreateTable(
                "dbo.Exams",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Type = c.String(maxLength: 10),
                        StartTime = c.Time(precision: 7),
                        EndTime = c.Time(precision: 7),
                        Degree = c.Byte(),
                        CourseID = c.Short(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Courses", t => t.CourseID, cascadeDelete: true)
                .Index(t => t.CourseID);
            
            CreateTable(
                "dbo.Courses",
                c => new
                    {
                        ID = c.Short(nullable: false, identity: true),
                        Name = c.String(maxLength: 30),
                        Hours = c.Byte(),
                        Discription = c.String(maxLength: 150),
                        MinDegree = c.Byte(),
                        MaxDegree = c.Byte(),
                        IsDeleted = c.Boolean(nullable: false, defaultValue: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.InstructorCourseStudents",
                c => new
                    {
                        ID = c.Short(nullable: false, identity: true),
                        InstructorID = c.Short(nullable: false),
                        CourseID = c.Short(nullable: false),
                        StudentID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Courses", t => t.CourseID, cascadeDelete: false)
                .ForeignKey("dbo.Instructors", t => t.InstructorID, cascadeDelete: false)
                .ForeignKey("dbo.Students", t => t.StudentID, cascadeDelete: false)
                .Index(t => t.InstructorID)
                .Index(t => t.CourseID)
                .Index(t => t.StudentID);
            
            CreateTable(
                "dbo.Instructors",
                c => new
                    {
                        ID = c.Short(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 30),
                        Email = c.String(maxLength: 30),
                        UserID = c.Int(nullable: false),
                        BranchID = c.Byte(nullable: false),
                        IsDeleted = c.Boolean(nullable: false, defaultValue: false),
                        TrainingMangerID = c.Short(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Users", t => t.UserID, cascadeDelete: true)
                .ForeignKey("dbo.Branches", t => t.BranchID, cascadeDelete: true)
                .ForeignKey("dbo.Instructors", t => t.TrainingMangerID)
                .Index(t => t.UserID, unique: true)
                .Index(t => t.BranchID)
                .Index(t => t.TrainingMangerID);
            
            CreateTable(
                "dbo.Branches",
                c => new
                    {
                        ID = c.Byte(nullable: false, identity: true),
                        Location = c.String(maxLength: 50),
                        Name = c.String(maxLength: 30),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.BranchIntackTracks",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        BranchID = c.Byte(nullable: false),
                        IntakeID = c.Byte(nullable: false),
                        TrackID = c.Byte(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Branches", t => t.BranchID, cascadeDelete: false)
                .ForeignKey("dbo.Intakes", t => t.IntakeID, cascadeDelete: false)
                .ForeignKey("dbo.Tracks", t => t.TrackID, cascadeDelete: false)
                .Index(t => t.BranchID)
                .Index(t => t.IntakeID)
                .Index(t => t.TrackID);
            
            CreateTable(
                "dbo.Intakes",
                c => new
                    {
                        ID = c.Byte(nullable: false, identity: true),
                        Number = c.Byte(nullable: false),
                        StartDate = c.DateTime(nullable: false, storeType: "date"),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Students",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 30),
                        Email = c.String(maxLength: 30),
                        College = c.String(maxLength: 50),
                        UserID = c.Int(nullable: false),
                        IsDeleted = c.Boolean(nullable: false, defaultValue: false),
                        BranchIntackTrackID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.BranchIntackTracks", t => t.BranchIntackTrackID, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserID, cascadeDelete: true)
                .Index(t => t.UserID, unique: true)
                .Index(t => t.BranchIntackTrackID);
            
            CreateTable(
                "dbo.StudentExams",
                c => new
                    {
                        StudentID = c.Int(nullable: false),
                        ExamID = c.Int(nullable: false),
                        FinalMark = c.Byte(),
                    })
                .PrimaryKey(t => new { t.StudentID, t.ExamID })
                .ForeignKey("dbo.Exams", t => t.ExamID, cascadeDelete: true)
                .ForeignKey("dbo.Students", t => t.StudentID, cascadeDelete: true)
                .Index(t => t.StudentID)
                .Index(t => t.ExamID);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 30),
                        Password = c.String(nullable: false, maxLength: 30),
                        IsDeleted = c.Boolean(nullable:false,defaultValue:false),

                    })
                .PrimaryKey(t => t.ID)
                .Index(t => t.Name, unique: true);
            
            CreateTable(
                "dbo.Tracks",
                c => new
                    {
                        ID = c.Byte(nullable: false, identity: true),
                        Name = c.String(maxLength: 30),
                        Department = c.String(maxLength: 30),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.TrackCourses",
                c => new
                    {
                        TrackID = c.Byte(nullable: false),
                        CourseID = c.Short(nullable: false),
                    })
                .PrimaryKey(t => new { t.TrackID, t.CourseID })
                .ForeignKey("dbo.Courses", t => t.CourseID, cascadeDelete: true)
                .ForeignKey("dbo.Tracks", t => t.TrackID, cascadeDelete: true)
                .Index(t => t.TrackID)
                .Index(t => t.CourseID);
            
            CreateTable(
                "dbo.QuestionPools",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CourseID = c.Short(nullable: false),
                        Type = c.String(maxLength: 10),
                        Body = c.String(maxLength: 200),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Courses", t => t.CourseID, cascadeDelete: false)
                .Index(t => t.CourseID);
            
            CreateTable(
                "dbo.McqQuestions",
                c => new
                    {
                        QuestionPoolID = c.Int(nullable: false),
                        Header = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.QuestionPoolID)
                .ForeignKey("dbo.QuestionPools", t => t.QuestionPoolID)
                .Index(t => t.QuestionPoolID);
            
            CreateTable(
                "dbo.Choices",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Body = c.String(maxLength: 50),
                        Answer = c.Boolean(nullable: false),
                        QuestionPoolID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.McqQuestions", t => t.QuestionPoolID, cascadeDelete: true)
                .Index(t => t.QuestionPoolID);
            
            CreateTable(
                "dbo.MCQAnswers",
                c => new
                    {
                        AnswerID = c.Int(nullable: false),
                        ChoiceID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.AnswerID, t.ChoiceID })
                .ForeignKey("dbo.Answers", t => t.AnswerID, cascadeDelete: true)
                .ForeignKey("dbo.Choices", t => t.ChoiceID, cascadeDelete: true)
                .Index(t => t.AnswerID)
                .Index(t => t.ChoiceID);
            
            CreateTable(
                "dbo.TextQuestions",
                c => new
                    {
                        QuestionPoolID = c.Int(nullable: false),
                        Header = c.String(maxLength: 50),
                        Answer = c.String(maxLength: 200),
                    })
                .PrimaryKey(t => t.QuestionPoolID)
                .ForeignKey("dbo.QuestionPools", t => t.QuestionPoolID)
                .Index(t => t.QuestionPoolID);
            
            CreateTable(
                "dbo.TrueFalseQuestions",
                c => new
                    {
                        QuestionPoolID = c.Int(nullable: false),
                        Header = c.String(maxLength: 50),
                        Answer = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.QuestionPoolID)
                .ForeignKey("dbo.QuestionPools", t => t.QuestionPoolID)
                .Index(t => t.QuestionPoolID);
            
            CreateTable(
                "dbo.TextAnswers",
                c => new
                    {
                        AnswerID = c.Int(nullable: false),
                        Answer = c.String(maxLength: 200),
                    })
                .PrimaryKey(t => t.AnswerID)
                .ForeignKey("dbo.Answers", t => t.AnswerID)
                .Index(t => t.AnswerID);
            
            CreateTable(
                "dbo.TrueFalseAnswers",
                c => new
                    {
                        AnswerID = c.Int(nullable: false),
                        Answer = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.AnswerID)
                .ForeignKey("dbo.Answers", t => t.AnswerID)
                .Index(t => t.AnswerID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TrueFalseAnswers", "AnswerID", "dbo.Answers");
            DropForeignKey("dbo.TextAnswers", "AnswerID", "dbo.Answers");
            DropForeignKey("dbo.ExamQuestionPools", "ExamID", "dbo.Exams");
            DropForeignKey("dbo.TrueFalseQuestions", "QuestionPoolID", "dbo.QuestionPools");
            DropForeignKey("dbo.TextQuestions", "QuestionPoolID", "dbo.QuestionPools");
            DropForeignKey("dbo.McqQuestions", "QuestionPoolID", "dbo.QuestionPools");
            DropForeignKey("dbo.Choices", "QuestionPoolID", "dbo.McqQuestions");
            DropForeignKey("dbo.MCQAnswers", "ChoiceID", "dbo.Choices");
            DropForeignKey("dbo.MCQAnswers", "AnswerID", "dbo.Answers");
            DropForeignKey("dbo.ExamQuestionPools", "QuestionPoolID", "dbo.QuestionPools");
            DropForeignKey("dbo.QuestionPools", "CourseID", "dbo.Courses");
            DropForeignKey("dbo.InstructorCourseStudents", "StudentID", "dbo.Students");
            DropForeignKey("dbo.Instructors", "TrainingMangerID", "dbo.Instructors");
            DropForeignKey("dbo.InstructorCourseStudents", "InstructorID", "dbo.Instructors");
            DropForeignKey("dbo.Instructors", "BranchID", "dbo.Branches");
            DropForeignKey("dbo.TrackCourses", "TrackID", "dbo.Tracks");
            DropForeignKey("dbo.TrackCourses", "CourseID", "dbo.Courses");
            DropForeignKey("dbo.BranchIntackTracks", "TrackID", "dbo.Tracks");
            DropForeignKey("dbo.Students", "UserID", "dbo.Users");
            DropForeignKey("dbo.Instructors", "UserID", "dbo.Users");
            DropForeignKey("dbo.StudentExams", "StudentID", "dbo.Students");
            DropForeignKey("dbo.StudentExams", "ExamID", "dbo.Exams");
            DropForeignKey("dbo.Students", "BranchIntackTrackID", "dbo.BranchIntackTracks");
            DropForeignKey("dbo.Answers", "StudentID", "dbo.Students");
            DropForeignKey("dbo.BranchIntackTracks", "IntakeID", "dbo.Intakes");
            DropForeignKey("dbo.BranchIntackTracks", "BranchID", "dbo.Branches");
            DropForeignKey("dbo.InstructorCourseStudents", "CourseID", "dbo.Courses");
            DropForeignKey("dbo.Exams", "CourseID", "dbo.Courses");
            DropForeignKey("dbo.Answers", "ExamQuestionPoolID", "dbo.ExamQuestionPools");
            DropIndex("dbo.TrueFalseAnswers", new[] { "AnswerID" });
            DropIndex("dbo.TextAnswers", new[] { "AnswerID" });
            DropIndex("dbo.TrueFalseQuestions", new[] { "QuestionPoolID" });
            DropIndex("dbo.TextQuestions", new[] { "QuestionPoolID" });
            DropIndex("dbo.MCQAnswers", new[] { "ChoiceID" });
            DropIndex("dbo.MCQAnswers", new[] { "AnswerID" });
            DropIndex("dbo.Choices", new[] { "QuestionPoolID" });
            DropIndex("dbo.McqQuestions", new[] { "QuestionPoolID" });
            DropIndex("dbo.QuestionPools", new[] { "CourseID" });
            DropIndex("dbo.TrackCourses", new[] { "CourseID" });
            DropIndex("dbo.TrackCourses", new[] { "TrackID" });
            DropIndex("dbo.Users", new[] { "Name" });
            DropIndex("dbo.StudentExams", new[] { "ExamID" });
            DropIndex("dbo.StudentExams", new[] { "StudentID" });
            DropIndex("dbo.Students", new[] { "BranchIntackTrackID" });
            DropIndex("dbo.Students", new[] { "UserID" });
            DropIndex("dbo.BranchIntackTracks", new[] { "TrackID" });
            DropIndex("dbo.BranchIntackTracks", new[] { "IntakeID" });
            DropIndex("dbo.BranchIntackTracks", new[] { "BranchID" });
            DropIndex("dbo.Instructors", new[] { "TrainingMangerID" });
            DropIndex("dbo.Instructors", new[] { "BranchID" });
            DropIndex("dbo.Instructors", new[] { "UserID" });
            DropIndex("dbo.InstructorCourseStudents", new[] { "StudentID" });
            DropIndex("dbo.InstructorCourseStudents", new[] { "CourseID" });
            DropIndex("dbo.InstructorCourseStudents", new[] { "InstructorID" });
            DropIndex("dbo.Exams", new[] { "CourseID" });
            DropIndex("dbo.ExamQuestionPools", new[] { "QuestionPoolID" });
            DropIndex("dbo.ExamQuestionPools", new[] { "ExamID" });
            DropIndex("dbo.Answers", new[] { "ExamQuestionPoolID" });
            DropIndex("dbo.Answers", new[] { "StudentID" });
            DropTable("dbo.TrueFalseAnswers");
            DropTable("dbo.TextAnswers");
            DropTable("dbo.TrueFalseQuestions");
            DropTable("dbo.TextQuestions");
            DropTable("dbo.MCQAnswers");
            DropTable("dbo.Choices");
            DropTable("dbo.McqQuestions");
            DropTable("dbo.QuestionPools");
            DropTable("dbo.TrackCourses");
            DropTable("dbo.Tracks");
            DropTable("dbo.Users");
            DropTable("dbo.StudentExams");
            DropTable("dbo.Students");
            DropTable("dbo.Intakes");
            DropTable("dbo.BranchIntackTracks");
            DropTable("dbo.Branches");
            DropTable("dbo.Instructors");
            DropTable("dbo.InstructorCourseStudents");
            DropTable("dbo.Courses");
            DropTable("dbo.Exams");
            DropTable("dbo.ExamQuestionPools");
            DropTable("dbo.Answers");
        }
    }
}
