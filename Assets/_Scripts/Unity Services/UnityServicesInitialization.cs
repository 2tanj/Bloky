using System;
using UnityEngine;
using Unity.Services.Core;
using Unity.Services.Authentication;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class UnityServicesInitialization : MonoBehaviour
{
    void Update()
    {
        if (Touchscreen.current.primaryTouch.press.isPressed)
        {
            Debug.Log(UnityServices.State);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
    async void Awake()
    {
        try
        {
            await UnityServices.InitializeAsync();
            Debug.Log("Unity Services succesfully initialized.");

            SetupAuthEvents();

            await PlayerAuthentication.SignInAnonymouslyAsync();

            //await CloudSave.SaveDataAsync(
            //    "maps", 
            //    new List<object> { 
            //        new { Name = "first", HS = 10 },
            //        new { Name = "second", HS = 20 },
            //        new { Name = "third", HS = 30 }
            //    });
            await CloudSave.LoadPlayerData();
        }
        catch (Exception e)
        {
            Debug.LogException(e);
        }
    }

    private void SetupAuthEvents()
    {
        AuthenticationService.Instance.SignedIn += () => {
            Debug.Log("Player succesfully signed in.");
            Debug.Log($"PlayerID: {AuthenticationService.Instance.PlayerId}");
            Debug.Log($"AccessToken: {AuthenticationService.Instance.AccessToken}");
        };

        AuthenticationService.Instance.SignInFailed += (err) => {
            Debug.LogError(err);
        };

        AuthenticationService.Instance.SignedOut += () => {
            Debug.Log($"Player signed out.");
        };

        AuthenticationService.Instance.Expired += () => {
            Debug.Log("Player session could not be refreshed and expired.");
        };
    }
}