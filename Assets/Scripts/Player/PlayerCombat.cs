using System.Collections;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public PlayerController controller;
    public Transform attackPos;
    public LayerMask enemiesLayer;
    public GameObject sprayAttackParticle;
    
    public float attackRange;
    // Prevent's user from spamming attacks
    private bool isAttacking = false;
    // Trigger for animation
    public bool attacked = false;
    public float attackDelay;
    
    // METHODS 
    private void Update() {
        if (Input.GetButtonDown("Fire1") && !isAttacking && PlayerController.alcoolEmGel>0) {
            attacked = true;
            StartCoroutine(Attack());
        } 
    }

    private IEnumerator Attack() {
        isAttacking = true;
        FindObjectOfType<AudioManager>().Play("Attack");
        PlayerController.alcoolEmGel--;
        
        // INSTATIATING SPRAY PARTICLES FACED CORRECTLY
        if (controller.isFacingRight) {
            Vector3 sprayPos = new Vector3(attackPos.position.x-0.6f, attackPos.position.y, attackPos.position.z);
            Instantiate(sprayAttackParticle, sprayPos, Quaternion.identity, transform);
        } else {
            Vector3 sprayPos = new Vector3(attackPos.position.x+0.6f, attackPos.position.y, attackPos.position.z);
            Instantiate(sprayAttackParticle, sprayPos, Quaternion.Euler(0, -180, 0), transform);
        }
        // Collider2D[] enemiesToDamage = Physics2D.OverlapCapsuleAll(attackPos.position, attackRange, enemiesLayer);
        Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, enemiesLayer);
        for (int i = 0; i < enemiesToDamage.Length; i++) {   
            if (enemiesToDamage[i].CompareTag("Enemy")) {
                enemiesToDamage[i].gameObject.GetComponent<CoronaController>().Die();
            } else if (enemiesToDamage[i].CompareTag("Enemy Boss")) {
                enemiesToDamage[i].gameObject.GetComponent<BossCorona>().TakeDamage();
            }
        }
        yield return new WaitForSeconds(attackDelay);
        isAttacking = false;
    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }
}
