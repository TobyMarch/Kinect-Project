using UnityEngine;
using System.Collections;

public class BDGameScript : MonoBehaviour {
	
	#region GameObject Variable Names
	public static string movementControllerScriptName = "BDAutoMove";
	public string intermediateObjectName = "Intermediate";
	public static string PlayerCharacterName = "Third Person PC";
	#endregion
	#region GameObject References
	/// <summary>
	/// Reference to the intermediate object, which handles translation between the Kinect and Unity
	/// </summary>
	private GameObject intermediateObject;
	/// <summary>
	/// Reference to the player character - NOT the avatar that matches the user's movements,
	/// but the character that is actually running around in-game
	/// </summary>
	private GameObject playerCharacter;
	
	/// <summary>
	/// GUIText to display the player's current score
	/// </summary>
	public GUIText coinCounter;
	/// <summary>
	/// GUIText to alert the user to their score updating
	/// </summary>
	public GUIText counterUpdate;
	#endregion
	#region private variables
	/// <summary>
	/// Bool value used to ensure that the game only calls LFNBG() while game is actually in session
	/// </summary>
	private bool continueGame;
	/// <summary>
	/// Bool value set true IFF the player holds the current exercise pose
	/// </summary>
	private bool poseComplete;
	
	private int updateTimeRemaining;
	
	
	
	/// <summary>
	/// Int value holding the player's current score
	/// </summary>
	private int playerScore;
	#endregion
	#region Public Variables
	public int coinValue = 1;
	public int chestValue = 10;
	public int updateDisplayTime = 20;
	#endregion
	
	#region Score Functions
	/// <summary>
	/// Called whenever the player character picks up a coin. Called by CoinScript
	/// </summary>
	public void CoinCollected () {
		playerScore += coinValue;
		counterUpdate.text += "+" + coinValue + "\n";
		updateTimeRemaining = updateDisplayTime;
		counterUpdate.enabled = true;
	}
	
	public void ChestCollected () {
		playerScore += chestValue;
		counterUpdate.text += "+" + chestValue + "\n";
		updateTimeRemaining = updateDisplayTime;
		counterUpdate.enabled = true;
	}
	
	void UpdateScore () {
		//Consistently update the score counter
		coinCounter.text = "Score: " + playerScore.ToString();
		if (updateTimeRemaining >= 0) {
			updateTimeRemaining--;
		}
		else {
			counterUpdate.enabled = false;
			counterUpdate.text = "";
		}
	}
	#endregion
	
	#region Gameplay and Pose-Related Functions
	/// <summary>
	/// Triggers the translation layer to display the next pose and begin listening for user movement
	/// </summary>
	public void TriggerNextPose() {
		if (continueGame) {
			//Debug.Log("Calling next pose from BDGS");
			//GameObject.Find(intermediateObjectName).GetComponent<TranslationLayer>().ListenForNextWholeBodyGesture();
			intermediateObject.GetComponent<TranslationLayer>().ListenForNextWholeBodyGesture();
		}
	}
	/// <summary>
	/// Checks the 'poseComplete' flag
	/// </summary>
	/// <returns>
	/// True if the user has met the current pose within the time limit, else False
	/// </returns>
	public bool GetPoseCompletion () {
		//return poseComplete;
		return intermediateObject.GetComponent<TranslationLayer>().heldCurrentPose;
	}
	
	/// <summary>
	/// Sets the poseComplete flag.
	/// </summary>
	/// <param name='poseCompletionIn'>
	/// Pose completion in.
	/// </param>
	public void SetPoseCompletion (bool poseCompletionIn) {
		poseComplete = poseCompletionIn;
	}
	/// <summary>
	/// Starts the game. Called by WholeBodyListenButtonPressed() in ButtonScript
	/// </summary>
	public void StartGame () {
		//GameObject.Find(PlayerCharacterName).GetComponent<BDAutoMove>().startMoving();
		playerCharacter.GetComponent<BDAutoMove>().StartMoving();
		continueGame = true;
	}
	/// <summary>
	/// Ends the game. Called in TranslationLayer once the system has reached the end of the pose list
	/// </summary>
	public void EndGame () {
		continueGame = false;
		//GameObject.Find(PlayerCharacterName).GetComponent<BDAutoMove>().stopMoving();
		playerCharacter.GetComponent<BDAutoMove>().StopMoving();
	}
	#endregion
	void Awake () {
		intermediateObject = GameObject.Find(intermediateObjectName);
		playerCharacter = GameObject.Find(PlayerCharacterName);
		playerScore = 0;
	}
	
	// Use this for initialization
	// GameObject pointers should be initialized here, rather than repeatedly using find() 
	/*void Start () {
		intermediateObject = GameObject.Find(intermediateObjectName);
		playerCharacter = GameObject.Find(PlayerCharacterName);
		playerScore = 0;
	}*/
	
	// Update is called once per frame
	void Update () {
		UpdateScore();
	}
}
