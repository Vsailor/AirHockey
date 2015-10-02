using UnityEngine;
using System.Collections;

public class ConnectToPhoton : Photon.MonoBehaviour {

	// Use this for initialization
	void Start ()
	{
        PhotonNetwork.autoJoinLobby = false;
        if (!PhotonNetwork.connected)
	    {
            PhotonNetwork.ConnectUsingSettings("0.1");
	        PhotonNetwork.playerName = "Default";
	    }
       	    
    }
    // Update is called once per frame
        void Update () {
	
	}
}
