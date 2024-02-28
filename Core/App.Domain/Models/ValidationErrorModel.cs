namespace App.Domain.Models;

public class ValidationErrorModel
{
    public string? FieldName { get; set; }
    public string? Message { get; set; }
}
