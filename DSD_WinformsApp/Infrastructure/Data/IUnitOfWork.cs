using DSD_WinformsApp.Infrastructure.Data.Services;

namespace DSD_WinformsApp.Infrastructure.Data
{
    public interface IUnitOfWork
    {
        IDocumentRepository Documents { get; }

        int Complete();
        void Dispose();
    }
}