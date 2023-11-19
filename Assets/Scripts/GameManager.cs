using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public List<string> levels;
    
    public int hp = 3;
    public int currentLevel;

    bool isLoadingLevel = false;

    // Unity singleton
    public static GameManager instance;

    void Start()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    //
    public void Win()
    {
        currentLevel++;
        if (!isLoadingLevel)
        {
            Invoke("LoadNextLevel", 1f);
            isLoadingLevel = true;
        }
    }

    void LoadNextLevel()
    {
        SceneManager.LoadScene(levels[currentLevel]);
        isLoadingLevel = false;
    }

    public void Lose() 
    {
        if(!isLoadingLevel)
        {
            hp--;

            if(hp == 0)
            {
                currentLevel = 0;
            }

            Invoke("LoadNextLevel", 1f);
        }
    }
}