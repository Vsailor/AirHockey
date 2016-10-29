using UnityEngine;
using Assets.Scripts.Components;

public class ButtonScript : MonoBehaviour
{
    private ConnectToPhoton PhotonCamera;
    void OnClick()
    {
        if (name == Controls.CreateGameButton)
        {
            Application.LoadLevel(Scenes.CreateGameMenuScene);
        }
        else if (name == Controls.JoinGameButton)
        {
            Application.LoadLevel(Scenes.EnterNameScene);
        }
        else if (name == Controls.QuitGameButton)
        {
            Application.Quit();
        }
        else if (name == Controls.BackToMainMenuButton)
        {
            PhotonCamera.ExitRoom();
            Application.LoadLevel(Scenes.MainMenuScene);
        }
        else if (name == Controls.CreateRoomButton)
        {
            var yourName = GameObject.Find(Controls.YourNameField).transform.FindChild("Label").GetComponent<UILabel>();
            var roomName = GameObject.Find(Controls.RoomNameField).transform.FindChild("Label").GetComponent<UILabel>();

            if (!string.IsNullOrEmpty(yourName.text) && yourName.text.Length <= 15 && 
                !string.IsNullOrEmpty(roomName.text) && roomName.text.Length <= 15)
            {
                PhotonCamera.PlayerName = yourName.text;
                PhotonCamera.CreateRoom(roomName.text);

                Application.LoadLevel(Scenes.LobbyScene);
            }
        }
        else if (name == Controls.SendButton)
        {
            GameObject.Find("Camera").GetComponent<ConnectToPhoton>().SendMessageInLobby();
        }
        else if (name == Controls.ReadyPlayButton)
        {
            if (PhotonCamera.PlayerIsMasterClient)
            {
                PhotonCamera.ReadyClick(!PhotonCamera.Player1, PhotonCamera.Player2);
            }
            else
            {
                PhotonCamera.ReadyClick(PhotonCamera.Player1, !PhotonCamera.Player2);
            }
        }
        else if (name == Controls.ConfirmNameButton)
        {
            var input = GameObject.Find(Controls.InputField).transform.FindChild("Label").GetComponent<UILabel>();
            if (input.text != "" && input.text.Length <= 15)
            {
                PhotonCamera.PlayerName = input.text;
                Application.LoadLevel(Scenes.JoinGameScene);
            }
        }
        else if (name == Controls.BackToNameScene)
        {
            Application.LoadLevel(Scenes.EnterNameScene);
        }
        else if (name == Controls.PlayOfflineGameButton)
        {
            Application.LoadLevel(Scenes.GameScene);
        }
        else if (name == Controls.TryConnectAgainButton)
        {
            PhotonCamera.Connect();
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ExitFromLobby()
    {
        if (PhotonNetwork.room != null)
        {
            PhotonNetwork.LeaveRoom();
        }
    }

    void Start()
    {
        PhotonCamera = GameObject.Find("Camera").GetComponent<ConnectToPhoton>();
    }

    ConnectToPhoton comp;
    UILabel buttonName;
    bool init;

    void Init()
    {
        comp = GameObject.Find("Camera").GetComponent<ConnectToPhoton>();
        buttonName = GameObject.Find(Controls.ReadyPlayButton).transform.FindChild("Label").GetComponent<UILabel>();
        init = false;
    }

    public void JoinRoom(string name)
    {
        try
        {
            PhotonNetwork.JoinRoom(name);
        }
        catch
        {
            if (PhotonNetwork.room != null)
            {
                PhotonNetwork.LeaveRoom();
                Application.LoadLevel(Scenes.JoinGameScene);
            }
        }
    }
}
