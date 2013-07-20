using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using Cirrious.MvvmCross.Plugins.Messenger;
using Cirrious.MvvmCross.ViewModels;
using CodeCamp.Core.Data.Entities;
using CodeCamp.Core.Services;

namespace CodeCamp.Core.ViewModels
{
    public class SponsorsViewModel : ViewModelBase
    {
        private readonly ICodeCampService _campService;

        public SponsorsViewModel(IMvxMessenger messenger, ICodeCampService campService) 
            : base(messenger)
        {
            _campService = campService;
        }

        private IList<Sponsor> _sponsors;
        public IList<Sponsor> Sponsors
        {
            get { return _sponsors; }
            set { _sponsors = value; RaisePropertyChanged(() => Sponsors); }
        } 

        public async Task Init()
        {
            bool successful = await SafeOperation(
                Task.Run(async () => Sponsors = await _campService.ListSponsors()));

            FinishedLoading(successful);
        }

        public ICommand ViewSponsorCommand
        {
            get
            {
                return new MvxCommand<Sponsor>(
                    sponsor => ShowViewModel<SponsorViewModel>(new SponsorViewModel.NavigationParameters(sponsor.Id)));
            }
        }
    }
}