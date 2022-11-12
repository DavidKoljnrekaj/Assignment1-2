using Microsoft.Extensions.DependencyInjection;
using System.Security.Claims;
namespace Shared.Auth;

public class AuthorizationPolicies
{
    public static void AddPolicies(IServiceCollection services)
    {
        services.AddAuthorizationCore(options =>
        {
            options.AddPolicy("MustBeAdmin", a =>
                a.RequireAuthenticatedUser().RequireClaim("Role", "Admin"));
            options.AddPolicy("MustBeRegistered", a =>
                a.RequireAuthenticatedUser().RequireClaim("Role", "RegisteredUser"));
        });
    }
}