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
        toAdd += "\n";
        t.text += toAdd;
        
        inputField.GetComponent<InputField>().text = "";
        PhotonView photonView = PhotonView.Find(1);
        photonView.RPC("Chat", PhotonTargets.All, t.text);
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
