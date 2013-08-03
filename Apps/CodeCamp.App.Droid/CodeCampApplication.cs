using System;
using System.Collections.Generic;
using System.Linq;
using Android.App;
using Android.Runtime;

namespace CodeCamp.App.Droid
{
    [Application(Theme = "@android:style/Theme.Holo.NoActionBar")]
    public class CodeCampApplication : Application
    {
        public CodeCampApplication(IntPtr javaReference, JniHandleOwnership transfer)
            : base(javaReference, transfer)
        {
        }
    }
}