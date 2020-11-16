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
    public partial class AddTestPage : ContentPage
    {
        public AddTestPage()
        {
            Title = "Test Information";
            InitializeComponent();
            string[] vs = { "Performance Assessment", "Objective Assessment" };
            TypePicker.ItemsSource = vs;
            TypePicker.SelectedItem = vs[0];

        }

        private void SaveTestInfoBtn_Clicked(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(AssessmentIDCell.Text) || string.IsNullOrWhiteSpace(AssessmentNameCell.Text) || string.IsNullOrWhiteSpace(AssessmentInfoCell.Text))
                {
                    DisplayAlert("Error", "Please input correct data", "OK");
                }
                else
                {
                    if (StartDate.Date > CoursePage.course.StartDate && StartDate.Date < CoursePage.course.EndDate)
                    {
                        int id = Convert.ToInt32(AssessmentIDCell.Text);
                        string name = AssessmentNameCell.Text;
                        string info = AssessmentInfoCell.Text;
                        DateTime start = StartDate.Date;
                        DateTime time = start + StartTime.Time;
                        string type = TypePicker.SelectedItem.ToString();
                        Assessment ass = new Assessment(id, CoursePage.course.CourseID, name, start, time, info, type);
                        CoursePage.course.Assessments.Add(ass);
                        WGU.AddAssessment(new Assessment(id, CoursePage.course.CourseID, name, start, time, info, type));
                        Navigation.PopAsync();
                    }
                    else
                    {
                        DisplayAlert("Error", "Test Date is Invalid. Test Date must be after the course starts and before the course ends, thank you.", "Ok");
                    }
                }
            }
            catch (ArgumentNullException ex)
            {
                DisplayAlert("Error", ex.Message, "OK");
            }
            catch (FormatException f)
            {
                DisplayAlert("Error", f.Message, "OK");
            }

        }

    }
}