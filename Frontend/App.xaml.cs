namespace Frontend
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            DependencyService.Register<HttpClient>();
            var name = File.ReadAllText("C:\\Users\\ionut\\Desktop\\AIssueReporter\\Frontend\\logged.txt");
            if (name.Length == 0)
                MainPage = new NavigationPage(new Views.WelcomePage());
            else
                MainPage = new NavigationPage(new Views.HomePage(name));
        }
    }
}
