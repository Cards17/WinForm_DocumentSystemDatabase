using AutoMapper;
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


            Application.Run(ServiceProvider.GetRequiredService<Document_MainView>());
        }
        public static IServiceProvider? ServiceProvider { get; private set; }
        static IHostBuilder CreateHostBuilder()
        {
            return Host.CreateDefaultBuilder()
                .ConfigureServices((context, services) => {
                    services.AddTransient<IUnitOfWork, UnitOfWork>();
                     services.AddTransient<IDocumentRepository, DocumentRepository>();
                    services.AddTransient<IDocumentDbContext, DocumentDbContext>();
                    services.AddAutoMapper(typeof(DocumentMappingProfiles));
                    services.AddTransient<Document_MainView>();
                });
        }
    }
}
