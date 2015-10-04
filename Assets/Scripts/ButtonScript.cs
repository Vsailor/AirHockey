using System;
using UnityEngine;
using System.Collections;
using System.Linq;
using UnityEngine.UI;

public class ButtonScript : MonoBehaviour
{
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
            Application.LoadLevel("MainMenuScene");
        }
        else if (name == "CreateRoomButton")
        {
            ConnectToPhoton camera = GameObject.Find("Camera").GetComponent<ConnectToPhoton>();
            camera.CreateRoom((GameObject.Find("RoomNameField").transform.FindChild("Label").GetComponent<UILabel>().text));
            camera.SavePlayerName(GameObject.Find("YourNameField").transform.FindChild("Label").GetComponent<UILabel>().text);
            Application.LoadLevel("LobbyScene");
        }
        else if (name == "SendButton")
        {
            GameObject.Find("Camera").GetComponent<ConnectToPhoton>().SendMessageInLobby();

        }
        else if (name == "ReadyPlayButton")
        {
            if (GameObject.Find("Camera").GetComponent<ConnectToPhoton>().GetMyPlayerName() == GameObject.Find("Player1").GetComponent<UILabel>().text)
            {
                if (GameObject.Find("Ready1").GetComponent<UISprite>().spriteName == "Green")
                {
                    GameObject.Find("Ready1").GetComponent<UISprite>().spriteName = "Red";
                }
                else
                {
                    GameObject.Find("Ready1").GetComponent<UISprite>().spriteName = "Green";
                }
            }
            else
            {
                if (GameObject.Find("Ready2").GetComponent<UISprite>().spriteName == "Green")
                {
                    GameObject.Find("Ready2").GetComponent<UISprite>().spriteName = "Red";
                }
                else
                {
                    GameObject.Find("Ready2").GetComponent<UISprite>().spriteName = "Green";
                }
            }
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
