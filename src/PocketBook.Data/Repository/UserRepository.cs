﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PocketBook.Data.Context;
using PocketBook.Domain.Entities;
using PocketBook.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PocketBook.Data.Repository
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(ApplicationDbContext context, ILogger logger) : base(context, logger) { }

        public override async Task<IEnumerable<User>> All()
        {
            try
            {
                return await dbSet.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} All function error", typeof(UserRepository));
                return new List<User>();
            }
        }

        public override async Task<bool> Upsert(User entity)
        {
            try
            {
                var existingUser = await dbSet.Where(x => x.Id == entity.Id).FirstOrDefaultAsync();

                if (existingUser == null)
                    return await Add(entity);

                existingUser.FirstName = entity.FirstName;
                existingUser.LastName = entity.LastName;
                existingUser.Email = entity.Email;

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} Upsert function error", typeof(UserRepository));
                return false;
            }
        }

        public override async Task<bool> Delete(Guid id)
        {
            try
            {
                var exist = await dbSet.Where(x => x.Id == id).FirstOrDefaultAsync();

                if (exist == null) return false;

                dbSet.Remove(exist);

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} Delete function error", typeof(UserRepository));
                return false;
            }
        }
    }
}
