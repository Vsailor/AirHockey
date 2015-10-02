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

    public void CreateLobby(GameObject inputField)
    {
        PhotonNetwork.CreateRoom(inputField.GetComponent<InputField>().text, new RoomOptions(), TypedLobby.Default);
    }

    public void JoinLobby()
    {
        PhotonNetwork.JoinRandomRoom();
    }

    public void SavePlayerName(GameObject inputField)
    {
        PhotonNetwork.playerName = inputField.GetComponent<InputField>().text;
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
