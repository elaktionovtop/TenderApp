using CommunityToolkit.Mvvm.Input;

using Microsoft.Extensions.DependencyInjection;

using System.Diagnostics;
using System.Windows;

using TenderApp.Models;
using TenderApp.Services;
using TenderApp.Views;

namespace TenderApp.ViewModels
{
    public partial class TenderListViewModel
        (IDbService<Tender> service) 
        : ListViewModel<Tender>(service)
    {

        //  команды Организатора (Category Manager)
        //  ---------------------------------------
        //  добавить тендер (окно заголовка тендера)
        //  [RelayCommand]
        //  protected virtual void CreateItem() 

        //  удалить (с подтверждением)
        //  [RelayCommand]
        //  protected virtual void DeleteItem() 

        //  окно заголоовка тендера
        //  [RelayCommand]
        //  protected virtual void EditItem()  

        //  ---------------------------------------

        // окно критериев тендера (CRUD)
        [RelayCommand]
        private void TenderCriteria()
        {
            Debug.WriteLine(nameof(TenderCriteria));
            new TenderCriterionListWindow(SelectedItem.Id)
                .ShowDialog();
        }

        // окно заявок тендера (CRUD)
        [RelayCommand]
        private void TenderProposal()
        {
            Debug.WriteLine(nameof(TenderProposal));
            new ProposalListWindow(SelectedItem.Id)
                .ShowDialog();
        }

        //  окно критериев (CRUD)
        [RelayCommand]
        private void Criteria()
        {
            Debug.WriteLine(nameof(Criteria));
            new CriterionListWindow().ShowDialog();
        }

        //  ---------------------------------------
        //  команды Участника (Buyer)
        //  ---------------------------------------
        //  окно заявок участника тендера(CRUD)
        [RelayCommand]
        private void BuyerTenderProposal()
        {
            Debug.WriteLine(nameof(BuyerTenderProposal));
            int buyerId = App.Services
                    .GetRequiredService<IAuthService>()
                    .CurrentUser.Id; 
            new ProposalListWindow(SelectedItem.Id, buyerId)
                .ShowDialog();
        }

        //  ---------------------------------------
        //  команды Администратора (Buyer)
        //  ---------------------------------------
        //  окно пользователей (CRUD)
        [RelayCommand]
        private void Users()
        {
            Debug.WriteLine(nameof(Users));
            new UserListWindow().ShowDialog();
        }
        //  ---------------------------------------

        protected override ItemViewModel<Tender> CreateItemViewModel(IDbService<Tender> service, Tender item)
        {
            return new TenderItemViewModel((TenderService)service, item);
        }

        protected override ItemViewModel<Tender> CreateItemViewModel(IDbService<Tender> service)
        {
            return new TenderItemViewModel((TenderService)service);
        }

        protected override Window CreateItemDialog(ItemViewModel<Tender> itemViewModel)
            => new TenderItemWindow(itemViewModel);
    }
}
