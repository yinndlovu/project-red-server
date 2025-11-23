using Microsoft.EntityFrameworkCore;
using ProjectRed.Core.Entities;
using ProjectRed.Core.Interfaces.Repositories;
using ProjectRed.Infrastructure.Data;

namespace ProjectRed.Infrastructure.Repositories
{
    public class UserRepository(AppDbContext dbContext) : IUserRepository
    {
        private readonly AppDbContext _dbContext = dbContext;

        public async Task AddAsync(User user)
        {
            await _dbContext.Users.AddAsync(user);
        }

        public async Task<User?> FindByEmail(string email)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == email);
            return user;
        }

        public async Task<User?> FindById(int id)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == id);
            return user;
        }

        public async Task<bool> UserEmailExists(string email)
        {
            var exists = await _dbContext.Users.AnyAsync(u => u.Email == email);
            return exists;
        }

        public async Task<bool> SaveChangesAsync()
        {
            try
            {
                var rows = await _dbContext.SaveChangesAsync();
                return rows > 0;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
    }
}
