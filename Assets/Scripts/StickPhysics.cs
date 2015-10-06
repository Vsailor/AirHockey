using UnityEngine;
using System.Collections;

public class StickPhysics : MonoBehaviour
{
    public Camera camera;
    public Transform trans;
    public float top = 0, bot = -29, left = -11, right = 28;
    public Rigidbody rb;
    Vector3 old = new Vector3();
    void OnMouseDrag()
    {      
        float distance_to_screen = camera.WorldToScreenPoint(gameObject.transform.position).z;
        Vector3 pos_move = camera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance_to_screen));

      if(!( pos_move.x* transform.position.x / transform.localPosition.x>bot&& pos_move.x * transform.position.x / transform.localPosition.x<top))
        pos_move.x = rb.position.x;
       if (!(pos_move.z * transform.position.z / transform.localPosition.z > left && pos_move.z * transform.position.z / transform.localPosition.z < right))
            pos_move.z = rb.position.z;
        rb.MovePosition(new Vector3(pos_move.x, rb.position.y, pos_move.z));



    }

    void OOnCollisionEnter(Collision collisionInfo)
    {
        //old=new Vector3(rb.position.x,rb.position.y,rb.position.z);

    }
    void OnCollisionStay(Collision collisionInfo)
    {
       // rb.MovePosition(old);

    }
}
