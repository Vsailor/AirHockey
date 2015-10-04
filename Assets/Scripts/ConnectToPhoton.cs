using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class ConnectToPhoton : Photon.MonoBehaviour
{
    // Use this for initialization
    void Start()
    {
        
    }
    public void Debug()
    {
        print(PhotonNetwork.room);
    }
    public void Connect()
    {
        PhotonNetwork.autoJoinLobby = true;
        PhotonNetwork.ConnectUsingSettings("0.1");
        PhotonNetwork.playerName = "Default";

    }
    public void SavePlayerName(string inputField)
    {
        PhotonNetwork.playerName = inputField;
    }
    public string GetMyPlayerName()
    {
        return PhotonNetwork.playerName;
    }
    public string GetOponentName()
    {
        if (PhotonNetwork.countOfPlayers == 2)
        {
            if (PhotonNetwork.playerList[0].name == PhotonNetwork.playerName)
            {
                return PhotonNetwork.playerList[1].name;
            }
            return PhotonNetwork.playerList[0].name;
        }
        return "";
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
    public bool Player1 = false;
    public bool Player2 = false;
    [PunRPC]
    void Chat(string NewMessage)
    {
        UILabel t = GameObject.Find("Chat").GetComponent<UILabel>();
        t.text = NewMessage;
    }
    [PunRPC]
    void ReadyLight(string playerName)
    {
        if (PhotonNetwork.isMasterClient)
        {
            Player1 = !Player1;
        }
        else
        {
            Player2 = !Player2;
        }
        if (Player1)
        {
            GameObject.Find("Ready1").GetComponent<UISprite>().spriteName = "Green";         
        }
        if (!Player1)
        {
            GameObject.Find("Ready1").GetComponent<UISprite>().spriteName = "Red";
        }
        if (Player2)
        {
            GameObject.Find("Ready2").GetComponent<UISprite>().spriteName = "Green";
        }
        if (!Player2)
        {
            GameObject.Find("Ready2").GetComponent<UISprite>().spriteName = "Red";
        }

    }
    public void ReadyClick(string name)
    {
        PhotonView photonView = PhotonView.Find(2);
        photonView.RPC("ReadyLight", PhotonTargets.All, name);
    }
    public void SendMessageInLobby()
    {
        GameObject inputField = GameObject.Find("InputField");
        UILabel t = GameObject.Find("Chat").GetComponent<UILabel>();
        if (getStringsCount(t.text) >= 6)
        {
            t.text = t.text.Remove(0, t.text.IndexOf('\n') + 1);
        }
        string toAdd = PhotonNetwork.playerName+ ": " + inputField.transform.FindChild("Label").GetComponent<UILabel>().text;
        if (toAdd.Length >= 26)
        {
            toAdd = toAdd.Remove(26);
        }

        while (toAdd.Contains("\n"))
        {
            toAdd = toAdd.Remove(toAdd.IndexOf('\n'), 1);
        }
        t.text += toAdd;
        t.text += "\n";

        inputField.transform.FindChild("Label").GetComponent<UILabel>().text = "";
        PhotonView photonView = PhotonView.Find(1);
        photonView.RPC("Chat", PhotonTargets.All, t.text);

    }

    public void CreateRoom(string roomName)
    {
        PhotonNetwork.JoinOrCreateRoom(roomName, new RoomOptions(), TypedLobby.Default);
    }
    // Update is called once per frame
    void Update()
    {
        if (PhotonNetwork.connectionState == ConnectionState.Disconnected)
        {
            Connect();
        }
        //if (PhotonNetwork.connectionState == ConnectionState.Disconnected)
        //{
        //    MessageBox.SetActive(true);
        //    MessageBox.transform.FindChild("Button").GetComponent<Button>().GetComponentInChildren<Text>().text = "Try again";
        //    MessageBox.transform.FindChild("MessageBoxText").GetComponent<Text>().text = "Failed to connect.\n Check your internet connection";

            //}
    }
}
