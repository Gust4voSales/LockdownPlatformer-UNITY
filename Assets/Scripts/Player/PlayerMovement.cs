using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public PlayerController controller;

    // The horizontal move is the +- speed; 
    private float horizontalMove = 0f;

    private bool shouldJump = false;

    // METHODS
    private void Update()
    {
        // MOVEMENT
        horizontalMove = Input.GetAxisRaw("Horizontal") * controller.runSpeed;
        controller.isRunning = horizontalMove != 0;

        // JUMP
        if (Input.GetAxisRaw("Vertical") > 0) {
            shouldJump = true;
        }
    }

    private void FixedUpdate() {
        controller.Move(horizontalMove * Time.fixedDeltaTime, shouldJump);
        shouldJump = false;
    }
}
