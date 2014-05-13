using UnityEngine;
using System.Collections;

/// <summary>
/// Script containing the majority of BoulderDash game logic, including score calculation
/// Author: Tobias March
/// Date: March-May 2014
/// </summary>
public class BDGameScript : MonoBehaviour {
	
	#region GameObject Variable Names
	/// <summary>
	/// Public string storing the name of the player character's movement script. Used for GameObject.Find() calls
	/// </summary>
	public static string movementControllerScriptName = "BDAutoMove";
	
	/// <summary>
	/// Public string storing the name of the intermediate object. Used for GameObject.Find() calls
	/// </summary>/
	public string intermediateObjectName = "Intermediate";
	
	/// <summary>
	/// Public string storing the name of the player character object. Used for GameObject.Find() calls
	/// </summary>
	public static string PlayerCharacterName = "Third Person PC";
	#endregion
	#region GameObject References
	/// <summary>
	/// Public GameObject reference to the intermediate object, which handles translation between the Kinect and Unity
	/// </summary>
	private GameObject intermediateObject;
	
	/// <summary>
	/// Public GameObject reference to the player character - NOT the avatar that matches the user's movements,
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
	
	/// <summary>
	/// Private int used to time how long score updates stay on the screen
	/// </summary>
	private int updateTimeRemaining;
	
	/// <summary>
	/// Int value holding the player's current score
	/// </summary>
	private int playerScore;
	#endregion
	#region Public Variables
	/// <summary>
	/// Public int holding the score value of a coin
	/// </summary>
	public int coinValue = 1;
	
	/// <summary>
	/// Public int holding the score value of a treasure chest.
	/// </summary>
	public int chestValue = 10;
	
	/// <summary>
	/// Public int corresponding to how long (in frames) score updates stay on the screen.
	/// </summary>
	public int updateDisplayTime = 20;
	#endregion
	
	#region Score Functions
	/// <summary>
	/// Called whenever the player character picks up a coin. Called by CoinScript
	/// </summary>
	public void CoinCollected () {
		//Update score
		playerScore += coinValue;
		//Displays a message telling the user that their score has increased
		counterUpdate.text += "+" + coinValue + "\n";
		updateTimeRemaining = updateDisplayTime;
		counterUpdate.enabled = true;
	}
	
	/// <summary>
	/// Called whenever the player picks up a treasure chest.  Called by TreasureChestScript
	/// </summary>
	public void ChestCollected () {
		//Update score
		playerScore += chestValue;
		//Displays a message telling the user that their score has increased
		counterUpdate.text += "+" + chestValue + "\n";
		updateTimeRemaining = updateDisplayTime;
		counterUpdate.enabled = true;
	}
	
	/// <summary>
	/// Displays the player score and update values.
	/// </summary>
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
		playerCharacter.GetComponent<BDAutoMove>().StartMoving();
		continueGame = true;
	}
	/// <summary>
	/// Ends the game. Called in TranslationLayer once the system has reached the end of the pose list
	/// </summary>
	public void EndGame () {
		continueGame = false;
		playerCharacter.GetComponent<BDAutoMove>().StopMoving();
	}
	#endregion
	/// <summary>
	/// used for intialization.
	/// </summary>
	void Awake () {
		intermediateObject = GameObject.Find(intermediateObjectName);
		playerCharacter = GameObject.Find(PlayerCharacterName);
		playerScore = 0;
	}
	
	/// <summary>
	/// Update is called once per frame
	/// </summary>
	void Update () {
		UpdateScore();
	}
}
