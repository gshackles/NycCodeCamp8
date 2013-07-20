using System.Threading.Tasks;
using Cirrious.MvvmCross.Plugins.Messenger;
using CodeCamp.Core.Data.Entities;
using CodeCamp.Core.Services;

namespace CodeCamp.Core.ViewModels
{
    public class SponsorViewModel : ViewModelBase
    {
        private readonly ICodeCampService _campService;

        public SponsorViewModel(IMvxMessenger messenger, ICodeCampService campService) 
            : base(messenger)
        {
            _campService = campService;
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
                Task.Run(async () => Sponsor = await _campService.GetSponsor(parameters.Id)));

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