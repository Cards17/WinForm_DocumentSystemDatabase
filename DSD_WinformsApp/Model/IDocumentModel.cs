namespace DSD_WinformsApp.Model
{
    public interface IDocumentModel
    {
        string Category { get; set; }
        string CreatedBy { get; set; }
        DateTime CreatedDate { get; set; }
        string Filename { get; set; }
        int Id { get; set; }
        string ModifiedBy { get; set; }
        DateTime ModifiedDate { get; set; }
        string Notes { get; set; }
        string Status { get; set; }
    }
}