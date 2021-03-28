using UnityEngine;

public class CoronaController : MonoBehaviour
{
    public Rigidbody2D rb;
    public GameObject CoronaDeathParticles;
    
    public bool isMovingRight;
    public float runSpeed = 100f;
    
    // METHODS
    private void FixedUpdate() {
        Move();
    }

    private void Move() {
        // PATROLLING MOVEMENT
        if (isMovingRight) {
            Vector2 coronaVelocity = new Vector2(runSpeed*Time.fixedDeltaTime, rb.velocity.y);
            rb.velocity = coronaVelocity;
            
            // Flip corona to face the correct side
            Flip();
        }
        else {
            Vector2 coronaVelocity = new Vector2(-(runSpeed*Time.fixedDeltaTime), rb.velocity.y);
            rb.velocity = coronaVelocity;
            
            // Flip corona to face the correct side
            Flip();
        }
    }

    private void OnTriggerEnter2D(Collider2D trigger) {
        if (trigger.gameObject.CompareTag("TurnTrigger")) {
            isMovingRight = !isMovingRight;
        }
    }

    public void Die() {
        Instantiate(CoronaDeathParticles, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    private void Flip() {
        SpriteRenderer renderer = gameObject.GetComponent<SpriteRenderer>();
        renderer.flipX = !isMovingRight;
    }
}
