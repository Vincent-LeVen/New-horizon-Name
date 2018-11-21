using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamMouseLook : MonoBehaviour {

	public Vector2 mouseLook;
	public float sensitivity = 5.0f;

	GameObject character;

	// Use this for initialization
	void Start () {
		character = this.transform.parent.gameObject;
	}

	// Update is called once per frame
	void Update () {
		Vector2 mouseDelta = new Vector2 (Input.GetAxisRaw ("Mouse X"), Input.GetAxisRaw ("Mouse Y"));
		//Debug.Log (mouseDelta);

		mouseDelta = Vector2.Scale (mouseDelta, new Vector2 (sensitivity , sensitivity ));
		mouseLook += mouseDelta;

		transform.localRotation = Quaternion.AngleAxis (-mouseLook.y, Vector3.right);
		character.transform.localRotation = Quaternion.AngleAxis (mouseLook.x, character.transform.up);

		BlockView ();
	}

	void BlockView()
	{
		if (mouseLook.y > 90.0f) 
		{
			mouseLook.y = 90.0f;
			transform.localRotation = Quaternion.AngleAxis (-mouseLook.y, Vector3.right);
		}

		if (mouseLook.y < -70.0f) 
		{
			mouseLook.y = -70.0f;
			transform.localRotation = Quaternion.AngleAxis (-mouseLook.y, Vector3.right);
		}
	}
}
