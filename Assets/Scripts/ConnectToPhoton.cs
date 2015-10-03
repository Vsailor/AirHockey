using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class ConnectToPhoton : Photon.MonoBehaviour
{
    public GameObject MessageBox;
    // Use this for initialization
    void Start()
    {
        Connect();
    }
    public void Connect()
    {
        PhotonNetwork.autoJoinLobby = true;
        PhotonNetwork.ConnectUsingSettings("0.1");
        PhotonNetwork.playerName = "Default";

    }
    // Update is called once per frame
    void Update()
    {
        if (PhotonNetwork.connectionState == ConnectionState.Disconnected)
        {
            MessageBox.SetActive(true);
            MessageBox.transform.FindChild("Button").GetComponent<Button>().GetComponentInChildren<Text>().text = "Try again";
            MessageBox.transform.FindChild("MessageBoxText").GetComponent<Text>().text = "Failed to connect.\n Check your internet connection";

        }
    }
}
