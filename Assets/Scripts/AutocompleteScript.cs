using UnityEngine;

public class AutocompleteScript : MonoBehaviour
{
    bool init = false;
    void Init()
    {
        if (!(PhotonNetwork.playerName == "Default" || PhotonNetwork.playerName == ""))
        {
            GetComponent<UILabel>().text = PhotonNetwork.playerName;
        }
        init = true;
    }

	void Update () {
        if (!init)
        {
            Init();
        }
	}
}
