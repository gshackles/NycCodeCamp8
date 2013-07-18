using System;

namespace CodeCamp.Core.ViewModels.Annotations
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class DoesNotRequireLoadingAttribute : Attribute
    {
    }
}
