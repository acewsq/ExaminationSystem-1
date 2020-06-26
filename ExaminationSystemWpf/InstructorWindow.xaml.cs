using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ExaminationSystemWpf
{
    /// <summary>
    /// Interaction logic for InstructorWindow.xaml
    /// </summary>
    public partial class InstructorWindow : Window
    {
        Context context = new Context();
        public int InstructorID { get; set; }
        public InstructorWindow(int id)
        {
            InitializeComponent();
            InstructorID = id;
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int id = InstructorID;
            var query = (from c in context.Courses
                         from t in context.InstructorCourseStudent
                         where c.ID == t.CourseID
                         where t.InstructorID == id
                         select c.Name).ToList();

            Displaycourse.ItemsSource = query;


        }
        McqQuestion AddedMCQ;
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (Displaycourse.SelectedItem != null)
            {
                var id = (from c in context.Courses
                          where c.Name == Displaycourse.SelectedItem.ToString()
                          select c.ID).FirstOrDefault();
                QuestionPool questionPool = new QuestionPool();
               // if(QuestionBody.Text=="")
                questionPool.Body = QuestionBody.Text;
                questionPool.CourseID = id;
                questionPool.Type = QuestionType.Text;
                context.QuestionPools.Add(questionPool);
                context.SaveChanges();
                if (QuestionType.SelectedIndex == 0)
                {
                    answerText.SetValue(Panel.ZIndexProperty, 1);//.Visibility = true;
                    TrueFalseAnswer.SetValue(Panel.ZIndexProperty, 0);


                    
                    var QuestionPoolID = new SqlParameter("@questionpoolId", questionPool.ID);
                    var header = new SqlParameter("@Header", QuestionHeader.Text);
                    var answer = new SqlParameter("@Answer", (answerText.Text));
                    var result = context.Database.SqlQuery<string>(" exec addTextQuestion @questionpoolId, @Header,@Answer ", QuestionPoolID, header, answer).FirstOrDefault();
                    if (string.Compare(result, "added sucessfully") == 0)
                    {
                        MessageBox.Show("added");
                    }

                }
                else if (QuestionType.SelectedIndex == 1)
                {
                    AddedMCQ = new McqQuestion();
                    AddedMCQ.QuestionPoolID = questionPool.ID;
                    var QuestionPoolID = new SqlParameter("@questionpoolId", questionPool.ID);
                    var header = new SqlParameter("@Header", QuestionHeader.Text);
                    var result = context.Database.SqlQuery<string>(" exec addMcqQuestion @questionpoolId, @Header", QuestionPoolID, header).FirstOrDefault();
                    if (string.Compare(result, "added sucessfully") == 0)
                    {
                        MessageBox.Show("added");
                    }
                }
                else if (QuestionType.SelectedIndex == 2)
                {
                    
                    if (TrueAnswer.IsChecked == true)
                    {

                        

                        var QuestionPoolID = new SqlParameter("@questionpoolId", questionPool.ID);
                        var header = new SqlParameter("@Header", QuestionHeader.Text);
                        var answer = new SqlParameter("@Answer", "True");
                        var result = context.Database.SqlQuery<string>(" exec AddTrueFalseQuestionPool @questionpoolId, @Header,@Answer ", QuestionPoolID, header, answer).FirstOrDefault();
                        if (string.Compare(result, "added sucessfully") == 0)
                        {
                            MessageBox.Show("added");
                        }

                    }
                    else if (FalseAnswer.IsChecked == true)
                    {
                        //trueFalseQuestion.Answer = false;
                        var QuestionPoolID = new SqlParameter("@questionpoolId", questionPool.ID);
                        var header = new SqlParameter("@Header", QuestionHeader.Text);
                        var answer = new SqlParameter("@Answer", "False");
                        var result = context.Database.SqlQuery<string>(" exec AddTrueFalseQuestionPool @questionpoolId, @Header,@Answer ", QuestionPoolID, header, answer).FirstOrDefault();
                        if (string.Compare(result, "added sucessfully") == 0)
                        {
                            MessageBox.Show("added");
                        }


                    }
                    
                }
                context.SaveChanges();
            }
        }
        private void LoadQuestion()
        {
            var id = (from c in context.Courses
                      where c.Name == Displaycourse.SelectedItem.ToString()
                      select c.ID).First();
            var query = (from Q in context.QuestionPools
                         where Q.CourseID == id
                         select Q);
            DisplayQuestion.ItemsSource = query.ToList();
        }
        private void Displaycourse_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            LoadQuestion();
        }

        private void ButtonEdit_click(object sender, RoutedEventArgs e)
        {

            //QuestionPool QuestionRow = DisplayQuestion.SelectedItem as QuestionPool;

            //QuestionPool question = (from Q in context.QuestionPools
            //                     where Q.ID == QuestionRow.ID
            //                     select Q).Single();
            //question.Type = QuestionRow.Type;
            //question.Body = QuestionRow.Body;
            //context.SaveChanges();
            //MessageBox.Show("Row Updated Successfully.");
            ////LoadQuestion();
        }

        private void ButtonDelete_click(object sender, RoutedEventArgs e)
        {

            //QuestionPool QuestionRow = DisplayQuestion.SelectedItem as QuestionPool;

            //QuestionPool question = (from Q in context.QuestionPools
            //                         where Q.ID == QuestionRow.ID
            //                         select Q).Single();
            //context.QuestionPools.Remove(question);
            //context.SaveChanges();
            //MessageBox.Show("Row Deleted Successfully.");
            //LoadQuestion();
        }

        private void AddChoise_Click(object sender, RoutedEventArgs e)
        {
            bool ans;
            if (Textchoise.Text == "")
            {
                MessageBox.Show("you must write in textbox the choise you  want to add ");
            }
            else
            {
                ViewChoise.Items.Add(newItem: (Textchoise.Text, answer.Text));

                if (answer.Text=="True")
                    ans = true;
                else
                    ans = false;
                context.Choices.Add(new Choice() { Body = Textchoise.Text, Answer = ans,QuestionPoolID= AddedMCQ.QuestionPoolID });

            }
            context.SaveChanges();

        }
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            ExamWindow examWindow = new ExamWindow(InstructorID);
            examWindow.Show();
        }

        private void ViewChoise_AddingNewItem(object sender, AddingNewItemEventArgs e)
        {
         
        }
    }
}
