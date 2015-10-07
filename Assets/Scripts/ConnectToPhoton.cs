using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class ConnectToPhoton : Photon.MonoBehaviour
{
    const int MAX_CHAT_STRINGS_COUNT = 6;
    const int MAX_CHAT_STRING_LENGTH = 40;
    public UILabel ChatText;
    GameObject inputField;
    int maxChatInputString;
    // Use this for initialization
    void Start()
    {
        if (Application.loadedLevelName == "LobbyScene")
        {
            ChatText = GameObject.Find("Chat").GetComponent<UILabel>();
            inputField = GameObject.Find("InputField");
            maxChatInputString = MAX_CHAT_STRING_LENGTH - (PlayerName + ": ").Length;
        }
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
    public string PlayerName
    {
        get
        {
            return PhotonNetwork.playerName;
        }
        set
        {
            PhotonNetwork.playerName = value;
        }
    }
    public string OponentName
    {
        get
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
    }


    private int getStringsCount(string s)
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
        if (NewMessage!=null)
        ChatText.text = NewMessage;
    }
    UISprite Ready1;
    UISprite Ready2;
    [PunRPC]
    void ReadyLight(bool player1, bool player2)
    {
        
        Ready1 = GameObject.Find("Ready1").GetComponent<UISprite>();
        Ready2 = GameObject.Find("Ready2").GetComponent<UISprite>();
        Player1 = player1;
        Player2 = player2;
        if (Player1)
        {
            Ready1.spriteName = "Green";         
        }
        else if (!Player1)
        {
            Ready1.spriteName = "Red";
        }

        if (Player2)
        {
            Ready2.spriteName = "Green";
        }
        else if (!Player2)
        {
            Ready2.spriteName = "Red";
        }

    }

    public void ReadyClick(bool player1, bool player2)
    {
        PhotonView photonView = PhotonView.Find(2);
        photonView.RPC("ReadyLight", PhotonTargets.All, player1, player2);
    }
    public bool PlayerIsMasterClient
    {
        get
        {
            return PhotonNetwork.isMasterClient;
        }
    }
    public void SendMessageInLobby()
    {
        if (ChatText.text.Contains("|"))
        {
            return;
        }
        if (ChatText.text != "")
        {
            ChatText.text += '\n';
        }
        if (getStringsCount(ChatText.text) >= MAX_CHAT_STRINGS_COUNT)
        {
            ChatText.text = ChatText.text.Remove(0, ChatText.text.IndexOf('\n') + 1);
        }
        string toAdd = PhotonNetwork.playerName+ ": " + inputField.transform.FindChild("Label").GetComponent<UILabel>().text;
        if (toAdd.Length >= maxChatInputString)
        {
            toAdd = toAdd.Remove(maxChatInputString);
        }

        while (toAdd.Contains("\n"))
        {
            toAdd = toAdd.Remove(toAdd.IndexOf('\n'), 1);
        }
        ChatText.text += toAdd;

        inputField.transform.FindChild("Label").GetComponent<UILabel>().text = "";
        PhotonView photonView = PhotonView.Find(1);
        photonView.RPC("Chat", PhotonTargets.All, ChatText.text);

    }

    public void ExitRoom()
    {
        PhotonNetwork.LeaveRoom();
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
