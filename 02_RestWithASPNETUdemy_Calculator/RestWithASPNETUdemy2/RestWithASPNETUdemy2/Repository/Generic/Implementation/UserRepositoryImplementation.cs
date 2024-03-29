﻿using APIAspNetCore5.Model.Context;
using RestWithASPNETUdemy.Data.VO;
using RestWithASPNETUdemy.Model;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace RestWithASPNETUdemy.Repository.Generic.Implementation
{
    public class UserRepositoryImplementation : IUserRepository
    {
        private readonly PostgreSQLContext _context;

        public UserRepositoryImplementation(PostgreSQLContext context)
        {
            _context = context;
        }

        public User ValidateCredentials(UserVO user)
        {
            var pass = ComputeHash(user.Password, new SHA256CryptoServiceProvider());
            return _context.Users.FirstOrDefault(u => u.UserName == user.UserName && u.Password == pass);
        }
        public User ValidateCredentials(string userName)
        {
            return _context.Users.FirstOrDefault(u => u.UserName == userName);
        }

        public bool RevokeToken(string userName)
        {
            var user = _context.Users.FirstOrDefault(u => u.UserName == userName);
            if (user != null) return false;
            user.RefreshToken = null;
            _context.SaveChanges();
            return true;
        }

        public User RefreshUserInfo(User user)
        {
            if (!_context.Users.Any(b => b.Id == user.Id)) return null;

            var result = _context.Users.SingleOrDefault(b => b.Id == user.Id);
            if (result != null)
            {
                try
                {
                    _context.Entry(result).CurrentValues.SetValues(user);
                    _context.SaveChanges();
                    return result;
                }
                catch (Exception)
                {
                    throw;
                }
            }
            else
            {
                return result;
            }
        }

        private string ComputeHash(string input, SHA256CryptoServiceProvider algorithm)
        {
            byte[] inputBytes = Encoding.UTF8.GetBytes(input);
            byte[] hashedBytes = algorithm.ComputeHash(inputBytes);
            return BitConverter.ToString(hashedBytes);
        }
    }
}
