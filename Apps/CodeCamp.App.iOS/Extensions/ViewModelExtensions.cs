using System;
using System.Linq.Expressions;
using System.Reflection;
using BigTed;
using CodeCamp.Core.ViewModels;
using MonoTouch.UIKit;

namespace CodeCamp.App.iOS.Extensions
{
    public static class ViewModelExtensions
    {
        public static void BindLoadingMessage<TViewModel>(this TViewModel viewModel, UIView view, Expression<Func<TViewModel, bool>> property, string message)
            where TViewModel : ViewModelBase
        {
            var expression = property.Body as MemberExpression;

            if (expression == null) return;

            var propertyInfo = expression.Member as PropertyInfo;

            if (propertyInfo == null) return;

            var hud = new BTProgressHUD();

            Action<bool> setMessageVisibility = visible =>
            {
                if (visible)
                {
                    hud.Show(message, maskType: BTProgressHUD.MaskType.Black);
                }
                else
                {
                    hud.Dismiss();
                }
            };

            setMessageVisibility((bool)propertyInfo.GetValue(viewModel, null));

            viewModel.PropertyChanged += (sender, e) => 
            {
                if (e.PropertyName == propertyInfo.Name)
                    setMessageVisibility((bool)propertyInfo.GetValue(viewModel, null));
            };
        }
    }
}

