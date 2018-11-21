using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpChecker : MonoBehaviour {

	public static JumpChecker instanceJC;

	public GameObject playerColl;
	public PlayerController player;

	public bool jumpingAllowed = false;

	// Use this for initialization
	void Start () {
		JumpChecker.instanceJC = this;
		Physics.IgnoreCollision(playerColl.GetComponent<CapsuleCollider>(), GetComponent<CapsuleCollider>());
	}
	
	// Update is called once per frame
	void Update () {
		//Debug.Log ("in Jump Checker " + jumpingAllowed);
	}

	void OnTriggerStay (Collider other) 
	{
		if (other.gameObject.tag == "Jumpable") 
		{
			jumpingAllowed = true;
		}

		float j = 0f;
		for (int i = 0; i < 16; i++) 
		{
			Vector3 vectorDirection = Quaternion.AngleAxis (j * 22.5f, Vector3.up) * Vector3.forward;
			RaycastHit hit;
			if (Physics.Raycast (transform.position, vectorDirection, out hit, 1f))
			{
				Debug.DrawRay(hit.point, hit.normal * 0.1f, Color.red, 0.25f);
				if ((Input.GetKeyDown ("space") || Input.GetButtonDown ("Fire2")) && hit.normal.y < player.angleAttaque && !player.onGround)
				{
					Vector3 v = player.rbPlayer.velocity;
					v.y = 0f;
					player.rbPlayer.velocity = v;
					player.rbPlayer.velocity = new Vector3 (player.rbPlayer.velocity.x, player.jumpForce, player.rbPlayer.velocity.z);
					//Debug.DrawRay(contact.point, contact.normal, Color.white, 1.25f);
					player.rbPlayer.AddForce (hit.normal * (player.speed/player.wallJumpReduction), ForceMode.VelocityChange);
					//Debug.Log (contact.normal * speed);
					break;
				}
			}
			j += 1f;
		}
	}

	void OnTriggerExit (Collider other) 
	{
		if (other.gameObject.tag == "Jumpable") 
		{
			jumpingAllowed = false;
		}
	}

	/*private void OnCollisionStay (Collision coll)
	{
		foreach (ContactPoint contact in coll.contacts)
		{

			if ((Input.GetKeyDown ("space") || Input.GetButtonDown ("Fire2")) && contact.normal.y < player.angleAttaque && !player.onGround)
			{
				Vector3 v = player.rbPlayer.velocity;
				v.y = 0f;
				player.rbPlayer.velocity = v;
				player.rbPlayer.velocity = new Vector3 (player.rbPlayer.velocity.x, player.jumpForce, player.rbPlayer.velocity.z);
				//Debug.DrawRay(contact.point, contact.normal, Color.white, 1.25f);
				player.rbPlayer.AddForce (contact.normal * (player.speed/player.wallJumpReduction), ForceMode.VelocityChange);
				//Debug.Log (contact.normal * speed);
			}
			Debug.DrawRay(contact.point, contact.normal, Color.red, 1.25f);
		}
	}*/
}
