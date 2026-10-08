using PortfolioApp.Models;

namespace PortfolioApp.ViewModels
{
    public class ProjectListViewModel
    {
        public List<Project> Projects { get; set; } = new();
        public List<string> AllTechnologies { get; set; } = new();
        public string? SelectedTechnology { get; set; }
        public string? SearchTerm { get; set; }

        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 6;
        public int TotalItems { get; set; }
        public int TotalPages => (int)Math.Ceiling(TotalItems / (double)PageSize);
    }
}
