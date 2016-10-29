using UnityEngine;
using Assets.Scripts.Components;

public class GameListChooseScript : Photon.MonoBehaviour
{
    private bool init;
    void Init()
    {
        lobbysList = GetComponent<UIPopupList>();
    }

    private UIPopupList lobbysList;
    private bool join;
	void Update () {
        if (!init)
        {
            Init();
            init = true;
        }

        if (lobbysList.selection != Controls.ListSelectionDefault && !join)
        {
            foreach (var item in PhotonNetwork.GetRoomList())
            {
                if (item.name == lobbysList.selection)
                {
                    PhotonNetwork.JoinRoom(item.name);
                    join = true;
                }
            }
        }

        if (PhotonNetwork.room != null)
        {
            if (PhotonNetwork.room.playerCount > 2)
            {
                PhotonNetwork.LeaveRoom();

                lobbysList.selection = Controls.ListSelectionDefault;
                join = false;
            }
            else
            {
                Application.LoadLevel(Scenes.LobbyScene);
            }
        }
	}
}
