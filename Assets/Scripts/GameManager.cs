using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public List<string> levels;
    
    public int hp = 3;
    public int currentLevel;

    public Transform transition;

    public AudioClip winSound;
    public AudioClip loseSound;
    public AudioClip gameOverSound;

    bool isLoadingLevel = false;

    Vector3 targetScale;

    AudioSource source;

    // Unity singleton
    public static GameManager instance;

    void Start()
    {
        Application.targetFrameRate = 60;
        
        source = GetComponent<AudioSource>();

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

    void Update()
    {
        transition.localScale = Vector3.MoveTowards(transition.localScale, targetScale, 100 * Time.deltaTime);
    }

    public void Win()
    {
        if (!isLoadingLevel)
        {
            currentLevel++;
            Invoke("LoadNextLevel", 1f);
            isLoadingLevel = true;
            targetScale = Vector3.one * 25;
            source.PlayOneShot(winSound);
        }
    }

    void LoadNextLevel()
    {
        SceneManager.LoadScene(levels[currentLevel]);
        isLoadingLevel = false;
        targetScale = Vector3.zero;
    }

    public void Lose() 
    {
        if(!isLoadingLevel)
        {
            hp--;

            if(hp == 0)
            {
                currentLevel = 0;
                source.PlayOneShot(gameOverSound);
            }
            else
            {
                source.PlayOneShot(loseSound);
            }

            Invoke("LoadNextLevel", 1f);
        }

        targetScale = Vector3.one * 25;
    }
}