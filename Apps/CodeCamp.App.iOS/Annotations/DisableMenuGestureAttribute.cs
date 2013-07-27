using System;

namespace CodeCamp.App.iOS.Annotations
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class DisableMenuGestureAttribute : Attribute
    {
    }
}