using System;
using Cirrious.MvvmCross.WindowsPhone.Views;
using CodeCamp.Core.WindowsPhone.ViewModels;

namespace CodeCamp.App.WindowsPhone.Views
{
    public partial class PivotView : MvxPhonePage
    {
        public PivotView()
        {
            InitializeComponent();
        }

        private void RefreshData(object sender, EventArgs e)
        {
            ViewModel.RefreshDataCommand.Execute(null);
        }

        private new PivotViewModel ViewModel
        {
            get { return (PivotViewModel) base.ViewModel; }
        }
    }
}