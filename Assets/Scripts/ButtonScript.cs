using System;
using UnityEngine;
using System.Collections;
using System.Linq;
using UnityEngine.UI;

public class ButtonScript : MonoBehaviour
{
    private ConnectToPhoton PhotonCamera;
    void OnClick()
    {
        if (name == "CreateGameButton")
        {
            Application.LoadLevel("CreateGameMenuScene");
        }
        else if (name == "JoinGameButton")
        {
            Application.LoadLevel("EnterNameScene");
        }
        else if (name == "QuitGameButton")
        {
            Application.Quit();
        }
        else if (name == "BackToMainMenuButton")
        {
            PhotonCamera.ExitRoom();
            Application.LoadLevel("MainMenuScene");
        }
        else if (name == "CreateRoomButton")
        {
            PhotonCamera.CreateRoom((GameObject.Find("RoomNameField").transform.FindChild("Label").GetComponent<UILabel>().text));
            PhotonCamera.PlayerName = GameObject.Find("YourNameField").transform.FindChild("Label").GetComponent<UILabel>().text;
            Application.LoadLevel("LobbyScene");
        }
        else if (name == "SendButton")
        {
            GameObject.Find("Camera").GetComponent<ConnectToPhoton>().SendMessageInLobby();

        }
        else if (name == "ReadyPlayButton")
        {
            PhotonCamera.ReadyClick(PhotonCamera.PlayerName);
        }
        else if (name == "ConfirmNameButton")
        {
            PhotonCamera.PlayerName = GameObject.Find("InputField").transform.FindChild("Label").GetComponent<UILabel>().text;
            Application.LoadLevel("JoinGameScene");
        }
        else if (name == "BackToNameScene")
        {
            Application.LoadLevel("EnterNameScene");
        }
    }
    public void LoadScene(string sceneName)
    {
        Application.LoadLevel(sceneName);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ExitFromLobby()
    {
        if (PhotonNetwork.room != null)
        {
            PhotonNetwork.LeaveRoom();
        }
    }

    void Start()
    {
        PhotonCamera = GameObject.Find("Camera").GetComponent<ConnectToPhoton>();
    }


    public void JoinRoom(string name)
    {

        try
        {
            PhotonNetwork.JoinRoom(name);
        }
        catch
        {
            if (PhotonNetwork.room != null)
            {
                PhotonNetwork.LeaveRoom();
                LoadScene("JoinGameScene");
            }
        }

    }


}
