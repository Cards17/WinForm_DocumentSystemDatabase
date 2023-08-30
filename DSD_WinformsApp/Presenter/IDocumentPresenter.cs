﻿using DSD_WinformsApp.Core.DTOs;

namespace DSD_WinformsApp.Presenter
{
    public interface IDocumentPresenter
    {
        Task<bool> DeleteBackUpFile(BackUpFileDto file);
        Task<bool> DeleteDocumentWithBackups(DocumentDto document);
        void EditDocument(DocumentDto document, byte[] fileDataBytes);
        Task<List<BackUpFileDto>> GetRelatedBackupFiles(int documentId);
        Task LoadDocuments();
        Task LoadDocumentsByFilter(string currentSearchQuery, string currentFilterCategory);
        void NextPage();
        void PreviousPage();
        Task ApplyFilters();
        void SaveDocument(DocumentDto document, byte[] fileDataBytes);
        Task SearchDocuments(string filterCriteria, string searchQuery);

        // User Repository methods
        Task<List<UserCredentialsDto>> LoadUsers();
        Task<List<UserCredentialsDto>> GetAllRegisteredUsers();
        void SaveUserRegistration(UserCredentialsDto userCredentials);
        Task<bool> ValidateUserCredentials(UserCredentialsDto userCredentials);
    }

}