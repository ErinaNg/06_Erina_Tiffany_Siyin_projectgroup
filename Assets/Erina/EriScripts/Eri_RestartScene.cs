using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Eri_RestartScene : MonoBehaviour
{
    public void restartScene()
    {
        SceneManager.LoadScene("Lvl1");
    }
    public void OnNextLevel()
    {
        SceneManager.LoadScene("Lvl1");
    }

    public void OnLevelsecond()
    {
        SceneManager.LoadScene("Lvl2");
    }


}
