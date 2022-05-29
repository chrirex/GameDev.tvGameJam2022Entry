using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntersectionController : MonoBehaviour {

    [SerializeField]
    public int[] validDirections;
    [HideInInspector]
    public Direction lastDirection;
    [HideInInspector]
    public enum Direction { North, East, South, West };

    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        print("Calling from Intersection Controller");
    }

    public int[] GetValidDirections() {
        return validDirections;
    }

    public void SetLastDirection(int dir) {
        lastDirection = (Direction)dir;
    }

    public int GetLastDirection() {
        return (int)lastDirection;
    }
}
