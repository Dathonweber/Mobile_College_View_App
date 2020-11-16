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
    public partial class EditCIInfoPage : ContentPage
    {
        public CourseInstructor ci = CoursePage.course.Instructor;
        public EditCIInfoPage()
        {           
            InitializeComponent();
            FillInfo();
        }

        private void FillInfo()
        {
            Name.Text = ci.Name;
            PhoneNumber.Text = ci.PhoneNumber;
            Email.Text = ci.Email;
        }
        private void EditCIInfoBtn_Clicked(object sender, EventArgs e)
        {
            if (isValidEmail(Email.Text))
            {
                if (string.IsNullOrEmpty(Name.Text) || string.IsNullOrEmpty(PhoneNumber.Text) || string.IsNullOrEmpty(Email.Text))
                {
                    DisplayAlert("Error", "Please Input Correct Data", "OK");
                }
                else
                {
                    ci.Name = Name.Text;
                    ci.PhoneNumber = PhoneNumber.Text;
                    ci.Email = Email.Text;
                    WGU.EditCourseInstructor(ci);
                    Navigation.PopToRootAsync();
                    var pages = Navigation.NavigationStack.ToList();
                    foreach (var page in pages)
                    {
                        Navigation.RemovePage(page);
                    }
                }
            }
            else
            {
                DisplayAlert("Error","Invalid Email Address","OK");
            }
        }

        private bool isValidEmail(string email)
        {
            try
            {
                var E = new System.Net.Mail.MailAddress(email);
                return E.Address == email;
            }
            catch
            {
                return false;
            }
        }
    }
}