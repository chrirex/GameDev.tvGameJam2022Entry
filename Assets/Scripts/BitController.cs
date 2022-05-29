using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BitController : MonoBehaviour {
    [HideInInspector]
    public GameObject spawnPoint;

    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        
    }

    public void AddSpawnPoint(GameObject spawn) {
        spawnPoint = spawn;
    }

    public GameObject GetSpawnPoint() {
        return spawnPoint;
    }
}
