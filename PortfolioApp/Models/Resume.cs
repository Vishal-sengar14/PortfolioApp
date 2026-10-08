namespace PortfolioApp.Models
{
    /// <summary>Uploaded resume/CV file metadata, editable from the Admin Panel.</summary>
    public class Resume
    {
        public int Id { get; set; }
        public string FileName { get; set; } = string.Empty;
        public string FilePath { get; set; } = string.Empty;
        public DateTime UploadedOn { get; set; } = DateTime.UtcNow;
        public bool IsActive { get; set; } = true;
    }
}
