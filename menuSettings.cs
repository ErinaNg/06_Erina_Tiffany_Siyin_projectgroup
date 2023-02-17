using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class menuSettings : MonoBehaviour
{
    [Header("Volume Setting")]
    [SerializeField] public Text volumeTextVaule = null;
    [SerializeField] public Scrollbar volumeSlider = null;

    [SerializeField] private GameObject confirmationPrompt = null;

    [Header("Gameplay Settings")]
    [SerializeField] public Text controllerSenTextVaule = null;
    [SerializeField] public Scrollbar controllerSenSilder = null;
    [SerializeField] private int defaultSen = 4;
    public int mainControllerSen = 4;

    [Header("Toggle Settings")]
    [SerializeField] private Toggle invertYToggle = null;

    //screen
    [SerializeField] private Dropdown resoultionDrpDown;

    private List<Resolution> filteredResoultions;
    private Resolution[] Resoultions;

    private float currentRefreshRate;
    private int currentResoultionIndex = 0;

    void Start()
    {
        Resoultions = Screen.resolutions;
        filteredResoultions = new List<Resolution>();

        resoultionDrpDown.ClearOptions();
        currentRefreshRate = Screen.currentResolution.refreshRate;

        for (int i = 0; i < Resoultions.Length; i++)
        {
            if (Resoultions[i].refreshRate == currentRefreshRate)
            {
                filteredResoultions.Add(Resoultions[i]);
            }
        }

        // list the details of resloution

        List<string> options = new List<string>();
        for (int i = 0; i < filteredResoultions.Count; i++)
        {
            string resolutionOption = filteredResoultions[i].width + "x" + filteredResoultions[i].height + " " + filteredResoultions[i].refreshRate + "Hz";
            options.Add(resolutionOption);
            if (filteredResoultions[i].width == Screen.width && filteredResoultions[i].height == Screen.height)
            {
                currentResoultionIndex = i;
            }
        }

        resoultionDrpDown.AddOptions(options);
        resoultionDrpDown.value = currentResoultionIndex;
        resoultionDrpDown.RefreshShownValue();
    }

    public void setResoultion(int resolutionIndex)
    {
        Resolution resolution = filteredResoultions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, true);
    }

    // exit game
    public void exit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
Application.Quit();
#endif
    }

    // voulume
    public void setVolume(float volume)
    {
        AudioListener.volume = volume;
        volumeTextVaule.text = volume.ToString("0.0");
    }

    public void volumeApply()
    {
        PlayerPrefs.SetFloat("masterVolume", AudioListener.volume);
        StartCoroutine(confirmationBox());
    }

    public IEnumerator confirmationBox()
    {
        confirmationPrompt.SetActive(true);
        yield return new WaitForSeconds(2);
        confirmationPrompt.SetActive(false);
    }

    // controller Sen

    public void setControllerSen(float sensitivity)
    {
        mainControllerSen = Mathf.RoundToInt(sensitivity);
        controllerSenTextVaule.text = sensitivity.ToString("0.0");
    }
}