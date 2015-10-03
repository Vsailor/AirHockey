using UnityEngine;
using System.Collections;

public class JoinButtonScript : Photon.MonoBehaviour {
    public GameObject MessageBox;
    public string LoadLevelName;
    bool loading = false;
    public void LoadLevel()
    {
        if (!loading)
        {
            GetComponent<ButtonScript>().JoinRoom(LoadLevelName);
        }
        loading = true;

    }
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (PhotonNetwork.room != null)
        {
            if (PhotonNetwork.room.playerCount > 2)
            {
                PhotonNetwork.LeaveRoom();
                MessageBox.SetActive(true);
                MessageBox.transform.FindChild("MessageBoxText").GetComponent<UnityEngine.UI.Text>().text = "Too many players in this room";
                loading = false;
            }
            else
            {
                GetComponent<ButtonScript>().LoadScene("LobbyScene");
            }
        }
	}
}
