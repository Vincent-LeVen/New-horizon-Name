using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstStart : MonoBehaviour {

    public GameObject blockStart;
    
	
	// Update is called once per frame
	void Update () {

        if(Input.GetMouseButton(0))
        {
            Instantiate (blockStart, transform.position, transform.rotation);
        }
		
	}
}
