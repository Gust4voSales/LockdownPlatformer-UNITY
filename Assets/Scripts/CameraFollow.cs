using UnityEngine;

public class CameraFollow : MonoBehaviour
{   
    public Transform target;
    
    public float smoothSpeed = 10f;
    public Vector3 offset;

    // METHODS
    private void LateUpdate() {
        Vector3 desiredPosition =  target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed*Time.deltaTime);
        // Smooth follow
        transform.position = smoothedPosition;
    }
}
