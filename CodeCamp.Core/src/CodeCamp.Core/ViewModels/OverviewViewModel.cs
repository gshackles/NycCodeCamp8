using System.Threading.Tasks;
using Cirrious.MvvmCross.Plugins.Messenger;

namespace CodeCamp.Core.ViewModels
{
    public class OverviewViewModel : ViewModelBase
    {
        public OverviewViewModel(IMvxMessenger messenger)
            : base(messenger)
        {
        }

        public async Task Init()
        {
            FinishedLoading(true);
        }
    }
}