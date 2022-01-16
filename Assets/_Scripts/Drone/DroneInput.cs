using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneInput : MonoBehaviour {

	public Vector2 desiredRotation; // euler
	public Vector3 desiredTranslation; // normal vector

	void Awake()
	{
		desiredRotation = Vector2.zero;
		desiredTranslation = Vector3.zero;
	}
	

	void Update() {

		InputUpdate();
	}

	void InputUpdate()
    {
		// look
		desiredRotation += new Vector2(Input.GetAxis("Vertical"), Input.GetAxis("Horizontal"));

		// translation
		Vector3 deltaTranslation = Vector3.zero;

		deltaTranslation += Input.GetKey("e") ? Vector3.up : Vector3.zero;
		deltaTranslation -= Input.GetKey("q") ? Vector3.up : Vector3.zero;
		deltaTranslation += Input.GetKey("w") ? Vector3.forward : Vector3.zero;
		deltaTranslation -= Input.GetKey("s") ? Vector3.forward : Vector3.zero;
		deltaTranslation += Input.GetKey("d") ? Vector3.right : Vector3.zero;
		deltaTranslation -= Input.GetKey("a") ? Vector3.right : Vector3.zero;

		desiredTranslation = deltaTranslation;
	}
}
