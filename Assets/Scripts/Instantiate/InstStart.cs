using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstStart : MonoBehaviour {

    public GameObject blockStart;
    
	
	// Update is called once per frame
	private void OnTriggerEnter(Collider other)
    {

        //if Collider...
        gameObject.SetActive(true);
            if (Input.GetKeyDown (KeyCode.A))
            {
                Instantiate (blockStart, transform.position, transform.rotation);
            }
        
	}
}
