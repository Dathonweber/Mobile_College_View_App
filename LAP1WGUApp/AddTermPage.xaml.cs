using System;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LAP1WGUApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddTermPage : ContentPage
    {

        public AddTermPage()
        {
            InitializeComponent();
            Title = "Add a Term";
        }
        private void AddBtn_Clicked(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(TermTitle.Text))
                {
                    DisplayAlert("Error","Please input a Term Name.","OK");
                }
                else
                {
                    if (TermStartDatePicker.Date < TermEndDatePicker.Date)
                    {
                        int termID = 0;
                        string termName = TermTitle.Text;
                        DateTime startDate = TermStartDatePicker.Date;
                        DateTime endDate = TermEndDatePicker.Date;
                        ObservableCollection<Course> courses = new ObservableCollection<Course>();
                        MainPage.terms1.Add(new Term(MainPage.terms1.Count + 1, termName, startDate, endDate, courses));
                        WGU.AddTerm(new Term(termID, termName, startDate, endDate, courses));
                        Navigation.PopAsync();
                    }
                    else
                    {
                        DisplayAlert("Error", "Invalid Date Selection, please select different dates", "OK");
                    }
                }
            }
            catch(FormatException f)
            {
                DisplayAlert("Error", f.Message, "OK");
            }
            catch(ArgumentNullException n)
            {
                DisplayAlert("Error",n.Message,"OK");
            }
        }
    }
}