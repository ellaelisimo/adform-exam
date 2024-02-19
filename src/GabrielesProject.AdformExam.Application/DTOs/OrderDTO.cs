namespace GabrielesProject.AdformExam.Application.DTOs;

public record OrderDTO
{
    public int Id { get; set; }

    public string? Status { get; set; }

    public int UserId { get; set; }

    public int ItemId { get; set; }

    public DateTime CreatedAt { get; set; }
}

public record NewOrderDTO
{
    public int UserId { get; set; }

    public int ItemId { get; set; }
}
