using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.Events;
using UnityEngine.UI;

public class JoinGameScript : Photon.MonoBehaviour
{
    public int Interval;
    public GameObject Room;
    public GameObject JoinButton;
    // Use this for initialization
    void Start()
    {
        list = new List<GameObject>();
    }

    private bool inited = false;
    private void init()
    {
        Room.SetActive(false);
        JoinButton.SetActive(false);
        inited = true;
    }

    public void RefreshList()
    {
        var rooms = PhotonNetwork.GetRoomList();
        //JoinButton.GetComponent<Button>().onClick.RemoveAllListeners();
        for (int i = 0; i < list.Count; i++)
        {

            Destroy(list[i]);
        }
        list.Clear();

        for (int i = 0; i < rooms.Count(); i++)
        {
            if (i == 0)
            {
                Room.SetActive(true);
                Room.GetComponent<UILabel>().text = rooms[0].name;
                JoinButton.SetActive(true);
                JoinButton.GetComponent<JoinButtonScript>().LoadLevelName = rooms[0].name;
            }
            else
            {

                GameObject room = Instantiate(Room);
                room.transform.parent = GameObject.Find("Panel").transform;
                room.transform.position = Room.transform.position;
                room.transform.position = new Vector3(room.transform.position.x, room.transform.position.y - i * Interval, room.transform.position.z);
                room.GetComponent<UILabel>().text = rooms[i].name;
                GameObject button = Instantiate(JoinButton);
                button.transform.parent = GameObject.Find("Panel").transform;
                button.transform.position = JoinButton.transform.position;
                button.transform.localScale = JoinButton.transform.localScale;
                var buttonComponent = JoinButton.GetComponent<ButtonScript>();
                button.transform.position = new Vector3(button.transform.position.x, button.transform.position.y - i * Interval, button.transform.position.z);
                button.GetComponent<JoinButtonScript>().LoadLevelName = rooms[i].name;
                list.AddRange(new[] { room, button });
            }

        }
    }

    private List<GameObject> list;
    // Update is called once per frame
    void Update()
    {
        if (!inited)
        {
            init();
        }
        if (Time.fixedTime % 10 == 0)
        {
            RefreshList();
        }

    }
}
