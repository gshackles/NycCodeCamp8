using System;
using System.Collections.Generic;
using System.Linq;
using Cirrious.MvvmCross.WindowsPhone.Views;
using CodeCamp.Core.ViewModels;

namespace CodeCamp.App.WindowsPhone.Views
{
    public partial class SponsorView : MvxPhonePage
    {
        public SponsorView()
        {
            InitializeComponent();
        }

        private void GoToWebsite(object sender, EventArgs e)
        {
            ((SponsorViewModel) ViewModel).ViewWebsiteCommand.Execute(null);
        }
    }
}