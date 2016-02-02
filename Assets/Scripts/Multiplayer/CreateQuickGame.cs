using UnityEngine;
using System.Collections;
using GooglePlayGames;
using UnityEngine.SocialPlatforms;
using GooglePlayGames.BasicApi.Multiplayer;
using System.Collections.Generic;
using System;

public class CreateQuickGame : MonoBehaviour {

	public bool m_roomLoaded = false;
	public bool m_peerConnected = false;
	public float m_roomProgress = 0.0f;
	public bool m_messageReceived = false;
	public string m_message = "";
	private bool mWaitingForAuth = false;
	private bool mWaitingForRoom = false;
	private string mStatusText = "Ready.";
	float mRoomSetupStartTime = 0.0f;
	private uint MinOpponents = 1;
	private uint MaxOpponents = 7;
	private uint GameVariant = 0;
	
	private VersusListener vsListener;

	void OnGUI() {
		/*if (mWaitingForRoom) {
			mStatusText = "Loading room... " + vsListener.m_roomProgress.ToString() + "%";
		}*/
		
		GUILayout.Label(mStatusText);
		
		if (mWaitingForAuth) {
			return;
		}
		
		if (mWaitingForRoom) {
			if (GUILayout.Button("Cancel")) {
				// Cancel room loading!
				mStatusText = "Loading room cancelled.";
				mWaitingForRoom = false;
				PlayGamesPlatform.Instance.RealTime.LeaveRoom();
			}
			return;
		}
		
		if (Social.localUser.authenticated) {
			if (GUILayout.Button("Sign Out")) {
				// Sign out!
				mStatusText = "Signing out.";
				((PlayGamesPlatform)Social.Active).SignOut();
			}
			if (GUILayout.Button("Invitation Screen")) {
				mWaitingForRoom = true;
				PlayGamesPlatform.Instance.RealTime.CreateWithInvitationScreen(MinOpponents, MaxOpponents, GameVariant, vsListener);
			}
			if (GUILayout.Button ("Invitation Inbox")) {
				PlayGamesPlatform.Instance.RealTime.AcceptFromInbox(vsListener);
			}
		}
		else {
			if (GUILayout.Button("Authenticate")) {
				// Authenticate
				mWaitingForAuth = true;
				mStatusText = "Authenticating...";
				Social.localUser.Authenticate((bool success) => {
					mWaitingForAuth = false;
					mStatusText = success ? "Successfully authenticated" : "Authentication failed.";
				});
			}
		}
	}

	// Use this for initialization
	void Start () {
		vsListener = new VersusListener();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
