using UnityEngine;
using System.Collections;

public class BallScript : MonoBehaviour {
    public AudioSource[] audio;
    
    void Start()
    {
        audio = GetComponents<AudioSource>();
        ResetObjects();
    }

    IEnumerator ResetObjects()
    {
        GetComponent<Rigidbody>().MovePosition(new Vector3(4.4f, 2.3f, 8f));
        GetComponent<Rigidbody>().Sleep();
        GetComponent<Rigidbody>().WakeUp();
        GameObject.Find("Stick").GetComponent<Rigidbody>().MovePosition(new Vector3(-27.2f, 4.2f, 8.7f));
        GameObject.Find("Stick2").GetComponent<Rigidbody>().MovePosition(new Vector3(35.6f, 4.2f, 8.7f));
        GameObject.Find("Stick").GetComponent<StickPhysics>().blocked = true;
        GameObject.Find("Stick2").GetComponent<StickPhysics>().blocked = true;
        yield return new WaitForSeconds(1);
        GameObject.Find("Stick").GetComponent<StickPhysics>().blocked = false;
        GameObject.Find("Stick2").GetComponent<StickPhysics>().blocked = false;

    }
    void Update()
    {
        if (transform.localPosition.x > 43 || transform.localPosition.x < -34)
        {
            audio[2].Play();
            StartCoroutine(ResetObjects());
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
