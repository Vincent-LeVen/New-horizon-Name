using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstStart2 : MonoBehaviour
{
    public GameObject baseBlock;
    public GameObject blockText;

    // Use this for initialization
    void Start () {
        baseBlock.SetActive(true);
        blockText.SetActive(false);
    }
	
	// Update is called once per frame
	private void OnTriggerEnter(Collider player)
    {
        if (player.gameObject.tag == "Player")
        {
            baseBlock.SetActive(false);
            blockText.SetActive(true);
            Debug.Log("entering");
        }
    }

    private void OnTriggerExit(Collider player)
    {
        if (player.gameObject.tag == "Player")
        {
            baseBlock.SetActive(true);
            blockText.SetActive(false);
            Debug.Log("exiting");
        }
    }
}
