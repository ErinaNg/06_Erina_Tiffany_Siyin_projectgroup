using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Eri_RestartScene : MonoBehaviour //pause game
{
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;
    private AudioSource audioSource;
    [SerializeField] private AudioSource clickingSound;

    public void Awake()
    {
        pauseMenuUI.SetActive(false);
        audioSource = GetComponent<AudioSource>();
    }
    private void Update()
    {
        //if currentstate != 0
        if (Input.GetKeyDown(KeyCode.Escape) && Eri_gemcollector.currentState != Eri_gemcollector.gamestate.gameover)
        {
            if (GameIsPaused)
            {
                Eri_gemcollector.currentState = Eri_gemcollector.gamestate.playing;
                AudioListener.pause = false;
                Resume();
            }
            else
            {
                Eri_gemcollector.currentState = Eri_gemcollector.gamestate.pause;
                AudioListener.pause = true;
                Pause();
            }
        }
    }


    public void Resume()
    {
        AudioListener.pause = false;
        Cursor.lockState = CursorLockMode.Locked;
        playAudio();
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    void Pause()
    {
        Cursor.lockState = CursorLockMode.Confined;
        playAudio();
        pauseMenuUI.SetActive(true); //enable
        Time.timeScale = 0f; //freeze 
        GameIsPaused = true;
       
    }

    public void QuitMenu()
    {
        playAudio();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }

    public void playAudio()
    {
        clickingSound.Play();
    }


    

}
