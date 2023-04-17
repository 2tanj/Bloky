using System.Threading.Tasks;
using UnityEngine;
using Unity.Services.Authentication;
using Unity.Services.Core;

public static class PlayerAuthentication
{
    // player info is stored locally
    // player loses progress on app uninstall
    public static async Task SignInAnonymouslyAsync()
    {
        try
        {
            await AuthenticationService.Instance.SignInAnonymouslyAsync();

            Debug.Log("Successful anonymouss sign in");
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
    
    public static async Task DeleteAccountAsync() => 
        await AuthenticationService.Instance.DeleteAccountAsync();
}
