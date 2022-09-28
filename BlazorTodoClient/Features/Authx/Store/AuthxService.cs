using BlazorTodoClient.Features.Authx.Store.Login;
using BlazorTodoClient.Features.Authx.Store.Logout;
using BlazorTodoClient.Features.Authx.Store.Register;
using BlazorTodoDtos.Authx;
using Fluxor;

namespace BlazorTodoClient.Features.Authx.Store;

public class AuthxService : IAuthxService
{
    private readonly ILogger<AuthxService> _logger;
    private readonly IDispatcher _dispatcher;

    public AuthxService(ILogger<AuthxService> logger, IDispatcher dispatcher) =>
        (_logger, _dispatcher) = (logger, dispatcher);

    public void Register(CreateUserDto dto)
    {
        _logger.LogInformation("Issuing action to register a new user");
        _dispatcher.Dispatch(new RegisterAction(dto));
    }

    public void Login(CreateAuthxTokensDto dto)
    {
        _logger.LogInformation("Issuing action to login");
        _dispatcher.Dispatch(new LoginAction(dto));
    }

    public void Logout()
    {
        _logger.LogInformation("Issuing action to logout");
        _dispatcher.Dispatch(new LogoutAction());
    }
}