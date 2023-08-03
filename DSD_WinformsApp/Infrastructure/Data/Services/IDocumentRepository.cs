using DSD_WinformsApp.Core.DTOs;

namespace DSD_WinformsApp.Infrastructure.Data.Services
{
    public interface IDocumentRepository
    {
        void CreateDocument(DocumentDto document, byte[] fileDataBytes);
        Task<List<DocumentDto>> GetAllDocuments();
        Task<bool> DeleteDocument(int documentId);
    }
}