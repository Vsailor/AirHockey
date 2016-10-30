using UnityEngine;
using System.Collections;
using Assets.Scripts.Components;

public class StartGame : Photon.MonoBehaviour
{
    void Start()
    {
        if (PhotonNetwork.isMasterClient)
        {
            PhotonNetwork.Instantiate("Stick", new Vector3(-27.2f, 4.277191f, 8.7f), Quaternion.identity, 0);
        }
        else
        {
            PhotonNetwork.Instantiate("Stick2", new Vector3(35.6f, 4.2777191f, 8.7f), Quaternion.identity, 0);
        }
    }

    void Update()
    {
        if (!init)
            Init();
    }

    private bool init;
    private void Init()
    {
        if (GameObject.Find(Controls.Stick1) == null || 
            GameObject.Find(Controls.Stick2) == null)
            return;

        if (PhotonNetwork.isMasterClient)
        { 
            PhotonNetwork.Instantiate("Ball", new Vector3(4.4f, 2.3f, 8f), Quaternion.identity, 0);
        }

        StartCoroutine(EnableLightIn(3));
        init = true;
    }

    IEnumerator EnableLightIn(float delay)
    {
        GameObject.Find(Controls.Stick1).GetComponent<StickPhysics>().blocked = true;
        GameObject.Find(Controls.Stick2).GetComponent<StickPhysics>().blocked = true;
        yield return new WaitForSeconds(delay);

        GameObject.Find("Light").transform.GetChild(2).gameObject.SetActive(false);
        GameObject.Find("Light").transform.GetChild(0).gameObject.SetActive(true);
        GetComponent<AudioSource>().Play();
        yield return new WaitForSeconds(1);
        GameObject.Find("Light").transform.GetChild(3).gameObject.SetActive(false);
        GameObject.Find("Light").transform.GetChild(1).gameObject.SetActive(true);
        GetComponent<AudioSource>().Play();
        GameObject.Find(Controls.Stick1).GetComponent<StickPhysics>().blocked = false;
        GameObject.Find(Controls.Stick2).GetComponent<StickPhysics>().blocked = false;
    }
}
