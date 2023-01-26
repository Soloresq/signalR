namespace SignalR.Maui.Client
{
    public partial class App : Application
    {
        public App(MainPageViewModel mainPageViewModel)
        {
            InitializeComponent();
            // Workaround see https://github.com/dotnet/maui/issues/11485
            MainPage = new MainPage(mainPageViewModel);
        }

        //protected override void CleanUp()
        //{
        //    base.CleanUp();
        //    ((ViewModelBase)MainPage.BindingContext).Dispose();
        //}
    }
}