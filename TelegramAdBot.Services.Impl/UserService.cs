﻿using System.Threading.Tasks;
using Telegram.Bot.Types;
using TelegramAdBot.DataAccess;
using TelegramAdBot.Entities;
using TelegramAdBot.Entities.Enums;
using TelegramAdBot.Services.Impl.Mappers;

namespace TelegramAdBot.Services.Impl
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<AppUser> CreateUser(User user, UserRole role)
        {
            return await _userRepository.AddAsync(user.ToEntity(role));
        }

        public async Task<AppUser> GetUserByTelegramIdAsync(int telegramId)
        {
            return await _userRepository.GetUserByTelegramIdAsync(telegramId);
        }
    }
}