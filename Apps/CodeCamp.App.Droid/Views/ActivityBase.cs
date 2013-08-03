using System;
using System.Windows.Input;
using Cirrious.MvvmCross.Droid.Views;
using CodeCamp.App.Droid.Extensions;
using CodeCamp.Core.ViewModels;
using LegacyBar.Library.BarActions;

namespace CodeCamp.App.Droid.Views
{
    public abstract class ActivityBase : MvxActivity
    {
        protected virtual ICommand RefreshCommand { get { return null; } }
        protected LegacyBar.Library.Bar.LegacyBar LegacyBar { get; private set; }

        protected override void OnViewModelSet()
        {
            base.OnViewModelSet();

            ((ViewModelBase) ViewModel).BindLoadingMessage(this, vm => vm.IsLoading, "Loading...");
        }

        public override void SetContentView(int layoutResId)
        {
            base.SetContentView(layoutResId);

            LegacyBar = FindViewById<LegacyBar.Library.Bar.LegacyBar>(Resource.Id.ActionBar);

            if (LegacyBar != null)
            {
                LegacyBar.Title = Title;
                LegacyBar.Theme = LegacyBarTheme.HoloBlack;

                LegacyBar.SetHomeAction(new ActionLegacyBarAction(this,
                                                                  () =>
                                                                      {
                                                                          Console.WriteLine("TODO: show menu");
                                                                      }, Resource.Drawable.Menu));

                if (RefreshCommand != null)
                    LegacyBar.AddAction(new ActionLegacyBarAction(this, () => RefreshCommand.Execute(null), Resource.Drawable.Refresh));
            }
        }
    }
}