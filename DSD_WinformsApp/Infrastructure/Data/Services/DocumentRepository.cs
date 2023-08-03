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
            
            // Generate a unique file name (e.g., using Guid) to avoid conflicts
            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(document.Filename);
            var filePath = Path.Combine(@"C:\Users\ricardo.piquero.jr\source\repos\Document Management Database Solution v.20\DSD_WinformsApp\Resources\UploadedFiles", fileName);

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
    }
}
