using GabrielesProject.AdformExam.Application.Interfaces;
using GabrielesProject.AdformExam.Domain.Entities;
using GabrielesProject.AdformExam.Domain.Exceptions;

namespace GabrielesProject.AdformExam.Application.Services
{
    public class ExternalUserService : IExternalUserService
    {
        private readonly HttpClient _httpClient;

        public ExternalUserService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ExternalUser> GetUserAsync(int id)
        {
            var response = await _httpClient.GetAsync($"https://jsonplaceholder.typicode.com/users/{id}");
            var user = await response.Content.ReadAsAsync<ExternalUser>();

            if(response.Content is null)
            {
                throw new NotFoundException($"User with {id} doesn't exist");
            }
            return user;
        }
    }
}
