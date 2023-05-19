using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;

public class PlayFabLogin : MonoBehaviour
{
    [SerializeField] private string username;

    void Start()
    {
        if (string.IsNullOrEmpty(PlayFabSettings.TitleId))
        {
            PlayFabSettings.TitleId = "C0B09";
        }


    }

    private bool IsValidUsername()
    {
        bool isValid = false;
        if (username.Length >= 3 && username.Length <= 24)
            isValid = true;

        return isValid;        
    }

    private void LoginWithCustomId()
    {
        Debug.Log($"Login to Playfab ass {username}");
        var request = new LoginWithCustomIDRequest {CustomId = username, CreateAccount = true};

        PlayFabClientAPI.LoginWithCustomID(request, OnLoginCustomSuccess, OnFailure);
    }

    private void OnLoginCustomSuccess(LoginResult result)
    {

    }
    private void OnFailure(PlayFabError error)
    {

    }

    public void SetUserName(string name)
    {
        username = name;
        PlayerPrefs.SetString("USERNAME", username);
    }

    public void Login()
    {
        if (!IsValidUsername()) return;

        LoginWithCustomId();

    }

}
