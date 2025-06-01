using Microsoft.Extensions.DependencyInjection;

using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

using TenderApp.Models;
using TenderApp.ViewModels;

namespace TenderApp.Views
{
    public partial class ProposalListWindow : Window
    {
        public ProposalListWindow(int tenderId, int buyerId = 0)
        {
            InitializeComponent();
            var viewModel = App.Services
                .GetRequiredService<ProposalListViewModel>();
            viewModel.TenderId = tenderId;
            viewModel.BuyerId = buyerId;
            viewModel.GetData();
            DataContext = viewModel;

            Loaded += (s, e)
                => ListWindowHelper.MoveFocus(dataGrid);

            viewModel.Items.CollectionChanged += (s, e)
                => ListWindowHelper.FocusSelectedRow(dataGrid);

            dataGrid.SelectionChanged += (s, e)
                => ListWindowHelper.FocusSelectedRow(dataGrid);

            dataGrid.MouseDoubleClick += (s, e) => ListWindowHelper
                .MouseDoubleClick<Proposal>(dataGrid, viewModel, e);

            dataGrid.PreviewKeyDown += (s, e) => ListWindowHelper
                .PreviewKeyDown<Proposal>(dataGrid, viewModel, e);
        }

        private void DataGrid_Loaded(object sender, RoutedEventArgs e)
        {
            var vm = DataContext as ProposalListViewModel;
            if(vm?.Items == null || vm.Items.Count == 0) return;

            var grid = sender as DataGrid;

            // Получаем список всех критериев из первой заявки
            var firstProposal = vm.Items.FirstOrDefault();
            if(firstProposal == null) return;

           // Список уникальных критериев по Id
           var criteria = firstProposal.Values
               .Select(v => v.TenderCriterion.Criterion)
               //.GroupBy(c => c.Id)
               //.Select(g => g.First())
               .ToList();

            foreach(var criterion in criteria)
            {
                // Колонка значения
                var valueColumn = new DataGridTextColumn
                {
                    Header = $"{criterion.Name}",
                    Binding = new Binding($"ValueMap[{criterion.Id}]")
                };
                grid?.Columns.Add(valueColumn);

                // Колонка оценки
                var scoreColumn = new DataGridTextColumn
                {
                    Header = $"(оценка)",
                    Binding = new Binding($"ScoreMap[{criterion.Id}]")
                };
                grid?.Columns.Add(scoreColumn);
            }
        }
    }
}
