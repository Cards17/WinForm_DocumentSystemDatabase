using AutoMapper;
using DSD_WinformsApp.Core.DTOs;
using DSD_WinformsApp.Core.Mapping_Profiles;
using DSD_WinformsApp.Infrastructure.Data;
using DSD_WinformsApp.Infrastructure.Data.Services;
using DSD_WinformsApp.Presenter;
using DSD_WinformsApp.View;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace DSD_WinformsApp
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var host = CreateHostBuilder().Build();
            ServiceProvider = host.Services;


            Application.Run(ServiceProvider.GetRequiredService<DocumentMainView>());
        }
        public static IServiceProvider? ServiceProvider { get; private set; }
        static IHostBuilder CreateHostBuilder()
        {
            return Host.CreateDefaultBuilder()
                .ConfigureServices((context, services) => {
                    services.AddTransient<IUnitOfWork, UnitOfWork>();
                    services.AddTransient<IDocumentRepository, DocumentRepository>();
                    services.AddTransient<IBackUpFileRepository, BackUpFileRepository>();
                    services.AddTransient<IDocumentDbContext, DocumentDbContext>();
                    services.AddTransient<IDocumentPresenter, DocumentPresenter>(); 

                    services.AddTransient<IUserRepository, UserRepository>();
                    services.AddAutoMapper(typeof(DocumentMappingProfiles));
                    services.AddTransient<IDocumentView, DocumentMainView>();
                    services.AddTransient<DocumentMainView>();
                  
                });
        }
    }
}
