using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class ConnectToPhoton : Photon.MonoBehaviour
{
    // Use this for initialization
    void Start()
    {
        
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
    public void SavePlayerName(string inputField)
    {
        PhotonNetwork.playerName = inputField;
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
