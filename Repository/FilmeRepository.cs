
using System.Data.SqlClient;
using Dapper;
using DOTNET_DAPPER.Models;

namespace DOTNET_DAPPER.Repository;

public class FilmeRepository : IFilmeRepository
{
    private readonly IConfiguration _configuration;
    private readonly string connectioString;

    public FilmeRepository(IConfiguration configuration)
    {
        _configuration = configuration;
        connectioString = _configuration.GetConnectionString("SqlConnection");
    }

    public async Task<IEnumerable<FilmeResponse>> BuscaFilmesAsync()
    {
        string sql = @"SELECT
                        f.id Id,
                        f.nome Nome,
                        f.ano Ano,
                        p.nome Produtora
                    FROM tb_filme f
                    JOIN tb_produtora p ON  f.id_produtora = p.id";

        using var con = new SqlConnection(connectioString);
        return await con.QueryAsync<FilmeResponse>(sql);
    }

    public async Task<FilmeResponse> BuscaFilmeAsync(int id)
    {
        string sql = @"SELECT 
                        f.id Id,
                        f.nome Nome,
                        f.ano Ano,
                        p.nome Produtora
                    FROM tb_filme f
                    JOIN tb_produtora p ON  f.id_produtora = p.id
                    WHERE f.id = @Id";

        using var con = new SqlConnection(connectioString);
        return await con.QueryFirstOrDefaultAsync<FilmeResponse>(sql, new { Id = id });
    }

    public async Task<bool> AdicionaAsync(FilmeRequest request)
    {
        string sql = @"INSERT INTO  tb_filme(nome, ano, id_produtora)
                        VALUES (@Nome, @Ano, @ProdutoraId)";

        using var con = new SqlConnection(connectioString);
        return await con.ExecuteAsync(sql, request) > 0;

    }

    public Task<bool> AtualizarAsync(FilmeRequest request, int id)
    {
        throw new NotImplementedException();
    }
    public Task<bool> DeletarAsync(int id)
    {
        throw new NotImplementedException();
    }


}
