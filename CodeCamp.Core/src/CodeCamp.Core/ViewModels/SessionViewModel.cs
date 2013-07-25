using System.Threading.Tasks;
using System.Windows.Input;
using Cirrious.MvvmCross.Plugins.Messenger;
using Cirrious.MvvmCross.ViewModels;
using CodeCamp.Core.Data.Entities;
using CodeCamp.Core.Services;

namespace CodeCamp.Core.ViewModels
{
    public class SessionViewModel : ViewModelBase
    {
        private readonly ICodeCampService _campService;

        public SessionViewModel(IMvxMessenger messenger, ICodeCampService campService) 
            : base(messenger)
        {
            _campService = campService;
        }

        private Session _session;
        public Session Session
        {
            get { return _session; }
            set { _session = value; RaisePropertyChanged(() => Session); }
        }

        public async Task Init(NavigationParameters parameters)
        {
            bool successful = await SafeOperation(
                Task.Run(async () => Session = await _campService.GetSession(parameters.Id)));

            FinishedLoading(successful);
        }

        public ICommand ViewSpeakerCommand
        {
            get
            {
                return new MvxCommand(() => ShowViewModel<SpeakerViewModel>(new SpeakerViewModel.NavigationParameters(Session.SpeakerId)));
            }
        }

        public class NavigationParameters
        {
            public int Id { get; set; }

            public NavigationParameters()
            {
            }

            public NavigationParameters(int id)
            {
                Id = id;
            }
        }
    }
}