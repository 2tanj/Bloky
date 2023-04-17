using System;
using UnityEngine;
using Unity.Services.Core;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

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
        }
        catch (Exception e)
        {
            Debug.LogException(e);
        }
    }
}
