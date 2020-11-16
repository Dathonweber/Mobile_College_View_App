using System;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LAP1WGUApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddCoursePage : ContentPage
    {
        public AddCoursePage()
        {
            Title = "Add A Course";
            InitializeComponent();
            TermIDCell.Text = MainPage.term.TermID.ToString();
            string[] CourseStatuses = { "Plan to Take", "In Progress", "Completed", "Dropped" };
            CourseStatusPicker.ItemsSource = CourseStatuses;
            CourseStatusPicker.SelectedItem = CourseStatuses[0];
        }

        private void AddCourseBtn_Clicked(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(CourseIDCell.Text) || string.IsNullOrWhiteSpace(CourseNameCell.Text) || string.IsNullOrWhiteSpace(CourseViewCell.Text))
                {
                    DisplayAlert("Error", "Please input correct data", "OK");
                }
                else
                {
                    if (CourseStartDate.Date > MainPage.term.StartDate && CourseStartDate.Date < MainPage.term.EndDate && CourseEndDate.Date > MainPage.term.StartDate &&
                        CourseEndDate.Date < MainPage.term.EndDate && CourseStartDate.Date < CourseEndDate.Date)
                    {
                        int courseID = Convert.ToInt32(CourseIDCell.Text);
                        int termID = Convert.ToInt32(TermIDCell.Text);
                        string courseName = CourseNameCell.Text;
                        DateTime startDate = CourseStartDate.Date;
                        DateTime endDate = CourseEndDate.Date;
                        string courseStatus = CourseStatusPicker.SelectedItem.ToString();
                        string courseView = CourseViewCell.Text;
                        string courseNotes = CourseNotesCell.Text;
                        WGU.AddCourse(new Course(courseID, termID, courseName, startDate, endDate, courseStatus, courseView, courseNotes, WGU.Instructor, new ObservableCollection<Assessment>()));
                        Navigation.PopAsync();
                    }
                    else
                    {
                        DisplayAlert("Error", "Invalid Dates, Please Make sure your dates are correct.", "OK");
                    }
                }
            }
            catch(FormatException ex)
            {
                DisplayAlert("Error", ex.Message, "Okay");
            } 
            catch(ArgumentNullException n)
            {
                DisplayAlert("Error", n.Message, "OK");
            }
        }        
    }
}