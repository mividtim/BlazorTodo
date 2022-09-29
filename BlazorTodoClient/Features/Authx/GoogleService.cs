namespace BlazorTodoClient.Features.Authx;

public class GoogleService : IGoogleService
{
    public string ClientId { get; }

    public GoogleService(string clientId) => ClientId = clientId;
}