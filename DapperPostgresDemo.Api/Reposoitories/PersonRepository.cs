using Dapper;
using DapperPostgresDemo.Api.Models;
using Npgsql;

namespace DapperPostgresDemo.Api.Reposoitories;

public class PersonRepository : IPersonRepository
{
    private readonly string _connectionString = "";
    private readonly IConfiguration _config;

    public PersonRepository(IConfiguration config)
    {
        _config = config;
        _connectionString = _config.GetConnectionString("default");
    }
    public async Task<Person> CreatePersonAsync(Person person)
    {
        using var connection = new NpgsqlConnection(_connectionString);
        var createdId = await connection.ExecuteScalarAsync<int>("INSERT INTO person (name, email) VALUES (@name, @email);select lastval();", person);
        person.Id = createdId;
        return person;
    }

    public async Task DeletePersonAsync(int id)
    {
        using var connection = new NpgsqlConnection(_connectionString);
        await connection.ExecuteAsync("DELETE FROM person WHERE id = @id", new { id });
    }

    public async Task<IEnumerable<Person>> GetAllPersonAsync()
    {
        using var connection = new NpgsqlConnection(_connectionString);
        return await connection.QueryAsync<Person>("SELECT * FROM person");
    }

    public async Task<Person?> GetPersonByIdAsync(int id)
    {
        using var connection = new NpgsqlConnection(_connectionString);
        return await connection.QueryFirstOrDefaultAsync<Person>("SELECT * FROM person where id = @id", new { id });
    }

    public async Task<Person> UpdatePersonAsync(Person person)
    {
        using var connection = new NpgsqlConnection(_connectionString);
        await connection.ExecuteAsync("UPDATE person SET name = @name, email = @email WHERE id = @id", person);
        return person;
    }
}
