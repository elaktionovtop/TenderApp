using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace TenderApp.Views
{
    public static class ItemWindowHelper
    {
        public static void BindingUpdating(Window window)
        {
            // Принудительное обновление binding'а у текущего элемента
            var element = FocusManager.GetFocusedElement(window)
                as FrameworkElement;
            if(element != null)
            {
                var binding = BindingOperations.GetBindingExpression
                    (element, TextBox.TextProperty);
                binding?.UpdateSource();
            }
        }

        public static void FocusFirstField(object sender)
        {
            if(sender is UIElement element)
            {
                element.Focus();

                if(element is TextBox textBox)
                {
                    textBox.SelectAll();
                }
            }
        }
    }
}
