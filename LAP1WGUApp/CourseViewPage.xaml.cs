using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace LAP1WGUApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CourseViewPage : ContentPage
    {
        public static Course course = CoursePage.course;

        public CourseInstructor instructor = new CourseInstructor();        
        public CourseViewPage()
        {
            instructor = course.Instructor;
            InitializeComponent();
            Title = course.CourseName;
            AssessmentsList.ItemsSource = course.Assessments;
            CourseInfoView.BindingContext = course;
            courseInstructor.BindingContext = instructor;
            courseStatusView.BindingContext = course;
            Appearing += PageLoad;
        }

        private void DeleteCourseBtn_Clicked(object sender, System.EventArgs e)
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                var result = await DisplayAlert("Confirmation", "Are you sure you want to delete this course?", "Yes", "No");
                if (result)
                {
                    WGU.DeleteCourse(course);
                    course.Assessments.Clear();
                    MainPage.term.Courses.Remove(course);
                    await Navigation.PopAsync();
                }
            });
        }

        private void EditCourse_Clicked(object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new EditCoursePage());
        }

        private void AddAssessmentBtn_Clicked(object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new AddTestPage());
        }

        private void AssessmentsList_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            TestsViewPage.test = (Assessment)AssessmentsList.SelectedItem;
            Navigation.PushAsync(new TestsViewPage());
        }

        async void NotesShareBtn_Clicked(object sender, System.EventArgs e)
        {
            var result = await DisplayAlert("Confirmation", "Do you want to share these Notes?", "Yes", "No");
            if (result)
            {
                var options = await DisplayAlert("Options", "How do you want to share your notes?", "Email", "SMS");
                if (options)
                {
                    List<string> emails = new List<string>();
                    emails.Add(course.Instructor.Email);
                    await SendEmail(course.CourseName + "Optional Notes", course.CourseNotes,emails);
                }
                else
                {
                   await SendSms(course.CourseNotes, course.Instructor.PhoneNumber);
                }
            }
        }

        private void CourseAlertBtn_Clicked(object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new AlertPage());
        }

        public async Task SendEmail(string subject, string body, List<string> recipients)
        {
            try
            {
                var message = new EmailMessage
                {
                    Subject = subject,
                    Body = body,
                    To = recipients,
                    //Cc = ccRecipients,
                    //Bcc = bccRecipients
                };
                await Email.ComposeAsync(message);
            }
            catch (FeatureNotSupportedException fbsEx)
            {
                // Email is not supported on this device
                 await DisplayAlert("Error", fbsEx.Message, "OK");

            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "OK");
            }
        }

        public async Task SendSms(string messageText, string recipient)
        {
            try
            {
                var message = new SmsMessage(messageText, new[] { recipient });
                await Sms.ComposeAsync(message);
            }
            catch (FeatureNotSupportedException ex)
            {
               await DisplayAlert("Error", ex.Message, "OK");
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "OK");
            }
        }

        private void PageLoad(object sender, EventArgs e)
        {
            course = CoursePage.course;
            Title = course.CourseName;
            AssessmentsList.ItemsSource = course.Assessments;
            CourseInfoView.BindingContext = course;
            courseInstructor.BindingContext = instructor;
            courseStatusView.BindingContext = course;
        }
    }
}