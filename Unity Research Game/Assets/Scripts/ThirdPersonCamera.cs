/// <summary>
/// UnityTutorials - A Unity Game Design Prototyping Sandbox
/// <copyright>(c) John McElmurray and Julian Adams 2013</copyright>
/// 
/// UnityTutorials homepage: https://github.com/jm991/UnityTutorials
/// 
/// This software is provided 'as-is', without any express or implied
/// warranty.  In no event will the authors be held liable for any damages
/// arising from the use of this software.
///
/// Permission is granted to anyone to use this software for any purpose,
/// and to alter it and redistribute it freely, subject to the following restrictions:
///
/// 1. The origin of this software must not be misrepresented; you must not
/// claim that you wrote the original software. If you use this software
/// in a product, an acknowledgment in the product documentation would be
/// appreciated but is not required.
/// 2. Altered source versions must be plainly marked as such, and must not be
/// misrepresented as being the original software.
/// 3. This notice may not be removed or altered from any source distribution.
/// </summary>
using UnityEngine;
using System.Collections;

public class ThirdPersonCamera : MonoBehaviour {
	
	#region Variables (private)
	[SerializeField]
	private float distanceAway;
	[SerializeField]
	private float distanceUp;
	[SerializeField]
	private float smooth;
	[SerializeField]
	private Transform follow;
	private Vector3 targetPosition;
	private GameObject thirdPersonCharacter;
	
	#endregion
	
	#region Properties (public)
	#endregion
	
	#region Unity event functions
	
	
	
	// Use this for initialization
	void Start () {
		follow = GameObject.FindWithTag("Player").transform;
		thirdPersonCharacter = GameObject.Find("Third Person PC");
		Debug.Log("TPC: " + thirdPersonCharacter.name);
		//follow = thirdPersonCharacter.transform;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	// LateUpdate is called once per frame, but after the regular update has completed
	void LateUpdate () {
		//setting the target position to be correct offset from the PC
		targetPosition = follow.position + follow.up * distanceUp - follow.forward * distanceAway;
		Debug.DrawRay(follow.position, Vector3.up * distanceUp, Color.red);
		Debug.DrawRay(follow.position, -1f * follow.forward * distanceAway, Color.blue);
		Debug.DrawLine(follow.position, targetPosition, Color.magenta);
		
		// making a smooth transition between its current position and the position it wants to be in
		transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * smooth);
		
		// make sure the camera is looking the right way!
		if (thirdPersonCharacter.GetComponent<BDAutoMove>().getDirectCameraFollow()) {
			transform.LookAt(follow);
		}
	}
	#endregion
}
