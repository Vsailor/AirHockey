using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.Events;
using UnityEngine.UI;

public class JoinGameScript : Photon.MonoBehaviour
{
    // Use this for initialization
    void Start()
    {

    }
    public UIPopupList RoomsList;
    private bool inited = false;
    private void init()
    {
        inited = true;
        RoomsList = GameObject.Find("RoomsList").GetComponent<UIPopupList>();
    }

    public void RefreshList()
    {
        RoomsList.items.Clear();
        var roomsInfo = PhotonNetwork.GetRoomList();
        var rl = from items in roomsInfo select items.name;
        RoomsList.items.Add("<Not choosen>");
        RoomsList.items.AddRange(rl);

    }


    // Update is called once per frame
    void Update()
    {
        if (!inited)
        {
            init();
        }
        if (Time.fixedTime % 2 == 0)
        {
            RefreshList();
        }

    }
}
