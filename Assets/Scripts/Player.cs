using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour {

    [SerializeField] float moveSpeed = 5f;
    [SerializeField]Transform movePoint;
    [SerializeField]LayerMask whatStopsMovement;
    [HideInInspector] public bool isPaused = false;
    public int numBitsCollected;
    bool facingRight = true;

    GameManager gameManager;
    GameObject soundEffects;

    // Start is called before the first frame update
    void Start() {
        movePoint.parent = null;
        //numBitsCollected = 0;
        gameManager = FindObjectOfType<GameManager>();
        soundEffects = GameObject.Find("SoundEffects");
    }

    // Update is called once per frame
    void Update() {
        if (isPaused) return;
        //Move player
        transform.position = Vector3.MoveTowards(transform.position, movePoint.position, moveSpeed * Time.deltaTime);

        // Move movePoint based on player input and collision detection
        if (Vector3.Distance(transform.position, movePoint.position) <= .05) {
            if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) == 1f) {
                if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f), .2f, whatStopsMovement)) {
                    movePoint.position += new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f);
                }
                if ((Input.GetAxisRaw("Horizontal") > 0 && !facingRight) || (Input.GetAxisRaw("Horizontal") < 0 && facingRight)) { // moving right, but not facing right
                    Flip();
                }
            }
            if (Mathf.Abs(Input.GetAxisRaw("Vertical")) == 1f) {
                if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(0f, Input.GetAxisRaw("Vertical"), 0f), .2f, whatStopsMovement)) {
                    movePoint.position += new Vector3(0f, Input.GetAxisRaw("Vertical"), 0f);
                }
            }
        }   
    }

    private void Flip() {
        gameObject.GetComponent<SpriteRenderer>().flipX = !gameObject.GetComponent<SpriteRenderer>().flipX;
        facingRight = !facingRight;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        GameObject spawner = GameObject.Find("Spawns");
        if (collision.gameObject.tag == "Ghost") {
            soundEffects.GetComponent<SoundEffectController>().PlayHitGhost();
            /*collision.gameObject.GetComponent<GhostController>().SetReturnPoint();
            collision.gameObject.GetComponent<GhostController>().DragToStart();*/
            GameObject start = GameObject.Find("StartLocale");
            movePoint.position = start.transform.position;
            transform.position = Vector3.MoveTowards(gameObject.transform.position, movePoint.position, 5f * Time.deltaTime);

            if (numBitsCollected > 0) {
                numBitsCollected--;
                spawner.GetComponent<SpawnController>().RespawnBit();
            }
        }
        if (collision.gameObject.tag == "Bit") {
            numBitsCollected++;
            //print(numBitsCollected);
            soundEffects.GetComponent<SoundEffectController>().PlayHitBit();
            GameObject spawn = collision.gameObject.GetComponent<BitController>().GetSpawnPoint();
            spawner.GetComponent<SpawnController>().DespawnBit(spawn, collision.gameObject);
        }
        if (collision.gameObject.tag == "Goal") {
            if (numBitsCollected == 4) {
                soundEffects.GetComponent<SoundEffectController>().PlayWinState();
                gameManager.WinGame();
            } else {
                soundEffects.GetComponent<SoundEffectController>().PlayNoWinState();
                gameManager.DisplayNotDone();
            }
        }
    }

    /*private void MoveTowardsCustom(Transform objectToMove, Vector3 toPostion, float duration) {
        float counter = 0;
        while (counter < duration) {
            counter += Time.deltaTime;
            Vector3 currentPos = objectToMove.position;
            float time = Vector3.Distance(currentPos, toPostion) / (duration - counter) * Time.deltaTime;

            objectToMove.position = Vector3.MoveTowards(currentPos, toPostion, time);
        }
    }*/

    /*private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "Ghost") {
            GameObject start = GameObject.Find("StartLocale");
            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            gameObject.transform.position = start.transform.position;
        }
    }*/
}
