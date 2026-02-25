using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class endGame : MonoBehaviour
{
    public GameObject endGameScreen;
    public void leaveTheTemple()
    {
        endGameScreen.SetActive(true);
        Time.timeScale = 0.0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
