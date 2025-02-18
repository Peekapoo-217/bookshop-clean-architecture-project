﻿using Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UseCases.Repositories;
using UseCases.TaskResults;

namespace UseCases
{
    public class AuthenticationManager
    {
        private readonly IUserRepository _userRepository;

        public AuthenticationManager(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<LoginResult> LoginAsync(string username, string password)
        {
            try
            {
                var storedUsers = await _userRepository.GetAllAsync();
                var foundUser = storedUsers.FirstOrDefault(_ => _.Username == username);

                if (foundUser == null)
                {
                    return new LoginResult(LoginResultCodes.UserNotFound, null, "User not found.");
                }

                var passwordHasher = new PasswordHasher<string>();
                var passwordVerificationResult = passwordHasher.VerifyHashedPassword(username, foundUser.Password, password);
                if (passwordVerificationResult == PasswordVerificationResult.Failed)
                {
                    return new LoginResult(LoginResultCodes.WrongPassword, null, "Wrong password!");
                }

                return new LoginResult(LoginResultCodes.Success, foundUser, "Success!");
            }
            catch (Exception ex)
            {
                return new LoginResult(LoginResultCodes.Error, null, ex.Message);
            }
        }

        public async Task<SignupResult> SignupAsync(User user)
        {
            try
            {
                var storedUsers = await _userRepository.GetAllAsync();
                var existingUser = storedUsers.FirstOrDefault(_ => _.Username == user.Username);

                if (existingUser != null)
                {
                    return SignupResult.UserExisted;
                }

                var passwordHasher = new PasswordHasher<string>();
                user.Password = passwordHasher.HashPassword(user.Username, user.Password);

                await _userRepository.AddAsync(user);

                return SignupResult.Success;
            }
            catch (Exception ex)
            {
                return new SignupResult(SignupResultCodes.Error, ex.Message);
            }
        }
    }
}
