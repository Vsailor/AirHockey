using UnityEngine;
using System.Collections;
using CnControls;

public class CameraScript : MonoBehaviour {

    public Transform target;
    public float scrollSpeed = 4, zoomMin = 3, zoomMax = 10, speedX = 200, speedY = 200;
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        var x = CnInputManager.GetAxis("Horizontal");
        var y = CnInputManager.GetAxis("Vertical");
        var xAngle = x * 200 * Time.deltaTime;
        var yAngle = y * 200 * Time.deltaTime;
        transform.RotateAround(target.position, target.up, xAngle);
    }
}
