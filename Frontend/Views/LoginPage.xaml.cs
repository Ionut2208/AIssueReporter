using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Newtonsoft.Json;
namespace Frontend.Views
{
    public partial class LoginPage : ContentPage
    {
        private readonly HttpClient _httpClient;
        public LoginPage()
        {
            InitializeComponent();
            _httpClient = new HttpClient();
        }

        private async void OnLoginClicked(object sender, EventArgs e)
        {
            string email = Email.Text;
            string password = Password.Text;

            var loginRequest = new
            {
                Email = email,
                Password = password
            };
            var jsonContent = JsonConvert.SerializeObject(loginRequest);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("https://localhost:7276/Users/login", content);
            if(response.IsSuccessStatusCode)
            {
                var resp = await response.Content.ReadAsStringAsync();
                string name = "";
                for(int i = 12; i < resp.Length - 2; i++)
                    name += resp[i].ToString();
                File.WriteAllText("C:\\Users\\ionut\\Desktop\\AIssueReporter\\Frontend\\logged.txt", name);
                await Navigation.PushAsync(new HomePage(name));
            }
            else
            {
                //var error = await response.Content.ReadAsStringAsync();
                await DisplayAlert("Login Failed", "Invalid email or password", "OK");
                Password.Text = "";
                return;
            }
        }

        private async void OnSignUpClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SignUpPage());
        }
    }

}
