namespace SignalR.Maui.Client
{
    public partial class App : Application
    {
        public App(MainPage mainPage)
        {
            InitializeComponent();
            MainPage = mainPage;
        }

        //protected override void CleanUp()
        //{
        //    base.CleanUp();
        //    ((ViewModelBase)MainPage.BindingContext).Dispose();
        //}
    }
}