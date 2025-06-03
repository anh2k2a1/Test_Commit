namespace Application.CQRS.DTOs
{
    public record UserCourseProgressDto
    {
        public string CourseId { get; set; } = string.Empty;
        public string CourseTitle { get; set; } = string.Empty;
        public int TotalItems { get; set; }
        public int CompletedItems { get; set; }
        public decimal ProgressPercentage { get; set; }
        public List<CourseItemProgressDto> ItemsProgress { get; set; } = new();
    }

    public record CourseItemProgressDto
    {
        public int ItemId { get; set; }
        public string Title { get; set; } = string.Empty;
        public bool IsCompleted { get; set; }
    }
}