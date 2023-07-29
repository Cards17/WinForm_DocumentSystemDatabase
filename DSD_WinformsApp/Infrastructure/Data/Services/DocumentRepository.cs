using AutoMapper;
using DSD_WinformsApp.Core.DTOs;
using DSD_WinformsApp.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
