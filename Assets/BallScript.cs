using UnityEngine;
using System.Collections;

public class BallScript : MonoBehaviour {
    public AudioSource[] audio;
    
    void Start()
    {
        audio = GetComponents<AudioSource>();
        var iterator=ResetObjects();
        while (iterator.MoveNext()) { }
    }

    IEnumerator ResetObjects()
    {
        GetComponent<Rigidbody>().MovePosition(new Vector3(4.4f, 2.3f, 8f));
        GetComponent<Rigidbody>().Sleep();
        GetComponent<Rigidbody>().WakeUp();
        GameObject.Find("Stick").GetComponent<StickPhysics>().blocked = true;
        GameObject.Find("Stick2").GetComponent<StickPhysics>().blocked = true;
        var position = GameObject.Find("Stick").transform.position;
        GameObject.Find("Stick").transform.Translate(-27.2f- position.x, 0, 8.7f- position.z);
        position = GameObject.Find("Stick2").transform.position;
        GameObject.Find("Stick2").transform.Translate(35.6f - position.x, 0, 8.6f - position.z);
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
