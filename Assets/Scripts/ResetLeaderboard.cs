using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetLeaderboard : MonoBehaviour {

	// Use this for initialization
	public void ResetLeaderBoard ()
	{
		PlayerPrefs.DeleteAll ();
	}
}
