using UnityEngine;
using System.Collections;

public class BDGameScript : MonoBehaviour {
	
	#region GameObject Variables
	public static string movementControllerScriptName = "BDAutoMove";
	
	public static string PlayerCharacterName = "Third Person PC";
	#endregion
	/// <summary>
	/// Starts the game. Called by WholeBodyListenButtonPressed() in ButtonScript
	/// </summary>
	public void startGame() {
		GameObject.Find(PlayerCharacterName).GetComponent<BDAutoMove>().startMoving();
	}
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
