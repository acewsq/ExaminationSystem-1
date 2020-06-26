using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ExaminationSystemWpf;

namespace ExaminationSystemWpf
{
    class Service
    {
        public Context context;
        public Service()
        {
            context = new Context();
        }

        public void CorrectTextQuestions(Dictionary<int, string> Answers, int SID, int ExamID)
        {
            try
            {
                foreach (var item in Answers)
                {
                    var sid = new SqlParameter("@SID", SID);
                    var examid = new SqlParameter("@ExamID", ExamID);

                    var question = new SqlParameter("@ExamQuesID", item.Key);
                    var answer = new SqlParameter("@Answer", item.Value);
                    var result = context.Database.SqlQuery<string>("exec pAnswerExamTextQues @SID,@ExamID,@ExamQuesID,@Answer",
                        sid, examid, question, answer).FirstOrDefault();
                    MessageBox.Show(result);
                }
                context.SaveChanges();
            }
            catch { }
        }

        public void CorrectTrueFalseQuestions(Dictionary<int, bool> Answers, int SID, int ExamID)
        {
            try
            {
                foreach (var item in Answers)
                {
                    var sid = new SqlParameter("@SID", SID);
                    var examid = new SqlParameter("@ExamID", ExamID);

                    var question = new SqlParameter("@ExamQuesID", item.Key);
                    var answer = new SqlParameter("@Answer", item.Value);
                    var result = context.Database.SqlQuery<string>("exec pAnswerExamTrueFalseQues @SID,@ExamID,@ExamQuesID,@Answer",
                        sid, examid, question, answer).FirstOrDefault();
                    MessageBox.Show(result);
                }
                context.SaveChanges();
            }
            catch { }
        }

        public void CorrectMCQQuestions(Dictionary<int, List<string>> Answers, int SID, int ExamID)
        {
            int choicescount = 0;
            try
            {
                foreach (var item in Answers)
                {
                    choicescount = 0;
                    foreach (var item1 in item.Value)
                    {

                        var sid = new SqlParameter("@SID", SID);
                        var examid = new SqlParameter("@ExamID", ExamID);
                        var question = new SqlParameter("@ExamQuesID", item.Key);

                        var questionpool = context.ExamQuestionPools.FirstOrDefault(e => e.ID == item.Key);
                        var choice = context.Choices.FirstOrDefault(c => c.Body == item1 && c.QuestionPoolID == questionpool.QuestionPoolID);
                        var ChoiceID = new SqlParameter("@choiceID", choice.ID);

                        var result = context.Database.SqlQuery<string>("exec pAnswerExamMcqQues @SID, @ExamID, @ExamQuesID, @choiceID",
                            sid, examid, question, ChoiceID).FirstOrDefault();
                        MessageBox.Show(result);
                        if (choicescount == item.Value.Count() - 1)
                        {
                            var sidd = new SqlParameter("@SID", SID);
                            var examidd = new SqlParameter("@ExamID", ExamID);

                            var questionpooll = context.ExamQuestionPools.FirstOrDefault(e => e.ID == item.Key).QuestionPoolID;
                            var questionn = new SqlParameter("@QuesID", (questionpooll));

                            var answerID =
                                (from A in context.Answers
                                 where A.ExamQuestionPoolID == item.Key
                                 select A).FirstOrDefault().ID;
                            var answerid = new SqlParameter("@answerd", answerID);

                            var resultt = context.Database.SqlQuery<int>("exec fCheckMcqQuestionAnswer @SID, @ExamID, @QuesID, @answerd",
                                sidd, examidd, questionn, answerid).FirstOrDefault();
                            //  MessageBox.Show(resultt.ToString());
                        }
                        choicescount++;

                        //exec dbo.fCheckMcqQuestionAnswer @SID, @ExamID, @QuesID, @answerd

                    }

                }
                context.SaveChanges();
            }
            catch { }
        }

        public List<ExamQuestionPool> GetQuestions(int ExamID)
        {
            try
            {
                List<ExamQuestionPool> questions = (from q in context.QuestionPools
                                                    from eq in context.ExamQuestionPools
                                                    where eq.QuestionPoolID == q.ID
                                                    where eq.ExamID == ExamID
                                                    select eq).ToList();
                return questions;
            }
            catch (Exception e)
            {
                return null;
            }

        }
        public List<Course> InsCourses(int InsID)
        {
            List<Course> query = (from c in context.Courses
                                  from t in context.InstructorCourseStudent
                                  where c.ID == t.CourseID
                                  where t.InstructorID == InsID
                                  select c).Distinct().ToList();

            return query;
        }
        public List<Exam> CourseExams(short CrsId)
        {
            List<Exam> query = context.Exams.Where(e => e.CourseID == CrsId).ToList();
            return query;
        }
        public Exam getExam(int ExamId)
        {
            Exam query = new Context().Exams.FirstOrDefault(ex => ex.ID == ExamId);
            return query;
        }
        public bool CheckExamMark(int ExamId, byte mark)
        {
            int sum = 0;
            try
            {
                if (context.ExamQuestionPools.Where(e => e.ExamID == ExamId).ToList() != null)
                {
                    foreach (var item in context.ExamQuestionPools.Where(e => e.ExamID == ExamId).ToList())
                    {
                        sum += item.Mark;
                    }

                    Exam exam = context.Exams.FirstOrDefault(ex => ex.ID == ExamId);
                    if (exam.Course.MaxDegree >= (sum + mark))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                }
                else
                {
                    return true;
                }

            }
            catch (Exception e3)
            {
                return false;
            }
        }
        public List<TextQuestion> CourseTextQuestions(short CrsId)
        {
            List<TextQuestion> query = (from t in context.TextQuestions
                                        from q in context.QuestionPools
                                        where t.QuestionPoolID == q.ID
                                        where q.CourseID == CrsId
                                        select t).ToList();
            return query;
        }
        public bool AddExam(Exam exam)
        {
            try
            {
                context.Exams.Add(exam);
                context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;

            }
        }
        public string AddQuestion(int examID, int questpoolID, byte mark)
        {
            try
            {
                if (context.ExamQuestionPools.FirstOrDefault(eq => eq.QuestionPoolID == questpoolID && eq.ExamID == examID) == null)
                {
                    context.ExamQuestionPools.Add(new ExamQuestionPool
                    {
                        ExamID = examID,
                        QuestionPoolID = questpoolID,
                        Mark = mark
                    });
                    Exam exam = context.Exams.FirstOrDefault(e => e.ID == examID);
                    exam.Degree += mark;

                    context.SaveChanges();
                    return "Added";
                }
                else
                {
                    return "Already Exists!";
                }
            }
            catch (Exception e)
            {
                return "Something Went Wrong!";

            }
        }
    }
}
