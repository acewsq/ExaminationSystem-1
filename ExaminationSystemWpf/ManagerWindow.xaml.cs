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
    /// Interaction logic for ManagerWindow.xaml
    /// </summary>
    public partial class ManagerWindow : Window
    {
        Context context = new Context();
        int INStructorId;
        public ManagerWindow(int isid)
        {
            InitializeComponent();
            INStructorId = isid;
        }

/// ////////////////////////////////////////////// new/////////////////////////////////////////////////////

        private void TBI_Click(object sender, RoutedEventArgs e)
        {
            TrackBranchIntakeWindow tbi = new TrackBranchIntakeWindow();
            tbi.Show();
        }
        private void Course_Click(object sender, RoutedEventArgs e)
        {
            CourseManagerWindow c = new CourseManagerWindow();
            c.Show();
        }

        private void Instructor_Click(object sender, RoutedEventArgs e)
        {
            InstructorManagerWindow i = new InstructorManagerWindow();
            i.Show();
        }


        private void Student_Click(object sender, RoutedEventArgs e)
        {
            StudentManagerWindow s = new StudentManagerWindow();
            s.Show();

        }

        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            InstructorWin instructorWin = new InstructorWin(INStructorId);
            instructorWin.Show();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            InsCrsStdWindow insCrsStdWindow = new InsCrsStdWindow(INStructorId);
            insCrsStdWindow.Show();
        }
    }
}
