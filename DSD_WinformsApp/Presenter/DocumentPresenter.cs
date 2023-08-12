using AutoMapper;
using DSD_WinformsApp.Core.DTOs;
using DSD_WinformsApp.Infrastructure.Data.Services;
using DSD_WinformsApp.Model;
using DSD_WinformsApp.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace DSD_WinformsApp.Presenter
{
    public class DocumentPresenter
    {
        private readonly IDocumentView _mainDocumentView;
        private readonly IDocumentRepository _documentRepository;
        private readonly IBackUpFileRepository _backUpFileRepository;
        public DocumentPresenter( IDocumentView mainDocumentView, IDocumentRepository documentRepository, IBackUpFileRepository backUpFileRepository)
        {
            _mainDocumentView = mainDocumentView;
            _documentRepository = documentRepository;
            _backUpFileRepository = backUpFileRepository;

        }
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

        public async Task<bool> DeleteDocument(DocumentDto document)
        {
            try
            {
                // Delete the document using the repository
                bool isDeleted = await _documentRepository.DeleteDocument(document.Id);

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

        public async Task<List<BackUpFileDto>> GetRelatedBackupFiles(int documentId)
        {
            return await _backUpFileRepository.GetRelatedBackupFiles(documentId);
        }


    }
}
