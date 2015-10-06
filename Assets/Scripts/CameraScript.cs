using UnityEngine;
using System.Collections;
using CnControls;

public class CameraScript : MonoBehaviour {

    public Transform target;
    public float scrollSpeed = 4, zoomMin = 3, zoomMax = 10, speedX = 200, speedY = 200;
    public bool invertHorizontal = true, invertVertical=false;
    private float xRotated ,yRotated;
    public float top, bot, left, right;
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        var x = CnInputManager.GetAxis("Horizontal");
        var y = CnInputManager.GetAxis("Vertical");
        var xAngle = x * speedX * Time.deltaTime;
        var yAngle = y * speedY * Time.deltaTime;
	    if (invertHorizontal) xAngle *= -1;
	    if (invertVertical) yAngle *= -1;
	    if (xAngle + xRotated < right && xAngle + xRotated > left)
	    {
	        transform.RotateAround(target.position, target.up, xAngle);
	        xRotated += xAngle;
	    }
	    if (yAngle + yRotated < top && yAngle + yRotated > bot)
	    {
	        transform.RotateAround(target.position, transform.right, yAngle);
	        yRotated += yAngle;
	    }
	}
}
