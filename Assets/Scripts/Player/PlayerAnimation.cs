using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    public PlayerController controller;
    public PlayerCombat combatController;
    public Animator animator;

    // METHODS
    private void Update() {
        if (controller.isRunning) {
            animator.SetBool("isRunning", true);
        } else {
            animator.SetBool("isRunning", false);
        }

        // if (Input.GetAxisRaw("Vertical") > 0) {
        //     animator.SetTrigger("jump");
        // }
        if (!FindObjectOfType<TerrainCheck>().isOnTerrain) {
            animator.SetBool("isJumping", true);
        } else {
            animator.SetBool("isJumping", false);
        }
        
        if(controller.damaged) {
            animator.SetBool("isJumping", false);
            animator.SetBool("isHurt", true);
        } else {
            animator.SetBool("isHurt", false);
        }

        if (combatController.attacked) {
            animator.SetTrigger("attacking");
        }
        combatController.attacked = false;
    }
}
