using DSD_WinformsApp.Core.DTOs;

namespace DSD_WinformsApp.Infrastructure.Data.Services
{
    public interface IDocumentRepository
    {
        Task<List<DocumentDto>> GetAllDocuments();
    }
}