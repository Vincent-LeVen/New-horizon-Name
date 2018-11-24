using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstStart2 : MonoBehaviour {

    public GameObject blockStart;

	// Use this for initialization
	void Start () {
        blockStart.SetActive(false);
	}
	
	// Update is called once per frame
	void OnTriggerEnter (Collider player)
    {
        if (player.gameObject.tag == "Player")
        {
            blockStart.SetActive(true);
        }
        else
        {
            blockStart.SetActive(false);
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
