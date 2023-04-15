using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Unity.Services.Core;
using Unity.Services.Authentication;
using Unity.Services.CloudSave;
using PlayFab;
using PlayFab.ClientModels;
public class StartScript : MonoBehaviour
{
    public async void Awake()
    {
        await UnityServices.InitializeAsync();
        await AuthenticationService.Instance.SignInAnonymouslyAsync();
    }
    public int gameScene;

    private void Start()
    {
        PlayFabSettings.TitleId = "1E9EE";
        var request = new LoginWithCustomIDRequest { CustomId = SystemInfo.deviceUniqueIdentifier, CreateAccount = true };
        PlayFabClientAPI.LoginWithCustomID(request, OnLoginSuccess, OnLoginFailure);
    }
    private void OnLoginSuccess(LoginResult result)
    {
        Debug.Log("ching chengs");
    }

    private void OnLoginFailure(PlayFabError error)
    {
        Debug.LogWarning("o noo");
        Debug.LogError("Here's some debug information:");
        Debug.LogError(error.GenerateErrorReport());
    }
    public void startGame()
    {
        SceneManager.LoadScene(gameScene);
    }
}
