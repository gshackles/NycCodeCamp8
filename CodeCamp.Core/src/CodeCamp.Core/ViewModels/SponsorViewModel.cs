using System.Threading.Tasks;
using System.Windows.Input;
using Cirrious.MvvmCross.Plugins.Messenger;
using Cirrious.MvvmCross.Plugins.WebBrowser;
using Cirrious.MvvmCross.ViewModels;
using CodeCamp.Core.Data.Entities;
using CodeCamp.Core.Services;
#if !WINDOWS_PHONE
using TaskEx = System.Threading.Tasks.Task;
#endif

namespace CodeCamp.Core.ViewModels
{
    public class SponsorViewModel : ViewModelBase
    {
        private readonly ICodeCampService _campService;
        private readonly IMvxWebBrowserTask _webBrowserTask;

        public SponsorViewModel(IMvxMessenger messenger, ICodeCampService campService, IMvxWebBrowserTask webBrowserTask) 
            : base(messenger)
        {
            _campService = campService;
            _webBrowserTask = webBrowserTask;
        }

        private Sponsor _sponsor;
        public Sponsor Sponsor
        {
            get { return _sponsor; }
            set { _sponsor = value; RaisePropertyChanged(() => Sponsor); }
        } 

        public async Task Init(NavigationParameters parameters)
        {
            bool successful = await SafeOperation(
                TaskEx.Run(async () => Sponsor = await _campService.GetSponsor(parameters.Id)));

            FinishedLoading(successful);
        }

        public ICommand ViewWebsiteCommand
        {
            get { return new MvxCommand(() => _webBrowserTask.ShowWebPage(Sponsor.Website)); }
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