using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TenderApp.Services
{
    public static class ServiceExtentions
    {
        public static ObservableCollection<T> ToObservableCollection<T>
            (this IEnumerable<T> source) => [.. source];
    }
}
