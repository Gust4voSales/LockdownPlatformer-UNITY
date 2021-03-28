using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level3Manager : MonoBehaviour
{
    public GameObject boss;
    public GameObject alcool;
    public GameObject mask;
    public GameObject vaccine;

    public int[] spawnBounds;
    private float spawnY = -4.16f;
    private bool isBossDead = false;

    // METHODS
    private void Start() {
        Invoke("ActivateBoss", 2.5f);
        InvokeRepeating("SpawnCollectables", 2.5f, 12f);
    }

    private void Update() {
        if (boss == null && !isBossDead) {
            isBossDead = true;
            BossKilled();
        }
    }

    private void BossKilled()  {
        CoronaController[] enemies = FindObjectsOfType<CoronaController>();

        foreach (CoronaController enemy in enemies) {
            enemy.Die();
        }
        
        // Spawn vaccine
        Instantiate(vaccine, new Vector2(1.4f, -3.8f), Quaternion.identity);
    }

    private void SpawnCollectables() {
        int randomChoice = UnityEngine.Random.Range(1, 7);

        if (randomChoice==1) {
            // INSTANTIANTE MASK (1/6 chance)
            int randomX = UnityEngine.Random.Range(spawnBounds[0], spawnBounds[1] + 1); // random x position in range
            
            Instantiate(mask, new Vector2(randomX, spawnY), Quaternion.identity);            
        } else if (randomChoice>1 && randomChoice <= 4) {
            // INSTANTIANTE ALCOOL (3/6 chance)
            int randomX = UnityEngine.Random.Range(spawnBounds[0], spawnBounds[1] + 1); // random x position in range
            
            Instantiate(alcool, new Vector2(randomX, spawnY), Quaternion.identity);
        } 
        // BAD LUCK :/ (2/6)

    }

    private void ActivateBoss() {
        boss.SetActive(true);
    }
}
