using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.Events;
using UnityEngine.UI;

public class JoinGameScript : Photon.MonoBehaviour
{
    public GameObject Room;
    public GameObject JoinButton;
	// Use this for initialization
	void Start () {
	    list = new List<GameObject>();
	}

    private bool inited = false;
    private void init()
    {
        Room.SetActive(false);
        JoinButton.SetActive(false);
        inited = true;
    }

    private void RefreshList()
    {
        var rooms = PhotonNetwork.GetRoomList();
        for (int i = 0; i < list.Count; i++)
        {
            Destroy(list[i]);
            list.Clear();
        }

        for (int i = 0; i < rooms.Count(); i++)
        {
            if (i == 0)
            {
                Room.SetActive(true);
                Room.GetComponent<Text>().text = rooms[0].name;
                JoinButton.SetActive(true);
                var buttonComponent = JoinButton.GetComponent<ButtonScript>();
                UnityAction buttonAction = delegate { buttonComponent.JoinRoom(rooms[0].name); };
                JoinButton.GetComponent<Button>().onClick.AddListener(buttonAction);
                JoinButton.GetComponent<Button>().onClick.AddListener(delegate { buttonComponent.LoadScene("LobbyScene"); });
                
            }
            else
            {
                GameObject room = Instantiate(Room);
                room.transform.position = new Vector3(room.transform.position.x, room.transform.position.y - i * 50f, room.transform.position.z);
                room.GetComponent<Text>().text = rooms[i].name;
                GameObject button = Instantiate(JoinButton);
                var buttonComponent = JoinButton.GetComponent<ButtonScript>();
                UnityAction buttonAction = delegate { buttonComponent.JoinRoom(rooms[i].name); };
                button.GetComponent<Button>().onClick.AddListener(buttonAction);
                button.GetComponent<Button>().onClick.AddListener(delegate { buttonComponent.LoadScene("LobbyScene"); });
                list.AddRange(new[] { room, button });
            }
        }
    }

    private List<GameObject> list; 
    // Update is called once per frame
	void Update () {
	    if (!inited)
	    {
            init();
	    }
	   RefreshList();
	}
}
