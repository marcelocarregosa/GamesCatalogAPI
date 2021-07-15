using GamesCatalogAPI.Entities;
using GamesCatalogAPI.Exceptions;
using GamesCatalogAPI.InputModel;
using GamesCatalogAPI.Repositories;
using GamesCatalogAPI.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GamesCatalogAPI.Services
{
    public class GameService : IGameService
    {
        private readonly IGameRepository _gameRepository;

        public GameService(IGameRepository gameRepository)
        {
            _gameRepository = gameRepository;
        }

        public async Task<List<GameViewModel>> Get(int page, int amount)
        {
            var games = await _gameRepository.Get(page, amount);

            return games.Select(game => new GameViewModel
            {
                Id = game.Id,
                Name = game.Name,
                Publisher = game.Publisher,
                Price = game.Price
            })
                               .ToList();
        }

        public async Task<GameViewModel> Get(Guid id)
        {
            var game = await _gameRepository.Get(id);

            if (game == null)
                return null;

            return new GameViewModel
            {
                Id = game.Id,
                Name = game.Name,
                Publisher = game.Publisher,
                Price = game.Price
            };
        }

        public async Task<GameViewModel> Insert(GameInputModel game)
        {
            var gameEntity = await _gameRepository.Get(game.Name, game.Publisher);

            if (gameEntity.Count > 0)
                throw new GameAlreadyRegisteredException();

            var jogoInsert = new Game
            {
                Id = Guid.NewGuid(),
                Name = game.Name,
                Publisher = game.Publisher,
                Price = game.Price
            };

            await _gameRepository.Insert(jogoInsert);

            return new GameViewModel
            {
                Id = jogoInsert.Id,
                Name = game.Name,
                Publisher = game.Publisher,
                Price = game.Price
            };
        }

        public async Task Update(Guid id, GameInputModel game)
        {
            var gameEntity = await _gameRepository.Get(id);

            if (gameEntity == null)
                throw new GameNotRegisteredException();

            gameEntity.Name = game.Name;
            gameEntity.Publisher = game.Publisher;
            gameEntity.Price = game.Price;

            await _gameRepository.Update(gameEntity);
        }

        public async Task Update(Guid id, double preco)
        {
            var gameEntity = await _gameRepository.Get(id);

            if (gameEntity == null)
                throw new GameNotRegisteredException();

            gameEntity.Price = preco;

            await _gameRepository.Update(gameEntity);
        }

        public async Task Delete(Guid id)
        {
            var game = await _gameRepository.Get(id);

            if (game == null)
                throw new GameNotRegisteredException();

            await _gameRepository.Delete(id);
        }

        public void Dispose()
        {
            _gameRepository?.Dispose();
        }
    }
}
