using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour {
	public GameObject guiPause;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown("escape"))
		{
			if (Time.timeScale == 1) 
			{
				guiPause.SetActive (true);
				ChangeTime ();
				Cursor.lockState = CursorLockMode.None;
				Cursor.visible = true;
			} 
			else 
			{
				guiPause.SetActive (false);
				ChangeTime ();
				Cursor.lockState = CursorLockMode.Locked;
				Cursor.visible = false;
			}
		}
	}

	public void ChangeTime()
	{
		if (Time.timeScale == 1) 
		{
			Time.timeScale = 0;
		} 
		else 
		{
			Time.timeScale = 1;
		}
	}

	public void SetCursorInvisible()
	{
		Cursor.visible = false;
	}
}
