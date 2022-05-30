using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject notDoneMsg;
    private bool isPaused = false;

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            SetGamePause();
        }    
    }

    public void WinGame() {
        Debug.Log("Game Won");
        SceneManager.LoadScene(4);
    }

    public void ExitGame() {
        Debug.Log("QUIT!");
        Application.Quit();
    }

    public void SetGamePause() {
        // Pause game
        isPaused = !isPaused;
        GameObject[] ghosts = GameObject.FindGameObjectsWithTag("Ghost");
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        foreach (GameObject obj in ghosts) {
            obj.GetComponent<GhostController>().isPaused = isPaused;
        }
        player.GetComponent<Player>().isPaused = isPaused;
        // Open Menu
        pauseMenu.SetActive(isPaused);
    }

    public void DisplayNotDone() {
        notDoneMsg.SetActive(true);
        Invoke("HideNotDone", 5.0f);
    }

    private void HideNotDone() {
        notDoneMsg.SetActive(false);
    }
}
