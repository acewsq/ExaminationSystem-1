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
    /// Interaction logic for InstructorManagerWindow.xaml
    /// </summary>
    public partial class InstructorManagerWindow : Window
    {
        Context context = new Context();
        public InstructorManagerWindow()
        {

            InitializeComponent();

            add_instructor_grid.Visibility = Visibility.Collapsed;
            Update_instructor_grid.Visibility = Visibility.Collapsed;
            Delete_instructor_grid.Visibility = Visibility.Collapsed;

            ///add instructor
            var branches = context.Branches.ToList();
            foreach (var item in branches)
            {
                bNamecmbox.Items.Add(item.Name);
            }
            add_instructor_grid.Visibility = Visibility.Collapsed;
            Update_instructor_grid.Visibility = Visibility.Collapsed;

            //Update instructor yasmine
            var query1 =
                from i in context.Instructors
                where i.IsDeleted == false
                select i;
            foreach (var item in query1)
            {
                InstructorNames.Items.Add(item);
            }
            InstructorNames.DisplayMemberPath = "Name";
            InstructorNames.SelectedValuePath = "ID";

            List<Branch> branchess = context.Branches.ToList();
            InstructorBranch.ItemsSource = branchess;
            InstructorBranch.DisplayMemberPath = "Name";
            InstructorBranch.SelectedValuePath = "ID";

            //delete instructor yasmine

            var query2 =
              from i in context.Instructors
              where i.IsDeleted == false
              select i;
            foreach (var item in query2)
            {
                InstructorNameCmbox.Items.Add(item);
            }
            InstructorNameCmbox.DisplayMemberPath = "Name";
            InstructorNameCmbox.SelectedValuePath = "ID";
        }


        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (List1.SelectedItems != null)
            {
                if (List1.SelectedIndex == 0)
                {
                    add_instructor_grid.Visibility = Visibility.Visible;

                }
                else
                    add_instructor_grid.Visibility = Visibility.Collapsed;

                if (List1.SelectedIndex == 1)
                {
                    Update_instructor_grid.Visibility = Visibility.Visible;

                }
                else
                    Update_instructor_grid.Visibility = Visibility.Collapsed;

                if (List1.SelectedIndex == 2)
                {
                    Delete_instructor_grid.Visibility = Visibility.Visible;

                }
                else
                    Delete_instructor_grid.Visibility = Visibility.Collapsed;


            }

        }

        //this is for the add button inside the grid
        private void Add_Instructor_Click(object sender, RoutedEventArgs e)
        {
            var query =
                from s in context.Users
                where s.Name == unameTxt.Text
                select s;

            var user = query.FirstOrDefault<User>();


            if (user != null)
            {
                if (user.IsDeleted == false)
                {
                    if (nameTxt.Text != "" && upassTxt.Text != "" && unameTxt.Text != "")
                    {
                        try
                        {
                            //MessageBox.Show("user is not exists");

                            var result = context.Database.SqlQuery<int>("exec AddInstructor @name,@email ,@BranchName , @userName , @userPassword",
                           new SqlParameter("@name", nameTxt.Text),
                           new SqlParameter("@email", emailTxt.Text),
                           new SqlParameter("@BranchName", bNamecmbox.SelectedItem.ToString()),
                           new SqlParameter("@userName", unameTxt.Text),
                           new SqlParameter("@userPassword", upassTxt.Text)
                           );

                            if (result.FirstOrDefault() == 1)
                            {
                                MessageBox.Show("Instructor Added Successfully");
                            }
                            else
                            {
                                MessageBox.Show("Instructor is already exists");
                            }
                        }

                        catch
                        {
                            MessageBox.Show("some error happens plz check the data ou entered");
                        }
                    }
                    else
                    {
                        MessageBox.Show("some data is missing");
                    }

                }
                else
                {
                    MessageBox.Show("user already exists");
                }

            }
            else
            {
                if (nameTxt.Text != "" && upassTxt.Text != "" && unameTxt.Text != "")
                {
                    try
                    {
                        //MessageBox.Show("user is not exists");
                        var result = context.Database.SqlQuery<int>("exec AddInstructor @name,@email ,@BranchName , @userName , @userPassword",
                       new SqlParameter("@name", nameTxt.Text),
                       new SqlParameter("@email", emailTxt.Text),
                       new SqlParameter("@BranchName", bNamecmbox.SelectedItem.ToString()),
                       new SqlParameter("@userName", unameTxt.Text),
                       new SqlParameter("@userPassword", upassTxt.Text)
                       );

                        if (result.FirstOrDefault() == 1)
                        {
                            MessageBox.Show("Instructor Added Successfully");
                        }
                        else
                        {
                            MessageBox.Show("Instructor is already exists");
                        }
                    }

                    catch
                    {
                        MessageBox.Show("some error happens plz check the data ou entered");
                    }
                }
                else
                {
                    MessageBox.Show("some data is missing");
                }
            }


        }


        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void Button_Upd_Click(object sender, RoutedEventArgs e)
        {
            Instructor instructor = context.Instructors
                   .Where(i => i.ID == (short)InstructorNames.SelectedValue).FirstOrDefault();
            if (instructor.IsDeleted == false)
            {
                if (InstructorName.Text != "")
                    instructor.Name = InstructorName.Text;
                if (InstructorEmail.Text != "")
                    instructor.Email = InstructorEmail.Text;
                if (InstructorBranch.Text != "")
                    instructor.BranchID = (byte)InstructorBranch.SelectedValue;
                context.SaveChanges();
                MessageBox.Show("The instructor information has been updated successfully");
            }
            else
            {
                MessageBox.Show("this instructor has been Deleted");
            }

        }

        private void Button_del_Click(object sender, RoutedEventArgs e)
        {
            Instructor instructor = context.Instructors.Where(i => i.ID == (short)InstructorNameCmbox.SelectedValue).FirstOrDefault();
            User user = context.Users.Where(u => u.ID == instructor.UserID).FirstOrDefault();
            instructor.IsDeleted = true;
            user.IsDeleted = true;
            context.SaveChanges();
            MessageBox.Show("this instructor has been deleted successfully");
        }
    }
}
