using System;
using System.Collections.ObjectModel;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LAP1WGUApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditTermPage : ContentPage
    {
        public EditTermPage()
        {
            InitializeComponent();
            Title = "Edit Term Information";
            FillTermInfo();
        }

        private void SaveBtn_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(TermTitle.Text))
            {
                DisplayAlert("Error", "Please Fill out All of the Information", "OK");
            }
            else
            {
                if (StartDatePicker.Date < EndDatePicker.Date)
                {
                    int termID = MainPage.term.TermID;
                    string TermName = TermTitle.Text;
                    DateTime startDate = StartDatePicker.Date;
                    DateTime endDate = EndDatePicker.Date;
                    MainPage.terms1.Add(new Term(termID, TermName, startDate, endDate, MainPage.term.Courses));
                    MainPage.terms1.Remove(MainPage.term);
                    WGU.EditTerm(new Term(termID, TermName, startDate, endDate, MainPage.term.Courses));
                    GoToMainPage();
                }
                else
                {
                    DisplayAlert("Error", "Please check your dates again.", "OK");
                }
            }
        }

        async void GoToMainPage()
        {
            await Navigation.PopToRootAsync();
        }

        public void FillTermInfo()
        {
            TermTitle.Text = MainPage.term.TermName;
            StartDatePicker.Date = MainPage.term.StartDate;
            EndDatePicker.Date = MainPage.term.EndDate;
        }
    }
}