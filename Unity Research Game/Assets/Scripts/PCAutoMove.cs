using UnityEngine;
using System.Collections;

public class PCAutoMove : MonoBehaviour {
	#region public variables
	[SerializeField]
	public static float moveSpeed = 10.0f;
	#endregion
	
	// generic movement function
	void autoMove () {
		transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
	}
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		autoMove();
		Debug.Log("Current Heading:" + transform.forward);
	}
}
