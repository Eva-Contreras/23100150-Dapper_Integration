namespace Orders.Api.models.dtos
{
    public sealed class StudentInfo
    {
        public const string SectionName = "student";
        public string Name { get; set; } = "";
        public string Password { get; set; } = "";
    }
}