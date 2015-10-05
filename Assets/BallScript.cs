using UnityEngine;
using System.Collections;

public class BallScript : MonoBehaviour {
    public AudioSource[] audio;
    
    void Start()
    {
        audio = GetComponents<AudioSource>();
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
        if (collision.gameObject.name == "Stick")
        {
            audio[1].Play();
        }

    }
}
