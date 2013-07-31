using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Android.Content;
using Android.Views;
using Cirrious.MvvmCross.Binding.Droid.BindingContext;
using Cirrious.MvvmCross.Binding.Droid.Views;
using CodeCamp.Core.Data.Entities;
using CodeCamp.Core.Extensions;

namespace CodeCamp.App.Droid.Views
{
    public class SessionListAdapter : MvxAdapter
    {
        public SessionListAdapter(Context context, IMvxAndroidBindingContext bindingContext)
            : base(context, bindingContext)
        {
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
            if (source is Session)
                templateId = Resource.Layout.SessionListItem;
            else
                templateId = Resource.Layout.SessionListHeader;

            return base.GetBindableView(convertView, source, templateId);
        }
    }
}