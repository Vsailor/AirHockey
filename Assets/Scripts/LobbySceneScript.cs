using UnityEngine;
using System.Collections;
using System.Linq;
using UnityEngine.UI;

public class LobbySceneScript : Photon.MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        player1 = GameObject.Find("Player1");
        player2 = GameObject.Find("Player2");
    }
    GameObject sendButton;
    ConnectToPhoton PhotonCamera;
    UILabel inputField;
    private GameObject player1;
    private GameObject player2;
    private UILabel Timer;
    private void Init()
    {
        if (PhotonNetwork.room != null)
        {
            init = true;
            GameObject.Find("RoomName").GetComponent<UILabel>().text = "Room \"" + PhotonNetwork.room.name + "\"";
            
        }
        PhotonCamera = GameObject.Find("Camera").GetComponent<ConnectToPhoton>();
        inputField = GameObject.Find("InputField").transform.FindChild("Label").GetComponent<UILabel>();
        sendButton = GameObject.Find("SendButton");
        Timer = GameObject.Find("Timer").GetComponent<UILabel>();
    }


    private bool init = false;

    public void ShowPlayers()
    {
        if (PhotonNetwork.isMasterClient)
        {
            
            player1.GetComponent<UILabel>().text = PhotonNetwork.playerName;
            GameObject.Find("Ready1").GetComponent<UISprite>().enabled = true;
            if (PhotonNetwork.otherPlayers.Length != 0)
            {
                player2.GetComponent<UILabel>().text = PhotonNetwork.otherPlayers[0].name;
                GameObject.Find("Ready2").GetComponent<UISprite>().enabled = true;
                PhotonView photonView = PhotonView.Find(1);
                UILabel t = GameObject.Find("Chat").GetComponent<UILabel>();
                photonView.RPC("Chat", PhotonTargets.All, t.text);
                photonView = PhotonView.Find(2);
                bool p1 = GameObject.Find("Camera").GetComponent<ConnectToPhoton>().Player1;
                bool p2 = GameObject.Find("Camera").GetComponent<ConnectToPhoton>().Player2;
                photonView.RPC("ReadyLight", PhotonTargets.All, p1, p2);
            }
            else
            {
                PhotonView photonView = PhotonView.Find(2);
                bool p1 = GameObject.Find("Camera").GetComponent<ConnectToPhoton>().Player1;
                photonView.RPC("ReadyLight", PhotonTargets.All, p1, false);
                player2.GetComponent<UILabel>().text = "";
                GameObject.Find("Ready2").GetComponent<UISprite>().enabled = false;
            }
        }
        else
        {
            if (PhotonNetwork.otherPlayers.Length != 0)
            {
                player1.GetComponent<UILabel>().text = PhotonNetwork.otherPlayers[0].name;
                GameObject.Find("Ready1").GetComponent<UISprite>().enabled = true;
                player2.GetComponent<UILabel>().text = PhotonNetwork.playerName;
                GameObject.Find("Ready2").GetComponent<UISprite>().enabled = true;
            }

        }
    }
    int startTime;
    int secondsLeft = 5;
    int oldTime = 0;
    private void Update()
    {
        if (!init)
        {
            Init();
        }
        if (init)
        {
            if (Timer.text.Length != 0)
            {
                if (oldTime != (int)(Time.fixedTime))
                {
                    secondsLeft -= 1;
                    Timer.text = secondsLeft.ToString();
                    if (secondsLeft == 0)
                    {
                        Application.LoadLevel("GameScene");
                    }
                    oldTime = (int)(Time.fixedTime);
                }
                
            }
            if (!(PhotonCamera.Player1 && PhotonCamera.Player2))
            {
                secondsLeft = 5;
                Timer.text = "";
                oldTime = 0;


            }
            if (Time.fixedTime % 2 == 0)
            {
                ShowPlayers();
                if (PhotonCamera.Player1 && PhotonCamera.Player2 && Timer.text.Length==0)
                {
                    oldTime = (int)(Time.fixedTime);
                    startTime = (int)(Time.fixedTime);
                    Timer.text = secondsLeft.ToString();

                }
            }
            if (Input.GetKeyDown(KeyCode.Return))
            {
                GameObject.Find("Camera").GetComponent<ConnectToPhoton>().SendMessageInLobby();
            }
        }
    }

}
