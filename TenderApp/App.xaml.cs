using TenderApp.ViewModels;
using TenderApp.Views;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

using System;
using System.Configuration;
using System.Data;
using System.Windows;

using TenderApp.Data;
using TenderApp.Services;

using static TenderApp.Data.TenderDbContext;
using TenderApp.Models;

namespace TenderApp
{
    public partial class App : Application
    {
        public static IServiceProvider Services { get; private set; } = null!;

        protected override void OnStartup(StartupEventArgs e)
        {
            var services = new ServiceCollection();

            // Регистрируем зависимости
            services.AddDbContext<TenderDbContext>(options =>
                options.UseSqlServer(ContextFactory.ConnectionString),
                contextLifetime: ServiceLifetime.Singleton,
                optionsLifetime: ServiceLifetime.Singleton);

            services.AddSingleton<IAuthService, AuthService>();
            services.AddTransient<LoginViewModel>();
            services.AddTransient<LoginWindow>();

            services.AddTransient<IDbService<Tender>, TenderService>();
            services.AddTransient<TenderListViewModel>();
            services.AddTransient<TenderListWindow>();

            services.AddTransient<IDbService<User>, UserService>(); 
            services.AddTransient<UserService>();
            services.AddTransient<UserListViewModel>();
            services.AddTransient<UserListWindow>();

            services.AddTransient<RoleService>();

            Services = services.BuildServiceProvider();

            // Проверка данных БД
            DbVerification.Perform();

            // Для отладки коммнтируем авторизацию
            Services.GetRequiredService<LoginWindow>().ShowDialog();

            // и загружаем главное окно
            //App.Services.GetRequiredService<TenderListWindow>().Show();
        }
    }
}
