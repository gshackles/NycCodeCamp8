using System.Collections.Generic;
using System.Windows.Input;
using Cirrious.MvvmCross.Plugins.Messenger;
using Cirrious.MvvmCross.ViewModels;
using CodeCamp.Core.ViewModels.Annotations;

namespace CodeCamp.Core.ViewModels
{
    [DoesNotRequireLoading]
    public class MenuViewModel : ViewModelBase
    {
        public MenuViewModel(IMvxMessenger messenger) 
            : base(messenger)
        {
        }

        public ICommand ShowSessionsCommand
        {
            get
            {
                return new MvxCommand(clearStackAndShow<SessionsViewModel>);
            }
        }

        public ICommand ShowSpeakersCommand
        {
            get
            {
                return new MvxCommand(clearStackAndShow<SpeakersViewModel>);
            }
        }

        public ICommand ShowMapCommand
        {
            get
            {
                return new MvxCommand(clearStackAndShow<MapViewModel>);
            }
        }

        private void clearStackAndShow<TViewModel>()
            where TViewModel : ViewModelBase
        {
            var presentationBundle = new MvxBundle(new Dictionary<string, string> { { PresentationBundleFlagKeys.ClearStack, "" } });

            ShowViewModel<TViewModel>(presentationBundle: presentationBundle);
        }
    }
}