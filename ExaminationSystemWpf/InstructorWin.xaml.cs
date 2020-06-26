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
    /// Interaction logic for InstructorWin.xaml
    /// </summary>
    public partial class InstructorWin : Window
    {
        int InstructorID;
        public InstructorWin(int insid)
        {
            InitializeComponent();
            InstructorID = insid;
        }

     
        private void Exam_Click(object sender, RoutedEventArgs e)
        {
            ExamWindow examWindow = new ExamWindow(InstructorID);
            examWindow.Show();


        }

        private void QuestionPool_Click(object sender, RoutedEventArgs e)
        {
            WindowQuestion windowQuestion = new WindowQuestion(InstructorID);
            windowQuestion.Show();
        }

        private void Assign_Click(object sender, RoutedEventArgs e)
        {
            AssignStudent assignStudent = new AssignStudent(InstructorID);
            assignStudent.Show();
        }

        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }
    }
}
