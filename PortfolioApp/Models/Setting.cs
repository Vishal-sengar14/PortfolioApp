namespace PortfolioApp.Models
{
    /// <summary>Generic key/value site setting (site title, meta description, maintenance mode, etc.).</summary>
    public class Setting
    {
        public int Id { get; set; }
        public string Key { get; set; } = string.Empty;
        public string? Value { get; set; }
    }
}
