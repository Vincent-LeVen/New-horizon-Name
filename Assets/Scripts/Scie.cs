using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scie : MonoBehaviour {

	public GameObject scieNormale;
	public GameObject scieSanglante;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void OnCollisionEnter (Collision coll)
	{
		if (coll.gameObject.tag == "Player") 
		{
			Debug.Log ("sedgf");
			GameObject scie = Instantiate<GameObject> (scieSanglante);
			scie.transform.localScale = new Vector3 (scieNormale.transform.localScale.x, scieNormale.transform.localScale.y, scieNormale.transform.localScale.z);
			scie.transform.position = new Vector3 (scieNormale.transform.position.x, scieNormale.transform.position.y, scieNormale.transform.position.z);
			scie.transform.rotation = Quaternion.Euler (scieNormale.transform.rotation.eulerAngles.x, scieNormale.transform.rotation.eulerAngles.y, scieNormale.transform.rotation.eulerAngles.z);
			Destroy (scieNormale.gameObject);
		}
	}
}
