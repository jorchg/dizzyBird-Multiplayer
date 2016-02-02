using UnityEngine;
using System.Collections;

public class RetryLevelFlappyMultiplayer : MonoBehaviour {

	public void OnClick () {
		Application.LoadLevel ("GameSceneFlappyMultiplayer");
	}

	public void OnNoClick () {
		Application.LoadLevel ("MainScene");
	}

	// Use this for initialization
	void Start () {
	
	}

	void Update () {

	}
}
