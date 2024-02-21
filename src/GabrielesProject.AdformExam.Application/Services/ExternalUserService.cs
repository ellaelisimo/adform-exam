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

            if(!response.IsSuccessStatusCode)
            {
                throw new NotFoundException($"User with {id} doesn't exist");
            }
            var user = await response.Content.ReadAsAsync<ExternalUser>();
            return user;
        }
    }
}
