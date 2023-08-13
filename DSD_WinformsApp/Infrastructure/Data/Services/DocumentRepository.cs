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
            var fileName = document.Filename; // Retain the original file name
            var filePath = Path.Combine(@"C:\Users\ricardo.piquero.jr\source\repos\DSD Solution v3.0\DSD_WinformsApp\Resources\UploadedFiles", fileName);

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
            existingDocument.Filename = updatedDocument.Filename;
            existingDocument.Category = updatedDocument.Category;
            existingDocument.Status = updatedDocument.Status;
            existingDocument.CreatedDate = updatedDocument.CreatedDate;
            existingDocument.CreatedBy = updatedDocument.CreatedBy;
            existingDocument.ModifiedBy = updatedDocument.ModifiedBy;
            existingDocument.ModifiedDate = updatedDocument.ModifiedDate;
            existingDocument.Notes = updatedDocument.Notes;

            // Remove the existing file from the server
            if (File.Exists(existingDocument.FilePath))
            {
                var backupFolderPath = Path.Combine(@"C:\Users\ricardo.piquero.jr\source\repos\DSD Solution v3.0\DSD_WinformsApp\Resources\BackupFiles");
                if (!Directory.Exists(backupFolderPath))
                {
                    Directory.CreateDirectory(backupFolderPath);
                }

                var backupFileName = Path.GetFileName(existingDocument.FilePath);
                var backupFilePath = Path.Combine(backupFolderPath, backupFileName);
                File.Move(existingDocument.FilePath, backupFilePath);

                // Create a new BackupFile record
                var backupFile = new BackUpFileModel
                {
                    Filename = backupFileName,
                    OriginalFilePath = existingDocument.FilePath,
                    BackupFilePath = backupFilePath,
                    BackupDate = DateTime.Now.Date,
                    Id = existingDocument.Id

                };

                // Add the new record to the BackupFiles DbSet
                _dbContext.BackupFiles.Add(backupFile);
            }

            if (updatedDocument.FileData != null)
            {
                //filename varible with guid

                var fileName = Path.GetFileName(updatedDocument.Filename);
                var filePath = Path.Combine(@"C:\Users\ricardo.piquero.jr\source\repos\DSD Solution v3.0\DSD_WinformsApp\Resources\UploadedFiles", fileName);
                // Save the updated file to the server
                File.WriteAllBytes(filePath, updatedDocument.FileData);

                existingDocument.FilePath = filePath;
            }


            // Save changes to the database
            _dbContext.SaveChanges();
            return true;
        }

    }
}
