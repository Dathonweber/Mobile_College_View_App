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
    public partial class AlertTestPage : ContentPage
    {
        public AlertTestPage()
        {
            Title = TestsViewPage.test.AssessmentName;
            InitializeComponent();
            TestAlertView.BindingContext = TestsViewPage.test;
        }

        private void SetAlertBtn_Clicked(object sender, EventArgs e)
        {
            DateTime dt = new DateTime();
            dt = DatePicker.Date + TimePicker.Time;
            DateTime dt2 = TestsViewPage.test.AssessmentDate.Date + TestsViewPage.test.AssessmentTime.TimeOfDay;
            TimeSpan ts = dt2.Subtract(DatePicker.Date);
            WGU.SetAssessmentNotifications(ts, dt);
            Navigation.PopAsync();
        }
    }
}