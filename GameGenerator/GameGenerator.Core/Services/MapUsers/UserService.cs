using GameGenerator.Core.Abstractions.Repositories.MapUsers;
using GameGenerator.Core.Abstractions.Services.MapUsers;
using GameGenerator.Core.Models.MapUsers;
using GameGenerator.Core.Models.OnGoingGame;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GameGenerator.Core.Services.MapUsers
{
    internal class UserService : IUserService
    {
        private readonly IUserRepository _repository;

        public UserService(IUserRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }
        public async Task<List<UserEntry>> GetAllAsync()
        {
            List<UserEntry> userEntries = await _repository.GetAllAsync();
            return userEntries;
        }

        public async Task<List<UserEntry>> GetAllByGroupAsync(string groupName)
        {
            List<UserEntry> userEntries = await _repository.GetAllByGroupAsync(groupName);
            return userEntries;
        }

        public async Task<UserEntry> GetByIdAsync(string userName)
        {
            UserEntry userEntries = await _repository.GetByIdAsync(userName);
            return userEntries;
        }

        public async Task<int> CreateAsync(UserEntry userEntry)
        {
            if (userEntry is null)
            {
                throw new ArgumentException(nameof(userEntry));
            }
            int affectedRows = await _repository.CreateAsync(userEntry);
            return affectedRows;
        }

        public async Task<int> DeleteAsync(string userName)
        {
            int affectedRows = await _repository.DeleteAsync(userName);
            return affectedRows;
        }

       
    }
}
