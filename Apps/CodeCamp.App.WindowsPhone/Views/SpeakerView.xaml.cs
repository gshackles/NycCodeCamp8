using System;
using System.Collections.Generic;
using System.Linq;
using Cirrious.MvvmCross.WindowsPhone.Views;
using CodeCamp.Core.ViewModels;

namespace CodeCamp.App.WindowsPhone.Views
{
    public partial class SpeakerView : MvxPhonePage
    {
        public SpeakerView()
        {
            InitializeComponent();
        }

        private void EmailSpeaker(object sender, EventArgs e)
        {
            ((SpeakerViewModel) ViewModel).EmailSpeakerCommand.Execute(null);
        }
    }
}