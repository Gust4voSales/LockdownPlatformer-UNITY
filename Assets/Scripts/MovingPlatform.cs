using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public bool isMovingRight;
    public float speed = 200f;


    // METHODS
    private void Update() {
        if (isMovingRight) {
            Vector2 newPosition = new Vector2(transform.position.x + (speed * Time.deltaTime), transform.position.y);
            transform.position = newPosition;
        } else {
            Vector2 newPosition = new Vector2(transform.position.x + (-speed * Time.deltaTime), transform.position.y);
            transform.position = newPosition;
        }
    }

    private void OnTriggerEnter2D(Collider2D trigger) {
        if (trigger.gameObject.CompareTag("TurnTrigger")) {
            isMovingRight = !isMovingRight;
        }
    }
}
