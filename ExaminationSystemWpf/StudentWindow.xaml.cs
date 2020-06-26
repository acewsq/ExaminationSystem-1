using System;
using System.Collections.Generic;
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
using System.Windows.Threading;

namespace ExaminationSystemWpf
{
    /// <summary>
    /// Interaction logic for StudentWindow.xaml
    /// </summary>
    public partial class StudentWindow : Window
    {
        Service service = new Service();
        int StudentID;
        TextBox textBox;
        RadioButton True;
        RadioButton False;
        CheckBox checkBox;
        DispatcherTimer timer;
        TimeSpan time;
        public StudentWindow(int Stdid)
        {
            InitializeComponent();
            CheckExam.Visibility = Visibility.Collapsed;
            SolveExam.Visibility = Visibility.Collapsed;
            Degrees.Visibility = Visibility.Collapsed;

            StudentID = Stdid;
            timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.Tick += Timer_Tick;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if(time.TotalSeconds>0)
            {
                time-=new TimeSpan(0,0,1);
                txttime.Text = time.ToString();
            }
            else
            {
                txttime.Text = time.ToString();
                timer.Stop();
                submit();
              

            }
        }

        private void Loadcombo()
        {
            cmbStudentCourses.ItemsSource = (from c in service.context.Courses
                                             from t in service.context.InstructorCourseStudent
                                             where t.CourseID == c.ID
                                             where t.StudentID == StudentID
                                             select c).Distinct().ToList();
            cmbStudentCourses.DisplayMemberPath = "Name";
            cmbStudentCourses.SelectedValuePath = "ID";

            cmbStudentCourses1.ItemsSource = (from c in service.context.Courses
                                              from t in service.context.InstructorCourseStudent
                                              where t.CourseID == c.ID
                                              where t.StudentID == StudentID
                                              select c).Distinct().ToList();
            cmbStudentCourses1.DisplayMemberPath = "Name";
            cmbStudentCourses1.SelectedValuePath = "ID";
        }
        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (List1.SelectedItems != null)
            {
                if (List1.SelectedIndex == 0)
                {
                    CheckExam.Visibility = Visibility.Visible;
                    SolveExam.Visibility = Visibility.Collapsed;
                    Loadcombo();

                }
                else
                    CheckExam.Visibility = Visibility.Collapsed;

                if (List1.SelectedIndex == 1)
                {
                    Degrees.Visibility = Visibility.Visible;
                    SolveExam.Visibility = Visibility.Collapsed;
                    Loadcombo();
                    this.Topmost = false;

                }
                else
                    Degrees.Visibility = Visibility.Collapsed;

            }
        }

        private void CmbStudentCourses_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbStudentCourses.SelectedItem != null)
            {
                try
                {
                    cmbStudentExams.ItemsSource = service.CourseExams((short)cmbStudentCourses.SelectedValue);
                    cmbStudentExams.DisplayMemberPath = "Type";
                    cmbStudentExams.SelectedValuePath = "ID";
                }
                catch (Exception ex)
                {

                }

            }
        }

        private void CmbStudentExams_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {


        }

        private void BtnStart_Click(object sender, RoutedEventArgs e)
        {
            if (cmbStudentExams.SelectedValue != null)
            {
                var query = service.context.StudentExams.FirstOrDefault(s => s.StudentID == StudentID && s.ExamID == (int)cmbStudentExams.SelectedValue);
                if (query != null)
                {
                    var test = service.context.Answers.FirstOrDefault(a => a.StudentID == StudentID && a.ExamQuestionPool.ExamID == (int)cmbStudentExams.SelectedValue);
                    if (test == null)
                    {
                        CheckExam.Visibility = Visibility.Collapsed;
                        SolveExam.Visibility = Visibility.Visible;
                        index = 0;
                        Textanswwer = new Dictionary<int, string>();
                        mcqanswer = new Dictionary<int, List<string>>();
                        truefalseanswer = new Dictionary<int, bool>();
                        questions = service.GetQuestions((int)cmbStudentExams.SelectedValue);
                        Exam exam = service.context.Exams.FirstOrDefault(ex=>ex.ID== (int)cmbStudentExams.SelectedValue);
                        time = new TimeSpan();
                        time = (TimeSpan)(exam.EndTime - exam.StartTime);
                        timer.Start();
                        txttime.Text =(exam.EndTime-exam.StartTime).ToString();
                        this.Topmost = true;
                        DisplayExam(StudentID, (int)cmbStudentExams.SelectedValue);
                    }
                    else
                    {
                        txtCheckResult.Text = "You are Already Solved This Exam";

                    }
                }
                else
                {
                    txtCheckResult.Text = "You are Not Allowed To solve This Exam";
                }

            }
        }
        int index;
        Dictionary<int, string> Textanswwer;
        Dictionary<int, bool> truefalseanswer;
        Dictionary<int, List<string>> mcqanswer;

        ExamQuestionPool Currentquestion;
        List<ExamQuestionPool> questions;

        private void DisplayExam(int SID, int ExamID)
        {

            Currentquestion = questions.ElementAt(index);
            DisplayQuestion(Currentquestion);

        }
        private void DisplayQuestion(ExamQuestionPool questionPool)
        {

            txtBody.Text = questionPool.QuestionPool.Body;

            if (questionPool.QuestionPool.Type == "Text")
            {
                opthions.Children.Clear();
                var query = (from Q in service.context.QuestionPools
                             from T in service.context.TextQuestions
                             where Q.CourseID == (short)cmbStudentCourses.SelectedValue && Q.Type == "Text" && T.QuestionPoolID == Q.ID
                             where T.QuestionPoolID == questionPool.QuestionPoolID
                             select new { Q.ID, Q.Body, Q.Type, T.Header, T.Answer }).ToList();               //service.context.TextQuestions.Where(t => t.QuestionPoolID == questionPool.QuestionPoolID).ToList();

                txtHeader.Text = query.ElementAt(0).Header;
                textBox = new TextBox() { Foreground = new SolidColorBrush(Color.FromArgb(0xFF, 0x05, 0x83, 0x06)), FontSize = 20, Width = 300, VerticalAlignment = VerticalAlignment.Center };
                opthions.Children.Add(textBox);
                textBox.TextChanged += TextBox_TextChanged;
                try
                {
                    if (Textanswwer[Currentquestion.ID] != "")
                    {
                        textBox.Text = Textanswwer[Currentquestion.ID];
                    }
                }
                catch (Exception e)
                {

                }

            }
            else if (questionPool.QuestionPool.Type == "True/False")
            {
                opthions.Children.Clear();
                var query = (from Q in service.context.QuestionPools
                             from T in service.context.TrueFalseQuestions
                             where Q.CourseID == (short)cmbStudentCourses.SelectedValue && Q.Type == "True/False" && T.QuestionPoolID == Q.ID
                             where T.QuestionPoolID == questionPool.QuestionPoolID
                             select new { Q.ID, Q.Body, Q.Type, T.Header, T.Answer }).ToList();               //service.context.TextQuestions.Where(t => t.QuestionPoolID == questionPool.QuestionPoolID).ToList();

                txtHeader.Text = query.ElementAt(0).Header;
                True = new RadioButton() { Foreground = new SolidColorBrush(Color.FromArgb(0xFF, 0x05, 0x83, 0x06)), FontSize = 20, Content = true, VerticalAlignment = VerticalAlignment.Center };
                False = new RadioButton() { Foreground = new SolidColorBrush(Color.FromArgb(0xFF, 0x05, 0x83, 0x06)), FontSize = 20, Content = false, VerticalAlignment = VerticalAlignment.Center };
                opthions.Children.Add(True);
                opthions.Children.Add(False);
                True.Checked += True_Checked;
                False.Checked += False_Checked;
                try
                {
                    if (truefalseanswer[Currentquestion.ID] == true)
                    {

                        True.IsChecked = true;
                    }
                    else if (truefalseanswer[Currentquestion.ID] == false)
                    { False.IsChecked = true; }

                }
                catch (Exception e)
                {

                }

            }
            else if (questionPool.QuestionPool.Type == "MCQ")
            {
                opthions.Children.Clear();
                var query = (from Q in service.context.QuestionPools
                             from T in service.context.McqQuestions
                             where Q.CourseID == (short)cmbStudentCourses.SelectedValue && Q.Type == "MCQ" && T.QuestionPoolID == Q.ID
                             where T.QuestionPoolID == questionPool.QuestionPoolID
                             select new { Q.ID, Q.Body, Q.Type, T.Header }).ToList();               //service.context.TextQuestions.Where(t => t.QuestionPoolID == questionPool.QuestionPoolID).ToList();

                txtHeader.Text = query.ElementAt(0).Header;
                var choices = service.context.Choices.Where(c => c.QuestionPoolID == questionPool.QuestionPoolID).ToList();
                foreach (var item in choices)
                {
                    checkBox = new CheckBox() { Foreground =new SolidColorBrush(Color.FromArgb(0xFF, 0x05, 0x83, 0x06)), FontSize = 20, Content = item.Body, VerticalAlignment = VerticalAlignment.Center };
                    opthions.Children.Add(checkBox);
                    checkBox.Checked += CheckBox_Checked;
                    checkBox.Unchecked += CheckBox_Unchecked;
                    try
                    {
                        if (mcqanswer.ContainsKey(Currentquestion.ID))
                        {
                            if (mcqanswer[Currentquestion.ID].Contains(checkBox.Content.ToString()))
                                checkBox.IsChecked = true;
                            else
                                checkBox.IsChecked = false;


                        }

                    }
                    catch (Exception e)
                    {

                    }
                }

            }
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            if (mcqanswer.ContainsKey(Currentquestion.ID))
            {
                if (mcqanswer[Currentquestion.ID].Contains((((CheckBox)sender).Content.ToString())))      //checkBox.Content.ToString()))
                    mcqanswer[Currentquestion.ID].Remove((((CheckBox)sender).Content.ToString()));        //checkBox.Content.ToString());
            }
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            if (mcqanswer.ContainsKey(Currentquestion.ID))
            {
                if (!mcqanswer[Currentquestion.ID].Contains(((CheckBox)sender).Content.ToString()))        //(checkBox.Content.ToString()))
                    mcqanswer[Currentquestion.ID].Add((((CheckBox)sender).Content.ToString()));  //checkBox.Content.ToString());
            }
            else
            {
                mcqanswer.Add(Currentquestion.ID, new List<string>());
                mcqanswer[Currentquestion.ID].Add(((CheckBox)sender).Content.ToString());   //checkBox.Content.ToString());
            }
        }

        private void False_Checked(object sender, RoutedEventArgs e)
        {
            if (truefalseanswer.ContainsKey(Currentquestion.ID))
            {
                truefalseanswer[Currentquestion.ID] = false;
            }
            else
            {
                truefalseanswer.Add(Currentquestion.ID, false);
            }

        }

        private void True_Checked(object sender, RoutedEventArgs e)
        {
            if (truefalseanswer.ContainsKey(Currentquestion.ID))
            {
                truefalseanswer[Currentquestion.ID] = true;
            }
            else
            {
                truefalseanswer.Add(Currentquestion.ID, true);
            }
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (Textanswwer.ContainsKey(Currentquestion.ID))
            {
                Textanswwer[Currentquestion.ID] = textBox.Text;
            }
            else
            {
                Textanswwer.Add(Currentquestion.ID, textBox.Text);
            }
        }

        private void submit()
        {
            service.CorrectTrueFalseQuestions(truefalseanswer, StudentID, (int)cmbStudentExams.SelectedValue);
            service.CorrectMCQQuestions(mcqanswer, StudentID, (int)cmbStudentExams.SelectedValue);
            service.CorrectTextQuestions(Textanswwer, StudentID, (int)cmbStudentExams.SelectedValue);
            this.Topmost = false;

            CheckExam.Visibility = Visibility.Visible;
            SolveExam.Visibility = Visibility.Collapsed;
            Loadcombo();
        }
        private void BtnSubmit_Click(object sender, RoutedEventArgs e)
        {
            timer.Stop();
            submit();
        }

        private void BtnPre_Click(object sender, RoutedEventArgs e)
        {
            if (index > 0)
            {
                index--;
                DisplayExam(StudentID, (int)cmbStudentExams.SelectedValue);

            }
        }

        private void BtnNext_Click(object sender, RoutedEventArgs e)
        {
            if (index < questions.Count - 1)
            {
                index++;
                DisplayExam(StudentID, (int)cmbStudentExams.SelectedValue);

            }
        }

        private void CmbStudentCourses1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbStudentCourses1.SelectedItem != null)
            {
                try
                {
                    cmbStudentExams1.ItemsSource = service.CourseExams((short)cmbStudentCourses1.SelectedValue);
                    cmbStudentExams1.DisplayMemberPath = "Type";
                    cmbStudentExams1.SelectedValuePath = "ID";
                }
                catch (Exception ex)
                {

                }

            }
        }

        private void CmbStudentExams1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
          

        }

        private void BtnShowDegree_Click(object sender, RoutedEventArgs e)
        {
            try
            {


                if (cmbStudentExams1.SelectedValue != null)
                {

                    var test = service.context.Answers.FirstOrDefault(a => a.StudentID == StudentID && a.ExamQuestionPool.ExamID == (int)cmbStudentExams.SelectedValue);
                    if (test != null)
                    {
                        Context context1 = new Context();
                        var FinalMark = context1.StudentExams.FirstOrDefault(s => s.ExamID == (int)cmbStudentExams1.SelectedValue && s.StudentID == StudentID).FinalMark;
                        var Mark = context1.Exams.FirstOrDefault(exam => exam.ID == (int)cmbStudentExams1.SelectedValue).Degree;
                        txtFinalMark.Content = FinalMark.ToString();
                        if (FinalMark > (Mark / 2))
                        {
                            txtFinalMarkState.Foreground = Brushes.Green;
                            txtFinalMarkState.Content = "PASSED";

                        }
                        else
                        {
                            txtFinalMarkState.Foreground = Brushes.Red;
                            txtFinalMarkState.Content = "FAILED";

                        }
                    }
                    else
                    {
                        var FinalMark = service.context.StudentExams.FirstOrDefault(s => s.ExamID == (int)cmbStudentExams1.SelectedValue && s.StudentID == StudentID).FinalMark;
                        var Mark = service.context.Exams.FirstOrDefault(exam => exam.ID == (int)cmbStudentExams1.SelectedValue).Degree;
                        txtFinalMark.Content = FinalMark.ToString();
                        if (FinalMark > (Mark / 2))
                        {
                            txtFinalMarkState.Foreground = Brushes.Green;
                            txtFinalMarkState.Content = "PASSED";

                        }
                        else
                        {
                            txtFinalMarkState.Foreground = Brushes.Red;
                            txtFinalMarkState.Content = "FAILED";

                        }
                    }
                }
            }
            catch
            {

            }
        }
    }
}
