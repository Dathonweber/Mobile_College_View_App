using Plugin.LocalNotifications;
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
    public partial class AlertPage : ContentPage
    {
        public AlertPage()
        {
            Title = CoursePage.course.CourseName;
            InitializeComponent();
            CourseAlertView.BindingContext = CoursePage.course;
        }
        private void SetAlertBtn_Clicked(object sender, EventArgs e)
        {
            DateTime AlertTime = new DateTime();
            AlertTime = DatePicker.Date + TimePicker.Time;
            TimeSpan date = CoursePage.course.EndDate.Subtract(DatePicker.Date);
            WGU.SetCourseNotifications(date, AlertTime);
            Navigation.PopAsync();
            
        }
    }
}