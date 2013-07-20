using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using Cirrious.MvvmCross.Plugins.Messenger;
using Cirrious.MvvmCross.ViewModels;
using CodeCamp.Core.Data.Entities;
using CodeCamp.Core.Services;

namespace CodeCamp.Core.ViewModels
{
    public class SpeakersViewModel : ViewModelBase
    {
        private readonly ICodeCampService _campService;

        public SpeakersViewModel(IMvxMessenger messenger, ICodeCampService campService) 
            : base(messenger)
        {
            _campService = campService;
        }

        private IList<Speaker> _speakers;
        public IList<Speaker> Speakers
        {
            get { return _speakers; }
            set { _speakers = value; RaisePropertyChanged(() => Speakers); }
        }

        public async Task Init()
        {
            bool successful = await SafeOperation(
                Task.Run(async () => Speakers = await _campService.ListSpeakers()));

            FinishedLoading(successful);
        }

        public ICommand ViewSpeakerCommand
        {
            get
            {
                return new MvxCommand<Speaker>(
                    speaker => ShowViewModel<SpeakerViewModel>(new SpeakerViewModel.NavigationParameters(speaker.Id)));
            }
        }
    }
}