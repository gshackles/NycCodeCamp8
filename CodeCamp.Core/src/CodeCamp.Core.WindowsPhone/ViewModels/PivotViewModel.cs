using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Cirrious.MvvmCross.Plugins.Messenger;
using Cirrious.MvvmCross.ViewModels;
using CodeCamp.Core.Services;
using CodeCamp.Core.ViewModels;

namespace CodeCamp.Core.WindowsPhone.ViewModels
{
    public class PivotViewModel : ViewModelBase
    {
        private readonly ICodeCampService _campService;

        public PivotViewModel(IMvxMessenger messenger, ICodeCampService campService) 
            : base(messenger)
        {
            _campService = campService;
        }

        public async Task Init()
        {
            bool successful = await SafeOperation(
                TaskEx.Run(async () =>
                                     {
                                         await _campService.ListSessions();

                                         reinitializeChildViewModels();
                                     }));

            FinishedLoading(successful);
        }

        private OverviewViewModel _overviewViewModel;
        public OverviewViewModel OverviewViewModel
        {
            get { return _overviewViewModel; }
            set { _overviewViewModel = value; RaisePropertyChanged(() => OverviewViewModel); }
        } 

        private SessionsViewModel _sessionsViewModel;
        public SessionsViewModel SessionsViewModel
        {
            get { return _sessionsViewModel; }
            set { _sessionsViewModel = value; RaisePropertyChanged(() => SessionsViewModel); }
        } 

        private SpeakersViewModel _speakersViewModel;
        public SpeakersViewModel SpeakersViewModel
        {
            get { return _speakersViewModel; }
            set { _speakersViewModel = value; RaisePropertyChanged(() => SpeakersViewModel); }
        } 

        private SponsorsViewModel _sponsorsViewModel;
        public SponsorsViewModel SponsorsViewModel
        {
            get { return _sponsorsViewModel; }
            set { _sponsorsViewModel = value; RaisePropertyChanged(() => SponsorsViewModel); }
        }

        public ICommand RefreshDataCommand
        {
            get
            {
                return new MvxCommand(async () =>
                {
                    bool successful = await SafeOperation(_campService.RefreshData(), () => IsLoading);
                    
                    if (successful)
                        reinitializeChildViewModels();
                });
            }
        }

        public ICommand ViewMapCommand
        {
            get { return new MvxCommand(() => ShowViewModel<MapViewModel>()); }
        }

        private void reinitializeChildViewModels()
        {
            OverviewViewModel = new OverviewViewModel(Messenger, _campService);
            OverviewViewModel.Init();

            SessionsViewModel = new SessionsViewModel(Messenger, _campService);
            SessionsViewModel.Init();

            SpeakersViewModel = new SpeakersViewModel(Messenger, _campService);
            SpeakersViewModel.Init();

            SponsorsViewModel = new SponsorsViewModel(Messenger, _campService);
            SponsorsViewModel.Init();
        }
    }
}