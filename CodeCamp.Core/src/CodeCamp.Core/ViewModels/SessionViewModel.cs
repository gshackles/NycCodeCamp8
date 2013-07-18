using System.Threading.Tasks;
using Cirrious.MvvmCross.Plugins.Messenger;
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