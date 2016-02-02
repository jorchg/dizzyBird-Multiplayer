﻿using UnityEngine;
using System.Collections;

public class dizzyBirdControllerFlappy : MonoBehaviour {

	public AudioSource[] audios;

	private float Xspeed = 2.5f;
	private float Yspeed = 5f;
	private bool gameOver = false;
	private bool OutOfBounds = false;
	private Animator dizzyBirdAnimator;

	private GameObject GameOverPanel;
	private GameObject dizzyBirdObj;

	private Component dizzyBirdFlappyTransform;

	IEnumerator OnCollisionEnter2D(Collision2D other) { //Mensaje de colisión con obstáculo
		NotificationCenter.DefaultCenter().PostNotification (this, "StopParallax", 0);
		NotificationCenter.DefaultCenter().PostNotification (this, "StopObstaclesGen", 0);
		NotificationCenter.DefaultCenter().PostNotification (this, "GameOver");
		dizzyBirdObj.GetComponents<AudioSource> ();
		audios[1].Play ();
		Debug.Log("Collision");
		rigidbody2D.velocity = new Vector2 (0, 0);
		rigidbody2D.gravityScale = 1f;

		gameOver = true;

		yield return new WaitForSeconds(1);
		GameOverPanel.SetActive (true);
		yield return new WaitForSeconds(1);
		dizzyBirdObj.SetActive (false);
	}


	void Awake () {
		NotificationCenter.DefaultCenter().PostNotification (this, "ResetScore", 0);
		dizzyBirdObj = GameObject.Find ("dizzyBirdFlappy");
		GameOverPanel = GameObject.Find ("GameOverPanel"); //Board de retry level
		dizzyBirdAnimator = GetComponent<Animator>();
		dizzyBirdObj.rigidbody2D.velocity = new Vector2 (Xspeed, 0);
		dizzyBirdObj.rigidbody2D.gravityScale = 0f;
		GameOverPanel.SetActive (false);
	}

	// Use this for initialization
	void Start () {
	}
	
	void FixedUpdate () {
		dizzyBirdAnimator.SetFloat ("YSpeed", rigidbody2D.velocity.y);
		if (gameOver == true) {
			dizzyBirdAnimator.SetBool ("GameOver", true);
		}
	}

	// Update is called once per frame
	void Update () {
		if (dizzyBirdObj.transform.position.y > 5.1f) {
			OutOfBounds = true;
		}
		else {
			OutOfBounds = false;
		}

		if (Input.GetMouseButtonDown (0) && gameOver == false && OutOfBounds == false) {
			audios[0].Play ();
			dizzyBirdObj.rigidbody2D.gravityScale = 1f;
			dizzyBirdObj.rigidbody2D.velocity = new Vector2 (Xspeed,Yspeed);
		}



		if (Input.GetKeyDown(KeyCode.Escape)) { Application.LoadLevel (0); }
	}
}
