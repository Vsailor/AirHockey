using UnityEngine;
using System.Collections;

public class CreateGameButtonScript : MonoBehaviour {
    void OnClick()
    {
        Application.LoadLevel("CreateGameMenuScene");
    }
}
