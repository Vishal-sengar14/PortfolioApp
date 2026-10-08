namespace PortfolioApp.ViewModels
{
    public class DashboardViewModel
    {
        public int TotalProjects { get; set; }
        public int TotalSkills { get; set; }
        public int TotalExperiences { get; set; }
        public int TotalEducations { get; set; }
        public int TotalMessages { get; set; }
        public int UnreadMessages { get; set; }
        public int TotalCertificates { get; set; }
        public int TotalTestimonials { get; set; }
        public List<(string Label, int Value)> ChartData { get; set; } = new();
    }
}
