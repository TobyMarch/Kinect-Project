using UnityEngine;
using System.Collections;

public class BDGameScript : MonoBehaviour {
	
	#region GameObject Variables
	public static string movementControllerScriptName = "BDAutoMove";
	public string intermediateObjectName = "Intermediate";
	public static string PlayerCharacterName = "Third Person PC";
	#endregion
	#region public variables
	public bool continueGame;
	#endregion
	
	/// <summary>
	/// Starts the game. Called by WholeBodyListenButtonPressed() in ButtonScript
	/// </summary>
	public void startGame() {
		GameObject.Find(PlayerCharacterName).GetComponent<BDAutoMove>().startMoving();
		continueGame = true;
	}
	
	/// <summary>
	/// Triggers the translation layer to display the next pose and begin listening for user movement
	/// </summary>
	public void triggerNextPose() {
		if (continueGame) {
			Debug.Log("Calling next pose from BDGS");
			GameObject.Find(intermediateObjectName).GetComponent<TranslationLayer>().ListenForNextWholeBodyGesture();
		}
	}
	/// <summary>
	/// 
	/// </summary>
	public void endGame() {
		continueGame = false;
		GameObject.Find(PlayerCharacterName).GetComponent<BDAutoMove>().stopMoving();
	}
	
	// Use this for initialization
	// GameObject pointers should be initialized here, rather than repeatedly using find() 
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
