using Plugin.LocalNotifications;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Xamarin.Forms;

namespace LAP1WGUApp
{

    public partial class MainPage : ContentPage
    {
        public static Term term;
        public static ObservableCollection<Term> terms1 = new ObservableCollection<Term>();
        public static ObservableCollection<Course> courses = new ObservableCollection<Course>();
        public static ObservableCollection<Assessment> Assessments = new ObservableCollection<Assessment>();

        public MainPage()
        {
            WGU.conn.CreateTable<Term>();
            WGU.conn.CreateTable<Course>();
            WGU.conn.CreateTable<CourseInstructor>();
            WGU.conn.CreateTable<Assessment>();
            WGU.conn.CreateTable<Notification>();
            Title = "Main Page";
            FillNotifications();
            InitializeComponent();
            FillTerms();
            AddTermBtn.Clicked += async (sender, args) =>
            {
                await Navigation.PushAsync(new AddTermPage());
            };
            PopulateDB();            

        }
        private void TermsView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            term = (Term)TermsView.SelectedItem;
            GoToCoursesPage();
            TermsView.SelectedItem = null;
        }
        async void GoToCoursesPage()
        {
            await Navigation.PushAsync(new CoursePage());
        }

       

        private void FillTerms()
        {
            List<Term> terms = WGU.conn.Table<Term>().ToList();

            foreach (Term term in terms)
            {
                terms1.Add(term);
            }

            TermsView.ItemsSource = terms1;
        }

        private void FillCourses()
        {
            List<Course> courses1 = new List<Course>();
            courses1 = WGU.conn.Table<Course>().ToList();
            foreach (Course course in courses1)
            {
                courses.Add(course);
            }

            for (int i = 0; i < terms1.Count; i++)
            {
                foreach (Course course in courses)
                {
                    if (course.TermID == terms1.ElementAt<Term>(i).TermID)
                    {
                        terms1.ElementAt<Term>(i).Courses.Add(course);
                    }
                }
            }

        }

        private void PopulateDB()
        {
            if (terms1.Count <= 0)
            {
                Term newTerm = new Term(1, "Term 1", new DateTime(2021, 01, 01), new DateTime(2021, 07, 01), new ObservableCollection<Course>());
                Course newCourse = new Course(123, newTerm.TermID, "Intro To Programming", new DateTime(2021, 01, 01), new DateTime(2021, 03, 01), "In Progress","This course is just as it says in the title an introduction class to programming and its many uses in modern society.",
                    "", new CourseInstructor(), new ObservableCollection<Assessment>());
                Assessment assessment2 = new Assessment(121, newCourse.CourseID, "Simple Coding Project", new DateTime(2021, 02, 28), new DateTime(2021, 01, 31, 23, 55, 55), 
                    "This assessment is a simple coding project to test the students skill in efficient and correct coding techniques.", "Performance Assessment");
                Assessment assessment1 = new Assessment(120, newCourse.CourseID, "120 Coding Test", new DateTime(2021,01,31), new DateTime(2021, 01, 31, 15, 00, 00), 
                    "This Coding Test is designed to determine the current knowledge base of the student prior to their Coding Project Assignment.", "Objective Assessment");
                newTerm.Courses.Add(newCourse);
                newCourse.Instructor = WGU.Instructor;
                newCourse.Assessments.Add(assessment1);
                newCourse.Assessments.Add(assessment2);
                WGU.conn.InsertOrReplace(newTerm);
                WGU.conn.InsertOrReplace(newCourse);
                WGU.conn.InsertOrReplace(assessment1);
                WGU.conn.InsertOrReplace(assessment2);
                WGU.conn.InsertOrReplace(WGU.Instructor);
                FillTerms();
                FillCourses();                
            }
        }

        private void FillAssessments()
        {
            List<Assessment> assessments = WGU.conn.Table<Assessment>().ToList();
            foreach (Assessment assessment  in assessments)
            {
                Assessments.Add(assessment);
            }

            foreach (Course course in courses)
            {
                foreach (Assessment assessment in Assessments)
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

            foreach (Course course in courses)
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

        private void FillNotifications()
        {
            List<Notification> ns = WGU.conn.Table<Notification>().ToList();

            foreach (Notification nt in ns)
            {
                if (DateTime.Now > nt.Dt)
                {
                    WGU.conn.Delete(nt);
                }
                else
                {
                    CrossLocalNotifications.Current.Show(nt.Title, nt.Body, nt.ID, nt.Dt);
                }
            }
        }
    }
}
