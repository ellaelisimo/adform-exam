namespace GabrielesProject.AdformExam.Application.DTOs;

public class OrderDTO
{
    public int Id { get; set; }

    public string? Status { get; set; }

    public int UserId { get; set; }

    public int ItemId { get; set; }

    public DateTime CreatedAt { get; set; }
}
