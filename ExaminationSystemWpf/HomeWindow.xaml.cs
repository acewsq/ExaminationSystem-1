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
    /// Interaction logic for HomeWindow.xaml
    /// </summary>
    public partial class HomeWindow : Window
    {
        public int InstructorID { get; set; }
        public HomeWindow(int isid)
        {
            InitializeComponent();
            InstructorID = isid;
        }

        private void Btnquestion_Click(object sender, RoutedEventArgs e)
        {
            WindowQuestion windowQuestion = new WindowQuestion(InstructorID);
            windowQuestion.Show();
        }

        private void BtnExam_Click(object sender, RoutedEventArgs e)
        {
            ExamWindow examWindow = new ExamWindow(InstructorID);
            examWindow.Show();
        }
    }
}
