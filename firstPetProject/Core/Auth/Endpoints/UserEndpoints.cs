
using firstPetProject.Core.Auth.BusinessLogic;
using firstPetProject.Core.Auth.Models;
using Microsoft.Win32;

namespace firstPetProject.Core.Auth.Endpoints
{
    public static class UserEndpoints
    {
        public static IEndpointRouteBuilder MapUsersEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapPost("register", Register);
            app.MapPost("login", Login);
            return app;
        }

        private static async Task<IResult> Register(
            RegisterUserRequest request,
            AccountService accountService)
        {
            try
            {
                await accountService.Register(request.Username, request.Email, request.Password);
                return Results.Ok(new { Message = "Регистрация успешно выполнена!" });
            }
            catch (Exception ex)
            {
                return Results.BadRequest(new { Error = ex.Message });
            }
        }

        private static async Task<IResult> Login(
            LoginUserRequest request,
            AccountService accountService,
            HttpContext context)
        {
            try
            {
                var token = await accountService.Login(request.Email, request.Password);

                var response = new { Token = token };
                return Results.Json(response);
            }
            catch (Exception ex)
            {
                return Results.Problem(detail: ex.Message, statusCode: StatusCodes.Status401Unauthorized);
            }
        }
    }
}
