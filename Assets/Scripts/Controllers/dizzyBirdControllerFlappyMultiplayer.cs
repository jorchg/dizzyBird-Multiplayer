using UnityEngine;
using System.Collections;
using System.Collections.Generic; 
using System.Runtime.Serialization.Formatters.Binary; 
using System.IO;
using GooglePlayGames;
using UnityEngine.SocialPlatforms;
using GooglePlayGames.BasicApi.Multiplayer;

public class dizzyBirdControllerFlappyMultiplayer : MonoBehaviour {

	private Multi1Pos MultiPos;
	private Multi1Pos dizzy1Pos;
	public byte[] PosToSend;
	public byte[] newPosition;
	public byte[] MultiClick;
	public byte[] newClick;

	private float Xspeed = 2.5f;
	private float Yspeed = 5f;
	private static VersusListener vsListener;
	
	private GameObject dizzyBirdObj;
	public static GameObject dizzyBirdObjMulti;

	private Object obj;

	private byte[] PositionX;
	private byte[] PositionY;
	private byte[] RotationZ;

	private static byte[] Byte_ConvertObjectToBinary_FromMulti1Pos(Multi1Pos obj)
	{
		MemoryStream o = new MemoryStream();
		BinaryFormatter bf = new BinaryFormatter();
		bf.Serialize(o, obj);
		return o.GetBuffer();
	}

	public static byte[] Byte_ConvertObjectToBinary(float obj)
	{
		MemoryStream o = new MemoryStream();
		BinaryFormatter bf = new BinaryFormatter();
		bf.Serialize(o, obj);
		return o.GetBuffer();
	}

	private static Multi1Pos Byte_ReadFromBinary(byte[] data)
	{
		if (data == null || data.Length == 0)
			return null;
		var ins = new MemoryStream(data);
		BinaryFormatter bf = new BinaryFormatter();
		return (Multi1Pos)bf.Deserialize(ins);
	}

	private void SendMessageMultiplayer() {
		bool reliable = true;

		PlayGamesPlatform.Instance.RealTime.SendMessageToAll(reliable, MultiClick);
	}

	//public void UpdateMulti1Position () {
	//	dizzyBirdObjMulti.rigidbody2D.velocity = new Vector2 (Xspeed, Yspeed);
	//	Debug.Log ("Updating Multi1 position");
	//}

	void Awake () {
		//PosToSend = new byte[2];
		vsListener = new VersusListener();
		NotificationCenter.DefaultCenter ().AddObserver (this, "UpdateMulti1Position");
		dizzyBirdObj = GameObject.Find ("dizzyOwn");
		dizzyBirdObjMulti = GameObject.Find ("dizzyMulti");
		dizzyBirdObj.rigidbody2D.gravityScale = 0f;
		dizzyBirdObjMulti.rigidbody2D.gravityScale = 0f;
		//dizzyBirdObjMulti.rigidbody2D.velocity = new Vector2 (Xspeed, 0f);
	}

	// Use this for initialization
	void Start () {
	}
	
	void FixedUpdate () {
		//aux = new Multi1Pos (dizzyBirdObj.transform.position.x, 
		//                     dizzyBirdObj.transform.position.y, 
		//                     dizzyBirdObj.transform.rotation.z);
		//PosToSend = Byte_ConvertObjectToBinary (aux);

		//MultiPos = new Multi1Pos (dizzyBirdObj.transform.position.x, 
		//                          	dizzyBirdObj.transform.position.y, 
		//                            dizzyBirdObj.transform.rotation.z);
		//PosToSend = Byte_ConvertObjectToBinary_FromMulti1Pos (MultiPos);
		//PosToSend = Byte_ConvertObjectToBinary (dizzyBirdObj.transform.position.y);

		//PosToSend = Byte_ConvertObjectToBinary (dizzyBirdObj.transform.position.y);
		dizzy1Pos = new Multi1Pos ("YPosition", 
		                           dizzyBirdObj.transform.position.x, 
		                           dizzyBirdObj.transform.position.y);
		PosToSend = Byte_ConvertObjectToBinary_FromMulti1Pos (dizzy1Pos);
		PlayGamesPlatform.Instance.RealTime.SendMessageToAll(false, PosToSend);
		//dizzyBirdObjMulti.transform.position = new Vector3 (dizzyBirdObjMulti.transform.position.x, 
		//                                                    vsListener.Position, 
		//                                                    dizzyBirdObjMulti.transform.position.z);
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown (0)) {
			dizzyBirdObj.rigidbody2D.gravityScale = 1f;
			dizzyBirdObj.rigidbody2D.velocity = new Vector2 (Xspeed, Yspeed);
			dizzyBirdObjMulti.rigidbody2D.velocity = new Vector2 (Xspeed, 0f);
		}

		//dizzyBirdObjMulti.transform.position = new Vector3 (dizzyBirdObjMulti.transform.position.x, 
		                                                    //VersusListener.Position, 
		                                                    //dizzyBirdObjMulti.transform.position.z);


		if (Input.GetKeyDown(KeyCode.Escape)) { Application.LoadLevel (0); }
	}
}
