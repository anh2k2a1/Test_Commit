namespace Domain.Models;

public record ReportBudgetResult
{
    public Dictionary<string, decimal> CurrentMonth { get; set; } = new();
    public Dictionary<string, decimal> PreviousMonth { get; set; } = new();
}
