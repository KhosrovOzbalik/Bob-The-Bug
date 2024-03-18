using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject MainMenu;
    public GameObject Levels;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void firstLevel()
    {
        SceneManager.LoadScene(1);
    }

    public void loadLevel(int x)
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void openLevels(int x)
    {
        SceneManager.LoadScene(x);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void resetLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void MuteToggle(bool muted)
    {
        if (muted)
        {
            AudioListener.volume = 0;
        }
        else
        {
            AudioListener.volume = 1;
        }
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
