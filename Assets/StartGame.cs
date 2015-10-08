using UnityEngine;
using System.Collections;

public class StartGame : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        StartCoroutine(EnableLightIn(3));
    }

    // Update is called once per frame
    void Update()
    {

    }
    IEnumerator EnableLightIn(float delay)
    {
        GameObject.Find("Stick").GetComponent<StickPhysics>().blocked = true;
        GameObject.Find("Stick2").GetComponent<StickPhysics>().blocked = true;
        yield return new WaitForSeconds(delay);

        GameObject.Find("Light").transform.GetChild(2).gameObject.SetActive(false);
        GameObject.Find("Light").transform.GetChild(0).gameObject.SetActive(true);
        GetComponent<AudioSource>().Play();
        yield return new WaitForSeconds(1);
        GameObject.Find("Light").transform.GetChild(3).gameObject.SetActive(false);
        GameObject.Find("Light").transform.GetChild(1).gameObject.SetActive(true);
        GetComponent<AudioSource>().Play();
        GameObject.Find("Stick").GetComponent<StickPhysics>().blocked = false;
        GameObject.Find("Stick2").GetComponent<StickPhysics>().blocked = false;

    }
}
