using System;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StickPhysics : MonoBehaviour
{
    public Camera camera;
    public Transform trans;
    public float top = 0, bot = -29, left = -11, right = 28;
    public Rigidbody rb;
    public float cornerLeftX = -25.76f;
    public float cornerLeftZ = 23.49f;
    public float cornerRad = 4.75f;
    public float cornerRightX = -25.76f;
    public float cornerRightZ = 23.49f;
    public bool blocked;
    Vector3 old = new Vector3();
    void OnMouseDrag()
    {
        if (blocked) return;
        float distance_to_screen = camera.WorldToScreenPoint(gameObject.transform.position).z;
        Vector3 pos_move = camera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance_to_screen));
        // rb.MovePosition(new Vector3(Mathf.Clamp(pos_move.x, bot, top), rb.position.y, Mathf.Clamp(pos_move.z,left,right)));
        // rb.MovePosition(new Vector3(pos_move.x, rb.position.y, pos_move.z));
        //   Vector3 v=new Vector3(-25.76f,0,23.49f);
        Vector3 v = new Vector3(cornerLeftX, 0, cornerLeftZ);
        if (pos_move.x < v.x && pos_move.z > v.z)
        {
            Vector3 p = new Vector3(pos_move.x - v.x, rb.position.y, pos_move.z - v.z);
            var c = Vector3.ClampMagnitude(p, cornerRad);

            rb.MovePosition(c + v);
        }
        else
        {
            v = new Vector3(cornerRightX, 0, cornerRightZ);
            if (pos_move.x < v.x && pos_move.z < v.z)
            {
                Vector3 p = new Vector3(pos_move.x - v.x, rb.position.y, pos_move.z - v.z);
                var c = Vector3.ClampMagnitude(p, cornerRad);

                rb.MovePosition(c + v);
            }
            else
            {
                rb.MovePosition(new Vector3(Mathf.Clamp(pos_move.x, bot, top), rb.position.y,
                    Mathf.Clamp(pos_move.z, left, right)));
            }
        }
        //  print(GetComponent<Rigidbody>().velocity);

    }

    void OnTriggerEnter(Collider collision)
    {
        print(collision.gameObject.name);
    }
    void OnCollisionStay(Collision collisionInfo)
    {
        // rb.MovePosition(old);

    }
}
