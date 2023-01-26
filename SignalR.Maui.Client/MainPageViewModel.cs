using SignalR.Client;
using System.Text;
using System.Windows.Input;

namespace SignalR.Maui.Client
{
    public class MainPageViewModel : ViewModelBase
    {
        private readonly ISignalRClient signalRClient;
        private readonly ICommandFactory commandFactory;
        private string userInput;
        private string chatHistory;
        private string username;
        private StringBuilder stringBuilder;

        public MainPageViewModel(ISignalRClient signalRClient, ICommandFactory commandFactory)
        {
            SendCommand = CreateCommand(Send);
            stringBuilder = new StringBuilder(ChatHistory);
            Username = "User";
            this.signalRClient = signalRClient;
            this.commandFactory = commandFactory;
        }

        public ICommand SendCommand { get; set; }

        public string Username
        {
            get { return username; }
            set
            {
                ChangeProperty(ref username, value);
            }
        }

        public string UserInput
        {
            get { return userInput; }
            set
            {
                ChangeProperty(ref userInput, value);
            }
        }

        public string ChatHistory
        {
            get { return chatHistory; }
            set
            {
                ChangeProperty(ref chatHistory, value);
            }
        }

        public void Send()
        {
            stringBuilder.AppendFormat("{0} {1} {2}", Username, DateTime.Now.DayOfWeek, DateTime.Now.ToShortTimeString());
            stringBuilder.AppendLine();
            stringBuilder.AppendLine(UserInput);
            stringBuilder.AppendLine();
            ChatHistory = stringBuilder.ToString();
            UserInput = string.Empty;

            //this.signalRClient.Execute()
        }
    }
}
