using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreenButtons : MonoBehaviour
{
    [SerializeField] private GameObject _singleplayer11, _singleplayer4, _multiplayer11, _multiplayer4;

    public void onMultiplayerClick()
    {
        _singleplayer11.SetActive(_singleplayer11.activeSelf ? false : false);
        _singleplayer4.SetActive(_singleplayer4.activeSelf ? false : false);
        _multiplayer11.SetActive(_multiplayer11.activeSelf ? false : true);
        _multiplayer4.SetActive(_multiplayer4.activeSelf ? false : true);
    }

    public void onSingleplayerClick()
    {
        _singleplayer11.SetActive(_singleplayer11.activeSelf ? false : true);
        _singleplayer4.SetActive(_singleplayer4.activeSelf ? false : true);
        _multiplayer11.SetActive(_multiplayer11.activeSelf ? false : false);
        _multiplayer4.SetActive(_multiplayer4.activeSelf ? false : false);
    }

    public void onMultiplayer11Click()
    {
        SceneManager.LoadScene("Multiplayer_11");
    }

    public void onMultiplayer4Click()
    {
        SceneManager.LoadScene("Multiplayer_4");
    }

    public void onSingleplayer11Click()
    {
        SceneManager.LoadScene("SinglePlayer_11");
    }

    public void onSingleplayer4Click()
    {
        SceneManager.LoadScene("SinglePlayer_4");
    }
}
