using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    
        public float moveSpeed = 5f; // Speed of movement
        public Rigidbody rb; // Reference to the Rigidbody component

        // Update is called once per frame
        void Update()
        {
            // Input for movement in horizontal (left/right) and vertical (forward/backward) directions
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");

            // Calculate movement direction based on input
            Vector3 movement = new Vector3(horizontalInput, 0f, verticalInput) * moveSpeed * Time.deltaTime;

            // Move the player based on the calculated movement
            rb.MovePosition(rb.position + transform.TransformDirection(movement));
        }
    
}
