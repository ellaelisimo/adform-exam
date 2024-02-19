namespace GabrielesProject.AdformExam.Application.DTOs;

public record UserDTO
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Address { get; set; }
}
