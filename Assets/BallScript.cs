using UnityEngine;
using System.Collections;

public class BallScript : MonoBehaviour {
    public AudioSource[] audio;
    
    void Start()
    {
        audio = GetComponents<AudioSource>();
    }

    void Update()
    {
        if (transform.localPosition.x > 43 || transform.localPosition.x < -34)
        {
            audio[2].Play();
            GetComponent<Rigidbody>().MovePosition(new Vector3(0,2.28f,0));
            GetComponent<Rigidbody>().Sleep();
            GetComponent<Rigidbody>().WakeUp();
        }
    }
    void OnCollisionEnter(Collision collision)
    {
        foreach (ContactPoint contact in collision.contacts)
        {
            Debug.DrawRay(contact.point, contact.normal, Color.white);
        }
        //    if (collision.relativeVelocity.magnitude > 2)
        if (collision.gameObject.name == "TableBorders")
        {
            audio[0].Play();
        }
        if (collision.gameObject.name == "Stick"|| collision.gameObject.name == "Stick2")
        {
            audio[1].Play();
        }

    }
}
