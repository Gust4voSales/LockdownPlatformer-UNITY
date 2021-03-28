using UnityEngine;

public class TerrainCheck : MonoBehaviour
{
    [SerializeField] private LayerMask platformLayerMask; // What is ground

    public bool isOnTerrain;

    // METHODS
    private void OnTriggerStay2D(Collider2D collider) {
        // Check if there's a collider and this collider's layer matches platformLayerMask
        isOnTerrain = collider != null && (((1 << collider.gameObject.layer) & platformLayerMask) != 0);
    }
    
    private void OnTriggerExit2D(Collider2D collision) {
        isOnTerrain = false;
    }
}
