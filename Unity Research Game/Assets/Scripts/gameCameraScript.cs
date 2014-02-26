using UnityEngine;
using System.Collections;

public class gameCameraScript : MonoBehaviour {
	
	#region GameObject Variables
	public static string intermediateObjectName = "Intermediate";
	public static string mainCameraObjectName = "MainCamera";
	/// <summary>
    /// Name of the GameObject to which ZigSkeleton script is attached
    /// </summary>
    public static string avatarGameObjectName = "Dana";
	#endregion
	
	#region Script-Specific Variables
	/// <summary>
	/// Number of scenes in the current level
	/// </summary>
	public static int endScene = 3;
	/// <summary>
	/// Threshold for the percent of completed poses necessary to 'clear' the scene and proceed
	/// </summary>
	public static double successThreshold = 0.6;
	/// <summary>
	/// Marker for the current scene within the level
	/// </summary>
	int currentScene = 0;
	/// <summary>
	/// Bool used to trigger complete scene animations
	/// </summary>
	public bool sceneSuccess = false;
	/// <summary>
	/// Default rotation of the kinect-driven zig skeleton.
	/// Used to orient the camera.
	/// </summary>
	Vector3 zigSkeletonRotation = new Vector3 ((float)12.63214,(float)192.0243,(float)357.5157);
	/// <summary>
	/// Array of positions within the world corresponding to different scenes.
	/// Used in transitions.
	/// </summary>
	Vector3[] camPositionArr = new [] {
		new Vector3((float)284.4079,(float)12.25069,(float)304.6943),
		new Vector3((float)-188.2264,(float)15.55525,(float)382.9391),
		new Vector3((float)28.18323,(float)19.69827,(float)88.96786),
		new Vector3((float)-109.361,(float)522.8292,(float)-362.8252)
	};
	
	string[] instructionArr = new string[] {
		"Build a bridge!", "Prop up the rock before it falls!",
		"Release the river!", "Throw the boulder!"
	};

	#endregion
	IEnumerator exampleWait() {
		yield return new WaitForSeconds(5);
	}
	
	
	/// <summary>
	/// Handles looping through the pose list at each scene, determines whether player advances
	/// </summary>
	public void checkProgression () {
		//reference the translation layer's score value
		double currentScore = (double)GameObject.Find(intermediateObjectName).GetComponent<TranslationLayer>().exerciseScore;
		//reference the total number of poses in the exercise routine
		double poseCount = (double)GameObject.Find(intermediateObjectName).GetComponent<TranslationLayer>().keypointsList.Count;
		//determine the user's current decimal score for this exercise
		double balanceScore = currentScore/poseCount;
		

		//Checks whether the player has met the success threshold for the scene
		if(balanceScore >= successThreshold) {
			//If the player hasn't reached the end of the level, load the next scene
			if(currentScene < endScene) {

				currentScene += 1;
				GameObject.FindGameObjectWithTag(mainCameraObjectName).transform.position = camPositionArr[currentScene];
				
				//Keep Avatar rotation constant to keep it in line with the Kinect input
				GameObject.FindGameObjectWithTag(mainCameraObjectName).transform.eulerAngles = zigSkeletonRotation;
				//Re-start the list of poses, and set local scores back to 0
				GameObject.Find(intermediateObjectName).GetComponent<TranslationLayer>().StartListeningForWholeBodyGesture();
			}
			//If the player has reached the end of the level, congratulations are in order
			else {
				GameObject.Find (intermediateObjectName).GetComponent<TranslationLayer>().feedbackText1.text = 
					"Level Complete!";
			}
		}
		else {
			GameObject.Find(intermediateObjectName).GetComponent<TranslationLayer>().feedbackText1.text = 
				(balanceScore*100)+"% Complete - Better Luck Next Time!";
		}
	}
	/// <summary>
	/// Controls conditional animation for all GameObjects in this level 
	/// </summary>
	public void animator() {
		//reference the translation layer's score value
		double currentScore = (double)GameObject.Find(intermediateObjectName).GetComponent<TranslationLayer>().exerciseScore;
		//reference the total number of poses in the exercise routine
		double poseCount = (double)GameObject.Find(intermediateObjectName).GetComponent<TranslationLayer>().keypointsList.Count;
		//determine the user's current decimal score for this exercise
		double balanceScore = currentScore/poseCount;
		
		//There may be an elegant way to conditionally animate GameObjects. This isn't it.
		switch (currentScene) {
		//for each scene, animate objects based on the player's balanceScore
		case 0:
			//Scene 0 involves moving a series of rocks up through the water,
			//referencing each rock in turn and applying conditional animation
			if (balanceScore >= 0.20) {
				if (GameObject.Find("SteppingStones0").transform.position.y < 5.427169) {
					GameObject.Find("SteppingStones0").transform.Translate(0,(float)0.04,0,Space.World);
				}
			}
			if (balanceScore >= 0.40) {
				if (GameObject.Find("SteppingStones1").transform.position.y < 5.427169) {
					GameObject.Find("SteppingStones1").transform.Translate(0,(float)0.04,0,Space.World);
				}
			}
			if (balanceScore >= 0.60) {
				if (GameObject.Find("SteppingStones2").transform.position.y < 5.92718) {
					GameObject.Find("SteppingStones2").transform.Translate(0,(float)0.04,0,Space.World);
				}
				if (!sceneSuccess) {
					sceneSuccess = true;
				}
			}
			if (balanceScore >= 0.80 || sceneSuccess) {
				if (GameObject.Find("SteppingStones3").transform.position.y < 6.427166
					&& GameObject.Find("SteppingStones2").transform.position.y >= 5.9) {
					GameObject.Find("SteppingStones3").transform.Translate(0,(float)0.04,0,Space.World);
				}
			}
			if (balanceScore >= 0.90 || sceneSuccess) {
				if (GameObject.Find("SteppingStones4").transform.position.y < 7.427169
					&& GameObject.Find("SteppingStones3").transform.position.y >= 6.4) {
					GameObject.Find("SteppingStones4").transform.Translate(0,(float)0.04,0,Space.World);
				}
			}
			break;
		case 1:
			//Scene 1 involves animating one GameObject to different heights
			if (balanceScore >= 0.20) {
				if (GameObject.Find("ScaffoldingStones").transform.position.y < 10.0) {
					GameObject.Find("ScaffoldingStones").transform.Translate(0,(float)0.04,0,Space.World);
				}
			}
			if (balanceScore >= 0.40) {
				if (GameObject.Find("ScaffoldingStones").transform.position.y < 11.0) {
					GameObject.Find("ScaffoldingStones").transform.Translate(0,(float)0.04,0,Space.World);
				}
			}
			if (balanceScore >= 0.60) {
				if (GameObject.Find("ScaffoldingStones").transform.position.y < 12.0) {
					GameObject.Find("ScaffoldingStones").transform.Translate(0,(float)0.04,0,Space.World);
				}
				if (!sceneSuccess) {
					sceneSuccess = true;
				}
			}
			if (balanceScore >= 0.80 || sceneSuccess) {
				if (GameObject.Find("ScaffoldingStones").transform.position.y < 13.0) {
					GameObject.Find("ScaffoldingStones").transform.Translate(0,(float)0.02,0,Space.World);
				}
			}
			if (balanceScore >= 0.90 || sceneSuccess) {
				if (GameObject.Find("ScaffoldingStones").transform.position.y < 14.95647) {
					GameObject.Find("ScaffoldingStones").transform.Translate(0,(float)0.02,0,Space.World);
				}
			}
			break;
		case 2:
			//Scene 2 involves animating multiple GameObjects at once
			if (balanceScore >= 0.20) {
				if (GameObject.Find("RiverWaterBlocked").transform.position.y < -10.0) {
					GameObject.Find("RiverWaterBlocked").transform.Translate(0,(float)0.1,0,Space.World);
				}
				if (GameObject.Find("DamRock30").transform.position.y > 1.0) {
					GameObject.Find("DamRock30").transform.Translate((float)-0.1,(float)-0.1,0,Space.World);
				}
			}
			if (balanceScore >= 0.40) {
				if (GameObject.Find("RiverWaterBlocked").transform.position.y < -6.0) {
					GameObject.Find("RiverWaterBlocked").transform.Translate(0,(float)0.1,0,Space.World);
				}
				if (GameObject.Find("RiverWaterMain").transform.position.y > 6.5) {
					GameObject.Find("RiverWaterMain").transform.Translate(0,(float)-0.01,0,Space.World);
				}
				if (GameObject.Find("DamRock33").transform.position.y > 4.0) {
					GameObject.Find("DamRock33").transform.Translate((float)-0.1,(float)-0.1,0,Space.World);
				}
			}
			if (balanceScore >= 0.60) {
				if (GameObject.Find("RiverWaterBlocked").transform.position.y < -2.0) {
					GameObject.Find("RiverWaterBlocked").transform.Translate(0,(float)0.1,0,Space.World);
				}
				if (GameObject.Find("RiverWaterMain").transform.position.y > 6.0) {
					GameObject.Find("RiverWaterMain").transform.Translate(0,(float)-0.01,0,Space.World);
				}
				if (GameObject.Find("DamRock33").transform.position.y > 1.0) {
					GameObject.Find("DamRock33").transform.Translate((float)-0.1,(float)-0.1,0,Space.World);
				}
				if (!sceneSuccess) {
					sceneSuccess = true;
				}
			}
			if (balanceScore >= 0.80 || sceneSuccess) {
				if (GameObject.Find("RiverWaterBlocked").transform.position.y < 2.0) {
					GameObject.Find("RiverWaterBlocked").transform.Translate(0,(float)0.025,0,Space.World);
				}
				if (GameObject.Find("RiverWaterMain").transform.position.y > 5.5) {
					GameObject.Find("RiverWaterMain").transform.Translate(0,(float)-0.01,0,Space.World);
				}
				if (GameObject.Find("DamRock32").transform.position.y > 0.0) {
					GameObject.Find("DamRock32").transform.Translate((float)-0.1,(float)-0.05,0,Space.World);
				}
			}
			if (balanceScore >= 0.90 || sceneSuccess) {
				if (GameObject.Find("RiverWaterBlocked").transform.position.y < 5.0) {
					GameObject.Find("RiverWaterBlocked").transform.Translate(0,(float)0.025,0,Space.World);
				}
				if (GameObject.Find("RiverWaterMain").transform.position.y > 5.0) {
					GameObject.Find("RiverWaterMain").transform.Translate(0,(float)-0.01,0,Space.World);
				}
				if (GameObject.Find("DamRock32").transform.position.y > 0.0) {
					GameObject.Find("DamRock32").transform.Translate((float)-0.1,(float)-0.05,0,Space.World);
				}
			}
			break;
		case 3:
			//Scene 3 involves animating one GameObject across multiple axes
			if (balanceScore >= 0.20) {
				if (GameObject.Find("LargeRock").transform.position.y < 522.0) {
					GameObject.Find("LargeRock").transform.Translate(0,(float)0.01,(float)-0.043,Space.World);
				}
			}
			if (balanceScore >= 0.40) {
				if (GameObject.Find("LargeRock").transform.position.y > 522.0
					&& GameObject.Find("LargeRock").transform.position.y < 524.0) {
					GameObject.Find("LargeRock").transform.Translate((float)-0.01,(float)0.01,(float)-0.05,Space.World);
				}
			}
			if (balanceScore >= 0.60) {
				if (GameObject.Find("LargeRock").transform.position.y > 524.0
					&& GameObject.Find("LargeRock").transform.position.y < 537.0) {
					GameObject.Find("LargeRock").transform.Translate((float)0.0324,(float)0.02,(float)0.00408,Space.World);
				}
			}
			if (balanceScore >= 0.80) {
				if (GameObject.Find("LargeRock").transform.position.y > 537.0
					&& GameObject.Find("LargeRock").transform.position.y < 538.0) {
					GameObject.Find("LargeRock").transform.Translate((float)0.04,(float)0.01,0,Space.World);
				}
			}
			if (balanceScore >= 0.90) {
				if (GameObject.Find("LargeRock").transform.position.x > -137.0
					&& GameObject.Find("LargeRock").transform.position.y > 438.00) {
					GameObject.Find("LargeRock").transform.Translate((float)0.8,(float)-0.5,0,Space.World);
				}
			}
			break;
		default:
			break;
		}
	}

		
	/// <summary>
	/// Starts the level at the first scene
	/// </summary>
	public void resetProgression () {
		currentScene = 0;
		GameObject.FindGameObjectWithTag(mainCameraObjectName).transform.position = camPositionArr[currentScene];
		GameObject.FindGameObjectWithTag(mainCameraObjectName).transform.eulerAngles = zigSkeletonRotation;
		sceneSuccess = false;
		//GameObject.Find("Balance GUI").GetComponentInChildren<Renderer>().enabled = true;
		//GameObject.Find("Carl").GetComponentInChildren<Renderer>().enabled = true;
		//GameObject.Find("PoseScroll").GetComponentInChildren<Renderer>().enabled = true;
	}
	/// <summary>
	/// Runs inbetween scenes to reset key variables
	/// Could be expanded later to include animations
	/// </summary>
	public void SceneTransition () {
		sceneSuccess = false;
		//Give the user a hint as to how their powers can be used
		GameObject.Find (intermediateObjectName).GetComponent<TranslationLayer>().feedbackText1.text = 
					instructionArr[currentScene];
	}
	public void IntroScreen () {
		//Temporarily hide Game UI Elements
		GameObject.Find("Balance GUI").GetComponentInChildren<Renderer>().enabled = false;
		GameObject.Find("Carl").GetComponentInChildren<Renderer>().enabled = false;
		GameObject.Find("PoseScroll").GetComponentInChildren<Renderer>().enabled = false;
	}
	// Use this for initialization
	void Start () {
		IntroScreen ();
		SceneTransition();
	}
	// Update is called once per frame
	void Update () {
		animator();
	}
	///<summary>
	/// Unity specific event, which is fired regularly.
    /// Used for physics calculations.
    /// In this case, it is used to determine whether to continue progressing through the game
    /// and is synced to the TranslationLayer's tickEvent()
    /// </summary>
	void FixedUpdate() {
		
	}
}
