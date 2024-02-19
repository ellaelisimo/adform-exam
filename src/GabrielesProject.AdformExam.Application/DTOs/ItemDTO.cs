namespace GabrielesProject.AdformExam.Application.DTOs;

public record ItemDTO
{
    public int Id { get; set; }

    public string? Name { get; set; }
}

public record NewItemDTO
{
    public string? Name { get; set; }
}
