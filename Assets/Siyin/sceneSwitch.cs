using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class sceneSwitch : MonoBehaviour  //i fix your script added some sound and working instructions buttons etc
{
    public Image playbtn;
    public GameObject uiHowToPlay;
    public GameObject uiInstructionPanel;
    public GameObject uiCloseButton;

    private AudioSource audioSource;
    [SerializeField] private AudioSource clickSound;

    public void Awake()
    {
        uiCloseButton.SetActive(false);
        uiHowToPlay.SetActive(false);
        uiInstructionPanel.SetActive(false);
        audioSource = GetComponent<AudioSource>();
    }


    public void exit()
    {
        playAudio();
        StartCoroutine(playSoundAgain());
    }
    public void playScene()
    {
        playAudio();
        StartCoroutine(playSound());
   
    }

    public void openinstructionMenu()
    {
        playAudio();
        uiCloseButton.SetActive(true);
        uiHowToPlay.SetActive(true);
        uiInstructionPanel.SetActive(true);
    }

    public void closeInstructionMenu()
    {
        playAudio();
        uiCloseButton.SetActive(false);
        uiHowToPlay.SetActive(false);
        uiInstructionPanel.SetActive(false);
    }

    public void playAudio()
    {
        clickSound.Play();
    }

    IEnumerator playSound()
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("TutorialLvl");
    }

    IEnumerator playSoundAgain()
    {
        yield return new WaitForSeconds(1);
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
Application.Quit();
#endif
    }
}

