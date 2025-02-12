using AutoMapper;
using firstPetProject.Core.Auth.Interfaces;
using firstPetProject.Core.Domain;
using firstPetProject.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Task = System.Threading.Tasks.Task;

namespace firstPetProject.Core.Auth.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public AccountRepository(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task Add(User user)
        {
            var userEntity = _mapper.Map<User>(user);

            await _context.Users.AddAsync(userEntity);
            await _context.SaveChangesAsync();
        }

        public User GetByUsername(string username)
        {
            return _context.Users.FirstOrDefault(u => u.Username == username);
        }

        public async Task<User?> GetByEmail(string email)
        {
            var userEntity = await _context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Email == email);

            return userEntity != null ? _mapper.Map<User>(userEntity) : null;
        }

        public async Task<bool> EmailExists(string email)
        {
            return await _context.Users.AnyAsync(u => u.Email == email);
        }

        public async Task<User?> GetById(Guid id)
        {
            var userEntity = await _context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.UserID == id);

            return userEntity != null ? _mapper.Map<User>(userEntity) : null;
        }
    }
}
