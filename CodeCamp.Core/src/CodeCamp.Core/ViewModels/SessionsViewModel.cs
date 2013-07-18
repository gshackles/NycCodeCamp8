using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using Cirrious.MvvmCross.Plugins.Messenger;
using Cirrious.MvvmCross.ViewModels;
using CodeCamp.Core.Data.Entities;
using CodeCamp.Core.Services;

namespace CodeCamp.Core.ViewModels
{
    public class SessionsViewModel : ViewModelBase
    {
        private readonly ICodeCampService _campService;

        public SessionsViewModel(IMvxMessenger messenger, ICodeCampService campService) 
            : base(messenger)
        {
            _campService = campService;
        }

        private IList<Session> _sessions;
        public IList<Session> Sessions
        {
            get { return _sessions; }
            set { _sessions = value; RaisePropertyChanged(() => Sessions); }
        } 
        
        public async Task Init()
        {
            bool successful = await SafeOperation(
                Task.Run(async () => Sessions = await _campService.ListSessions()));

            FinishedLoading(successful);
        }

        public ICommand ViewSessionCommand
        {
            get
            {
                return new MvxCommand<Session>(
                    session => ShowViewModel<SessionViewModel>(new SessionViewModel.NavigationParameters(session.Id)));
            }
        }
    }
}
