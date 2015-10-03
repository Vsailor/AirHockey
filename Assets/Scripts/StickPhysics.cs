using UnityEngine;
using System.Collections;

public class StickPhysics : MonoBehaviour
{
    public Camera camera;
    public float distance = 100;
    public Rigidbody rb;
    void OnMouseDrag()
    {      
        float distance_to_screen = camera.WorldToScreenPoint(gameObject.transform.position).z;
        Vector3 pos_move = camera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance_to_screen));

        rb.MovePosition ( new Vector3(pos_move.x, rb.position.y, pos_move.z));            
    }

    void OnCollisionStay(Collision collisionInfo)
    {
        print("asg");
        
    }
}
