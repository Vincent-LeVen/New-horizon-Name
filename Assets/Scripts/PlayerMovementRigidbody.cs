using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovementRigidbody : MonoBehaviour {

	public float walkSpeed = 10.0f;
	public float runSpeed = 30.0f;
	private float speed;
	public float jumpForce = 10.0f;
	public float groundSpeed = 30.0f;
	public float wallJumpReduction = 10.0f;
	private Rigidbody rbPlayer;

	[Range (0.0f, 1.0f)]public float angleAttaque = 0.5f;

	bool onGround = false;

	// Use this for initialization
	void Start () 
	{
		Cursor.lockState = CursorLockMode.Locked;
		rbPlayer = GetComponent<Rigidbody> ();
		speed = walkSpeed;
	}

	// Update is called once per frame
	void FixedUpdate ()
	{
		RaycastHit hit;
		Vector3 physicsCentre = this.transform.position + this.GetComponent<CapsuleCollider> ().center;

		//Debug.DrawRay (physicsCentre, Vector3.down*1.1f, Color.red, 1);
		if (Physics.Raycast (physicsCentre, Vector3.down, out hit, 1.1f)) {
			if (hit.transform.gameObject.tag != "Player") {
				onGround = true;
			}
		}
		else 
		{
			onGround = false;
		}

		IsRunning ();


		if (onGround) 
		{
			float translation = Input.GetAxisRaw ("Vertical") * (speed*groundSpeed);
			float straffe = Input.GetAxisRaw ("Horizontal") * (speed*groundSpeed);
			translation *= Time.deltaTime;
			straffe *= Time.deltaTime;
			//transform.Translate(straffe, 0.0f, translation);

			Vector3 force = new Vector3 (straffe, 0.0f, translation);
			force = transform.localToWorldMatrix.MultiplyVector (force);
			rbPlayer.AddForce (force, ForceMode.VelocityChange);

			Vector3 v = rbPlayer.velocity;
			v.x = 0f;
			v.z = 0f;
			rbPlayer.velocity = v;

		} 
		else 
		{
			float translation = Input.GetAxis ("Vertical") * speed;
			float straffe = Input.GetAxis ("Horizontal") * speed;
			translation *= Time.deltaTime;
			straffe *= Time.deltaTime;
			//transform.Translate(straffe, 0.0f, translation);

			Vector3 force = new Vector3 (straffe, 0.0f, translation);
			force = transform.localToWorldMatrix.MultiplyVector (force);
			rbPlayer.AddForce (force, ForceMode.VelocityChange);

		}

		if ((Input.GetKeyDown ("space") || Input.GetButtonDown ("Fire2")) && onGround) 
		{
			//rbPlayer.AddForce (Vector3.up * jumpForce, ForceMode.Impulse);
			rbPlayer.velocity = new Vector3 (rbPlayer.velocity.x, jumpForce, rbPlayer.velocity.z);
		}

		if (Input.GetKeyDown("escape"))
		{
			Cursor.lockState = CursorLockMode.None;
		}
		Debug.Log (onGround);
	}

	private void OnCollisionStay (Collision coll)
	{
		foreach (ContactPoint contact in coll.contacts)
		{
			
			if ((Input.GetKeyDown ("space") || Input.GetButtonDown ("Fire2")) && contact.normal.y < angleAttaque)
			{
				Vector3 v = rbPlayer.velocity;
				v.y = 0f;
				rbPlayer.velocity = v;
				rbPlayer.velocity = new Vector3 (rbPlayer.velocity.x, jumpForce, rbPlayer.velocity.z);
				//Debug.DrawRay(contact.point, contact.normal, Color.white, 1.25f);
				rbPlayer.AddForce (contact.normal * (speed/wallJumpReduction), ForceMode.VelocityChange);
				//Debug.Log (contact.normal * speed);
			}
		}
	}

	private void OnCollisionEnter (Collision coll)
	{
		if (coll.gameObject.tag == "KillZone") 
		{
			Scene scene = SceneManager.GetActiveScene(); 
			SceneManager.LoadScene (scene.name);
		}
	}

	private void IsRunning()
	{
		if (Input.GetKey (KeyCode.LeftShift)) 
		{
			speed = runSpeed;
		} 
		else 
		{
			speed = walkSpeed;
		}
	}
}
