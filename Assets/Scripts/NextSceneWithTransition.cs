using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NextSceneWithTransition : MonoBehaviour {
	private float timeFinished = 0.0f;
	private bool canGoToNext = false;
	public string nextScene;

	public GameObject guiNextLevel;
	public Text timer;
	public Text bestTimer;
	private string lvlName;

	public PlayerController meatBoy;

	private AudioSource source;
	public AudioClip exploSound;
	public AudioClip flailSound;

	public AudioClip forestSound;
	public AudioClip factorySound;
	// Use this for initialization
	void Start () {
		source = GetComponent<AudioSource>();
		lvlName = SceneManager.GetActiveScene ().name;
		if (SceneManager.GetActiveScene ().name.StartsWith("1.")) {
			source.PlayOneShot (forestSound, 0.5f);
		}
		if (SceneManager.GetActiveScene ().name.StartsWith("2.")) {

			source.PlayOneShot (factorySound, 0.5f);
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey(KeyCode.N) && Time.time - timeFinished > 0.5f && canGoToNext)
		{
			SceneManager.LoadScene (nextScene);
		}
		if (Input.GetKey(KeyCode.R) && Time.time - timeFinished > 0.5f && canGoToNext)
		{
			SceneManager.LoadScene (SceneManager.GetActiveScene().name);
			meatBoy.canMove = true;
			Cursor.lockState = CursorLockMode.Locked;
			Cursor.visible = false;
		}
	}

	void OnTriggerEnter(Collider coll)
	{
		if (coll.gameObject.tag == "Player")
		{
			guiNextLevel.SetActive(true);
			canGoToNext = true;
			timeFinished = Time.time;
			timer.text = meatBoy.timerSinceDeath.ToString ("F");
			if (meatBoy.timerSinceDeath < PlayerPrefs.GetFloat(lvlName, 999.0f))
			{
				PlayerPrefs.SetFloat (lvlName, meatBoy.timerSinceDeath);
			}
			bestTimer.text = PlayerPrefs.GetFloat(lvlName).ToString("F");
			meatBoy.canMove = false;
			meatBoy.rbPlayer.velocity = Vector3.zero;
			Cursor.lockState = CursorLockMode.None;
			Cursor.visible = true;
			meatBoy.cameraFar.SetActive (true);
			meatBoy.cameraBase.SetActive (false);

			source.PlayOneShot (exploSound, 0.5f);
			source.PlayOneShot (flailSound, 0.5f);
		}
	}
}