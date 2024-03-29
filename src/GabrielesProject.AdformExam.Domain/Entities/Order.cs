﻿namespace GabrielesProject.AdformExam.Domain.Entities;

public record Order
{
    public int Id { get; set; }

    public string? Status { get; set; }

    public int UserId { get; set; }

    public int ItemId { get; set; }

    public DateTime CreatedAt { get; set; }
}
