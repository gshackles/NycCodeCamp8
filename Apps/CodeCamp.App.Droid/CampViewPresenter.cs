using System;
using Android.Content;
using Cirrious.CrossCore;
using Cirrious.CrossCore.Platform;
using Cirrious.MvvmCross.Droid.Views;
using CodeCamp.Core.ViewModels;

namespace CodeCamp.App.Droid
{
    public class CampViewPresenter : MvxAndroidViewPresenter
    {
        public override void Show(Cirrious.MvvmCross.ViewModels.MvxViewModelRequest request)
        {
            var activity = Activity;
            if (activity == null)
            {
                MvxTrace.Warning("Cannot Resolve current top activity");
                return;
            }
            var requestTranslator = Mvx.Resolve<IMvxAndroidViewModelRequestTranslator>();
            var intent = requestTranslator.GetIntentFor(request);

            // Android, Y U NO CLEAR STACK?
            if (request.PresentationValues != null &&
                request.PresentationValues.ContainsKey(PresentationBundleFlagKeys.ClearStack))
            {
                intent.AddFlags(ActivityFlags.ClearTop | ActivityFlags.NewTask);
            }

            activity.StartActivity(intent);
        }
    }
}