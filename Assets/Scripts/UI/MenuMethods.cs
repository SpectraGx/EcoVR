using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuMethods : MonoBehaviour
{
    void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        AudioListener.pause = false;
        Time.timeScale = 1f;
    }
    public void LoadByText(string text)
    {
        SceneManager.LoadScene(text);
    }
    /*public void LoadBySceneField(SceneField SceneToLoad)
    {
        SceneManager.LoadScene(SceneToLoad);
    }*/
    public void QuitGame()
    {
        Debug.Log("Quitting...");
        Application.Quit();
    }
}
