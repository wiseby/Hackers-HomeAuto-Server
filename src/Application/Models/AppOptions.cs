namespace Application.Models
{
    public class AppOptions
    {
        public const string Position = "AppOptions";

        public string ConnectionString { get; set; }
        public string Database { get; set; }
    }
}