using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows.Data;

namespace TenderApp.Services
{
    public static class ServiceExtentions
    {
        public static ObservableCollection<T> ToObservableCollection<T>
            (this IEnumerable<T> source) => [.. source];
    }
}
