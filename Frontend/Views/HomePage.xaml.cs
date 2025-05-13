using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Frontend.Views
{
    public partial class HomePage: ContentPage
    {
        public HomePage(string name)
        {
            InitializeComponent();
            WelcomeLabel.Text = "Welcome, " + name;
        }

        private async void OnSubmitIssueClicked(object sender, EventArgs e)
        { }

        private async void OnViewIssuesClicked(object sender, EventArgs e)
        { }

        private async void OnLogoutClicked(object sender, EventArgs e)
        {
            File.WriteAllText("C:\\Users\\ionut\\Desktop\\AIssueReporter\\Frontend\\logged.txt", "");
            await Navigation.PushAsync(new WelcomePage());
        }
    }
}
