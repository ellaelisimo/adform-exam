namespace GabrielesProject.AdformExam.Domain.Entities;

public record Order
{
    public int Id { get; set; }

    public string? Status { get; set; }

    public int UserId { get; set; }
}
