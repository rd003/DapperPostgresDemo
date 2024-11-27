using DapperPostgresDemo.Api.Models;

namespace DapperPostgresDemo.Api.Reposoitories;

public interface IPersonRepository
{
    Task<Person> CreatePersonAsync(Person person);
    Task<Person> UpdatePersonAsync(Person person);
    Task DeletePersonAsync(int id);
    Task<IEnumerable<Person>> GetAllPersonAsync();
    Task<Person?> GetPersonByIdAsync(int id);
}
