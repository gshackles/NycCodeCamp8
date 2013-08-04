using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using Android.Content;
using Android.Views;
using Cirrious.MvvmCross.Binding.Droid.BindingContext;
using Cirrious.MvvmCross.Binding.Droid.Views;
using CodeCamp.Core.Data.Entities;
using CodeCamp.Core.Extensions;

namespace CodeCamp.App.Droid.Views.Adapters
{
    public class SponsorListAdapter : MvxAdapter
    {
        private readonly ICommand _itemClickCommand;

        public SponsorListAdapter(Context context, IMvxAndroidBindingContext bindingContext, ICommand itemClickCommand)
            : base(context, bindingContext)
        {
            _itemClickCommand = itemClickCommand;
        }

        protected override void SetItemsSource(IEnumerable value)
        {
            var tiers = (IEnumerable<SponsorTier>) value;
            var flattenedList = new List<object>();

            foreach (var tier in tiers ?? new List<SponsorTier>())
            {
                flattenedList.Add(tier.Name);
                flattenedList.AddRange(tier.Sponsors);
            }

            base.SetItemsSource(flattenedList);
        }

        protected override View GetBindableView(View convertView, object source, int templateId)
        {
            templateId = source is Sponsor
                             ? Resource.Layout.SponsorListItem
                             : Resource.Layout.SponsorListHeader;

            var view = base.GetBindableView(convertView, source, templateId);

            if (source is Sponsor && convertView == null)
                view.Click += (sender, args) => _itemClickCommand.Execute(source);

            return view;
        }
    }
}