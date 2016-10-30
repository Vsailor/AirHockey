using System;
using UnityEngine;
using Assets.Scripts.Components;

public class ConnecterScript : Photon.MonoBehaviour
{
    public float x;
    public float y;
    public float z;

    public static void SendAcceleration(Vector3 vector)
    {
        PhotonView photonView = PhotonView.Find(Controls.ConnecterID);
        photonView.RPC("GetAcceleration", PhotonTargets.All, vector.x, vector.y, vector.z);
    }

    [PunRPC]
    private void GetAcceleration(Single x, Single y, Single z)
    {
        this.x = x;
        this.y = y;
        this.z = z;
    }
}
