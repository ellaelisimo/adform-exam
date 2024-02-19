using GabrielesProject.AdformExam.Domain.Entities;

namespace GabrielesProject.AdformExam.Application.Interfaces;

public interface IExternalUserService
{
    public Task<ExternalUser> GetUserAsync(int id);
}
