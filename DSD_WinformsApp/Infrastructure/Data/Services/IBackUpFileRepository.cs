using DSD_WinformsApp.Core.DTOs;

namespace DSD_WinformsApp.Infrastructure.Data.Services
{
    public interface IBackUpFileRepository
    {
        Task<List<BackUpFileDto>> GetRelatedBackupFiles(int documentId);
    }
}