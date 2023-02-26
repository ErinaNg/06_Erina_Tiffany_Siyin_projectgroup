using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EriReplay : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject replayA;
  
    private void Awake()
    {
        replayA.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void ReplayGame()
    {
        replayA.SetActive(true);
        Cursor.lockState = CursorLockMode.Confined;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}
