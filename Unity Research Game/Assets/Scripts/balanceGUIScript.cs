using UnityEngine;
using System.Collections;

public class balanceGUIScript : MonoBehaviour {
	
	/// <summary>
	/// Other GameObjects referenced to update balance GUI
	/// </summary>
	#region GameObject Variables
	public static string intermediateObjectName = "Intermediate";
	#endregion
	
	/// <summary>
	/// Textures swapped in to provide visual feedback on mastery of current exercise
	/// </summary> 
	#region GUI Textures
	public Texture2D balance0Tex;
	public Texture2D balance1Tex;
	public Texture2D balance2Tex;
	public Texture2D balance3Tex;
	public Texture2D balance4Tex;
	#endregion
	
	
	public void updateBalanceLevel () {
		//reference the translation layer's score value
		double currentScore = (double)GameObject.Find(intermediateObjectName).GetComponent<TranslationLayer>().exerciseScore;
		//reference the total number of poses in the exercise routine
		double poseCount = (double)GameObject.Find(intermediateObjectName).GetComponent<TranslationLayer>().keypointsList.Count;
		
		double balanceScore = currentScore/poseCount;
		
		//update the GUI
		if (balanceScore >= 0.90) {
			guiTexture.texture = balance4Tex;
		}
		else if (balanceScore >= 0.75) {
			guiTexture.texture = balance3Tex;
		}
		else if (balanceScore >= 0.5) {
			guiTexture.texture = balance2Tex;
		}
		else if (balanceScore >= 0.25) {
			guiTexture.texture = balance1Tex;
		}
		else {
			guiTexture.texture = balance0Tex;	
		}
	}
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		updateBalanceLevel();
	}
}
