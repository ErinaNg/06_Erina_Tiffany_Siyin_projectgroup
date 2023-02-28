using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Eri_gemcollector : MonoBehaviour
{
    public float gem;
    public Text gemText;
    public float Timer;
    public Text TimerText;
    public float TimerSeconds;
    public GameObject gemParticle;
    public bool isGameOver;
    public static bool IsWinShown;
    private bool GameOverIsPlayed = false;

    //[SerializeField] private AudioSource collectSound;
    //[SerializeField] private AudioSource deathSound;

    //public GameObject GameOverUI;
    //public GameObject GameWinUI;
    //public GameObject PauseMenu;
    //public AudioClip GameWinSound;
    //public AudioClip GameLoseSound;
    //public AudioSource audioSource;

    // Start is called before the first frame update

    void Awake()
    {
        //GameOverUI.SetActive(false);
        //GameWinUI.SetActive(false);
        //PauseMenu.SetActive(false);
        //audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isGameOver)
            return;

        Timer = Timer + Time.deltaTime;
        TimerSeconds = Mathf.FloorToInt(Timer % 60);
        TimerText.text = "Timer: " + TimerSeconds.ToString();
        if (Timer >= 60 && gem < 8)
        {
            SceneManager.LoadScene("GameLose");
        }
    }

    public void SetGameOver(bool isWin)
    {
        isGameOver = true;

        if (isWin)
        {

            //audioSource.PlayOneShot(GameWinSound);
            //GameOverUI.SetActive(false);
            //PauseMenu.SetActive(false);
            //GameWinUI.SetActive(true);
            //IsWinShown = true;
        }
        else
        {
            //if (!GameOverIsPlayed)
            //{
            //    audioSource.PlayOneShot(GameLoseSound);

            //    GameOverIsPlayed = true;
            //}
            //GameOverUI.SetActive(true);
            //GameWinUI.SetActive(false);
            //PauseMenu.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Coin")
        {
            //collectSound.Play();
            Destroy(other.gameObject);
            gem = gem + 1;
            gemText.text = "CoinScore: " + gem.ToString();
            Instantiate(gemParticle, gameObject.transform.position, Quaternion.identity);
            Destroy(GameObject.FindGameObjectWithTag("Particle"), 2);
        }

        if (other.gameObject.tag == "Enemy" && BossScript.IfAttackPlayer)
        {
            //deathSound.Play();
            if (isGameOver)
            {
                SetGameOver(true);
            }
            //SceneManager.LoadScene("GameLose");
        }

        if (other.gameObject.tag == "Goal")
        {
            if (gem >= 100)       //Win // Add another && OnCollision with end goal then win
            {
               // SceneManager.LoadScene("GameWin");
            }
        }

        if(other.gameObject.tag == "Door" && !TutorialKnightScript.TutorialKnight.IsActive)
        {
            SceneManager.LoadScene("Lvl1");
        }
    }

    void OnCollisionEnter(Collision otherObj)
    {
        if (otherObj.gameObject.tag == "Particle")
        {
            Destroy(gameObject, .5f);
        }
    }
}
