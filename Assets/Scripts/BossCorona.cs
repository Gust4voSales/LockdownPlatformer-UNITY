using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BossCorona : MonoBehaviour
{
    public new Rigidbody2D rigidbody;   
    public Image healthBar;
    public GameObject corona;
    public GameObject bouncyCorona;
    public GameObject deathParticles;
    public Animator animator;

    public int MAX_HEALTH = 12;
    public int life;

    // THE BOSS HAS A BAR OF HEALTH
    // IT GETS STRONGER WHEN IT LOSES HEALTH

    // METHODS
    private void Awake() {
        animator = gameObject.GetComponent<Animator>();
    }

    private void Start() {
        life = MAX_HEALTH;
        StartCoroutine(SpawnCoronas());
    }

    private void Update() {
        // Debug.Log(life/MAX_HEALTH);
        healthBar.fillAmount = (float)life/(float)MAX_HEALTH;
    }

    private IEnumerator SpawnCoronas() {
        // SPAWN CORONAS
        yield return new WaitForSeconds(1); 

        animator.SetTrigger("multiply");
        for (int coronaIndex=0; coronaIndex<5; coronaIndex++) {
            float randomX = UnityEngine.Random.Range(-transform.localScale.x, transform.localScale.x);
            Vector2 spawnPos = new Vector2(transform.position.x + randomX, transform.position.y);
            
            GameObject coronaInstance;
            if (life < 8 && coronaIndex >= 4) {
                coronaInstance = Instantiate(bouncyCorona, spawnPos, Quaternion.identity);
            } else {
                coronaInstance = Instantiate(corona, spawnPos, Quaternion.identity);
            }
            
            SetSpawnCoronaProperties(coronaInstance);
            
            yield return new WaitForSeconds(0.15f); 
        }
        // MOVE DOWN
        StartCoroutine(MoveDown());

        // AFTER 6S MOVE TO THE START POSITION 
        yield return new WaitForSeconds(7); 
        StartCoroutine(MoveUp());
        
        // START ALL OVER AGAIN 
        yield return new WaitForSeconds(4); 
        StartCoroutine(SpawnCoronas());
    }

    private IEnumerator MoveUp() {
        rigidbody.velocity = Vector2.up;
        yield return new WaitForSeconds(2);
        rigidbody.velocity = Vector2.zero;
    }

    private IEnumerator MoveDown() {
        rigidbody.velocity = Vector2.down;
        yield return new WaitForSeconds(2);
        rigidbody.velocity = Vector2.zero;
    }

    private void SetSpawnCoronaProperties(GameObject coronaInstance) {
        bool isMovingRight = UnityEngine.Random.Range(0, 2) == 1 ? true : false;
        corona.gameObject.GetComponent<CoronaController>().isMovingRight = isMovingRight;

        Rigidbody2D coronaRB = coronaInstance.GetComponent<Rigidbody2D>();
        coronaRB.bodyType = RigidbodyType2D.Dynamic;
        coronaRB.gravityScale = 8;

        float randomX = UnityEngine.Random.Range(-5, 6);
        float randomY = UnityEngine.Random.Range(1600, 2400);
        
        coronaRB.AddForce(new Vector2(randomX*1000, randomY));
    }

    public void TakeDamage() {
        animator.SetTrigger("damaged");
        FindObjectOfType<AudioManager>().Play("CoronaDamaged");

        if (life<=1) {
            Die();
        } else {
            life--;
        }
    }

    private void Die() {
        Instantiate(deathParticles, transform.position, Quaternion.identity);
        Instantiate(deathParticles, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
