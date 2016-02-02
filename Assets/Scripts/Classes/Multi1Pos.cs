using UnityEngine;
using System.Collections;

[System.Serializable]
public class Multi1Pos
{
	public string Name;
	public float Multi1PosX;
	public float Multi1PosY;
	
	public Multi1Pos(string Name, float PosX, float PosY)
	{
		Name = Name;
		Multi1PosX = PosX;
		Multi1PosY = PosY;
	}
}
