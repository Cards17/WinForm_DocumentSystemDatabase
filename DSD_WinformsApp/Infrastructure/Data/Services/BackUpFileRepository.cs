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
    public class BackUpFileRepository : IBackUpFileRepository
    {
        private readonly IDocumentDbContext _dbContext;
        private readonly IMapper _mapper;
        public BackUpFileRepository(IDocumentDbContext documentDbContext, IMapper mapper)
        {

            _dbContext = documentDbContext;
            _mapper = mapper;

        }

        // Get related backup files based on documentId
        public async Task<List<BackUpFileDto>> GetRelatedBackupFiles(int documentId)
        {
            var relatedBackupFiles = await _dbContext.BackupFiles
                .Where(b => b.Id == documentId)
                .ToListAsync();

            var backUpFiles = _mapper.Map<List<BackUpFileModel>, List<BackUpFileDto>>(relatedBackupFiles);
            return backUpFiles;
        }

    }
}
