using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class AutocompleteScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
    bool init = false;
    void Init()
    {
        if (!(PhotonNetwork.playerName == "Default" || PhotonNetwork.playerName == ""))
        {
            GetComponent<InputField>().text = PhotonNetwork.playerName;
        }
        init = true;
    }
	// Update is called once per frame
	void Update () {
        if (!init)
        {
            Init();
        }
	}
}
