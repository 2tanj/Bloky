using System.Threading.Tasks;
using UnityEngine;
using Unity.Services.Authentication;
using Unity.Services.Core;
using System;

public static class PlayerAuthentication
{
    private static readonly IAuthenticationService _service = 
        AuthenticationService.Instance;

    // player info is stored locally
    // player loses progress on app uninstall
    public static async Task SignInAnonymouslyAsync()
    {
        try
        {
            await _service.SignInAnonymouslyAsync();
            Debug.Log("Successful anonymous sign in");
        }
        catch (AuthenticationException e)
        {
            Debug.LogException(e);
        }
        catch (RequestFailedException e)
        {
            Debug.LogException(e);
        }
    }

    public static async Task SignOutAsync()
    {
        try
        {
            _service.SignOut();
            await Task.CompletedTask;
        }
        catch (Exception e)
        {
            Debug.LogException(e);
        }
    }
    
    public static async Task DeleteAccountAsync() => 
        await _service.DeleteAccountAsync();

    public static string GetPlayerId() => _service.PlayerId;
}
