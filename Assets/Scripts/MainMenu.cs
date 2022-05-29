using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {
    public void StartGame() {
        int maze = Random.Range(1, 4);
        SceneManager.LoadScene(maze);
    }

    public void ExitGame() {
        Debug.Log("Quit");
        Application.Quit();
    }
}
