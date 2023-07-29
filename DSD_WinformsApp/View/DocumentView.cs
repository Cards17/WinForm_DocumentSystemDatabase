using DSD_WinformsApp.Core.DTOs;
using DSD_WinformsApp.Infrastructure.Data;
using DSD_WinformsApp.Infrastructure.Data.Services;
using DSD_WinformsApp.Presenter;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DSD_WinformsApp.View
{
    public partial class DocumentView : Form, IDocumentView
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly DocumentPresenter _presenter;
        public DocumentView(IUnitOfWork unitOfWork)
        {
            InitializeComponent();
            _unitOfWork = unitOfWork;
            _presenter = new DocumentPresenter(this, _unitOfWork.Documents);
            DocumentView_Load(null, null);
        }

        private async void DocumentView_Load(object? sender, EventArgs? e)
        {
            await _presenter.LoadDocuments();
        }


         public void BindData(List<DocumentDto> documents)
        {
            dataGridView1.DataSource = documents;
        }
    }
}
