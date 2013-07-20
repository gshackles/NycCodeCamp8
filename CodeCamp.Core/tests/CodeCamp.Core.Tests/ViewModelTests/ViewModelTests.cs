using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Cirrious.MvvmCross.Plugins.Messenger;
using CodeCamp.Core.Messaging.Messages;
using CodeCamp.Core.ViewModels;
using CodeCamp.Core.ViewModels.Annotations;
using NUnit.Framework;

namespace CodeCamp.Core.Tests.ViewModelTests
{
    public class ViewModelTests : ViewModelTestsBase
    {
        [Test]
        public void ReportError_MessageProvided_PublishesErrorMessage()
        {
            string expectedErrorMessage = "Expected message";
            var viewModel = new TestViewModel(Messenger);
            string publishedError = null;
            Messenger.Subscribe<ErrorMessage>(msg => publishedError = msg.Message);

            viewModel.ReportError(expectedErrorMessage);

            Assert.AreEqual(expectedErrorMessage, publishedError);
        }

        [Test]
        public async void SafeOperation_NoFlagProvided_NoExceptionThrown_ReturnsTrue()
        {
            var viewModel = new TestViewModel(Messenger);
            string publishedError = null;
            Messenger.Subscribe<ErrorMessage>(msg => publishedError = msg.Message);

            bool successful = await viewModel.SafeOperation(Task.FromResult(true));

            Assert.True(successful);
            Assert.Null(publishedError);
        }

        [Test]
        public async void SafeOperation_NoFlagProvided_ExceptionThrown_ReturnsFalseAndReportsError()
        {
            var viewModel = new TestViewModel(Messenger);
            string publishedError = null;
            Messenger.Subscribe<ErrorMessage>(msg => publishedError = msg.Message);

            bool successful = await viewModel.SafeOperation(Task.Run(() => { throw new Exception("boom"); }));

            Assert.False(successful);
            Assert.NotNull(publishedError);
        }

        [Test]
        public async void SafeOperation_FlagProvided_NoExceptionThrown_ReturnsTrue()
        {
            var viewModel = new TestViewModel(Messenger);
            string publishedError = null;
            Messenger.Subscribe<ErrorMessage>(msg => publishedError = msg.Message);

            bool successful = await viewModel.SafeOperation(Task.FromResult(true), () => viewModel.Flag);

            Assert.True(successful);
            Assert.Null(publishedError);
            Assert.AreEqual(2, viewModel.FlagUpdateCount);
        }

        [Test]
        public async void SafeOperation_FlagProvided_ExceptionThrown_ReturnsFalseAndReportsError()
        {
            var viewModel = new TestViewModel(Messenger);
            string publishedError = null;
            Messenger.Subscribe<ErrorMessage>(msg => publishedError = msg.Message);

            bool successful = await viewModel.SafeOperation(Task.Run(() => { throw new Exception("boom"); }), () => viewModel.Flag);

            Assert.False(successful);
            Assert.NotNull(publishedError);
            Assert.AreEqual(2, viewModel.FlagUpdateCount);
        }

        [Test]
        public void ClassNotMarkedWithDoesNotRequireLoadingAttribute_StartsOutInLoadingState()
        {
            var viewModel = new RequiresLoadingViewModel(Messenger);

            Assert.IsTrue(viewModel.IsLoading);
        }

        [Test]
        public void ClassMarkedWithDoesNotRequireLoadingAttribute_StartsOutInNotLoadingState()
        {
            var viewModel = new DoesNotRequireLoadingViewModel(Messenger);

            Assert.IsFalse(viewModel.IsLoading);
        }


        class TestViewModel : ViewModelBase
        {
            public TestViewModel(IMvxMessenger messenger)
                : base(messenger)
            {
            }

            public new Task<bool> SafeOperation(Task operation, Expression<Func<bool>> operationFlag = null)
            {
                return base.SafeOperation(operation, operationFlag);
            }

            public new void ReportError(string message)
            {
                base.ReportError(message);
            }

            public int FlagUpdateCount { get; private set; }

            private bool _flag;
            public bool Flag
            {
                get { return _flag; }
                set
                {
                    _flag = value;
                    FlagUpdateCount++;
                }
            }
        }

        class RequiresLoadingViewModel : ViewModelBase
        {
            public RequiresLoadingViewModel(IMvxMessenger messenger)
                : base(messenger)
            {
            }
        }

        [DoesNotRequireLoading]
        class DoesNotRequireLoadingViewModel : ViewModelBase
        {
            public DoesNotRequireLoadingViewModel(IMvxMessenger messenger)
                : base(messenger)
            {
            }
        }
    }
}