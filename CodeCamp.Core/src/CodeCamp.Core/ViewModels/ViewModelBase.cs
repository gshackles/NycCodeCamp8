using System;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;
using Cirrious.MvvmCross.Plugins.Messenger;
using Cirrious.MvvmCross.ViewModels;
using CodeCamp.Core.Extensions;
using CodeCamp.Core.Messaging.Messages;
using CodeCamp.Core.ViewModels.Annotations;

namespace CodeCamp.Core.ViewModels
{
    public static class PresentationBundleFlagKeys
    {
        public const string ClearStack = "__CLEAR_STACK__";
    }

    public abstract class ViewModelBase : MvxViewModel
    {
        protected ViewModelBase(IMvxMessenger messenger)
        {
            Messenger = messenger;

            IsLoading = !this.HasAttribute<DoesNotRequireLoadingAttribute>();
        }

        public IMvxMessenger Messenger { get; private set; }
        public event EventHandler<bool> LoadingComplete;

        private bool _isLoading;
        public bool IsLoading
        {
            get { return _isLoading; }
            private set { _isLoading = value; RaisePropertyChanged(() => IsLoading); }
        }

        protected void FinishedLoading(bool successful)
        {
            IsLoading = false;

            if (LoadingComplete != null)
                LoadingComplete.Invoke(this, successful);
        }

        protected async Task<bool> SafeOperation(Task operation, Expression<Func<bool>> operationFlag = null)
        {
            Action<bool> setOperationFlag = isInProgress =>
            {
                if (operationFlag == null) return;

                var memberSelectorExpression = operationFlag.Body as MemberExpression;
                if (memberSelectorExpression == null) return;

                var property = memberSelectorExpression.Member as PropertyInfo;
                if (property == null) return;

                property.SetValue(this, isInProgress, null);
            };

            try
            {
                setOperationFlag(true);

                await operation;

                return true;
            }
            catch (Exception)
            {
                ReportError("Something went wrong :(");

                return false;
            }
            finally
            {
                setOperationFlag(false);
            }
        }

        protected void ReportError(string message)
        {
            Messenger.Publish(new ErrorMessage(this, message));
        }
    }
}