using firstPetProject.Core.Auth.Interfaces;
using firstPetProject.Core.Auth.Repositories;
using firstPetProject.Core.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Task = System.Threading.Tasks.Task;

namespace firstPetProject.Core.Auth.BusinessLogic
{
    public class AccountService
    {
        private readonly IAccountRepository accountRepository;
        private readonly JwtService jwtService;
        public AccountService(IAccountRepository accountRepository, JwtService jwtService)
        {
            this.accountRepository = accountRepository;
            this.jwtService = jwtService;
        }
        public async Task Register(string Username, string Password, string Email)
        {
            if (await accountRepository.EmailExists(Email))
            {
                throw new Exception("Данный пользователь уже существует!");
            }
            var user = new User
            {
                Username = Username,
                Email = Email,
                UserID = Guid.NewGuid(),
            };
            var passHash = new PasswordHasher<User>().HashPassword(user, Password);
            user.Password = passHash;
            await accountRepository.Add(user);
        }

        public async Task<string> Login(string Email, string Password)
        {
            var user = await accountRepository.GetByEmail(Email)
                       ?? throw new Exception("Пользователь не найден!");

            var result = new PasswordHasher<User>().
                VerifyHashedPassword(user, user.Password, Password);

            if (result == PasswordVerificationResult.Success)
            {
                return jwtService.GenerateToken(user);
            }
            else
            {
                throw new Exception("Неверный адрес электронной почты или пароль!");
            }


        }
    }
}
