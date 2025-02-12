namespace firstPetProject.Core.Domain.Entities
{
    public class Task
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public DateTime CreatingDate { get; set; }
        public DateTime EndingDate { get; set; }
    }
    public static class Status
    {
        public const string New = "New";
        public const string InProgress = "In Progress";
        public const string Completed = "Completed";
    }

    public static class Category
    {
        public const string Home = "Home";
        public const string Work = "Work";
        public const string Other = "Other";
    }
}
