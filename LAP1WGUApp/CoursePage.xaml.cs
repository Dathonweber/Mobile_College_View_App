using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LAP1WGUApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CoursePage : ContentPage
    {
        public static Course course;
        Term term = MainPage.term;
        public CoursePage()
        {
            Title = term.TermName + "  " + term.StartDate.ToString("MMM yyyy") + " To " + term.EndDate.ToString("MMM yyyy");
            InitializeComponent();
            StartDate.Date = term.StartDate;
            EndDate.Date = term.EndDate;            
            Appearing += OnLoad;
        }

        private void DeleteTermBtn_Clicked(object sender, EventArgs e)
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                var result = await DisplayAlert("Confirmation", "Are you sure you want to delete this term?", "Yes", "No");
                if (result)
                {
                    WGU.DeleteTerm(term);
                    MainPage.terms1.Remove(term);
                    await Navigation.PopAsync();
                }
            });
        }

        async void EditTermBtn_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new EditTermPage());
        }

        private void CoursesList_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            course = (Course)CoursesList.SelectedItem;
            GoToCoursesViewPage();
            CoursesList.SelectedItem = null;
        }

        async void AddCourseBtn_Clicked(object sender, EventArgs e)
        {
            if (term.Courses.Count < 6)
            {
                await Navigation.PushAsync(new AddCoursePage());


            }
            else
            {
                await DisplayAlert("Error", "The maximum number of courses per term is 6.", "OK");
            }
        }

        async void GoToCoursesViewPage()
        {
            await Navigation.PushAsync(new CourseViewPage());
        }

        private void OnLoad(object sender, EventArgs e)
        {
            FillCourses();
            FillAssessments();
            FillInstructors();
        }

        private void FillCourses()
        {
            term.Courses.Clear();
            CoursesList.ItemsSource = null;
            MainPage.courses.Clear();
            List<Course> courses1 = new List<Course>();
            courses1 = WGU.conn.Table<Course>().ToList();
            foreach (Course course in courses1)
            {
                MainPage.courses.Add(course);
            }

            for (int i = 0; i < MainPage.terms1.Count; i++)
            {
                foreach (Course course in MainPage.courses)
                {
                    if (course.TermID == MainPage.terms1.ElementAt<Term>(i).TermID)
                    {
                        MainPage.terms1.ElementAt<Term>(i).Courses.Add(course);
                    }
                }
            }

            CoursesList.ItemsSource = term.Courses;
        }

        private void FillAssessments()
        {
            MainPage.Assessments.Clear();
            List<Assessment> assessments = WGU.conn.Table<Assessment>().ToList();
            foreach (Assessment assessment in assessments)
            {
                MainPage.Assessments.Add(assessment);
            }

            foreach (Course course in MainPage.courses)
            {
                foreach (Assessment assessment in MainPage.Assessments)
                {
                    if (assessment.CourseID == course.CourseID)
                    {
                        course.Assessments.Add(assessment);
                    }
                }
            }
        }

        private void FillInstructors()
        {

            List<CourseInstructor> instructors = WGU.conn.Table<CourseInstructor>().ToList();
            foreach (Course course in MainPage.courses)
            {
                foreach (CourseInstructor ci in instructors)
                {
                    if (ci.CourseID == course.CourseID)
                    {
                        course.Instructor = ci;
                    }
                }
            }
        }
    }
}