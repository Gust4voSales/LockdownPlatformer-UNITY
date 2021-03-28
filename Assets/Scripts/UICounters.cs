using UnityEngine;
using UnityEngine.UI;

public class UICounters : MonoBehaviour
{
    public Text alcoolTextObj;
    public Text maskTextObj;

    // METHODS
    private void Update() {
        alcoolTextObj.text = "x " + PlayerController.alcoolEmGel;
        
        if (PlayerController.remainingLives<0) {
            maskTextObj.text = "";
        } else {
            maskTextObj.text = "x " + PlayerController.remainingLives;
        }
    }
}
