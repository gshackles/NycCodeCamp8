using System.Threading.Tasks;
using Cirrious.MvvmCross.Plugins.Messenger;
using System;
using System.Windows.Input;
using Cirrious.MvvmCross.ViewModels;
using CodeCamp.Core.Services;

namespace CodeCamp.Core.ViewModels
{
    public class OverviewViewModel : ViewModelBase
    {
        private readonly ICodeCampService _campService;

        public OverviewViewModel(IMvxMessenger messenger, ICodeCampService campService)
            : base(messenger)
        {
            _campService = campService;
        }

        public async Task Init()
        {
            FinishedLoading(true);
        }

        public event EventHandler<bool> DataRefreshComplete;

        private bool _isRefreshing;
        public bool IsRefreshing
        {
            get { return _isRefreshing; }
            set { _isRefreshing = value; RaisePropertyChanged(() => IsRefreshing); }
        }

        public ICommand RefreshDataCommand
        {
            get
            {
                return new MvxCommand(async () =>
                {
                    bool successful = await SafeOperation(Task.Run(async () => 
                    {
                        await _campService.RefreshData();
                    }), () => IsRefreshing);

                    if (DataRefreshComplete != null)
                        DataRefreshComplete.Invoke(this, successful);
                });
            }
        }
    }
}