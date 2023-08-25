using AutoMapper;
using DSD_WinformsApp.Core.DTOs;
using DSD_WinformsApp.Infrastructure.Data.Services;
using DSD_WinformsApp.Model;
using DSD_WinformsApp.View;
using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DSD_WinformsApp.Presenter
{
    public class DocumentPresenter : IDocumentPresenter
    {
        private readonly IDocumentView _mainDocumentView;
        private readonly IDocumentRepository _documentRepository;
        private readonly IBackUpFileRepository _backUpFileRepository;
        private readonly IUserRepository _userRepository;
        public DocumentPresenter(IDocumentView mainDocumentView, IDocumentRepository documentRepository, IBackUpFileRepository backUpFileRepository, IUserRepository userRepository)
        {
            _mainDocumentView = mainDocumentView;
            _documentRepository = documentRepository;
            _backUpFileRepository = backUpFileRepository;
            _userRepository = userRepository;

        }

        // DocumentRepository methods
        public async Task LoadDocuments()
        {
            List<DocumentDto> documents = await _documentRepository.GetAllDocuments();
            _mainDocumentView.BindDataMainView(documents);
        }

        public void SaveDocument(DocumentDto document, byte[] fileDataBytes)
        {
            // Set the file data to the DocumentDto
            document.FileData = fileDataBytes;
            _documentRepository.CreateDocument(document, fileDataBytes); // Pass fileDataBytes to the repository
        }

        public void EditDocument(DocumentDto document, byte[] fileDataBytes)
        {
            // Set the file data to the DocumentDto
            document.FileData = fileDataBytes;
            _documentRepository.EditDocument(document.Id, document); // Pass fileDataBytes to the repository
        }

        public async Task<bool> DeleteBackUpFile(BackUpFileDto file)
        {
            try
            {
                // Delete the document using the repository
                bool isDeleted = await _backUpFileRepository.DeleteBackupFiles(file.Id);

                if (isDeleted)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task SearchDocuments(string filterCriteria, string searchQuery)
        {
            List<DocumentDto> filteredDocuments = await _documentRepository.GetFilteredDocuments(searchQuery, filterCriteria);
            _mainDocumentView.BindDataMainView(filteredDocuments);
        }


        //BackUpFileRepository methods
        public async Task<List<BackUpFileDto>> GetRelatedBackupFiles(int documentId)
        {
            return await _backUpFileRepository.GetRelatedBackupFiles(documentId);
        }

        public async Task<bool> DeleteDocumentWithBackups(DocumentDto document)
        {
            try
            {
                // Delete the backup files associated with the document
                await _backUpFileRepository.DeleteBackupFiles(document.Id);

                // Delete the document using the repository
                bool isDeleted = await _documentRepository.DeleteDocument(document.Id);

                return isDeleted;
            }
            catch (Exception)
            {
                return false;
            }
        }


        // User Repository methods

        public async Task<List<UserCredentialsDto>> GetAllRegisteredUsers()
        {
            var users = await _userRepository.GetAllUsers();
            return users;
        }

        public async Task<List<UserCredentialsDto>> LoadUsers()
        {
            List<UserCredentialsDto> users = await _userRepository.GetAllUsers();
            return users;
        }

        public void SaveUserRegistration(UserCredentialsDto userCredentials)
        {
            // Set the user credentials to the UserCredentialsDto
            _userRepository.RegisterUser(userCredentials);
        }

        public async Task<bool> ValidateUserCredentials(UserCredentialsDto userCredentials)
        {
            try
            {
                var user = await _userRepository.GetUserByEmail(userCredentials.EmailAddress);

                if (user != null && user.Password == userCredentials.Password)
                {
                    return true; // Valid credentials
                }
                else
                {
                    return false; // Invalid credentials
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions and return false for invalid credentials
                // Log the exception or display an error message as needed
                return false;
            }
        }

    }
}
