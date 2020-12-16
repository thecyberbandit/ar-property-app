using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour {

    public static SceneController instance;

    private void Awake()
    {
        instance = this;
    }

    public void LoadScene(int index)
    {
        SceneManager.LoadScene(index);
    }
    public void ButtonPressed_Quit()
    {
        Application.Quit();

    }
}
