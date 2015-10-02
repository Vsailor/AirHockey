using UnityEngine;
using System.Collections;
using System.Linq;
using UnityEngine.UI;

public class LobbySceneScript : Photon.MonoBehaviour {

	// Use this for initialization
	void Start () {
        player1 = GameObject.Find("Player1");
        player2 = GameObject.Find("Player2");
    }

    private GameObject player1;
    private GameObject player2;
    private void Init()
    {
        if (PhotonNetwork.room != null)
        {
            init = true;
            GameObject.Find("RoomName").GetComponent<Text>().text = "Room \"" + PhotonNetwork.room.name + "\"";

        }

    }


    private bool init = false;

    private void ShowPlayers()
    {
        if (PhotonNetwork.isMasterClient)
        {
            player1.GetComponent<Text>().text = PhotonNetwork.playerName;
            if (PhotonNetwork.otherPlayers.Length != 0)
            {
                player2.GetComponent<Text>().text = PhotonNetwork.otherPlayers[0].name;
            }
            else
            {
                player2.GetComponent<Text>().text = "";

            }
        }
        else
        {
            player1.GetComponent<Text>().text = PhotonNetwork.otherPlayers[0].name;
            player2.GetComponent<Text>().text = PhotonNetwork.playerName;
        }
    }

    // Update is called once per frame
    private void Update()
    {
        if (!init)
        {
            Init();
        }
        ShowPlayers();
    }

}
