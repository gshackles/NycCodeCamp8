using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using Cirrious.MvvmCross.Plugins.Email;
using Cirrious.MvvmCross.Plugins.Messenger;
using Cirrious.MvvmCross.ViewModels;
using CodeCamp.Core.Data.Entities;
using CodeCamp.Core.Services;
#if !WINDOWS_PHONE
using TaskEx = System.Threading.Tasks.Task;
#endif

namespace CodeCamp.Core.ViewModels
{
    public class SpeakerViewModel : ViewModelBase
    {
        private readonly ICodeCampService _campService;
        private readonly IMvxComposeEmailTask _composeEmailTask;

        public SpeakerViewModel(IMvxMessenger messenger, ICodeCampService campService, IMvxComposeEmailTask composeEmailTask) 
            : base(messenger)
        {
            _campService = campService;
            _composeEmailTask = composeEmailTask;
        }

        private Speaker _speaker;
        public Speaker Speaker
        {
            get { return _speaker; }
            set { _speaker = value; RaisePropertyChanged(() => Speaker); }
        } 

        private IList<Session> _sessions;
        public IList<Session> Sessions
        {
            get { return _sessions; }
            set { _sessions = value; RaisePropertyChanged(() => Sessions); }
        } 

        public ICommand ViewSessionCommand
        {
            get
            {
                return new MvxCommand<Session>(
                    session => ShowViewModel<SessionViewModel>(new SessionViewModel.NavigationParameters(session.Id)));
            }
        }

        public ICommand EmailSpeakerCommand
        {
            get
            {
                return new MvxCommand(() =>
                    _composeEmailTask.ComposeEmail(Speaker.EmailAddress, null, "NYC Code Camp 8", null, false));
            }
        }

        public async Task Init(NavigationParameters parameters)
        {
            bool successful = await SafeOperation(
                TaskEx.Run(async () =>
                                   {
                                       Speaker = await _campService.GetSpeaker(parameters.Id);
                                       Sessions = await _campService.ListSessionsBySpeaker(parameters.Id);
                                   }));

            FinishedLoading(successful);
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