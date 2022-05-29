using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour {
    List<GameObject> spawnPoints;
    List<GameObject> usedPoints;
    [SerializeField] GameObject bitsObject;
    [SerializeField] int numOfBits;
    
    // Start is called before the first frame update
    void Start() {
        spawnPoints = new List<GameObject>();
        usedPoints = new List<GameObject>();
        GameObject[] spawnArray = GameObject.FindGameObjectsWithTag("BitsSpawn");
        foreach(GameObject obj in spawnArray) {
            spawnPoints.Add(obj);
        }

        for (int i = 0; i < numOfBits; i++) {
            GameObject spawn = GetSpawnPoint();
            CreateBitAtSpawn(spawn);
            UseSpawnPoint(spawn);
        }
    }

    public void RespawnBit() {
        GameObject spawn = GetSpawnPoint();
        CreateBitAtSpawn(spawn);
        UseSpawnPoint(spawn);
    }

    public void DespawnBit(GameObject spawn, GameObject bit) {
        Destroy(bit);

        spawnPoints.Add(spawn);
        usedPoints.Remove(spawn);
    }

    private GameObject GetSpawnPoint() {
        int idx = Random.Range(0, spawnPoints.Count);
        return spawnPoints[idx];
    }

    private void CreateBitAtSpawn(GameObject spawn) {
        GameObject bit = Instantiate(bitsObject);
        bit.transform.position = spawn.transform.position;
        bit.GetComponent<BitController>().AddSpawnPoint(spawn);
    }

    private void UseSpawnPoint(GameObject spawn) {
        usedPoints.Add(spawn);
        spawnPoints.Remove(spawn);
    }
}
