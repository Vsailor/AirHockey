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
    int getStringsCount(string s)
    {
        int count = 0;
        for (int i = 0; i < s.Length; i++)
        {
            if (s[i] == '\n')
            {
                count++;
            }
        }
        return count;
    }
    [PunRPC]
    void Chat(string NewMessage)
    {
        Text t = GameObject.Find("Chat").GetComponent<Text>();
        t.text = NewMessage;
    }
    public void SendMessageInLobby(GameObject inputField)
    {
        Text t = GameObject.Find("Chat").GetComponent<Text>();
        if (getStringsCount(t.text) >= 7)
        {
            t.text = t.text.Remove(0, t.text.IndexOf('\n') + 1);
        }
        string toAdd = PhotonNetwork.playerName + " [" + DateTime.Now.ToLongTimeString() + "]: " + inputField.GetComponent<InputField>().text;
        if (toAdd.Length >= 59)
        {
            toAdd = toAdd.Remove(59);
        }

        while (toAdd.Contains("\n"))
        {
            toAdd = toAdd.Remove(toAdd.IndexOf('\n'), 1);
        }
        t.text += toAdd;
        t.text += "\n";

        inputField.GetComponent<InputField>().text = "";
        PhotonView photonView = PhotonView.Find(1);
        photonView.RPC("Chat", PhotonTargets.All, t.text);
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
