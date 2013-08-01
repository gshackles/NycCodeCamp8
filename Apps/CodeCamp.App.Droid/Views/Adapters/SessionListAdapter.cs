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
    public class SessionListAdapter : MvxAdapter
    {
        private readonly ICommand _itemClickCommand;

        public SessionListAdapter(Context context, IMvxAndroidBindingContext bindingContext, ICommand itemClickCommand)
            : base(context, bindingContext)
        {
            _itemClickCommand = itemClickCommand;
        }

        protected override void SetItemsSource(IEnumerable value)
        {
            var timeSlots = (IEnumerable<TimeSlot>) value;
            var flattenedList = new List<object>();

            foreach (var timeSlot in timeSlots ?? new List<TimeSlot>())
            {
                flattenedList.Add(string.Format("{0} - {1}", timeSlot.StartTime.FormatTime(), timeSlot.EndTime.FormatTime()));
                flattenedList.AddRange(timeSlot.Sessions);
            }

            base.SetItemsSource(flattenedList);
        }

        protected override View GetBindableView(View convertView, object source, int templateId)
        {
            templateId = source is Session
                             ? Resource.Layout.SessionListItem
                             : Resource.Layout.SessionListHeader;

            var view = base.GetBindableView(convertView, source, templateId);

            if (source is Session && convertView == null)
            {
                view.Click += (sender, args) =>
                {
                    _itemClickCommand.Execute(source);
                };
            }

            return view;
        }
    }
}