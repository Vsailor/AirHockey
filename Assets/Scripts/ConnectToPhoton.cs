using UnityEngine;
using Assets.Scripts.Components;

public class ConnectToPhoton : Photon.MonoBehaviour
{
    public static int ConnectAttemptCount = 0;
    private const int MAX_CHAT_STRINGS_COUNT = 6;
    private const int MAX_CHAT_STRING_LENGTH = 40;
    public UILabel ChatText;
    private GameObject inputField;
    private int maxChatInputString;

    // Use this for initialization
    void Start()
    {
        if (Application.loadedLevelName == Scenes.LobbyScene)
        {
            ChatText = GameObject.Find(Controls.Chat).GetComponent<UILabel>();
            inputField = GameObject.Find(Controls.InputField);
            maxChatInputString = MAX_CHAT_STRING_LENGTH - (PlayerName + ": ").Length;
        }
        if (!PhotonNetwork.connectedAndReady)
        {
            Connect();
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
            return string.Empty;
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

    public bool Player1;
    public bool Player2;

    [PunRPC]
    void Chat(string NewMessage)
    {
        if (NewMessage != null)
            ChatText.text = NewMessage;
    }

    UISprite Ready1;
    UISprite Ready2;
    [PunRPC]
    void ReadyLight(bool player1IsReady, bool player2IsReady)
    {
        Ready1 = GameObject.Find(Controls.Ready1).GetComponent<UISprite>();
        Ready2 = GameObject.Find(Controls.Ready2).GetComponent<UISprite>();

        Player1 = player1IsReady;
        Player2 = player2IsReady;

        if (Player1)
        {
            Ready1.spriteName = Controls.Green;
        }
        else if (!Player1)
        {
            Ready1.spriteName = Controls.Red;
        }

        if (Player2)
        {
            Ready2.spriteName = Controls.Green;
        }
        else if (!Player2)
        {
            Ready2.spriteName = Controls.Red;
        }
    }

    public void ReadyClick(bool player1, bool player2)
    {
        PhotonView photonView = PhotonView.Find(2);
        photonView.RPC(Controls.ReadyLight, PhotonTargets.All, player1, player2);
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
        string inputFieldText = inputField.transform.FindChild("Label").GetComponent<UILabel>().text;
        if (string.IsNullOrEmpty(inputFieldText))
        {
            return;
        }

        if (inputFieldText.Length <= 1)
        {
            return;
        }

        if (ChatText.text.Contains("|"))
        {
            ChatText.text = ChatText.text.Remove(ChatText.text.IndexOf('|'));
        }

        if (!string.IsNullOrEmpty(ChatText.text))
        {
            ChatText.text += '\n';
        }

        if (getStringsCount(ChatText.text) >= MAX_CHAT_STRINGS_COUNT)
        {
            ChatText.text = ChatText.text.Remove(0, ChatText.text.IndexOf('\n') + 1);
        }

        string toAdd = PhotonNetwork.playerName + ": " + inputFieldText;
        if (toAdd.Length >= maxChatInputString)
        {
            toAdd = toAdd.Remove(maxChatInputString);
        }

        while (toAdd.Contains("\n"))
        {
            toAdd = toAdd.Remove(toAdd.IndexOf('\n'), 1);
        }

        ChatText.text += toAdd;

        inputField.transform.FindChild("Label").GetComponent<UILabel>().text = string.Empty;

        PhotonView photonView = PhotonView.Find(1);
        photonView.RPC(Controls.Chat, PhotonTargets.All, ChatText.text);
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
    public static string LastScene = Scenes.MainMenuScene;
    void Update()
    {
        if (PhotonNetwork.connectionState == ConnectionState.Disconnected)
        {
            if (Application.loadedLevelName != Scenes.FailedToConnectScene)
            {
                ConnectToPhoton.ConnectAttemptCount++;
                if (ConnectToPhoton.ConnectAttemptCount >= 5 && Application.loadedLevelName != Scenes.FailedToConnectScene)
                {
                    print(ConnectToPhoton.ConnectAttemptCount);
                    ConnectToPhoton.LastScene = Application.loadedLevelName;
                    Application.LoadLevel(Scenes.FailedToConnectScene);
                }
            }
        }
        else
        {
            ConnectToPhoton.ConnectAttemptCount = 0;
        }

        if (PhotonNetwork.connectedAndReady && Application.loadedLevelName == Scenes.FailedToConnectScene)
        {
            if (ConnectToPhoton.LastScene == Scenes.LobbyScene)
            {
                Application.LoadLevel(Scenes.MainMenuScene);
            }
            else
            {
                Application.LoadLevel(ConnectToPhoton.LastScene);
            }
        }
    }
}
