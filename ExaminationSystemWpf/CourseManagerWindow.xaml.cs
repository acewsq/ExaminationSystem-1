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
    /// Interaction logic for CourseManagerWindow.xaml
    /// </summary>
    public partial class CourseManagerWindow : Window
    {
        Context context = new Context();
        public CourseManagerWindow()
        {
            InitializeComponent();
            addCourseGrid.Visibility = Visibility.Collapsed;
            updateCourseGrid.Visibility = Visibility.Collapsed;
            DeleteCourseGrid.Visibility = Visibility.Collapsed;



            //for the update
            var crs = context.Courses.Where(c=>c.IsDeleted==false).ToList();
            foreach (var item in crs)
            {
                courses.Items.Add(item.Name);
                coursesList.Items.Add(item.Name);
            }
            //   courses.SelectedItem = crs.First().Name;

        }


        private void load()
        {
            Context context1 = new Context();
            var crs = context.Courses.Where(c=>c.IsDeleted==false).ToList();
            courses.Items.Clear();
            coursesList.Items.Clear();
            foreach (var item in crs)
            {
                courses.Items.Add(item.Name);
                coursesList.Items.Add(item.Name);
            }
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (List1.SelectedItems != null)
            {
                if (List1.SelectedIndex == 0)
                {
                     addCourseGrid.Visibility = Visibility.Visible;
                    load();
                }
                else
                    addCourseGrid.Visibility = Visibility.Collapsed;

                if (List1.SelectedIndex == 1)
                {
                    updateCourseGrid.Visibility = Visibility.Visible;
                    load();
                }
                else
                    updateCourseGrid.Visibility = Visibility.Collapsed;


                if (List1.SelectedIndex == 2)
                {
                    DeleteCourseGrid.Visibility = Visibility.Visible;
                    load();
                }
                else
                    DeleteCourseGrid.Visibility = Visibility.Collapsed;



            }

        }


        private void Add_Course_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (cName.Text != "" && min.Text != "" && max.Text != "")
                {
                    var result = context.Database.SqlQuery<int>("exec AddCourse @name, @hour, @desc ,@minDeg,@maxDeg",
                       new SqlParameter("@name", cName.Text),
                       new SqlParameter("@hour", Int16.Parse(cHours.Text)),
                       new SqlParameter("@desc", cDes.Text),
                       new SqlParameter("@minDeg", Int16.Parse(min.Text)),
                       new SqlParameter("@maxDeg", Int16.Parse(max.Text))
                       );
                    // MessageBox.Show("the return value is : " + result.FirstOrDefault().ToString());
                    if (result.FirstOrDefault() == 1)
                    {
                        MessageBox.Show("Course Added Successfully");
                    }
                    else
                    {
                        MessageBox.Show("Course is already exists");
                    }
                }
                else MessageBox.Show(" plz check the data is entered correctly");

            }
            catch (Exception err)
            {
                MessageBox.Show("some error happens plz check the data ou entered");
            }
        }


        private void update_course_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var result = context.Database.SqlQuery<int>("exec UpdateCourse @name, @hour, @desc,@minDeg,@maxDeg",
                   new SqlParameter("@name", courses.SelectedItem.ToString()),
                   new SqlParameter("@hour", Int16.Parse(cHOURS.Text)),
                   new SqlParameter("@desc", cDES.Text),
                   new SqlParameter("@minDeg", Int16.Parse(MIN.Text)),
                   new SqlParameter("@maxDeg", Int16.Parse(MAX.Text))
                   );

                if (result.FirstOrDefault() == 1)
                {
                    MessageBox.Show("Course updated Successfully");
                }

            }
            catch
            {
                MessageBox.Show("some error happens plz check the data you entered");
            }


        }


        
        private void courses_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var course = context.Courses.FirstOrDefault(c => c.Name == courses.SelectedItem.ToString());
            cHOURS.Text = course.Hours.ToString();
            cDES.Text = course.Discription.ToString();
            MIN.Text = course.MinDegree.ToString();
            MAX.Text = course.MaxDegree.ToString();
        }


        private void courses_del_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var course = context.Courses.FirstOrDefault(c => c.Name == coursesList.SelectedItem.ToString());

        }

        private void delete_course_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                String CrsName = coursesList.SelectedItem.ToString();
                context.Courses.FirstOrDefault(c => c.Name == CrsName).IsDeleted = true;
                context.SaveChanges();

                MessageBox.Show("deleted successfully");
            }
            catch
            {
                MessageBox.Show("some error happen ");
            }
        }



    }
}
