using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostController : MonoBehaviour {
    [SerializeField] float moveSpeed = 5.5f;
    [SerializeField] Transform movePoint;
    [SerializeField] LayerMask whatStopsMovement;
    [SerializeField] LayerMask changeDirection;
    [SerializeField] LayerMask startPos;
    [SerializeField] float overlapSize;
    [HideInInspector] public bool isPaused = false;
    Transform returnPoint;
    bool facingRight = true;
    /*bool isDragging = false;*/
    
    public enum Direction { North, East, South, West };
    List<Direction> validDirections;

    public Direction _direction;

    // Start is called before the first frame update
    void Start() {
        movePoint.parent = null;
        _direction = (Direction)Random.Range(0, 4);
        Flip();
        validDirections = new List<Direction>();
    }

    // Update is called once per frame
    void Update() {
        if (isPaused) return;
        try {
            // Move Ghost
            transform.position = Vector3.MoveTowards(transform.position, movePoint.position, moveSpeed * Time.deltaTime);

            Vector3 newVector = GetNewVector();

            if (Vector3.Distance(transform.position, movePoint.position) <= .05f) {

                // ** Move the move point in the _direction. If the layer is an intersection, change the _direction AFTER moving
                if (!Physics2D.OverlapCircle(movePoint.position + newVector, .2f, whatStopsMovement)) {
                    movePoint.position += newVector;
                }
                if (Physics2D.OverlapCircle(movePoint.position, overlapSize, changeDirection)) {
                    Collider2D collider = Physics2D.OverlapCircle(movePoint.position, overlapSize, changeDirection);
                    ChangeDirection(collider);
                }
                /*if (Physics2D.OverlapCircle(movePoint.position, overlapSize, startPos)) {
                    movePoint.position = returnPoint.position;
                }*/
            }
        } catch (System.Exception ex) {
            print(ex.Message);
        }
    }

    /*public void SetReturnPoint() {
        returnPoint = gameObject.transform;
    }

    public void DragToStart() {
        isDragging = true;
        GameObject start = GameObject.Find("StartLocale");
        movePoint.position = start.transform.position;
    }*/

    private void Flip() {
        if ((_direction == Direction.East && !facingRight) || (_direction == Direction.West && facingRight)) {
            gameObject.GetComponent<SpriteRenderer>().flipX = !gameObject.GetComponent<SpriteRenderer>().flipX;
            facingRight = !facingRight;
        }
    }

    private void ChangeDirection(Collider2D collision) {
        validDirections.Clear();
        int[] validDirectionsInt = collision.gameObject.GetComponent<IntersectionController>().GetValidDirections();
        if (validDirectionsInt[0] == 1) validDirections.Add(Direction.North);
        if (validDirectionsInt[1] == 1) validDirections.Add(Direction.East);
        if (validDirectionsInt[2] == 1) validDirections.Add(Direction.South);
        if (validDirectionsInt[3] == 1) validDirections.Add(Direction.West);

        int idx = GetDirectionIdx(validDirections);

        int currentLastDir = collision.gameObject.GetComponent<IntersectionController>().GetLastDirection();
        if (validDirections.Count > 1 && validDirections[idx] == (Direction)currentLastDir) {
            validDirections.Remove(validDirections[idx]);
            idx = GetDirectionIdx(validDirections);
        }
        _direction = validDirections[idx];
        int lastDirIdx = (int)_direction;
        collision.gameObject.GetComponent<IntersectionController>().SetLastDirection(lastDirIdx);
        Flip();
    }

    int GetDirectionIdx(List<Direction> directions) {
        return Random.Range(0, validDirections.Count);
    }

    Vector3 GetNewVector() {
        Vector3 nv;
        switch (_direction) {
            case Direction.North:
                nv = new Vector3(0, 1, 0);
                break;
            case Direction.East:
                nv = new Vector3(1, 0, 0);
                break;
            case Direction.South:
                nv = new Vector3(0, -1, 0);
                break;
            default:
                nv = new Vector3(-1, 0, 0);
                break;
        }
        return nv;
    }
}
