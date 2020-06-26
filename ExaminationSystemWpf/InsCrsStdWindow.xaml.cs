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
    /// Interaction logic for InsCrsStdWindow.xaml
    /// </summary>
    public partial class InsCrsStdWindow : Window
    {
        Context context = new Context();
        public int mangerId { get; set; }
        public InsCrsStdWindow(int id)
        {
           
            InitializeComponent();
            mangerId = id;
            GridInstructorCourseStudent.Visibility = Visibility.Collapsed;
        }
        private void LoadCourse()
        {

            var query =context.Courses.ToList();
            cmbCourse.ItemsSource = query;
            cmbCourse.DisplayMemberPath = "Name";
            cmbCourse.SelectedValuePath = "ID";


        }
        private void LoadInstructor()
        {
            var query = context.Instructors.ToList();
            cmbInstructor.ItemsSource = query;
            cmbInstructor.DisplayMemberPath = "Name";
            cmbInstructor.SelectedValuePath = "ID";
        }
        private void LoadStudent()
        {
            var query = context.Students.ToList();
            cmbStudent.ItemsSource = query;
            cmbStudent.DisplayMemberPath = "Name";
            cmbStudent.SelectedValuePath = "ID";

        }
        private void LoadTrack()
        {

        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (List1.SelectedItems != null)
            {
                if (List1.SelectedIndex == 0)
                {

                    GridInstructorCourseStudent.Visibility = Visibility.Visible;

                    LoadCourse();
                    LoadInstructor();
                    LoadStudent();
                }
                else
                    GridInstructorCourseStudent.Visibility = Visibility.Collapsed;


            }
        }

        private void BtnAddInstructorCourseStudent_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var InstructorId = (short)cmbInstructor.SelectedValue;
                var CourseId = (short)cmbCourse.SelectedValue;
                var StudentId = (int)cmbStudent.SelectedValue;
                if (context.InstructorCourseStudent.FirstOrDefault(ics => ics.CourseID ==CourseId&& ics.InstructorID==InstructorId&&ics.StudentID== StudentId)==null)
                {
                    context.InstructorCourseStudent.Add(new InstructorCourseStudent { CourseID = CourseId, StudentID = StudentId, InstructorID = InstructorId });
                    context.SaveChanges();
                    MessageBox.Show("added");
                }
                else
                    MessageBox.Show("Already Added");

            }
            catch
            {
                MessageBox.Show("somthing wrong");
            }
        }

        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }
    }
}
