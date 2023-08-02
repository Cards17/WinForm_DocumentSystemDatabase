using DSD_WinformsApp.Core.DTOs;

namespace DSD_WinformsApp.Infrastructure.Data.Services
{
    public interface IDocumentRepository
    {
        void CreateDocument(DocumentDto document);
        Task<List<DocumentDto>> GetAllDocuments();
    }
}