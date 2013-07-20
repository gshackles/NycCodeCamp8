using Cirrious.MvvmCross.Plugins.Messenger;
using CodeCamp.Core.ViewModels.Annotations;

namespace CodeCamp.Core.ViewModels
{
    [DoesNotRequireLoading]
    public class MapViewModel : ViewModelBase
    {
        public MapViewModel(IMvxMessenger messenger) 
            : base(messenger)
        {
        }
    }
}