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
    /// Interaction logic for StudentManagerWindow.xaml
    /// </summary>
    public partial class StudentManagerWindow : Window
    {
        Context context = new Context();
        private void LoadStudents()
        {
            var query =
                (from i in context.Students
                 where i.IsDeleted == false
                 select i).ToList();
            studentNames.Items.Clear();
            StdName.Items.Clear();
            foreach (var item in query)
            {
                studentNames.Items.Add(item);

                StdName.Items.Add(item);

            }
            studentNames.DisplayMemberPath = "Name";
            studentNames.SelectedValuePath = "ID";
            StdName.DisplayMemberPath = "Name";
            StdName.SelectedValuePath = "ID";
        }


        public StudentManagerWindow()
        {
            InitializeComponent();

            AddStd.Visibility = Visibility.Collapsed;
            UpdateStd.Visibility = Visibility.Collapsed;
            DeleteStd.Visibility = Visibility.Collapsed;

            List<Branch> brs = context.Branches.ToList();
            BranchName.ItemsSource = brs;
            BranchName.DisplayMemberPath = "Name";
            BranchName.SelectedValuePath = "ID";
            BranchName.SelectedItem = brs.FirstOrDefault();

            List<Intake> intakes = context.Intakes.ToList();
            InakeNumber.ItemsSource = intakes;
            InakeNumber.DisplayMemberPath = "Number";
            InakeNumber.SelectedValuePath = "ID";
            InakeNumber.SelectedItem = intakes.FirstOrDefault();

            LoadStudents();


                  }

        private void BranchName_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (BranchName.SelectedValue != null)
            {
                var query = (from i in context.BranchIntackTracks
                             from t in context.Tracks
                             where i.TrackID == t.ID
                             where i.BranchID == (byte)BranchName.SelectedValue
                             select t).ToList();
                TrackName.ItemsSource = query;
                TrackName.DisplayMemberPath = "Name";
                TrackName.SelectedValuePath = "ID";

            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (StudentUsername.Text != "" && StudentPassword.Text != ""
              && StudentName.Text != "")
            {
                var query =
                    from use in context.Users
                    where use.Name == StudentUsername.Text
                    select use.Name;
                if (query.FirstOrDefault() != StudentUsername.Text)
                {
                    User user = new User()
                    {
                        Name = StudentUsername.Text,
                        Password = StudentPassword.Text,
                    };
                    context.Users.Add(user);
                    context.SaveChanges();

                    byte intakeID = (byte)InakeNumber.SelectedValue;
                    byte trackID = (byte)TrackName.SelectedValue;
                    byte branchID = (byte)BranchName.SelectedValue;

                    BranchIntackTrack BIT = context.BranchIntackTracks
                        .Where(b => b.IntakeID == intakeID && b.TrackID == trackID && b.BranchID == branchID).FirstOrDefault();
                    int branchIntakeTrackID = BIT.ID;

                    Student student = new Student();
                    student.Name = StudentName.Text;
                    student.College = StudentCollege.Text;
                    student.Email = StudentEmail.Text;
                    student.UserID = user.ID;
                    student.BranchIntackTrackID = branchIntakeTrackID;
                    context.Students.Add(student);
                    context.SaveChanges();
                    MessageBox.Show("Student Added Successfully");
                }
                else
                {
                    MessageBox.Show("Username is already taken");
                }
            }
            else
            {
                MessageBox.Show("Some Empty fields are required");
            }

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Student std =
               context.Students
               .Where(c => c.Name == studentNames.Text)
               .FirstOrDefault();

            if (studentName.Text != "")
                std.Name = studentName.Text;
            if (studentCollege.Text != "")
                std.College = studentCollege.Text;
            if (email.Text != "")
                std.Email = email.Text;

            context.SaveChanges();

            MessageBox.Show("Student Updated Successfully");
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Student student = context.Students.Where(s => s.ID == (int)StdName.SelectedValue).FirstOrDefault();
            User user = context.Users.Where(u => u.ID == student.UserID).FirstOrDefault();
            student.IsDeleted = true;
            user.IsDeleted = true;
            context.SaveChanges();
            MessageBox.Show("This student has been successfully deleted");

        }

        private void List1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (List1.SelectedItems != null)
            {
                if (List1.SelectedIndex == 0)
                {
                    AddStd.Visibility = Visibility.Visible;
                    LoadStudents();
                }
                else
                    AddStd.Visibility = Visibility.Collapsed;

                if (List1.SelectedIndex == 1)
                {
                    UpdateStd.Visibility = Visibility.Visible;
                    LoadStudents();

                }
                else
                    UpdateStd.Visibility = Visibility.Collapsed;

                if (List1.SelectedIndex == 2)
                {
                    DeleteStd.Visibility = Visibility.Visible;
                    LoadStudents();

                }
                else
                    DeleteStd.Visibility = Visibility.Collapsed;


            }
        }

        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

    }
}
