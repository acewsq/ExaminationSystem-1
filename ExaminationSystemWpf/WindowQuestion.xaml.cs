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
    /// Interaction logic for QuestionWindow.xaml
    /// </summary>
    /// 

    public partial class WindowQuestion : Window
    {

        int instructor_id;
        Context context = new Context();

        TextBox textBox;
        TextBox TextboxAnswerChoise;
        StackPanel answerChoise;
        StackPanel stackpanelTrueFalse;
        RadioButton TrueChoise;
        RadioButton FalseChoise;
        RadioButton TrueAnswer;
        RadioButton FalseAnswer;
        StackPanel stackpanelMCQ = new StackPanel();
        Button addChoise;
        QuestionPool questionPool;
        List<Choice> choices = new List<Choice>();
        public WindowQuestion(int id)
        {

            instructor_id = id;
            InitializeComponent();

            GridDeleteQuestion.Visibility = Visibility.Collapsed;
            GridAddQuestion.Visibility = Visibility.Collapsed;
            GridUpdateQuestion.Visibility = Visibility.Collapsed;
            GridShowQuestion.Visibility = Visibility.Collapsed;

            DatGridDisplayQuestion.SelectedValuePath = "ID";
            //textBox = new TextBox();
            //textBox.Width = 450;
            //answerQuestion.Children.Add(textBox);
            DGridDisplayUpdateQuestion.SelectedValuePath = "ID";
            DispayChoiseAnswer.SelectedValuePath = "ID";


        }
        private void LoadCourse(ComboBox comboBox)
        {
            //var query = (from c in context.Courses
            //             from t in context.InstructorCourseStudent
            //             where c.ID == t.CourseID
            //             where t.InstructorID == instructor_id
            //             select c.Name).ToList();

             var query = (from c in context.Courses 
                         from t in context.InstructorCourseStudent
                         where c.ID == t.CourseID
                         where t.InstructorID == instructor_id
                         select c.Name ).Distinct().ToList();
            comboBox.ItemsSource = query;
        }



        //Add Question

        //private void BtnAddQuestion(object sender, RoutedEventArgs e)
        //{
        //    GridDeleteQuestion.Visibility = Visibility.Collapsed;
        //    GridUpdateQuestion.Visibility = Visibility.Collapsed;
        //    GridAddQuestion.Visibility = Visibility.Visible;


        //    LoadCourse(DisplaycourseAdd);
        //}


        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (List1.SelectedItems != null)
            {
                if (List1.SelectedIndex == 0)
                {
                    GridDeleteQuestion.Visibility = Visibility.Collapsed;
                    GridUpdateQuestion.Visibility = Visibility.Collapsed;
                    GridShowQuestion.Visibility = Visibility.Collapsed;
                    GridAddQuestion.Visibility = Visibility.Visible;


                    LoadCourse(DisplaycourseAdd);


                }
                else if (List1.SelectedIndex == 1)


                {
                    GridDeleteQuestion.Visibility = Visibility.Collapsed;
                    GridAddQuestion.Visibility = Visibility.Collapsed;
                    GridUpdateQuestion.Visibility = Visibility.Visible;
                    GridUpdateTrueFalseAnswer.Visibility = Visibility.Collapsed;
                    GridUpdateMcqAnswer.Visibility = Visibility.Collapsed;
                    GridUpdateTextAnswer.Visibility = Visibility.Collapsed;
                    GridAddDeleteChoise.Visibility = Visibility.Collapsed;
                    GridShowQuestion.Visibility = Visibility.Collapsed;


                    LoadCourse(DisplaycourseUpdate);

                }
                else if (List1.SelectedIndex == 2)

                {
                    try
                    {
                        GridAddQuestion.Visibility = Visibility.Collapsed;
                        GridUpdateQuestion.Visibility = Visibility.Collapsed;
                        GridShowQuestion.Visibility = Visibility.Collapsed;
                        GridDeleteQuestion.Visibility = Visibility.Visible;
                        LoadCourse(Displaycourse);
                    }
                    catch
                    {
                        MessageBox.Show("something wrong");
                    }
                }
                else if (List1.SelectedIndex == 3)
                {
                    GridAddQuestion.Visibility = Visibility.Collapsed;
                    GridUpdateQuestion.Visibility = Visibility.Collapsed;
                    GridDeleteQuestion.Visibility = Visibility.Collapsed;
                    GridShowQuestion.Visibility = Visibility.Visible;
                    LoadCourse(cmbDisplayCourseShow);
                }
            }
        }


        private void AddQuestion(object sender, RoutedEventArgs e)
        {
            try
            {
                if (DisplaycourseAdd.SelectedItem != null)
                {
                    var id = (from c in context.Courses
                              where c.Name == DisplaycourseAdd.SelectedItem.ToString()
                              select c.ID).FirstOrDefault();
                    questionPool = new QuestionPool();
                    if (QuestionBody.Text != "")
                    {
                        questionPool.Body = QuestionBody.Text;
                        questionPool.CourseID = id;
                        if (AddQuestionType.Text != "")
                        {
                            questionPool.Type = AddQuestionType.Text;
                            context.QuestionPools.Add(questionPool);
                            context.SaveChanges();


                            if (QuestionHeader.Text != "")
                            {

                                if (AddQuestionType.SelectedIndex == 0)
                                {
                                    if (textBox.Text != "")
                                    {
                                        var QuestionPoolID = new SqlParameter("@questionpoolId", questionPool.ID);
                                        var header = new SqlParameter("@Header", QuestionHeader.Text);
                                        var answer = new SqlParameter("@Answer", (textBox.Text));
                                        var result = context.Database.SqlQuery<string>(" exec addTextQuestion @questionpoolId, @Header,@Answer ", QuestionPoolID, header, answer).FirstOrDefault();
                                        if (string.Compare(result, "added sucessfully") == 0)
                                        {
                                            MessageBox.Show("added");
                                        }
                                    }
                                    else
                                    {
                                        MessageBox.Show("you must add answer of question");
                                    }


                                }
                                else if (AddQuestionType.SelectedIndex == 1)
                                {


                                    var QuestionPoolID = new SqlParameter("@questionpoolId", questionPool.ID);
                                    var header = new SqlParameter("@Header", QuestionHeader.Text);
                                    var result = context.Database.SqlQuery<string>(" exec addMcqQuestion @questionpoolId, @Header", QuestionPoolID, header).FirstOrDefault();
                                    if (string.Compare(result, "added sucessfully") == 0)
                                    {
                                        foreach (var item in choices)
                                        {
                                            item.QuestionPoolID = questionPool.ID;
                                            context.Choices.Add(item);
                                        }
                                        MessageBox.Show("added");
                                    }
                                }
                                else if (AddQuestionType.SelectedIndex == 2)
                                {
                                    if (TrueAnswer.IsChecked == true)
                                    {


                                        var QuestionPoolID = new SqlParameter("@questionpoolId", questionPool.ID);
                                        var header = new SqlParameter("@Header", QuestionHeader.Text);
                                        var answer = new SqlParameter("@Answer", "True");
                                        var result = context.Database.SqlQuery<string>(" exec AddTrueFalseQuestionPool @questionpoolId, @Header,@Answer", QuestionPoolID, header, answer).FirstOrDefault();
                                        if (string.Compare(result, "added sucessfully") == 0)
                                        {
                                            MessageBox.Show("added");
                                        }

                                    }
                                    else if (FalseAnswer.IsChecked == true)
                                    {

                                        var QuestionPoolID = new SqlParameter("@questionpoolId", questionPool.ID);
                                        var header = new SqlParameter("@Header", QuestionHeader.Text);
                                        var answer = new SqlParameter("@Answer", "False");
                                        var result = context.Database.SqlQuery<string>(" exec AddTrueFalseQuestionPool @questionpoolId, @Header,@Answer ", QuestionPoolID, header, answer).FirstOrDefault();
                                        if (string.Compare(result, "added sucessfully") == 0)
                                        {
                                            MessageBox.Show("added");
                                        }


                                    }


                                }
                            }
                            else
                            {
                                MessageBox.Show("You must add Header To question");
                            }
                        }
                        else
                        {
                            MessageBox.Show("You must add choise type of question");
                        }

                    }
                    else
                    {
                        MessageBox.Show("You must add Body To question");
                    }
                }
                context.SaveChanges();
            }
            catch
            {
                MessageBox.Show("You must Enter input Correctly");
            }

        }
        private void AddQuestionType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (AddQuestionType.SelectedIndex == 0)
                {
                    answerQuestion.Children.Clear();
                    textBox = new TextBox();
                    textBox.Width = 450;
                    answerQuestion.Children.Add(textBox);


                }
                else if (AddQuestionType.SelectedIndex == 1)
                {

                    answerQuestion.Children.Clear();

                    TrueChoise = new RadioButton();
                    FalseChoise = new RadioButton();
                    answerChoise = new StackPanel();
                    TextboxAnswerChoise = new TextBox();
                    addChoise = new Button();
                    addChoise.Name = "AddChoise";
                    addChoise.Click += addchoise_click;

                    TextboxAnswerChoise.Width = 150;
                    answerChoise.Width = 100;
                    addChoise.Width = 100;
                    addChoise.Content = "AddChoice";

                    TrueChoise.Content = true;
                    FalseChoise.Content = false;

                    answerChoise.Children.Add(TrueChoise);
                    answerChoise.Children.Add(FalseChoise);

                    answerQuestion.Children.Add(TextboxAnswerChoise);
                    answerQuestion.Children.Add(answerChoise);

                    answerQuestion.Children.Add(addChoise);
                }

                else if (AddQuestionType.SelectedIndex == 2)
                {

                    answerQuestion.Children.Clear();

                    TrueAnswer = new RadioButton();
                    FalseAnswer = new RadioButton();
                    stackpanelTrueFalse = new StackPanel();

                    TrueAnswer.Content = true;

                    FalseAnswer.Content = false;

                    stackpanelTrueFalse.Children.Add(TrueAnswer);

                    stackpanelTrueFalse.Children.Add(FalseAnswer);

                    answerQuestion.Children.Add(stackpanelTrueFalse);
                }
            }
            catch
            {
                MessageBox.Show("You must Enter Data Correctly ");
            }
        }
        private void addchoise_click(object sender, RoutedEventArgs e)
        {
            try
            {
                bool ans;
                if (TextboxAnswerChoise.Text == "")
                {
                    MessageBox.Show("you must write in textbox the choise you  want to add ");
                }
                else
                {
                    //ViewChoise.Items.Add(newItem: (Textchoise.Text, answer.Text));

                    if (TrueChoise.IsChecked == true)
                        ans = true;
                    else
                        ans = false;
                    choices.Add(new Choice
                    {
                        Body = TextboxAnswerChoise.Text,
                        Answer = ans
                    });


                }
            }
            catch
            {
                MessageBox.Show("something wrong");
            }









        }
        //Update Question


        //private void BtnUpdateQuestion(object sender, RoutedEventArgs e)
        //{


        //    GridDeleteQuestion.Visibility = Visibility.Collapsed;
        //    GridAddQuestion.Visibility = Visibility.Collapsed;
        //    GridUpdateQuestion.Visibility = Visibility.Visible;
        //    GridUpdateTrueFalseAnswer.Visibility = Visibility.Collapsed;
        //    GridUpdateMcqAnswer.Visibility = Visibility.Collapsed;
        //    GridUpdateTextAnswer.Visibility = Visibility.Collapsed;
        //    GridAddDeleteChoise.Visibility = Visibility.Collapsed;


        //    LoadCourse(DisplaycourseUpdate);


        //}
        private void LoadChoise(int questionid)
        {
            try
            {
                var id = (from c in context.Courses
                          where c.Name == DisplaycourseUpdate.SelectedItem.ToString()
                          select c.ID).FirstOrDefault();
                var query = (from Q in context.QuestionPools
                             from C in context.Choices
                             where C.QuestionPoolID == Q.ID
                             where Q.CourseID == id && C.QuestionPoolID == questionid
                             select new { C.ID, C.Body, C.Answer });

                DispayChoiseAnswer.ItemsSource = query.ToList();
            }
            catch
            {
                MessageBox.Show("something wrong");
            }

        }
        private void QuestionTypeUpdate_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (QuestionTypeUpdate.SelectedIndex == 0)
                {
                    GridUpdateTrueFalseAnswer.Visibility = Visibility.Collapsed;
                    GridUpdateMcqAnswer.Visibility = Visibility.Collapsed;
                    GridUpdateTextAnswer.Visibility = Visibility.Visible;
                    GridAddDeleteChoise.Visibility = Visibility.Collapsed;
                    LoadTextQuestion(DGridDisplayUpdateQuestion, DisplaycourseUpdate);

                }
                if (QuestionTypeUpdate.SelectedIndex == 1)
                {
                    GridUpdateTrueFalseAnswer.Visibility = Visibility.Collapsed;
                    GridUpdateMcqAnswer.Visibility = Visibility.Visible;
                    GridUpdateTextAnswer.Visibility = Visibility.Collapsed;
                    GridAddDeleteChoise.Visibility = Visibility.Visible;
                    LoadMcqQuestion(DGridDisplayUpdateQuestion, DisplaycourseUpdate);

                }

                else if (QuestionTypeUpdate.SelectedIndex == 2)
                {
                    GridUpdateTrueFalseAnswer.Visibility = Visibility.Visible;
                    GridUpdateMcqAnswer.Visibility = Visibility.Collapsed;
                    GridUpdateTextAnswer.Visibility = Visibility.Collapsed;
                    GridAddDeleteChoise.Visibility = Visibility.Collapsed;
                    LoadTrueFalseQuestion(DGridDisplayUpdateQuestion, DisplaycourseUpdate);



                }
            }
            catch
            {
                MessageBox.Show("something wrong");
            }
        }

        int questionId;
        private void Updatequestion_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (DGridDisplayUpdateQuestion.SelectedItem != null)
                {
                    var questionselected = int.Parse(DGridDisplayUpdateQuestion.SelectedValue.ToString());



                    QuestionPool question = (from Q in context.QuestionPools
                                             where Q.ID == questionId
                                             select Q).Single();
                    if (UpdateQuestionBody.Text != null)
                    {
                        if (UpdateQuestionHeader.Text != null)
                        {
                            if (question.Type == "Text")
                            {
                                if (TextboxUpdateQuestionAnswer.Text != null)
                                {
                                    GridUpdateTrueFalseAnswer.Visibility = Visibility.Collapsed;
                                    GridUpdateMcqAnswer.Visibility = Visibility.Collapsed;
                                    GridUpdateTextAnswer.Visibility = Visibility.Visible;
                                    GridAddDeleteChoise.Visibility = Visibility.Collapsed;


                                    var ID = new SqlParameter("@QuestionPoolID", question.ID);
                                    var Body = new SqlParameter("@QuestionBody", UpdateQuestionBody.Text);
                                    var Header = new SqlParameter("@QuestionHeader", UpdateQuestionHeader.Text);
                                    var Answer = new SqlParameter("@QuestionAnswer", TextboxUpdateQuestionAnswer.Text);
                                    var result = context.Database.SqlQuery<string>(" exec updateTextQuestion @QuestionPoolID, @QuestionBody,@QuestionHeader,@QuestionAnswer ", ID, Body, Header, Answer).FirstOrDefault();
                                    if (string.Compare(result, "Update Succefully") == 0)
                                    {
                                        MessageBox.Show("Updated");
                                        LoadTextQuestion(DGridDisplayUpdateQuestion, DisplaycourseUpdate);

                                    }
                                }
                                else
                                {
                                    MessageBox.Show("you must add the answer of th question");
                                }

                            }
                            else if (question.Type == "True/False")
                            {
                                GridUpdateTrueFalseAnswer.Visibility = Visibility.Visible;
                                GridUpdateMcqAnswer.Visibility = Visibility.Collapsed;
                                GridUpdateTextAnswer.Visibility = Visibility.Collapsed;
                                GridAddDeleteChoise.Visibility = Visibility.Collapsed;

                                var ID = new SqlParameter("@QuestionPoolID", question.ID);
                                var Body = new SqlParameter("@QuestionBody", UpdateQuestionBody.Text);
                                var Header = new SqlParameter("@QuestionHeader", UpdateQuestionHeader.Text);
                                if (UpdateTrueAnswer.IsChecked == true)
                                {
                                    var Answer = new SqlParameter("@QuestionAnswer", "True");
                                    var result = context.Database.SqlQuery<string>(" exec updateTruefalseQuestion @QuestionPoolID, @QuestionBody,@QuestionHeader,@QuestionAnswer ", ID, Body, Header, Answer).FirstOrDefault();

                                    if (string.Compare(result, "Update Succefully") == 0)
                                    {
                                        MessageBox.Show("Updated");
                                        LoadTrueFalseQuestion(DGridDisplayUpdateQuestion, DisplaycourseUpdate);

                                    }

                                }
                                else if (UpdateFalseAnswer.IsChecked == true)
                                {
                                    var Answer = new SqlParameter("@QuestionAnswer", "False");
                                    var result = context.Database.SqlQuery<string>(" exec updateTruefalseQuestion @QuestionPoolID, @QuestionBody,@QuestionHeader,@QuestionAnswer ", ID, Body, Header, Answer).FirstOrDefault();

                                    if (string.Compare(result, "Update Succefully") == 0)
                                    {
                                        MessageBox.Show("Updated");
                                        LoadTrueFalseQuestion(DGridDisplayUpdateQuestion, DisplaycourseUpdate);

                                    }
                                }
                                else
                                {
                                    MessageBox.Show("you must check the answer of question");
                                }
                            }
                            else if (question.Type == "MCQ")
                            {
                                GridUpdateTrueFalseAnswer.Visibility = Visibility.Collapsed;
                                GridUpdateMcqAnswer.Visibility = Visibility.Visible;
                                GridUpdateTextAnswer.Visibility = Visibility.Collapsed;
                                GridAddDeleteChoise.Visibility = Visibility.Visible;

                                var ID = new SqlParameter("@QuestionPoolID", question.ID);
                                var Body = new SqlParameter("@QuestionBody", UpdateQuestionBody.Text);
                                var Header = new SqlParameter("@QuestionHeader", UpdateQuestionHeader.Text);

                                var result = context.Database.SqlQuery<string>(" exec updateMcqQuestion @QuestionPoolID, @QuestionBody,@QuestionHeader ", ID, Body, Header).FirstOrDefault();
                                if (string.Compare(result, "Update Succefully") == 0)
                                {
                                    MessageBox.Show("Updated");
                                    LoadMcqQuestion(DGridDisplayUpdateQuestion, DisplaycourseUpdate);

                                }
                            }
                        }
                        else
                        {
                            MessageBox.Show("you must enter Header of the question you want updated");
                        }
                    }

                    else
                    {
                        MessageBox.Show("you must enter Body of the question you want updated");
                    }
                }
                else
                {
                    MessageBox.Show("you must select question you want update");
                }
            }
            catch
            {
                MessageBox.Show("please check the input data");
            }

        }

        private void BtnaddChoise(object sender, RoutedEventArgs e)
        {

            try
            {


                var questionId = int.Parse(DGridDisplayUpdateQuestion.SelectedValue.ToString());
                QuestionPool question = (from Q in context.QuestionPools
                                         where Q.ID == questionId
                                         select Q).Single();
                bool ans;
                if (TextBoxUpdateChoiseAnswer.Text == "")
                {
                    MessageBox.Show("you must write in textbox the choise you  want to add ");
                }
                else
                {


                    if (UpdateTrueChoiseAnswer.IsChecked == true)
                        ans = true;
                    else
                        ans = false;
                    context.Choices.Add(new Choice() { Body = TextBoxUpdateChoiseAnswer.Text, Answer = ans, QuestionPoolID = question.ID });
                    context.SaveChanges();
                    LoadChoise(questionId);
                }


            }
            catch
            {
                MessageBox.Show("please check input data");
            }
        }

        private void Displayquestion_selectionchanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {

                if (DGridDisplayUpdateQuestion.SelectedItem != null)
                {


                    if (QuestionTypeUpdate.SelectedIndex == 0)
                    {

                        GridUpdateTrueFalseAnswer.Visibility = Visibility.Collapsed;
                        GridUpdateMcqAnswer.Visibility = Visibility.Collapsed;
                        GridUpdateTextAnswer.Visibility = Visibility.Visible;
                        GridAddDeleteChoise.Visibility = Visibility.Collapsed;

                        questionId = int.Parse(DGridDisplayUpdateQuestion.SelectedValue.ToString());
                        var id = (from c in context.Courses
                                  where c.Name == DisplaycourseUpdate.SelectedItem.ToString()
                                  select c.ID).FirstOrDefault();
                        var query = (from Q in context.QuestionPools
                                     from T in context.TextQuestions
                                     where Q.ID == T.QuestionPoolID
                                     where Q.CourseID == id && Q.Type == "Text" && T.QuestionPoolID == questionId
                                     select new { Q.ID, Q.Body, Q.Type, T.Header, T.Answer }).FirstOrDefault();
                        UpdateQuestionBody.Text = query.Body;
                        UpdateQuestionHeader.Text = query.Header;
                        TextboxUpdateQuestionAnswer.Text = query.Answer;






                    }
                    else if (QuestionTypeUpdate.SelectedIndex == 1)
                    {
                        GridUpdateTrueFalseAnswer.Visibility = Visibility.Collapsed;
                        GridUpdateMcqAnswer.Visibility = Visibility.Visible;
                        GridUpdateTextAnswer.Visibility = Visibility.Collapsed;
                        GridAddDeleteChoise.Visibility = Visibility.Visible;

                        questionId = int.Parse(DGridDisplayUpdateQuestion.SelectedValue.ToString());

                        var id = (from c in context.Courses
                                  where c.Name == DisplaycourseUpdate.SelectedItem.ToString()
                                  select c.ID).FirstOrDefault();
                        var query = (from Q in context.QuestionPools
                                     from M in context.McqQuestions
                                     where Q.ID == M.QuestionPoolID
                                     where Q.CourseID == id && Q.Type == "MCQ" && M.QuestionPoolID == questionId
                                     select new { Q.ID, Q.Body, Q.Type, M.Header }).FirstOrDefault();
                        UpdateQuestionBody.Text = query.Body;
                        UpdateQuestionHeader.Text = query.Header;

                        LoadChoise(questionId);


                    }
                    else if (QuestionTypeUpdate.SelectedIndex == 2)
                    {
                        questionId = int.Parse(DGridDisplayUpdateQuestion.SelectedValue.ToString());
                        GridUpdateTrueFalseAnswer.Visibility = Visibility.Visible;
                        GridUpdateMcqAnswer.Visibility = Visibility.Collapsed;
                        GridUpdateTextAnswer.Visibility = Visibility.Collapsed;
                        GridAddDeleteChoise.Visibility = Visibility.Collapsed;



                        var id = (from c in context.Courses
                                  where c.Name == DisplaycourseUpdate.SelectedItem.ToString()
                                  select c.ID).FirstOrDefault();
                        var query = (from Q in context.QuestionPools
                                     from T in context.TrueFalseQuestions
                                     where Q.ID == T.QuestionPoolID
                                     where Q.CourseID == id && Q.Type == "True/False" && T.QuestionPoolID == questionId
                                     select new { Q.ID, Q.Body, Q.Type, T.Header, T.Answer }).FirstOrDefault();
                        UpdateQuestionBody.Text = query.Body;
                        UpdateQuestionHeader.Text = query.Header;
                        if (query.Answer == true)
                        {
                            UpdateTrueAnswer.IsChecked = true;
                        }
                        else
                        {
                            UpdateFalseAnswer.IsChecked = true;
                        }


                    }
                }

            }
            catch
            {
                MessageBox.Show("something wrong");
            }


        }
        private void DeleteChoise(object sender, RoutedEventArgs e)
        {
            try
            {
                if (DispayChoiseAnswer.SelectedItem != null)
                {
                    var ChoiseId = int.Parse(DispayChoiseAnswer.SelectedValue.ToString());
                    var query
                        = (from C in context.Choices
                           where C.ID == ChoiseId
                           select C).Single();
                    context.Choices.Remove(query);
                    context.SaveChanges();
                    LoadChoise(questionId);
                }
                else
                {
                    MessageBox.Show("please select the choise you want delete from the table of answer");
                }
            }
            catch
            {
                MessageBox.Show("something wrong");
            }

        }








        //Delete Question
        //private void DeleteQuestion_Click1(object sender, RoutedEventArgs e)
        //{
        //    try { 
        //    GridAddQuestion.Visibility = Visibility.Collapsed;
        //    GridUpdateQuestion.Visibility = Visibility.Collapsed;
        //    GridDeleteQuestion.Visibility = Visibility.Visible;
        //    LoadCourse(Displaycourse);
        //    }
        //    catch
        //    {
        //        MessageBox.Show("something wrong");
        //    }
        //}
        private void LoadTextQuestion(DataGrid dataGrid, ComboBox comboBox)
        {
            try
            {
                var id = (from c in context.Courses
                          where c.Name == comboBox.SelectedItem.ToString()
                          select c.ID).FirstOrDefault();
                var query = (from Q in context.QuestionPools
                             from T in context.TextQuestions
                             where Q.CourseID == id && Q.Type == "Text" && T.QuestionPoolID == Q.ID
                             select new { Q.ID, Q.Body, Q.Type, T.Header, T.Answer });
                dataGrid.ItemsSource = query.ToList();
            }
            catch
            {
                MessageBox.Show("something wrong");
            }
        }
        private void LoadMcqQuestion(DataGrid dataGrid, ComboBox comboBox)
        {
            try
            {
                var id = (from c in context.Courses
                          where c.Name == comboBox.SelectedItem.ToString()
                          select c.ID).FirstOrDefault();
                var query = (from Q in context.QuestionPools
                             from M in context.McqQuestions
                             where Q.CourseID == id && Q.Type == "MCQ" && M.QuestionPoolID == Q.ID
                             select new { Q.ID, Q.Body, Q.Type, M.Header });
                dataGrid.ItemsSource = query.ToList();
            }
            catch
            {
                MessageBox.Show("something wrong");
            }
        }
        private void LoadTrueFalseQuestion(DataGrid dataGrid, ComboBox comboBox)
        {
            try
            {
                var id = (from c in context.Courses
                          where c.Name == comboBox.SelectedItem.ToString()
                          select c.ID).FirstOrDefault();
                var query = (from Q in context.QuestionPools
                             from T in context.TrueFalseQuestions
                             where Q.CourseID == id && Q.Type == "True/False" && T.QuestionPoolID == Q.ID
                             select new { Q.ID, Q.Body, Q.Type, T.Header, T.Answer });
                dataGrid.ItemsSource = query.ToList();
            }
            catch
            {
                MessageBox.Show("something wrong");
            }
        }

        private void QuestionType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (QuestionType.SelectedIndex == 0)
                {
                    LoadTextQuestion(DatGridDisplayQuestion, Displaycourse);



                }
                else if (QuestionType.SelectedIndex == 1)
                {
                    LoadMcqQuestion(DatGridDisplayQuestion, Displaycourse);



                }


                else if (QuestionType.SelectedIndex == 2)
                {
                    LoadTrueFalseQuestion(DatGridDisplayQuestion, Displaycourse);

                    //DisplayQuestion.Children.Add(DatGridDisplayQuestion);

                }
            }
            catch
            {
                MessageBox.Show("something wrong");
            }
        }

        private void DeleteQuestion_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (DatGridDisplayQuestion.SelectedItem != null)
                {


                    var questionId = int.Parse(DatGridDisplayQuestion.SelectedValue.ToString());
                    QuestionPool question = (from Q in context.QuestionPools
                                             where Q.ID == questionId
                                             select Q).Single();
                    if (context.ExamQuestionPools.FirstOrDefault(c => c.QuestionPoolID == question.ID) == null)
                    {
                        if (question.Type == "Text")
                        {
                            var ID = new SqlParameter("@QuestionPoolID", question.ID);
                            var Type = new SqlParameter("@QuestionType", question.Type);
                            var result = context.Database.SqlQuery<string>(" exec DeleteQuestionpool @QuestionPoolID, @QuestionType ", ID, Type).FirstOrDefault();
                            if (string.Compare(result, "delete succefully") == 0)
                            {
                                MessageBox.Show("Deleted");
                                LoadTextQuestion(DatGridDisplayQuestion, Displaycourse);
                            }

                        }
                        else if (question.Type == "MCQ")
                        {
                            var ID = new SqlParameter("@QuestionPoolID", question.ID);
                            var Type = new SqlParameter("@QuestionType", question.Type);
                            var result = context.Database.SqlQuery<string>(" exec DeleteQuestionpool @QuestionPoolID, @QuestionType ", ID, Type).FirstOrDefault();
                            if (string.Compare(result, "delete succefully") == 0)
                            {
                                MessageBox.Show("Deleted");
                                LoadMcqQuestion(DatGridDisplayQuestion, Displaycourse);
                            }
                        }
                        else if (question.Type == "True/False")
                        {
                            var ID = new SqlParameter("@QuestionPoolID", question.ID);
                            var Type = new SqlParameter("@QuestionType", question.Type);
                            var result = context.Database.SqlQuery<string>(" exec DeleteQuestionpool @QuestionPoolID, @QuestionType ", ID, Type).FirstOrDefault();
                            if (string.Compare(result, "delete succefully") == 0)
                            {
                                MessageBox.Show("Deleted");
                                LoadTrueFalseQuestion(DatGridDisplayQuestion, Displaycourse);
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("sorry you cant delete the question because it exists in Exam");
                    }
                }
                else
                {
                    MessageBox.Show("please select the question you want delete");
                }
            }
            catch
            {
                MessageBox.Show("something wrong");
            }
        }



        //Show Question

        private void QuestionTypeShow_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (ShowQuestionType.SelectedIndex == 0)
                {
                    LoadTextQuestion(DatGridShowQuestion, cmbDisplayCourseShow);



                }
                else if (ShowQuestionType.SelectedIndex == 1)
                {
                    LoadMcqQuestion(DatGridShowQuestion, cmbDisplayCourseShow);



                }


                else if (ShowQuestionType.SelectedIndex == 2)
                {
                    LoadTrueFalseQuestion(DatGridShowQuestion, cmbDisplayCourseShow);



                }
            }
            catch
            {
                MessageBox.Show("something wrong");
            }

        }

        //Close The form
        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }


    }
}
