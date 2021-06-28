using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {
#pragma warning disable 649 // Ignores error that is displayed if we don't use a Serialized field.

    // Horizontal Movement Fields
    [SerializeField] CharacterController controller;
    [SerializeField] float speed = 11f;
    Vector2 horizontalInput;

    // Jumping Fields 
    [SerializeField] float jumpHeight = 50f;
    bool jump;

    // Gravity Fields
    [SerializeField] float gravity = -20f; // - 9.81 is real world, -30 is snappier.
    Vector3 verticalVelocity = Vector3.zero;
    [SerializeField] LayerMask groundMask;
    bool isGrounded;

    private void FixedUpdate() {

        // Reset gravity when touching ground
        isGrounded = Physics.CheckSphere(transform.position, 0.1f, groundMask);
        if (isGrounded) {
            verticalVelocity.y = 0;
        }
        
        // Horizontal Movement (WASD)
        Vector3 horizontalVelocity = (transform.right * horizontalInput.x + transform.forward * horizontalInput.y) * speed;
        controller.Move(horizontalVelocity * Time.deltaTime);

        // Jump: v = sqrt(-2 * jumpHeight * gravity)
        if (jump) {
            if (isGrounded) {
                verticalVelocity.y = Mathf.Sqrt(-2 * jumpHeight * gravity);
            }
            jump = false;
        }

        // (!isGrounded) Enforced Gravity 
        verticalVelocity.y += gravity * Time.deltaTime;
        controller.Move(verticalVelocity * Time.deltaTime); // (meters/second)^2
    }

    public void ReceiveInput(Vector2 _horizontalInput) {
        horizontalInput = _horizontalInput;
    }

    public void onJumpPressed() {
        jump = true;
    }
}
