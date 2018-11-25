using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstStart2 : MonoBehaviour {

    public GameObject blockStart;
    public GameObject baseBlockStart;

    // Use this for initialization
    void Start () {
        blockStart.SetActive(false);
        baseBlockStart.SetActive(true);
    }
	
	// Update is called once per frame
	private void OnTriggerEnter(Collider player)
    {
        if (player.gameObject.tag == "Player")
        {
            blockStart.SetActive(true);
            baseBlockStart.SetActive(false);
            Debug.Log("entering");
        }
    }

    private void OnTriggerExit(Collider player)
    {
        if (player.gameObject.tag == "Player")
        {
            blockStart.SetActive(false);
            baseBlockStart.SetActive(true);
            Debug.Log("exiting");
        }
    }

    /*void OnTriggerExit (Collider player)
    {
        if (player.gameObject.tag == "Player")
        {
            blockStart.SetActive(false);
        }
    }*/
}
