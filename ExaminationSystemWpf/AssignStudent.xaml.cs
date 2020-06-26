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

namespace ExaminationSystemWpf
{
    /// <summary>
    /// Interaction logic for AssignStudent.xaml
    /// </summary>
    public partial class AssignStudent : Window
    {
        int instructorID;
        Context context = new Context();
        public AssignStudent(int Insd)
        {
            InitializeComponent();
            gridAssign.Visibility = Visibility.Collapsed;

            instructorID = Insd;
            cmbInstructorCourses.ItemsSource = (from c in context.Courses
                                                from t in context.InstructorCourseStudent
                                                where t.CourseID == c.ID
                                                where t.InstructorID == instructorID
                                                select c).Distinct().ToList();
            cmbInstructorCourses.DisplayMemberPath = "Name";
            cmbInstructorCourses.SelectedValuePath = "ID";
        }

        private void CmbInstructorCourses_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (cmbInstructorCourses.SelectedValue != null)
                {
                    cmbCoursesExams.ItemsSource = context.Exams.Where(exam => exam.CourseID == (short)cmbInstructorCourses.SelectedValue).Distinct().ToList();
                    cmbCoursesExams.DisplayMemberPath = "Type";
                    cmbCoursesExams.SelectedValuePath = "ID";
                    gridStudent.ItemsSource = (from s in context.Students
                                               from t in context.InstructorCourseStudent
                                               where t.StudentID == s.ID
                                               where t.CourseID == (short)cmbInstructorCourses.SelectedValue
                                               select new { s.ID, s.Name, s.College, s.Email }).ToList();
                    gridStudent.SelectedValuePath = "ID";
                }
            }
            catch { }


        }
        private void GridStudent_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Addstudent_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (cmbInstructorCourses.SelectedValue != null)
                {
                    if (gridStudent.SelectedValue != null)
                    {
                        if (context.StudentExams.FirstOrDefault(s => s.StudentID == (int)gridStudent.SelectedValue) == null)
                        {
                            context.StudentExams.Add(new StudentExam
                            {
                                ExamID = (int)cmbCoursesExams.SelectedValue,
                                StudentID = (int)gridStudent.SelectedValue,
                                FinalMark = 0
                            });

                            context.SaveChanges();
                            MessageBox.Show("Added");
                        }
                        else
                            MessageBox.Show("Already Added");

                    }
                    else
                    {
                        MessageBox.Show("please select student you want add in exam");

                    }
                }

                else
                {
                    MessageBox.Show("please select the Type of Exam you want assin in it student");
                }
            }catch
            { }
        }

        private void DeleteStudent_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (gridStudent.SelectedItem != null)
                {

                    var examID = (int)cmbCoursesExams.SelectedValue;
                    var studentId = (int)gridStudent.SelectedValue;
                    var query = (from S in context.StudentExams
                                 where S.ExamID == examID && S.StudentID == studentId
                                 select S).FirstOrDefault();
                    if (query != null)
                    {
                        context.StudentExams.Remove(query);
                        context.SaveChanges();
                        MessageBox.Show("Deleted");

                    }
                    else
                        MessageBox.Show("Not Assigned");


                }
                else
                {
                    MessageBox.Show("please select the student you want delete from the exam");
                }
            }
            catch { }
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(List1.SelectedItem!=null)
            {
                if(List1.SelectedIndex==0)
                {
                    gridAssign.Visibility = Visibility.Visible;
                }
                else
                    gridAssign.Visibility = Visibility.Collapsed;

            }
        }

        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }
    }
}
