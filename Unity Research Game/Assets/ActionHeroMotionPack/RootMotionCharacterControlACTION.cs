		
using UnityEngine;
using System.Collections;


[AddComponentMenu("Mixamo/Demo/Root Motion Character")]
public class RootMotionCharacterControlACTION: MonoBehaviour
{
	public float turningSpeed = 90f;
	public RootMotionComputer computer;
	public CharacterController character;
	
	void Start()
	{
		// validate component references
		if (computer == null) computer = GetComponent(typeof(RootMotionComputer)) as RootMotionComputer;
		if (character == null) character = GetComponent(typeof(CharacterController)) as CharacterController;
		
		// tell the computer to just output values but not apply motion
		computer.applyMotion = false;
		// tell the computer that this script will manage its execution
		computer.isManagedExternally = true;
		// since we are using a character controller, we only want the z translation output
		computer.computationMode = RootMotionComputationMode.XZTranslation;
		// initialize the computer
		computer.Initialize();
		
		// set up properties for the animations
		animation["idle"].layer = 0; animation["idle"].wrapMode = WrapMode.Loop;
		animation["walk"].layer = 1; animation["walk"].wrapMode = WrapMode.Loop;
		animation["run"].layer = 1; animation["run"].wrapMode = WrapMode.Loop;
		animation["eaglejump"].layer = 3; animation["eaglejump"].wrapMode = WrapMode.Once;
		animation["injuredwalk"].layer = 3; animation["injuredwalk"].wrapMode = WrapMode.Once;
		animation["jumpaway"].layer = 3; animation["jumpaway"].wrapMode = WrapMode.Once;
		animation["jumpdown"].layer = 3; animation["jumpdown"].wrapMode = WrapMode.Once;
		animation["ladder"].layer = 3; animation["ladder"].wrapMode = WrapMode.Once;
		animation["push"].layer = 3; animation["push"].wrapMode = WrapMode.Once;
		animation["ropeclimb"].layer = 3; animation["ropeclimb"].wrapMode = WrapMode.Once;
		animation["hang"].layer = 3; animation["hang"].wrapMode = WrapMode.Once;
		animation["ropeswing"].layer = 3; animation["ropeswing"].wrapMode = WrapMode.Once;
		
		animation.Play("idle");
		
	}
	
	void Update()
	{
		float targetMovementWeight = 0f;
		float throttle = 0f;
		
		// turning keys
		if (Input.GetKey(KeyCode.A)) transform.Rotate(Vector3.down, turningSpeed*Time.deltaTime);
		if (Input.GetKey(KeyCode.D)) transform.Rotate(Vector3.up, turningSpeed*Time.deltaTime);
		
		// forward movement keys
		// ensure that the locomotion animations always blend from idle to moving at the beginning of their cycles
		if (Input.GetKeyDown(KeyCode.W) && 
			(animation["walk"].weight == 0f || animation["run"].weight == 0f))
		{
			animation["walk"].normalizedTime = 0f;
			animation["run"].normalizedTime = 0f;
		}
		if (Input.GetKey(KeyCode.W))
		{
			targetMovementWeight = 1f;
		}
		if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) throttle = 1f;
				
		// blend in the movement

		animation.Blend("run", targetMovementWeight*throttle, 0.5f);
		animation.Blend("walk", targetMovementWeight*(1f-throttle), 0.5f);
		// synchronize timing of the footsteps
		animation.SyncLayer(1);
		
		// all the other animations, such as punch, kick, attach, reaction, etc. go here
		if (Input.GetKeyDown(KeyCode.Alpha1)) animation.CrossFade("eaglejump", 0.2f);
		if (Input.GetKeyDown(KeyCode.Alpha2)) animation.CrossFade("injuredwalk", 0.2f);
		if (Input.GetKeyDown(KeyCode.Alpha3)) animation.CrossFade("jumpaway", 0.2f);
		if (Input.GetKeyDown(KeyCode.Alpha4)) animation.CrossFade("jumpdown", 0.2f);
		if (Input.GetKeyDown(KeyCode.Alpha5)) animation.CrossFade("ladder", 0.2f);
		if (Input.GetKeyDown(KeyCode.Alpha6)) animation.CrossFade("push", 0.2f);
		if (Input.GetKeyDown(KeyCode.Alpha7)) animation.CrossFade("ropeclimb", 0.2f);
		if (Input.GetKeyDown(KeyCode.Alpha8)) animation.CrossFade("hang", 0.2f);
		if (Input.GetKeyDown(KeyCode.Alpha9)) animation.CrossFade("ropeswing", 0.2f);

	}
	
	void LateUpdate()
	{
		computer.ComputeRootMotion();
		
		// move the character using the computer's output
		character.SimpleMove(transform.TransformDirection(computer.deltaPosition)/Time.deltaTime);
	}
}