using System.Windows.Input;
using CrossUI.Touch.Dialog.Elements;

namespace CodeCamp.App.iOS.Views.Elements
{
    public class CommandBindableSection<TElement> : BindableSection<TElement>
        where TElement : Element, IBindableElement
    {
        private readonly ICommand _selectedCommand;

        public CommandBindableSection(string caption, ICommand selectedCommand)
            : base(caption)
        {
            _selectedCommand = selectedCommand;
        }

        protected override void SetItemsSource(System.Collections.IEnumerable value)
        {
            base.SetItemsSource(value);

            foreach (TElement element in Elements)
            {
                element.Tapped += () => _selectedCommand.Execute(element.DataContext);
            }
        }
    }
}