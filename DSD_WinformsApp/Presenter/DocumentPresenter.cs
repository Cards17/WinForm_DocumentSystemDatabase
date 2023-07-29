using AutoMapper;
using DSD_WinformsApp.Core.DTOs;
using DSD_WinformsApp.Infrastructure.Data.Services;
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
        private readonly IDocumentView _view;
        private readonly IDocumentRepository _repository;
        public DocumentPresenter( IDocumentView view, IDocumentRepository repository )
        {
            _view = view;
            _repository = repository;

        }
        public async Task LoadDocuments()
        {
            List<DocumentDto> documents = await _repository.GetAllDocuments();
            _view.BindData(documents);

        }
    }
}
