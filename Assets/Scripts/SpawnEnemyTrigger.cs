using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemyTrigger : MonoBehaviour
{
    public GameObject corona;

    public Vector2 spawnPosition;
    public float coronaSpeed = 160f;
    private bool spawned = false;

    // METHODS
    private void OnTriggerEnter2D(Collider2D collider) {
        if (!spawned) {
            spawned = true;
            CoronaController coronaController = corona.gameObject.GetComponent<CoronaController>();
            coronaController.runSpeed = coronaSpeed;

            Instantiate(corona, spawnPosition, Quaternion.identity);
        }
    }
}
