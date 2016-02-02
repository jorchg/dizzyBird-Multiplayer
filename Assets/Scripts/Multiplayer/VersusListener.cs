using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using GooglePlayGames.BasicApi.Multiplayer;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class VersusListener : RealTimeMultiplayerListener {

	public bool m_roomLoaded = false;
	public bool m_peerConnected = false;
	public float m_roomProgress = 0.0f;
	public float aux;
	public bool m_messageReceived = false;
	public string m_message = "";
	//public static dizzyBirdControllerFlappyMultiplayer.Multi1Pos Position;
	public float Position;
	private Multi1Pos MultiPos;

	//public static dizzyBirdControllerFlappyMultiplayer.Multi1Pos MultiPos;

	private static Multi1Pos Byte_ReadFromBinary_ToMulti1Pos(byte[] data)
	{
		if (data == null || data.Length == 0)
			return null;
		var ins = new MemoryStream(data);
		BinaryFormatter bf = new BinaryFormatter();
		return (Multi1Pos)bf.Deserialize(ins);
	}

	public static float Byte_ReadFromBinary(byte[] data)
	{
		if (data == null || data.Length == 0)
			return 0;
		var ins = new MemoryStream(data);
		BinaryFormatter bf = new BinaryFormatter();
		return (float)bf.Deserialize(ins);
	}

	public void OnRoomSetupProgress(float percent) {
		m_roomProgress = percent;
		Debug.Log("Versus Listener: Callback - " + System.Reflection.MethodBase.GetCurrentMethod().Name + " called");
		Debug.Log("Room Progress update: " + percent.ToString() + "%");
	}
	
	public void OnRoomConnected(bool success) {
		m_roomLoaded = success;
		if (success) {
			Application.LoadLevel (3);
		}
		Debug.Log("Versus Listener: Callback - " + System.Reflection.MethodBase.GetCurrentMethod().Name + " called");
	}
	
	public void OnLeftRoom() {
		m_roomLoaded = false;
		Debug.Log("Versus Listener: Callback - " + System.Reflection.MethodBase.GetCurrentMethod().Name + " called");
	}
	
	public void OnPeersConnected(string[] participantIds) {
		m_peerConnected = true;
		Debug.Log("Versus Listener: Callback - " + System.Reflection.MethodBase.GetCurrentMethod().Name + " called");
	}
	
	public void OnPeersDisconnected(string[] participantIds) {
		m_peerConnected = false;
		Debug.Log("Versus Listener: Callback - " + System.Reflection.MethodBase.GetCurrentMethod().Name + " called");
	}
	
	public void OnRealTimeMessageReceived(bool isReliable, string senderId, byte[] data) {
		//Position = Byte_ReadFromBinary(data);
		//MultiPos = Byte_ReadFromBinary_ToMulti1Pos (data);
		//Debug.Log ("Update X Position received: "+MultiPos.Multi1PosX);
		//Debug.Log ("Update Y Position received: "+MultiPos.Multi1PosY);
		MultiPos = Byte_ReadFromBinary_ToMulti1Pos (data);
		if (MultiPos.Name.Equals ("YPosition")) {
			Position = MultiPos.Multi1PosY;
		}
		Debug.Log ("Position received: " + Position);
		Debug.Log("Versus Listener: Callback - " + System.Reflection.MethodBase.GetCurrentMethod().Name + " called");
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
