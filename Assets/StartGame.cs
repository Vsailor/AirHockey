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
        yield return new WaitForSeconds(delay);

        GameObject.Find("Lightning").transform.GetChild(2).gameObject.SetActive(false);
        GameObject.Find("Lightning").transform.GetChild(0).gameObject.SetActive(true);
        GetComponent<AudioSource>().Play();
        yield return new WaitForSeconds(1);
        GameObject.Find("Lightning").transform.GetChild(3).gameObject.SetActive(false);
        GameObject.Find("Lightning").transform.GetChild(1).gameObject.SetActive(true);
        GetComponent<AudioSource>().Play();

    }
}
