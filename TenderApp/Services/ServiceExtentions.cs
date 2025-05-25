using System.Collections.ObjectModel;

namespace TenderApp.Services
{
    public static class ServiceExtentions
    {
        public static ObservableCollection<T> ToObservableCollection<T>
            (this IEnumerable<T> source) => [.. source];
    }
}
