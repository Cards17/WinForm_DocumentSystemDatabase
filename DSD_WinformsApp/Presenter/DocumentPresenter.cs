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

        #region Document Page Pagination Fields
        private List<DocumentDto> allDocuments = null!;
        private int currentPage = 1;
        private int itemsPerPage = 10;

        // Initial values for search query and category filter
        private string currentSearchQuery = "";
        private string currentFilterCategory = "";
        private List<DocumentDto> filteredDocuments = new List<DocumentDto>();
        #endregion

        #region Manage Users Page Pagination Fields
        private List<UserCredentialsDto> allUsers = null!;
        private int currentUsersPage = 1;
        private int itemsUsersPerPage = 10;

        private string currentUsersSearchQuery = ""; // Initial value for search query
        private string currentUsersJobFilter = "";
        private List<UserCredentialsDto> filteredUsers = new List<UserCredentialsDto>();

        #endregion

        public DocumentPresenter(IDocumentView mainDocumentView, IDocumentRepository documentRepository, IBackUpFileRepository backUpFileRepository, IUserRepository userRepository)
        {
            _mainDocumentView = mainDocumentView;
            _documentRepository = documentRepository;
            _backUpFileRepository = backUpFileRepository;
            _userRepository = userRepository;

        }

        #region DocumentRepository methods

        // DocumentRepository methods
        public async Task LoadDocuments()
        {
            List<DocumentDto> documents = await _documentRepository.GetAllDocuments();
            _mainDocumentView.BindDataMainView(documents);
        }
        public async Task LoadDocumentsByFilter(string currentSearchQuery, string currentFilterCategory)
        {
            // i wan to capture values from 
            filteredDocuments = await _documentRepository.GetFilteredDocuments(currentSearchQuery, currentFilterCategory);
            _mainDocumentView.BindDataMainView(filteredDocuments);
        }

        public async Task<bool> CheckForDuplicateFileName(string fileName)
        {
            // Load documents from the repository (replace with your actual repository method)
            List<DocumentDto> allDocuments = await _documentRepository.GetAllDocuments();

            // Check for duplicate file names
            return allDocuments.Any(d => d.Filename == fileName);
        }

        #region Document Page Pagination Methods
        public void AddNewDocument(DocumentDto newDocument)
        {
            filteredDocuments.Add(newDocument);
            SetCurrentPageData();
        }

        private void SetCurrentPageData()
        {
            // Ensure currentPage is within valid bounds
            currentPage = Math.Max(1, Math.Min(currentPage, TotalPages()));

            int startIndex = (currentPage - 1) * itemsPerPage;
            int endIndex = Math.Min(startIndex + itemsPerPage, filteredDocuments.Count);

            var currentPageDocuments = filteredDocuments.Skip(startIndex).Take(endIndex - startIndex).ToList();
            _mainDocumentView.BindDataMainView(currentPageDocuments);

            // Update the label after changing the current page
            _mainDocumentView.UpdatePageLabel(currentPage, TotalPages());
        }

        private int TotalPages()
        {
            return (int)Math.Ceiling((double)filteredDocuments.Count / itemsPerPage);
        }

        public void NextPage()
        {

            if (currentPage * itemsPerPage < filteredDocuments.Count)
            {
                currentPage++;
                SetCurrentPageData();
            }

        }

        public void PreviousPage()
        {
            if (currentPage > 1)
            {
                currentPage--;
                SetCurrentPageData();
            }
        }

        public async Task ApplyFilters()
        {
            currentSearchQuery = _mainDocumentView.GetSearchQuery().Trim()?? string.Empty;
            currentFilterCategory = _mainDocumentView.GetFilterCategory() ?? string.Empty;

            currentPage = 1; // Reset the page when applying filters

            // Get the filtered documents
            filteredDocuments = await _documentRepository.GetFilteredDocuments(currentSearchQuery, currentFilterCategory);
            SetCurrentPageData();

        }
        #endregion

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

        #endregion

        #region User Repository methods

        #region Manage Users Page Pagination Methods
        public async Task ApplyUsersPageFilters()
        {
            currentUsersSearchQuery = _mainDocumentView.GetSearchUserQuery().Trim() ?? string.Empty;
            currentUsersJobFilter = _mainDocumentView.GetFilterUsersCategory() ?? string.Empty;

            currentPage = 1; // Reset the page when applying filters

            // Get filtered users
            filteredUsers = await _userRepository.GetFilteredUsers(currentUsersSearchQuery, currentUsersJobFilter);
            SetCurrentUsersPageData();


        }
                                            
        public void NextUsersPage()
        {

            if (currentUsersPage * itemsUsersPerPage < filteredUsers.Count)
            {
                currentUsersPage++;
                SetCurrentUsersPageData();
            }
        }

        public void BackUsersPage()
        {
            if (currentUsersPage > 1)
            {
                currentUsersPage--;
                SetCurrentUsersPageData();
            }
        }

        private void SetCurrentUsersPageData()
        {
            // Ensure currentPage is within valid bounds
            currentUsersPage = Math.Max(1, Math.Min(currentUsersPage, UsersTotalPages()));

            int startIndex = (currentUsersPage - 1) * itemsUsersPerPage;
            int endIndex = Math.Min(startIndex + itemsUsersPerPage, filteredUsers.Count);

            var currentPageUsers = filteredUsers.Skip(startIndex).Take(endIndex - startIndex).ToList();
            _mainDocumentView.BindDataManageUsers(currentPageUsers);

            // Update the label after changing the current page
            _mainDocumentView.UpdateUsersPageLabel(currentUsersPage, UsersTotalPages());

        }

        private int UsersTotalPages()
        {
            return (int)Math.Ceiling((double)filteredUsers.Count / itemsUsersPerPage);
        }


        #endregion

        // User Repository methods
        public async Task<List<UserCredentialsDto>> GetAllRegisteredUsers()
        {
            var users = await _userRepository.GetAllUsers();
            return users;
        }

        public async Task LoadUsers()
        {
            List<UserCredentialsDto> users = await _userRepository.GetAllUsers();
            _mainDocumentView.BindDataManageUsers(users);
        }

        public void SaveUserRegistration(UserCredentialsDto userCredentials)
        {
            // Set the user credentials to the UserCredentialsDto
            _userRepository.RegisterUser(userCredentials);
        }
        
        public void EditUser(UserCredentialsDto user)
        {
            _userRepository.EditUser(user.UserId, user);
        }

        public async Task<bool> ValidateUserCredentials(UserCredentialsDto userCredentials)
        {
            try
            {
                var user = await _userRepository.GetUserByEmail(userCredentials.EmailAddress);

                if (user != null && user.Password == userCredentials.Password)
                {
                    // Valid credentials, store the user's role
                    string userRole = _userRepository.GetUserRole(user.EmailAddress);

                    if (userRole == "Admin")
                    {
                        _mainDocumentView.ToggleManageUsersButtonVisibility(true);
                        _mainDocumentView.SetUsernameLabel($"{user.Firstname} {user.Lastname}");
                    }
                    else
                    {
                        _mainDocumentView.ToggleManageUsersButtonVisibility(false);
                        _mainDocumentView.SetUsernameLabel($"{user.Firstname} {user.Lastname}");
                    }

                    _mainDocumentView.ShowDocumentView();


                    // Return both the result and the username
                    return true;
                }
                else
                {
                    // Return both the result and an empty username
                    return false;
                }
            }
            catch (Exception )
            {
                return false;
            }
        }

        public async Task LoadUsersByFilter(string currentUsersSearchQuery, string currentUsersJobFilter)
        {
            allUsers = await _userRepository.GetFilteredUsers(currentUsersSearchQuery, currentUsersJobFilter);
            _mainDocumentView.BindDataManageUsers(allUsers);
        }

        public async Task<bool> DeleteUser(UserCredentialsDto user)
        {
            try
            {
                // Delete the user using the repository
                bool isDeleted = await _userRepository.DeleteUser(user.UserId);

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
                return false; // User not found, cannot delete.
            }
        }

        #endregion

    }
}
