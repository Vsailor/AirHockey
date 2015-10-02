using UnityEngine;
using System.Collections;

public class JoinButtonScript : MonoBehaviour {
    public string LoadLevelName;
    public void LoadLevel()
    {
        GetComponent<ButtonScript>().JoinRoom(LoadLevelName);
    }
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
