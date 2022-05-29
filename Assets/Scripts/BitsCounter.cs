using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BitsCounter : MonoBehaviour {

    [SerializeField] GameObject player;
    public Text bitCollectedText;


    // Update is called once per frame
    void Update() {
        int bitsCollected = player.GetComponent<Player>().numBitsCollected;
        bitCollectedText.text = "Bits gathered: " + bitsCollected + "/4";
    }
}
