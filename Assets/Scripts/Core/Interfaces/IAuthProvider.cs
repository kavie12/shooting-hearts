// Provides access token for secure endpoints requessts from server.
public interface IAuthProvider
{
    string AccessToken { get; }
}