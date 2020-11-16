using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LAP1WGUApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditCoursePage : ContentPage
    {
        public EditCoursePage()
        {
            InitializeComponent();
            Title = "Edit Course Information";
            FillCourseInfo();
        }       
        private void GoToCoursesPage()
        {
            Navigation.PopToRootAsync();
            var pages = Navigation.NavigationStack.ToList();
            foreach (var page in pages)
            {
                Navigation.RemovePage(page);
            }
            
        }

        private void SaveCourseBtn_Clicked(object sender, EventArgs e)
        {
            Course course = new Course(Convert.ToInt32(CourseIDCell.Text), Convert.ToInt32(TermIDCell.Text), CourseNameCell.Text, CourseStartDate.Date, 
                CourseEndDate.Date, CourseStatusPicker.SelectedItem.ToString(), 
                CourseViewCell.Text, CourseNotesCell.Text, CoursePage.course.Instructor, CoursePage.course.Assessments);
            MainPage.term.Courses.Remove(CoursePage.course);
            MainPage.term.Courses.Add(course);
            WGU.EditCourse(course);            
            GoToCoursesPage();
        }

        private void FillCourseInfo()
        {
            CourseIDCell.Text = CoursePage.course.CourseID.ToString();
            TermIDCell.Text = CoursePage.course.TermID.ToString();
            CourseNameCell.Text = CoursePage.course.CourseName;
            CourseViewCell.Text = CoursePage.course.DetailedCourseView;
            CourseNotesCell.Text = CoursePage.course.CourseNotes;
            CourseStartDate.Date = CoursePage.course.StartDate;
            CourseEndDate.Date = CoursePage.course.EndDate;
            string[] CourseStatuses = { "Plan to Take", "In Progress", "Completed", "Dropped" };
            CourseStatusPicker.ItemsSource = CourseStatuses;
            CourseStatusPicker.SelectedItem = CoursePage.course.CourseStatus;
        }

        private void EditCIBtn_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new EditCIInfoPage());
        }
    }
}