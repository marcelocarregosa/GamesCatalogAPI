using GamesCatalogAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GamesCatalogAPI.Repositories
{
    public interface IGameRepository : IDisposable
    {
        Task<List<Game>> Get(int page, int amount);
        Task<Game> Get(Guid id);
        Task<List<Game>> Get(string name, string publisher);
        Task Insert(Game game);
        Task Update(Game game);
        Task Delete(Guid id);
    }
}
