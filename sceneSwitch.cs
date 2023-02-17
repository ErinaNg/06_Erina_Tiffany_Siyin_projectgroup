using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class sceneSwitch : MonoBehaviour
{
    public Image playbtn;

    public void playScene()
    {
        SceneManager.LoadScene("startScene");
    }
}
