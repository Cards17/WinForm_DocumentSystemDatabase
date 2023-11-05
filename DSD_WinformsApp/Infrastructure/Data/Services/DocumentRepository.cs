using AutoMapper;
using DSD_WinformsApp.Core.DTOs;
using DSD_WinformsApp.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Reflection.Metadata;

namespace DSD_WinformsApp.Infrastructure.Data.Services
{
    public class DocumentRepository : IDocumentRepository
    {
        private readonly IDocumentDbContext _dbContext;
        private readonly IMapper _mapper;

        public DocumentRepository(IDocumentDbContext dbContext, IMapper mapper)
        {

            _dbContext = dbContext;
            _mapper = mapper;

        }
        // Get all doucments method
        public async Task<List<DocumentDto>> GetAllDocuments()
        {
            var allDocuments = await _dbContext.Documents.ToListAsync();
            var documents = _mapper.Map<List<DocumentModel>, List<DocumentDto>>(allDocuments);
            return documents;
        }

        // Create a document
        public void CreateDocument(DocumentDto document, byte[] fileDataBytes)
        {
            var documentModel = _mapper.Map<DocumentDto, DocumentModel>(document);
            var fileName = document.Filename;
            var fileNameExtension = document.FilenameExtension;
            var filePath = Path.Combine(@"C:\Users\ricardo.piquero.jr\source\repos\DSD Solution\DSD_WinformsApp\Resources\UploadedFiles", fileName + "." + fileNameExtension);

            // Save the file to the server
            File.WriteAllBytes(filePath, fileDataBytes);

            documentModel.FilePath = filePath;

            _dbContext.Documents.Add(documentModel);
            _dbContext.SaveChanges();
        }

        // Delete a document
        public async Task<bool> DeleteDocument(int documentId)
        {
            var document = await _dbContext.Documents.FindAsync(documentId);
            if (document == null)
            {
                return false; // Document not found, cannot delete.
            }

            // Remove the associated file from the server
            if (File.Exists(document.FilePath))
            {
                File.Delete(document.FilePath);
            }


            // Delete associated backup files
            var backupFilesToDelete = await _dbContext.BackupFiles
                .Where(b => b.Id == documentId)
                .ToListAsync();

            foreach (var backupFile in backupFilesToDelete)
            {
                // Delete the backup file from the file system
                File.Delete(backupFile.BackupFilePath);
            }

            _dbContext.Documents.Remove(document);
            _dbContext.SaveChanges();
            return true;
        }

        // Edit a document
        public async Task<bool> EditDocument(int documentId, DocumentDto updatedDocument)
        {
            var existingDocument = await _dbContext.Documents.FindAsync(documentId);
            if (existingDocument == null)
            {
                return false; // Document not found, cannot edit.
            }

            // Update properties of the existing document with the data from the updatedDocument DTO
            existingDocument.DocumentVersion = updatedDocument.DocumentVersion;
            existingDocument.Filename = updatedDocument.Filename;
            existingDocument.Category = updatedDocument.Category;
            existingDocument.Status = updatedDocument.Status;
            existingDocument.CreatedDate = updatedDocument.CreatedDate;
            existingDocument.CreatedBy = updatedDocument.CreatedBy;
            existingDocument.ModifiedBy = updatedDocument.ModifiedBy;
            existingDocument.ModifiedDate = updatedDocument.ModifiedDate;
            existingDocument.Notes = updatedDocument.Notes;

            //Move the existing file to the backup folder
            if (File.Exists(existingDocument.FilePath))
            {
                // versioning
                //var currentVersion = _dbContext.BackupFiles.Where(x => x.Id == existingDocument.Id).Max(x => x.Version);

                var backupFolderPath = Path.Combine(@"C:\Users\ricardo.piquero.jr\source\repos\DSD Solution\DSD_WinformsApp\Resources\BackupFiles");
                if (!Directory.Exists(backupFolderPath))
                {
                    Directory.CreateDirectory(backupFolderPath);
                }

                var backupFileName = Path.GetFileName(existingDocument.FilePath);
                var backupFilePath = Path.Combine(backupFolderPath, backupFileName);
                File.Move(existingDocument.FilePath, backupFilePath);

                // Calculate the new version for the next backup
                var latestBackupVersion = _dbContext.BackupFiles
                    .Where(x => x.Id == existingDocument.Id)
                    .OrderByDescending(x => x.Version)
                    .FirstOrDefault();

                var newVersion = latestBackupVersion != null ? latestBackupVersion.Version + 1.0 : 0;

                // Create a new BackupFile record
                var newBackupFile = new BackUpFileModel
                {
                    DocumentVersion = existingDocument.DocumentVersion,
                    Filename = backupFileName,
                    OriginalFilePath = existingDocument.FilePath,
                    BackupFilePath = backupFilePath,
                    BackupDate = DateTime.Now.Date,
                    Id = existingDocument.Id,
                    Version = newVersion

                };

                // Add the new record to the BackupFiles DbSet
                _dbContext.BackupFiles.Add(newBackupFile);
            }

            if (updatedDocument.FileData != null)
            {
                var fileName = Path.GetFileName(updatedDocument.Filename);
                var filePath = Path.Combine(@"C:\Users\ricardo.piquero.jr\source\repos\DSD Solution\DSD_WinformsApp\Resources\UploadedFiles", fileName);
                // Save the updated file to the server
                File.WriteAllBytes(filePath, updatedDocument.FileData);

                existingDocument.FilePath = filePath;
            }


            // Save changes to the database
            _dbContext.SaveChanges();
            return true;
        }

        public async Task<List<DocumentDto>> GetFilteredDocuments(string searchQuery, string filterCriteria)
        {
            // add condition for search and filter was empty or null
            var allDocuments = await _dbContext.Documents
                .OrderByDescending(d => d.Id)
                .ToListAsync();
            var categoryFilter = GetFilterCategories(filterCriteria);
            var searchFilter = searchQuery ?? "";

            var filteredDocuments = allDocuments
                .Where(document =>
                    (string.IsNullOrEmpty(searchFilter) || document.Filename.Contains(searchFilter, StringComparison.OrdinalIgnoreCase)) &&
                    (string.IsNullOrEmpty(filterCriteria) || categoryFilter(document.Category)))
                .ToList();

            var documents = _mapper.Map<List<DocumentModel>, List<DocumentDto>>(filteredDocuments);
            return documents;
        }

        private Func<string, bool> GetFilterCategories(string filterCriteria)
        {
            switch (filterCriteria)
            {
                case "Board Resolutions":
                    return category => category == "Board Resolutions";
                case "Canteen Policies":
                    return category => category == "Canteen Policies";
                case "COOP Policies":
                    return category => category == "COOP Policies";
                case "COOP Article & By Laws":
                    return category => category == "COOP Article & By Laws";
                case "Minutes of the Meeting":
                    return category => category == "Minutes of the Meeting";
                case "Regulatory Requirements":
                    return category => category == "Regulatory Requirements";
                // Add more cases for other filter criteria
                case "All Categories":
                default:
                    return category => true; // Show all documents for "Select Category" and other cases
            }
        }



    }
}
