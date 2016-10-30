using UnityEngine;
using System.Collections;
using Assets.Scripts.Components;

public class BallScript : MonoBehaviour {
    public AudioSource[] audio;
    

    IEnumerator ResetObjects()
    {
        GetComponent<Rigidbody>().MovePosition(new Vector3(4.4f, 2.3f, 8f));
        GetComponent<Rigidbody>().Sleep();
        GetComponent<Rigidbody>().WakeUp();
        GameObject.Find(Controls.Stick1).GetComponent<StickPhysics>().blocked = true;
        GameObject.Find(Controls.Stick2).GetComponent<StickPhysics>().blocked = true;
        var position = GameObject.Find(Controls.Stick1).transform.position;
        GameObject.Find(Controls.Stick1).transform.Translate(-27.2f- position.x, 0, 8.7f- position.z);
        position = GameObject.Find(Controls.Stick2).transform.position;
        GameObject.Find(Controls.Stick2).transform.Translate(35.6f - position.x, 0, 8.6f - position.z);
        yield return new WaitForSeconds(1);
        GameObject.Find(Controls.Stick1).GetComponent<StickPhysics>().blocked = false;
        GameObject.Find(Controls.Stick2).GetComponent<StickPhysics>().blocked = false;

    }

    private bool init;
    private void Init()
    {
        transform.Rotate(270,0,0);
        audio = GetComponents<AudioSource>();
        ResetObjects();
        init = true;
    }

    void Update()
    {
        if (!init)
        {
            Init();
        }

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
        if (collision.gameObject.name == Controls.Stick1 || collision.gameObject.name == Controls.Stick2)
        {
            audio[1].Play();
        }

    }
}
