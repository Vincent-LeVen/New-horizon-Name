using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextSceneOnCollision : MonoBehaviour {

	public string nextScene;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider coll)
	{
		if (coll.gameObject.tag == "Player")
		{
			SceneManager.LoadScene (nextScene);
		}
	}
}
