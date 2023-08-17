using DSD_WinformsApp.Core.DTOs;

namespace DSD_WinformsApp.Presenter
{
    public interface IDocumentPresenter
    {
        Task<bool> DeleteBackUpFile(BackUpFileDto file);
        Task<bool> DeleteDocumentWithBackups(DocumentDto document);
        void EditDocument(DocumentDto document, byte[] fileDataBytes);
        Task<List<BackUpFileDto>> GetRelatedBackupFiles(int documentId);
        Task LoadDocuments();
        void SaveDocument(DocumentDto document, byte[] fileDataBytes);
        void SaveUserRegistration(UserCredentialsDto userCredentials);
        Task<bool> ValidateUserCredentials(UserCredentialsDto userCredentials);
    }
}