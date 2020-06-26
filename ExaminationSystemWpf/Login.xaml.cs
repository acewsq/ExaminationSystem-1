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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ExaminationSystemWpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Context context = new Context();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var Username = new SqlParameter("@username", (username.Text));
            var Password = new SqlParameter("@password", (password.Text));

            var result = context.Database.SqlQuery<string>(" exec pLogin @username, @password ", Username, Password).FirstOrDefault();


            if (string.Compare(result, "Manager") == 0)
            {
                var query =
                  (from I in context.Instructors
                   from U in context.Users
                   where I.UserID == U.ID
                   where U.Name == username.Text && U.Password == password.Text
                   select I.ID).FirstOrDefault();
                ManagerWindow managerWindow = new ManagerWindow(query);
                managerWindow.Show();
                this.Close();

            }
            else if (string.Compare(result, "Instructor") == 0)
            {
                var query =
                    (from I in context.Instructors
                     from U in context.Users
                     where I.UserID == U.ID
                     where U.Name == username.Text && U.Password == password.Text
                     select I.ID).FirstOrDefault();
                InstructorWin homeWindow = new InstructorWin(query);
                homeWindow.Show();
                this.Close();
            }
            else if (string.Compare(result, "Student") == 0)
            {
                var query =
                  (from I in context.Students
                   from U in context.Users
                   where I.UserID == U.ID
                   where U.Name == username.Text && U.Password == password.Text
                   select I.ID).FirstOrDefault();
                StudentWindow studentWindow = new StudentWindow(query);
                studentWindow.Show();
            }
            else
            {
                MessageWindow message = new MessageWindow("Not Valid User");
                message.Show();

            }


        }

        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

      
    }
}
