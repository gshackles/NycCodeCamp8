using System;
using System.Linq.Expressions;
using System.Reflection;
using Android.App;
using Android.Content;
using CodeCamp.Core.ViewModels;

namespace CodeCamp.App.Droid.Extensions
{
    public static class ViewModelExtensions
    {
        public static void BindLoadingMessage<TViewModel>(this TViewModel viewModel, Context context,
                                                          Expression<Func<TViewModel, bool>> property, string message)
            where TViewModel : ViewModelBase
        {
            var expression = property.Body as MemberExpression;

            if (expression == null) return;

            var propertyInfo = expression.Member as PropertyInfo;

            if (propertyInfo == null) return;

            var progressDialog = new ProgressDialog(context) { Indeterminate = true };
            progressDialog.SetMessage(message);

            Action<bool> setMessageVisibility =
                visible =>
                    {
                        if (visible)
                            progressDialog.Show();
                        else
                            progressDialog.Hide();
                    };

            setMessageVisibility((bool) propertyInfo.GetValue(viewModel, null));

            viewModel.PropertyChanged +=
                (sender, args) =>
                    {
                        if (args.PropertyName == propertyInfo.Name)
                            setMessageVisibility((bool)propertyInfo.GetValue(viewModel, null));
                    };
        }
    }
}