using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {
	public CamMouseLook cam;
	public Text timer;
	[HideInInspector] public float timeLastDeath = 0f;
	[HideInInspector] public float timerSinceDeath = 0f;

	private bool isAlive = true;
	[HideInInspector]public bool canMove = true;
	private float justDied = 0f;
	public GameObject model3D;

	public GameObject cameraBase;
	public GameObject cameraFar;

	private Vector3 spawnPoint;

	public float airStraffe = 2.0f;
	public float walkSpeed = 10.0f;
	public float runSpeed = 30.0f;
	[HideInInspector] public float speed;
	private float fadeSpeed = 0.5f;

	public float wallJumpForce = 10.0f;
	public float jumpForce = 10.0f;
	public float jumpForceFade = 10.0f;
	public float maxTimeJump = 0.2f;
	private float actualTimeJump = 1.5f;
	private bool jumped = false;
	private bool doubleWalljumpCounter = false;

	private Vector3 previousPosition;
	public float airBufferDivider = 180f;

	public float groundSpeed = 30.0f;
	public float wallJumpReduction = 10.0f;
	public float speedDivisionStraffe = 2.0f;
	public Rigidbody rbPlayer;

	[Range (0.0f, 1.0f)]public float angleAttaque = 0.5f;

	[HideInInspector] public bool onGround = false;

	private ParticleSystem particuleSystem;

	private AudioSource source;
	public AudioClip deathSound;
	public AudioClip walkSound;
	public AudioClip walkSound2;
	public AudioClip walkSound3;
	public AudioClip jumpSound;
	public AudioClip landSound;
	public AudioClip spawnSound;

	private float walksoundPlayed;
	private float timeBetweenSteps;


	// Use this for initialization
	void Start () 
	{
		source = GetComponent<AudioSource>();
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
		rbPlayer = GetComponent<Rigidbody> ();
		speed = walkSpeed;
		spawnPoint = transform.position;
		previousPosition = transform.position;
		particuleSystem = GetComponent<ParticleSystem> ();
		timeLastDeath = Time.time;
		canMove = true;
		cameraBase.SetActive (true);
		cameraFar.SetActive (false);
		model3D.SetActive (true);
		Time.timeScale = 1;
		source.PlayOneShot (spawnSound, 1.0f);
	}

	void Update ()
	{
		if (canMove) 
		{
			Jumping ();

			WallJumping ();

			if (Input.GetKeyDown(KeyCode.R)) 
			{
				source.PlayOneShot (deathSound, 1.0f);
				particuleSystem.Play ();
				rbPlayer.velocity = Vector3.zero;
				justDied = Time.time;
				isAlive = false;
				canMove = false;
				cameraBase.SetActive (false);
				cameraFar.SetActive (true);
				model3D.SetActive (false);
			}
		}


		if (Time.time - justDied > 1.0f && !isAlive) 
		{
			source.PlayOneShot (spawnSound, 1.0f);
			transform.position = spawnPoint;
			cameraBase.SetActive (true);
			cameraFar.SetActive (false);
			model3D.SetActive (true);
			timeLastDeath = Time.time;
			cam.mouseLook = new Vector2 (0f, 0f);
			justDied = 0f;
			isAlive = true;
			canMove = true;
		}
	}

	// Update is called once per frame
	void FixedUpdate ()
	{
		GroundChecking ();

		IsRunning ();

		if (canMove) 
		{
			Moving ();
		}

		timerSinceDeath = Time.time - timeLastDeath;
		//timerSinceDeath = Mathf.Round (timerSinceDeath);
		timer.text = timerSinceDeath.ToString("F");


		//Debug.Log (jumpingCollider.jumpingAllowed);
	}

	private void OnCollisionEnter (Collision coll)
	{
		if (coll.gameObject.tag == "KillZone" && isAlive) 
		{
			source.PlayOneShot (deathSound, 1.0f);
			particuleSystem.Play ();
			rbPlayer.velocity = Vector3.zero;
			justDied = Time.time;
			isAlive = false;
			canMove = false;
			cameraBase.SetActive (false);
			cameraFar.SetActive (true);
			model3D.SetActive (false);
		}
	}

	private void IsRunning()
	{
		if (Input.GetKey (KeyCode.LeftShift)) 
		{
			speed = runSpeed;
			timeBetweenSteps = 0.25f;
		} 
		else 
		{
			speed = walkSpeed;
			timeBetweenSteps = 0.45f;
		}
	}

	private void GroundChecking()
	{
		RaycastHit hit;
		Vector3 physicsCentre = this.transform.position + this.GetComponent<CapsuleCollider> ().center;

		bool wasOnGround;
		if (onGround) 
		{
			wasOnGround = true;
		} 
		else 
		{
			wasOnGround = false;
		}

		float j = 0.0f;
		for (int i = 0; i < 16; i++) 
		{
			Vector3 rayStartPoint = physicsCentre + new Vector3 ((Mathf.Cos(j * (Mathf.PI/8)) * 0.42f), 0f, (Mathf.Sin(j * (Mathf.PI/8)) * 0.42f));
			Debug.DrawRay (rayStartPoint, Vector3.down*0.6f, Color.red, 0.25f);

			if (Physics.Raycast (rayStartPoint, Vector3.down, out hit, 0.6f)) {
				if (hit.transform.gameObject.tag != "Player") {
					onGround = true;
					break;
				}
			}
			else 
			{
				onGround = false;
			}

			j += 1.0f;
		}

		if (!wasOnGround && onGround) 
		{
			source.PlayOneShot (landSound, 0.8f);
		}

	}

	private void Moving ()
	{
		float translation = 0f;
		float straffe = 0f;

		if (onGround) 
		{
			translation = Input.GetAxisRaw ("Vertical") * ((speed * fadeSpeed) * groundSpeed);
			straffe = Input.GetAxisRaw ("Horizontal") * (((speed * fadeSpeed)*groundSpeed) / speedDivisionStraffe);
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

			if (fadeSpeed < 1.1f)
			{
				fadeSpeed += 0.1f;
			}

			if (translation == 0 && straffe == 0 )
			{
				fadeSpeed = 0.5f;
			}

			if ((translation != 0f || straffe != 0f) && Time.time - walksoundPlayed > timeBetweenSteps)
			{
				int soundChoice = Random.Range (1, 4);
				if (soundChoice == 1) {
					source.PlayOneShot (walkSound, Random.Range(0.10f, 0.25f));
				} else if (soundChoice == 2) {
					source.PlayOneShot (walkSound2, Random.Range(0.10f, 0.25f));
				} else {
					source.PlayOneShot (walkSound3, Random.Range(0.10f, 0.25f));
				}
				walksoundPlayed = Time.time;
			}
		} 
		else 
		{
			translation = Input.GetAxis ("Vertical") * speed;
			straffe = Input.GetAxis ("Horizontal") * speed * airStraffe;
			translation *= Time.deltaTime;
			straffe *= Time.deltaTime;
			float airControlBuffer = CalculateAirControlBuffer (translation, straffe);
			Vector3 force = new Vector3 (straffe * airControlBuffer, 0.0f, translation * airControlBuffer);
			force = transform.localToWorldMatrix.MultiplyVector (force);
			rbPlayer.AddForce (force, ForceMode.VelocityChange);
		}

		previousPosition = transform.position;
	}

	private float CalculateAirControlBuffer (float translation, float straffe)
	{
		Vector3 currentDirection = transform.position - previousPosition;
		currentDirection = new Vector3 (currentDirection.x, 0f, currentDirection.z);
		Vector3 direction = new Vector3 (straffe, 0f, translation);
		direction = transform.TransformDirection (direction);
		float angle = Vector3.Angle (currentDirection, direction);
		if (angle > 30) {
			float airControlBuffer = 1 + (angle / airBufferDivider);
			//Debug.Log (angle);
			return airControlBuffer;
		}
		return 1.0f;
	}
		
	private void Jumping()
	{
		//Jump nuancé
		if ((Input.GetKeyDown ("space") || Input.GetButtonDown ("Fire2")) && onGround) 
		{
			//rbPlayer.AddForce (new Vector3 (0, jumpForce, 0), ForceMode.Impulse);
			rbPlayer.velocity = new Vector3 (rbPlayer.velocity.x, jumpForce, rbPlayer.velocity.z);
			actualTimeJump = Time.time;
			jumped = true;
			source.PlayOneShot (jumpSound, 1.0f);
		}

		if ((Input.GetKey ("space") || Input.GetButton ("Fire2")) && Time.time-actualTimeJump < maxTimeJump)
		{
			//rbPlayer.velocity += new Vector3 (0, jumpForceHold, 0);
			rbPlayer.velocity = new Vector3 (rbPlayer.velocity.x, (jumpForce - (jumpForceFade*(Time.time-actualTimeJump))), rbPlayer.velocity.z);
		}

		if ((Input.GetKeyUp("space") || Input.GetButtonUp("Fire2")) && Time.time-actualTimeJump < maxTimeJump && jumped) 
		{
			rbPlayer.velocity = new Vector3 (rbPlayer.velocity.x, 0f, rbPlayer.velocity.z);
			jumped = false;
		}

		// Jump binaire
		/*if ((Input.GetKeyDown ("space") || Input.GetButtonDown ("Fire2")) && onGround) 
		{
			//rbPlayer.AddForce (new Vector3 (0, jumpForce, 0), ForceMode.Impulse);
			rbPlayer.velocity = new Vector3 (rbPlayer.velocity.x, jumpForceLow, rbPlayer.velocity.z);
			actualTimeJump = Time.time;
			maxJumped = false;
		}*/

		/*if ((Input.GetKey ("space") || Input.GetButton ("Fire2")) && Time.time-actualTimeJump > maxTimeJump && !maxJumped)
		{
			rbPlayer.velocity = new Vector3 (rbPlayer.velocity.x, jumpForce, rbPlayer.velocity.z);
			maxJumped = true;
		}*/
	}

	private void WallJumping()
	{
		float j = 0f;
		for (int i = 0; i < 16; i++) 
		{
			Vector3 vectorDirection = Quaternion.AngleAxis (j * 22.5f, Vector3.up) * Vector3.forward;
			RaycastHit hit;
			if (Physics.Raycast (transform.position, vectorDirection, out hit, 1f)) 
			{
				//Debug.DrawRay(hit.point, hit.normal * 0.1f, Color.red, 0.25f);
				if ((Input.GetKeyDown ("space") || Input.GetButtonDown ("Fire2")) && hit.normal.y < angleAttaque && !onGround && !doubleWalljumpCounter) 
				{
					Vector3 v = rbPlayer.velocity;
					v.y = 0f;
					rbPlayer.velocity = v;
					rbPlayer.velocity = new Vector3 (rbPlayer.velocity.x, wallJumpForce, rbPlayer.velocity.z);
					rbPlayer.AddForce (hit.normal * (speed / wallJumpReduction), ForceMode.VelocityChange);
					doubleWalljumpCounter = true;
					source.PlayOneShot (jumpSound, 1.0f);
					break;
				}
			} 
			else 
			{
				doubleWalljumpCounter = false;
			}
			j += 1f;
		}
	}
}
