using System.Collections.Generic;
using Cirrious.MvvmCross.Binding.BindingContext;
using CodeCamp.Core.Extensions;
using CodeCamp.Core.ViewModels;
using CrossUI.Touch.Dialog.Elements;
using MonoTouch.UIKit;
using CodeCamp.App.iOS.Views.Elements;

namespace CodeCamp.App.iOS.Views
{
    public class SessionView : DialogViewControllerBase
    {
        public SessionView()
            : base(UITableViewStyle.Grouped)
        {
        }

        protected override void OnLoadingComplete()
        {
            base.OnLoadingComplete();

            var bindings = this.CreateInlineBindingTarget<SessionViewModel>();

            var sections = new List<Section>();
            sections.Add(new Section
            {
                new TransparentMultilineElement
                {
                    Font = AppStyles.EntityTitleFont
                }.Bind(bindings, el => el.Caption, vm => vm.Session.Title),
                new TransparentStringElement(ViewModel.Session.StartTime.FormatTime() + " - " + ViewModel.Session.EndTime.FormatTime()),
                new TransparentStringElement().Bind(bindings, el => el.Caption, vm => vm.Session.RoomName)
            });

            if (ViewModel.Session.SpeakerId.HasValue)
            {
                sections.Add(new Section
                {
                    new StyledMultilineElement 
                    { 
                        Accessory = UITableViewCellAccessory.DisclosureIndicator,
                        BackgroundColor = AppStyles.SemiTransparentCellBackgroundColor,
                        TextColor = AppStyles.ListTitleColor,
                        Font = AppStyles.ListTitleFont
                    }
                    .Bind(bindings, el => el.Caption, vm => vm.Session.SpeakerName)
                    .Bind(bindings, el => el.SelectedCommand, vm => vm.ViewSpeakerCommand),
                });
            }

            sections.Add(new Section
            {
                new TransparentMultilineElement()
                    .Bind(bindings, el => el.Caption, vm => vm.Session.Abstract, "MultiLine")
            });

            Root = new RootElement("Session Info")
            {
                sections
            };
            Root.UnevenRows = true;
        }

        private new SessionViewModel ViewModel
        {
            get { return (SessionViewModel)base.ViewModel; }
        }
    }
}