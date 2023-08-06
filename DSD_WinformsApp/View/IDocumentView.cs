using DSD_WinformsApp.Core.DTOs;
using DSD_WinformsApp.Infrastructure.Data;
using System.Reflection.Metadata;

namespace DSD_WinformsApp.View
{
    public interface IDocumentView
    {
        void BindDataMainView(List<DocumentDto> documents);
    }
}