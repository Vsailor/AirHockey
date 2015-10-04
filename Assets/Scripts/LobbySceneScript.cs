﻿using UnityEngine;
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
    UILabel inputField;
    private GameObject player1;
    private GameObject player2;
    private void Init()
    {
        if (PhotonNetwork.room != null)
        {
            init = true;
            GameObject.Find("RoomName").GetComponent<UILabel>().text = "Room \"" + PhotonNetwork.room.name + "\"";
            
        }
        inputField = GameObject.Find("InputField").transform.FindChild("Label").GetComponent<UILabel>();
        sendButton = GameObject.Find("SendButton");
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
                Text t = GameObject.Find("Chat").GetComponent<Text>();
                photonView.RPC("Chat", PhotonTargets.All, t.text);
            }
            else
            {
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

    private void Update()
    {
        if (!init)
        {
            Init();
        }
        if (Time.fixedTime % 2 == 0)
        {
            ShowPlayers();
        }
        if (Input.GetKeyDown(KeyCode.Return) && inputField.text != "")
        {
            GameObject.Find("Camera").GetComponent<ConnectToPhoton>().SendMessageInLobby();
        }
    }

}
