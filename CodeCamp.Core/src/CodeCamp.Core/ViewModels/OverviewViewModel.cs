using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Cirrious.MvvmCross.Plugins.Messenger;
using Cirrious.MvvmCross.ViewModels;
using CodeCamp.Core.Data.Entities;
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

        private IList<TimeSlot> _timeSlots;
        public IList<TimeSlot> TimeSlots
        {
            get { return _timeSlots; }
            set { _timeSlots = value; RaisePropertyChanged(() => TimeSlots); }
        } 

        public async Task Init()
        {
            bool successful = await SafeOperation(
                Task.Run(async () => TimeSlots = await getNextTwoSlotsAsync()));

            FinishedLoading(successful);
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
                        TimeSlots = await getNextTwoSlotsAsync();
                    }), () => IsRefreshing);

                    if (DataRefreshComplete != null)
                        DataRefreshComplete.Invoke(this, successful);
                });
            }
        }

        public ICommand ViewFullScheduleCommand
        {
            get { return new MvxCommand(() => ShowViewModel<SessionsViewModel>()); }
        }

        private async Task<IList<TimeSlot>> getNextTwoSlotsAsync()
        {
            return (from slot in await _campService.ListSessions()
                    where slot.EndTime >= DateTime.UtcNow
                    orderby slot.StartTime
                    select slot).Take(2).ToList();
        } 
    }
}