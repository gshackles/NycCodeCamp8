using Cirrious.MvvmCross.Binding.BindingContext;

namespace CodeCamp.App.iOS.Views.Elements
{
    public interface IBindableElement
        : IMvxBindingContextOwner
    {
        object DataContext { get; set; }
    }
}