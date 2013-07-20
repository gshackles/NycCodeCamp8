using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using Cirrious.MvvmCross.Plugins.Messenger;
using Cirrious.MvvmCross.ViewModels;
using CodeCamp.Core.Data.Entities;
using CodeCamp.Core.Services;

namespace CodeCamp.Core.ViewModels
{
    public class SpeakerViewModel : ViewModelBase
    {
        private readonly ICodeCampService _campService;

        public SpeakerViewModel(IMvxMessenger messenger, ICodeCampService campService) 
            : base(messenger)
        {
            _campService = campService;
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

        public async Task Init(NavigationParameters parameters)
        {
            bool successful = await SafeOperation(
                Task.Run(async () =>
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