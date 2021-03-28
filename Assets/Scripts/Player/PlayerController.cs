using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D rb;
    public Collider2D col;
    public GameManager gameManager;

    public float runSpeed = 400f;
    public float jumpForce = 2000f;
    public bool isRunning = false;
    public static int remainingLives;
    public static int alcoolEmGel = 0;
    public bool damaged = false;
    // Var to check which direction the Player is currently facing 
    public bool isFacingRight = true;

    // METHODS
    private void Awake() {
        gameManager = FindObjectOfType<GameManager>();
    }

    public void Update() {
        // Player has fallen
        if (transform.position.y < -12) {
            gameManager.GameOver();
        }
    }

    public void Move(float movement, bool jump) {
        Vector2 playerVelocity = new Vector2(movement, rb.velocity.y);
        rb.velocity = playerVelocity;

        // If moving to the right and currently facing left
        if (movement>0 && !isFacingRight) {
            Flip();
        } 
        // If moving to the left and currently facing right
        else if (movement<0 && isFacingRight) {
            Flip();
        }

        if (jump && isOnTerrain()) {
            rb.AddForce(Vector2.up * jumpForce);
            FindObjectOfType<AudioManager>().Play("Jump");
        }

    }

    private void Die() {
        col.enabled = false;
        // rb.gravityScale = 0.85f;
        gameManager.GameOver();
    }

    public void ResetPlayer() {
        alcoolEmGel = 0;
        remainingLives = 0;
    }

    public IEnumerator TakeDamage() {
        damaged = true;      
        FindObjectOfType<AudioManager>().Play("Damaged");     

        if(remainingLives<1) {
            Die();
        } 
        // Decrease remainingLives and make player temporarily invulnerable
        else {
            // Change the Player's layer to one that he ignores enemies
            float invulnerableDelay = 1.1f;
            gameObject.layer = LayerMask.NameToLayer("Ignore Enemies");
            Invoke("resetPlayerLayer", invulnerableDelay); // Reset the Player's layer after delay

            remainingLives--;
        }
        yield return new WaitForSeconds(1);
        damaged = false;
    }

    private void resetPlayerLayer() {
        gameObject.layer = LayerMask.NameToLayer("Default");
    }

    // Get the TerrainCheck component and return its isOnTerrain value 
    private bool isOnTerrain() {
        return transform.Find("TerrainCheck").GetComponent<TerrainCheck>().isOnTerrain;
    }

    private void Flip() {
        isFacingRight = !isFacingRight;

        Vector3 playerScale = transform.localScale;
        playerScale.x *= -1;
        transform.localScale = playerScale;
    }
}
