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
    /// Interaction logic for TrackBranchIntakeWindow.xaml
    /// </summary>
    public partial class TrackBranchIntakeWindow : Window
    {
        Context context = new Context();
        public TrackBranchIntakeWindow()
        {
            InitializeComponent();

            //branch to intake to track

            add_track_grid.Visibility = Visibility.Collapsed;
            add_intake_grid.Visibility = Visibility.Collapsed;
            add_branch_grid.Visibility = Visibility.Collapsed;
            BIT_grid.Visibility = Visibility.Collapsed;

            List<Branch> branches = context.Branches.ToList();
            BranchNameBIT.ItemsSource = branches;
            BranchNameBIT.SelectedItem = branches.FirstOrDefault();
            BranchNameBIT.DisplayMemberPath = "Name";
            BranchNameBIT.SelectedValuePath = "ID";


            List<Track> tracks = context.Tracks.ToList();
            TrackNameBIT.ItemsSource = tracks;
            TrackNameBIT.SelectedItem = branches.FirstOrDefault();
            TrackNameBIT.DisplayMemberPath = "Name";
            TrackNameBIT.SelectedValuePath = "ID";

            List<Intake> intakes = context.Intakes.ToList();
            IntakeNumberBIT.ItemsSource = intakes;
            IntakeNumberBIT.SelectedItem = intakes.FirstOrDefault();
            IntakeNumberBIT.DisplayMemberPath = "Number";
            IntakeNumberBIT.SelectedValuePath = "ID";
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (List1.SelectedItems != null)
            {
                if (List1.SelectedIndex == 0)
                {
                    add_track_grid.Visibility = Visibility.Visible;

                }
                else
                    add_track_grid.Visibility = Visibility.Collapsed;

                if (List1.SelectedIndex == 1)
                {
                    add_intake_grid.Visibility = Visibility.Visible;

                }
                else
                    add_intake_grid.Visibility = Visibility.Collapsed;

                if (List1.SelectedIndex == 2)
                {
                    add_branch_grid.Visibility = Visibility.Visible;

                }
                else
                    add_branch_grid.Visibility = Visibility.Collapsed;

                if (List1.SelectedIndex == 3)
                {
                    BIT_grid.Visibility = Visibility.Visible;

                }
                else
                    BIT_grid.Visibility = Visibility.Collapsed;


            }

        }


        //track 
        private void Add_Track(object sender, RoutedEventArgs e)
        {
            if (TrackName.Text != "" && TrackDept.Text != "")
            {
                Track track = new Track()
                {
                    Name = TrackName.Text,
                    Department = TrackDept.Text,
                };
                context.Tracks.Add(track);
                context.SaveChanges();
                MessageBox.Show("Added Sucessfully");
            }
            else
            {
                MessageBox.Show("Please enter empty fields");
            }

        }

        //Branch
        private void Add_Branch(object sender, RoutedEventArgs e)
        {
            var queryLocation =
                from L in context.Branches
                where L.Location == BranchLocation.Text
                select L.Location;

            var queryName =
                from N in context.Branches
                where N.Name == BranchName.Text
                select N.Name;

            if (BranchName.Text != "" && BranchLocation.Text != "")
            {
                if (queryLocation.FirstOrDefault() ==
                      BranchLocation.Text && queryName.FirstOrDefault() == BranchName.Text)
                {
                    MessageBox.Show("Branch is already added");
                }
                else
                {
                    Branch branch = new Branch()
                    {
                        Name = BranchName.Text,
                        Location = BranchLocation.Text,
                    };

                    context.Branches.Add(branch);
                    context.SaveChanges();
                    MessageBox.Show("Added Sucessfully");
                }
            }
            else
            {
                MessageBox.Show("Please fill empty fields");
            }
        }


        //Intake
        private void Add_Intake(object sender, RoutedEventArgs e)
        {
            try
            {
                byte IntakeNum = byte.Parse(IntakeNumber.Text);

                var query =
                    from i in context.Intakes
                    where i.Number == IntakeNum
                    select i.Number;
                try
                {
                    if (IntakeNumber.Text != "" && StartDate.Text != "")
                    {
                        if (query.FirstOrDefault() != IntakeNum)
                        {
                            Intake intake = new Intake()
                            {
                                Number = byte.Parse(IntakeNumber.Text),

                                StartDate = DateTime.Parse(StartDate.Text),
                            };
                            context.Intakes.Add(intake);
                            context.SaveChanges();
                            MessageBox.Show("Added Sucessfully");
                        }
                        else
                        {
                            MessageBox.Show("This intake is already added");
                        }
                    }
                    else
                    {
                        MessageBox.Show("please enter empty fields");
                    }

                }

                catch (Exception r)
                {
                    MessageBox.Show("Invalid inputs");
                }


            }
            catch
            {
                MessageBox.Show("Invalid inputs");
            }
        }
        private void Branch_Intake_Track_Add(object sender, RoutedEventArgs e)
        {
            try
            {
                List<BranchIntackTrack> branchIntackTracks =
                   context.BranchIntackTracks.ToList();//.Where(s => s.BranchID == (byte)BranchName.SelectedValue
                                                       //&& s.TrackID == (byte)TrackName.SelectedValue && s.IntakeID == (byte)IntakeNumber.SelectedValue).FirstOrDefault();
                foreach (var item in branchIntackTracks)
                {
                    if (item.BranchID == (byte)BranchNameBIT.SelectedValue
                    && item.TrackID == (byte)TrackNameBIT.SelectedValue
                    && item.IntakeID == (byte)IntakeNumberBIT.SelectedValue)
                    {
                        MessageBox.Show("this track and intake are already assigned to this branch");

                    }
                }


                BranchIntackTrack branchIntackTrack =
                        new BranchIntackTrack()
                        {
                            BranchID = (byte)BranchNameBIT.SelectedValue,
                            TrackID = (byte)TrackNameBIT.SelectedValue,
                            IntakeID = (byte)IntakeNumberBIT.SelectedValue,
                        };
                context.BranchIntackTracks.Add(branchIntackTrack);
                context.SaveChanges();
                MessageBox.Show("this intake and track are sucessfully assigned to this branch");
            }catch
            {

            }
        }

        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }
    }
}
