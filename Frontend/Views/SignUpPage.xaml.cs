using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Frontend.Views
{
    public partial class SignUpPage : ContentPage
    {
        private readonly HttpClient _httpClient;
        public SignUpPage()
        {
            InitializeComponent();
            _httpClient = new HttpClient();
        }

        private async void OnSignUpClicked(object sender, EventArgs e)
        {
            
            var signupRequest = new
            {
                FirstName = FirstName.Text,
                LastName = LastName.Text,
                Email = Email.Text,
                Phone = PhoneNumber.Text,
                Password = Password.Text
            };

            var jsonContent = JsonConvert.SerializeObject(signupRequest);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("https://localhost:7276/Users/signup", content);

            if (response.IsSuccessStatusCode)
            {
                var resp = await response.Content.ReadAsStringAsync();
                string name = "";
                for (int i = 12; i < resp.Length - 2; i++)
                    name += resp[i].ToString();
                File.WriteAllText("C:\\Users\\ionut\\Desktop\\AIssueReporter\\Frontend\\logged.txt", name);
                await Navigation.PushAsync(new HomePage(name));

            }
            else
            {
                await DisplayAlert("Sign Up Failed", "This email address is already used!", "OK");
                Email.Text = "";
                return;
            }
        }

        private async void OnLoginClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new LoginPage());
        }
    }
}
