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
    /// Interaction logic for ExamWindow.xaml
    /// </summary>
    public partial class ExamWindow : Window
    {
        DispatcherTimer dispatcherTimer;
        bool hidden;
        MessageWindow MessageWindow;
        double panelWidth;
        int InsID;
        Service Service = new Service();
        public ExamWindow(int insID)
        {
            InitializeComponent();
            InsID = insID;

            LoadComboBoxes();
            AddGrid.Visibility = Visibility.Collapsed;
            textGrid.Visibility = Visibility.Collapsed;
            TrufaleGrid.Visibility = Visibility.Collapsed;
            MCQGrid.Visibility = Visibility.Collapsed;
            UpdateGrid.Visibility = Visibility.Collapsed;
            Deletegrid.Visibility = Visibility.Collapsed;


            dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, 10);
            dispatcherTimer.Tick += DispatcherTimer_Tick;
            panelWidth = sidePanel.Width;
        }
        private void LoadComboBoxes()
        {
            try
            {
                cmbInstructorCourses.ItemsSource = Service.InsCourses(InsID);
                cmbInstructorCourses.DisplayMemberPath = "Name";
                cmbInstructorCourses.SelectedValuePath = "ID";

                cmbInstructorCourses1.ItemsSource = Service.InsCourses(InsID);
                cmbInstructorCourses1.DisplayMemberPath = "Name";
                cmbInstructorCourses1.SelectedValuePath = "ID";

                cmbInstructorCourses2.ItemsSource = Service.InsCourses(InsID);
                cmbInstructorCourses2.DisplayMemberPath = "Name";
                cmbInstructorCourses2.SelectedValuePath = "ID";

                cmbInstructorCourses3.ItemsSource = Service.InsCourses(InsID);
                cmbInstructorCourses3.DisplayMemberPath = "Name";
                cmbInstructorCourses3.SelectedValuePath = "ID";


                cmbInstructorCourseupdate.ItemsSource = Service.InsCourses(InsID);
                cmbInstructorCourseupdate.DisplayMemberPath = "Name";
                cmbInstructorCourseupdate.SelectedValuePath = "ID";

                cmbInstructorCourses5.ItemsSource = Service.InsCourses(InsID);
                cmbInstructorCourses5.DisplayMemberPath = "Name";
                cmbInstructorCourses5.SelectedValuePath = "ID";
            }
            catch (Exception e)
            { }
        }

        private void DispatcherTimer_Tick(object sender, EventArgs e)
        {
            if (hidden)
            {
                sidePanel.Width += 1;
                if (sidePanel.Width >= panelWidth)
                {
                    dispatcherTimer.Stop();
                    hidden = false;
                }
            }
            else
            {
                sidePanel.Width -= 1;
                if (sidePanel.Width <= 45)
                {
                    dispatcherTimer.Stop();
                    hidden = true;
                }
            }
        }

        private void PanelHeader_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            dispatcherTimer.Start();
        }


        private void BtnCreateExam_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Exam exam = new Exam()
                {
                    Type = txtExamType.Text,
                    StartTime = TimeSpan.Parse(txtExamStart.Text),
                    EndTime = TimeSpan.Parse(txtExamEnd.Text),
                    CourseID = (short)cmbInstructorCourses.SelectedValue,
                    Degree = 0


                };
                if (Service.AddExam(exam))
                {
                    MessageWindow = new MessageWindow("Added Successfully!");
                    MessageWindow.Show();
                }
                else
                {
                    MessageWindow = new MessageWindow("SomeThing Went Wrong!");
                    MessageWindow.Show();
                }
            }
            catch (Exception ex)
            {
                MessageWindow = new MessageWindow("SomeThing Went Wrong!");
                MessageWindow.Show();
            }

        }

        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                this.Close();
            }
        }

        private void Image_MouseDown_1(object sender, MouseButtonEventArgs e)
        {

            if (e.LeftButton == MouseButtonState.Pressed)
            {
                AddGrid.Visibility = Visibility.Visible;
            }
            else
            {
                AddGrid.Visibility = Visibility.Collapsed;

            }
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (List1.SelectedItems != null)
            {
                if (List1.SelectedIndex == 0)
                {
                    AddGrid.Visibility = Visibility.Visible;
                    LoadComboBoxes();
                }
                else
                    AddGrid.Visibility = Visibility.Collapsed;

                if (List1.SelectedIndex == 1)
                {
                    UpdateGrid.Visibility = Visibility.Visible;
                    LoadComboBoxes();
                }
                else
                    UpdateGrid.Visibility = Visibility.Collapsed;
                if (List1.SelectedIndex == 2)
                {
                    textGrid.Visibility = Visibility.Visible;
                    LoadComboBoxes();
                }
                else
                    textGrid.Visibility = Visibility.Collapsed;

                if (List1.SelectedIndex == 3)
                {
                    TrufaleGrid.Visibility = Visibility.Visible;
                    LoadComboBoxes();
                }
                else
                    TrufaleGrid.Visibility = Visibility.Collapsed;
                if (List1.SelectedIndex == 4)
                {
                    MCQGrid.Visibility = Visibility.Visible;
                    LoadComboBoxes();
                }
                else
                    MCQGrid.Visibility = Visibility.Collapsed;
                if (List1.SelectedIndex == 5)
                {
                    Deletegrid.Visibility = Visibility.Visible;
                    LoadComboBoxes();
                    ;
                }
                else
                    Deletegrid.Visibility = Visibility.Collapsed;

            }
        }

        private void CmbInstructorCourses1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbInstructorCourses1.SelectedItem != null)
            {
                try
                {
                    cmbInstructorExams.ItemsSource = Service.CourseExams((short)cmbInstructorCourses1.SelectedValue);
                    cmbInstructorExams.DisplayMemberPath = "Type";
                    cmbInstructorExams.SelectedValuePath = "ID";
                }
                catch (Exception ex)
                {

                }
                try
                {
                    gridTextquestions.ItemsSource = (from t in Service.context.TextQuestions
                                                     where t.questionPool.CourseID == (short)cmbInstructorCourses1.SelectedValue
                                                     select new { ID = t.QuestionPoolID, t.Header, t.questionPool.Body, t.Answer }).ToList();

                    gridTextquestions.SelectedValuePath = "ID";

                }
                catch (Exception ex)
                {

                }
            }
        }

        private void CmbInstructorExams_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbInstructorExams.SelectedItem != null)
            {
                try
                {
                    Exam exam = Service.getExam((int)cmbInstructorExams.SelectedValue);
                    examtype.Text = exam.Type;
                    examStart.Text = exam.StartTime.ToString();
                    examEnd.Text = exam.EndTime.ToString();
                    degree.Text = exam.Degree.ToString();

                }
                catch (Exception ex)
                {

                }
            }
        }

        private void BtnAddTextQues_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Service.CheckExamMark((int)cmbInstructorExams.SelectedValue, byte.Parse(textmark.Text)))
                {
                    if (gridTextquestions.SelectedValue != null && textmark.Text != null)
                    {
                        string result = Service.AddQuestion((int)cmbInstructorExams.SelectedValue, (int)gridTextquestions.SelectedValue, byte.Parse(textmark.Text));
                        MessageWindow = new MessageWindow(result);
                        MessageWindow.Show();
                        try
                        {
                            Exam exam = Service.getExam((int)cmbInstructorExams.SelectedValue);
                            examtype.Text = exam.Type;
                            examStart.Text = exam.StartTime.ToString();
                            examEnd.Text = exam.EndTime.ToString();
                            degree.Text = exam.Degree.ToString();

                        }
                        catch (Exception ex)
                        {

                        }
                    }
                }
                else
                {
                    MessageWindow = new MessageWindow("Exam Mark out of Range");
                    MessageWindow.Show();
                }

            }
            catch (Exception e1)
            {
                MessageWindow = new MessageWindow("Something went wrong !");
                MessageWindow.Show();
            }
        }

        private void BtnAddtruefalseQues_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                if (Service.CheckExamMark((int)cmbInstructorExams1.SelectedValue, byte.Parse(textmark1.Text)))
                {
                    if (gridtruefalsequestions.SelectedValue != null && textmark1.Text != null)
                    {
                        string result = Service.AddQuestion((int)cmbInstructorExams1.SelectedValue, (int)gridtruefalsequestions.SelectedValue, byte.Parse(textmark1.Text));
                        MessageWindow = new MessageWindow(result);
                        MessageWindow.Show();
                        try
                        {
                            Exam exam = Service.getExam((int)cmbInstructorExams.SelectedValue);
                            examtype1.Text = exam.Type;
                            examStart1.Text = exam.StartTime.ToString();
                            examEnd1.Text = exam.EndTime.ToString();
                            degree1.Text = exam.Degree.ToString();

                        }
                        catch (Exception ex)
                        {

                        }
                    }
                }
                else
                {
                    MessageWindow = new MessageWindow("Exam Mark out of Range");
                    MessageWindow.Show();
                }

            }
            catch (Exception e1)
            {
                MessageWindow = new MessageWindow("Something went wrong !");
                MessageWindow.Show();
            }
        }

        private void CmbInstructorCourses2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (cmbInstructorCourses2.SelectedItem != null)
            {
                try
                {
                    cmbInstructorExams1.ItemsSource = Service.CourseExams((short)cmbInstructorCourses2.SelectedValue);
                    cmbInstructorExams1.DisplayMemberPath = "Type";
                    cmbInstructorExams1.SelectedValuePath = "ID";
                }
                catch (Exception ex)
                {

                }
                try
                {
                    gridtruefalsequestions.ItemsSource = (from t in Service.context.TrueFalseQuestions
                                                          where t.QuestionPool.CourseID == (short)cmbInstructorCourses2.SelectedValue
                                                          select new { ID = t.QuestionPoolID, t.Header, t.QuestionPool.Body, t.Answer }).ToList();

                    gridtruefalsequestions.SelectedValuePath = "ID";

                }
                catch (Exception ex)
                {

                }
            }
        }

        private void CmbInstructorExams1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (cmbInstructorExams1.SelectedItem != null)
            {
                try
                {
                    Exam exam = Service.getExam((int)cmbInstructorExams1.SelectedValue);
                    examtype1.Text = exam.Type;
                    examStart1.Text = exam.StartTime.ToString();
                    examEnd1.Text = exam.EndTime.ToString();
                    degree1.Text = exam.Degree.ToString();


                }
                catch (Exception ex)
                {

                }
            }
        }

        private void CmbInstructorCourses3_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (cmbInstructorCourses3.SelectedItem != null)
            {
                try
                {
                    cmbInstructorExams2.ItemsSource = Service.CourseExams((short)cmbInstructorCourses3.SelectedValue);
                    cmbInstructorExams2.DisplayMemberPath = "Type";
                    cmbInstructorExams2.SelectedValuePath = "ID";
                }
                catch (Exception ex)
                {

                }
                try
                {
                    gridmcqquestions.ItemsSource = (from t in Service.context.McqQuestions
                                                    where t.QuestionPool.CourseID == (short)cmbInstructorCourses3.SelectedValue
                                                    select new { ID = t.QuestionPoolID, t.Header, t.QuestionPool.Body }).ToList();

                    gridmcqquestions.SelectedValuePath = "ID";

                }
                catch (Exception ex)
                {

                }
            }
        }

        private void CmbInstructorExams2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbInstructorExams2.SelectedItem != null)
            {
                try
                {
                    Exam exam = Service.getExam((int)cmbInstructorExams2.SelectedValue);
                    examtype2.Text = exam.Type;
                    examStart2.Text = exam.StartTime.ToString();
                    examEnd2.Text = exam.EndTime.ToString();
                    degree2.Text = exam.Degree.ToString();

                }
                catch (Exception ex)
                {

                }
            }
        }

        private void BtnAddmcqQues_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                if (Service.CheckExamMark((int)cmbInstructorExams2.SelectedValue, byte.Parse(textmark2.Text)))
                {
                    if (gridmcqquestions.SelectedValue != null && textmark2.Text != null)
                    {
                        string result = Service.AddQuestion((int)cmbInstructorExams2.SelectedValue, (int)gridmcqquestions.SelectedValue, byte.Parse(textmark2.Text));
                        MessageWindow = new MessageWindow(result);
                        MessageWindow.Show();
                        try
                        {
                            Exam exam = Service.getExam((int)cmbInstructorExams.SelectedValue);
                            examtype2.Text = exam.Type;
                            examStart2.Text = exam.StartTime.ToString();
                            examEnd2.Text = exam.EndTime.ToString();
                            degree2.Text = exam.Degree.ToString();

                        }
                        catch (Exception ex)
                        {

                        }

                    }
                }
                else
                {
                    MessageWindow = new MessageWindow("Exam Mark out of Range");
                    MessageWindow.Show();
                }

            }
            catch (Exception e1)
            {
                MessageWindow = new MessageWindow("Something went wrong !");
                MessageWindow.Show();
            }
        }

        private void BtnUpdateExam_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Exam exam = Service.context.Exams.FirstOrDefault(ex => ex.ID == (int)cmbInstructorExamsupdate.SelectedValue);
                exam.Type = txtExamTypeupdate.Text;
                exam.StartTime = TimeSpan.Parse(txtExamStartupdate.Text);
                exam.EndTime = TimeSpan.Parse(txtExamEndupdate.Text);
                Service.context.SaveChanges();
                MessageWindow = new MessageWindow("Updated Successfully");
                MessageWindow.Show();
            }
            catch (Exception exp)
            {
                MessageWindow = new MessageWindow("Something went wrong!");
                MessageWindow.Show();
            }

        }

        private void CmbInstructorExamsupdate_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (cmbInstructorExamsupdate.SelectedItem != null)
            {
                try
                {
                    Exam exam = Service.getExam((int)cmbInstructorExamsupdate.SelectedValue);
                    txtExamTypeupdate.Text = exam.Type;
                    txtExamStartupdate.Text = exam.StartTime.ToString();
                    txtExamEndupdate.Text = exam.EndTime.ToString();
                    Examdegreeupdate.Content = exam.Degree.ToString();


                }
                catch (Exception ex)
                {

                }
            }
        }

        private void CmbInstructorCourseupdate_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                cmbInstructorExamsupdate.ItemsSource = Service.CourseExams((short)cmbInstructorCourseupdate.SelectedValue);
                cmbInstructorExamsupdate.DisplayMemberPath = "Type";
                cmbInstructorExamsupdate.SelectedValuePath = "ID";
            }
            catch (Exception ex)
            {

            }
        }

        private void CmbInstructorCourses5_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                cmbInstructorExams5.ItemsSource = Service.CourseExams((short)cmbInstructorCourses5.SelectedValue);
                cmbInstructorExams5.DisplayMemberPath = "Type";
                cmbInstructorExams5.SelectedValuePath = "ID";
            }
            catch (Exception ex)
            {

            }
        }

        private void CmbInstructorExams5_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (cmbInstructorExams5.SelectedItem != null)
            {
                try
                {
                    Exam exam = Service.getExam((int)cmbInstructorExams5.SelectedValue);
                    examtype5.Text = exam.Type;
                    examStart5.Text = exam.StartTime.ToString();
                    examEnd5.Text = exam.EndTime.ToString();
                    degree5.Text = exam.Degree.ToString();

                    gridquestions.ItemsSource = (from t in Service.context.ExamQuestionPools
                                                 where t.ExamID == (int)cmbInstructorExams5.SelectedValue
                                                 select new { ID = t.QuestionPoolID, t.QuestionPool.Type, t.QuestionPool.Body, t.Mark }).ToList();
                    gridquestions.SelectedValuePath = "ID";

                }
                catch (Exception ex)
                {

                }
            }
        }

        private void BtnDeleteQues_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                
                var question = Service.context.ExamQuestionPools.FirstOrDefault(exa => exa.QuestionPoolID == (int)gridquestions.SelectedValue
                  && exa.ExamID == (int)cmbInstructorExams5.SelectedValue);
                if (Service.context.Answers.FirstOrDefault(ans => ans.ExamQuestionPoolID == question.ID) == null)
                {
                    byte mark = question.Mark;
                    var query = Service.context.ExamQuestionPools.Remove(question);
                    Service.context.SaveChanges();
                    Exam examx = Service.context.Exams.FirstOrDefault(ex => ex.ID == (int)cmbInstructorExams5.SelectedValue);
                    examx.Degree -= mark;
                    Service.context.SaveChanges();
                    MessageWindow message = new MessageWindow("Deleted Successfully");
                    message.Show();
                    if (cmbInstructorExams5.SelectedItem != null)
                    {
                        try
                        {
                            Exam exam = Service.getExam((int)cmbInstructorExams5.SelectedValue);
                            examtype5.Text = exam.Type;
                            examStart5.Text = exam.StartTime.ToString();
                            examEnd5.Text = exam.EndTime.ToString();
                            degree5.Text = exam.Degree.ToString();

                            gridquestions.ItemsSource = (from t in Service.context.ExamQuestionPools
                                                         where t.ExamID == (int)cmbInstructorExams5.SelectedValue
                                                         select new { ID = t.QuestionPoolID, t.QuestionPool.Type, t.QuestionPool.Body, t.Mark }).ToList();
                            gridquestions.SelectedValuePath = "ID";

                        }
                        catch (Exception ex)
                        {

                        }
                    }
                }
                else
                {
                    MessageWindow messageWindow = new MessageWindow("Student Answered This Exam Can't Delete");
                    messageWindow.Show();
                }
            }
            catch(Exception exp )
            {

            }
        }

        private void ListViewItem_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }
    } 
}
