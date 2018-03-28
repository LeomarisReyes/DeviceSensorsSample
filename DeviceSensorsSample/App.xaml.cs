using DeviceSensorsSample.Views;
using Xamarin.Forms;

namespace DeviceSensorsSample
{
    public partial class App : Application
    {
        public static INavigation Navigation { get; set; }
        public App()
        {
            InitializeComponent();
            var NavPage = new NavigationPage(new DeviceSensorsSamplePage());
            Navigation = NavPage.Navigation;
            MainPage = NavPage;
        } 
       
        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
