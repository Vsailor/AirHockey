using UnityEngine;
using System.Collections;

public class GameListChooseScript : Photon.MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
    bool init = false;
    void Init()
    {
        list = GetComponent<UIPopupList>();
    }
    UIPopupList list;
    bool join = false;
	// Update is called once per frame
	void Update () {
        if (!init)
        {
            Init();
            init = true;
        }
        if (list.selection != "<Not choosen>" && !join)
        {
            foreach (var item in PhotonNetwork.GetRoomList())
            {
                if (item.name == list.selection)
                {
                    PhotonNetwork.JoinRoom(item.name);
                    join = true;
                }
            }
        }
        if (PhotonNetwork.room != null)
        {

            if (PhotonNetwork.room.playerCount >2)
            {
                PhotonNetwork.LeaveRoom();

                list.selection = "<Not choosen>";
                join = false;
            }
            else
            {

                Application.LoadLevel("LobbyScene");
            }
        }
	}
}
