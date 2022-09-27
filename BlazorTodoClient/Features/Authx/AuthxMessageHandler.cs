using System.Net.Http.Headers;

namespace BlazorTodoClient.Features.Authx;

public class AuthxMessageHandler : DelegatingHandler
{
    private string? _token;

    public void AddBearer(string token)
    {
        _token = token;
    }

    public void RemoveBearer()
    {
        _token = null;
    }

    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        request.Headers.Authorization = _token is null ? null : new AuthenticationHeaderValue("Bearer", _token);
        return base.SendAsync(request, cancellationToken);
    }
}