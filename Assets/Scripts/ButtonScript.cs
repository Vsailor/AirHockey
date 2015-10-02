using System;
using UnityEngine;
using System.Collections;
using System.Linq;
using UnityEngine.UI;

public class ButtonScript : Photon.MonoBehaviour
{
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

    public void CreateRoom(GameObject inputField)
    {
        PhotonNetwork.CreateRoom(inputField.GetComponent<InputField>().text, new RoomOptions(), TypedLobby.Default);
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

    public void SavePlayerName(GameObject inputField)
    {
        PhotonNetwork.playerName = inputField.GetComponent<InputField>().text;
    }

}
