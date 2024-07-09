using DOTNET_DAPPER.Models;

namespace DOTNET_DAPPER.Repository;

    public interface IFilmeRepository
    {
        Task<IEnumerable<FilmeResponse>> BuscaFilmesAsync();
        Task<FilmeResponse> BuscaFilmeAsync(int id);
        Task<bool> AdicionaAsync (FilmeRequest request);
        Task<bool> AtualizarAsync (FilmeRequest request, int id);
        Task<bool> DeletarAsync(int id);

    }
