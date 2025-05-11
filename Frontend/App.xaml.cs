namespace Frontend
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            DependencyService.Register<HttpClient>();

            MainPage = new NavigationPage(new Views.WelcomePage());
        }
    }
}
