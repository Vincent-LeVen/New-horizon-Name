using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneOnClick : MonoBehaviour {

	// Use this for initialization
	public void LoadSceneByName (string scene) {
		SceneManager.LoadScene (scene);
	}

	public void ReloadScene () {
		string scene = SceneManager.GetActiveScene ().name;
		SceneManager.LoadScene (scene);
	}
}
