using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace muskit
{
	[RequireComponent(typeof(Rigidbody))]
	[RequireComponent(typeof(DroneInput))]
	public class DroneController : MonoBehaviour
	{
		private Rigidbody rb;

		void Awake()
        {
			rb = GetComponent<Rigidbody>();
        }

		void Start()
        {

        }

        // Physics
        void FixedUpdate()
        {
            HandleInput();
            HandlePhysics();
        }

        private void HandleInput()
        {
            
        }

        private void HandlePhysics()
        {
            rb.AddForce(transform.up * Physics.gravity.magnitude * rb.mass);
        }
    }
}