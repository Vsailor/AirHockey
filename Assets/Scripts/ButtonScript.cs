using UnityEngine;
using System.Collections;

public class ButtonScript : MonoBehaviour {
    public void LoadScene(string sceneName)
    {
        Application.LoadLevel(sceneName);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    // Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
