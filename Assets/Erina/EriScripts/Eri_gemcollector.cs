using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class Eri_gemcollector : MonoBehaviour , IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        Pause = !Pause;
    }
    public float gem;
    public Text gemText;
    [SerializeField] private Image uiFill;
    [SerializeField] private Text TimerText;
    public int Duration;
    private int remainDuration;
    private bool Pause;
    public GameObject gemParticle;
    public bool isGameOver;
    public static bool IsWinShown;
    private bool GameOverIsPlayed = false;

    [SerializeField] private AudioSource collectSound;
    [SerializeField] private AudioSource popSound;
    [SerializeField] private AudioSource deathSound;
    [SerializeField] private AudioSource GameWinSound;
    [SerializeField] private AudioSource GameLoseSound;
    [SerializeField] private AudioSource uibuttonSound;
    [SerializeField] private AudioSource bgmSound;
    public GameObject GameOverUI;
    public GameObject GameWinUI;
    public GameObject uiMessage;
    private AudioSource audioSource;

    public static gamestate currentState = gamestate.playing;
    public enum gamestate
    {
        gameover,
        pause,
        playing
    }


    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    private void Start()
    {
        Being(Duration);
        //anim = GetComponentInChildren<Animator>();
        uiMessage.SetActive(false);
    }
    private void Being(int Second)
    {
        remainDuration = Second;
        StartCoroutine(UpdateTimer());
    }

    private IEnumerator UpdateTimer()
    {
        while(remainDuration >= 0)
        {
            if(!Pause)
            {
                TimerText.text = $"{remainDuration / 60:00} : {remainDuration % 60:00}";
                uiFill.fillAmount = Mathf.InverseLerp(0, Duration, remainDuration);
                remainDuration--;
                yield return new WaitForSeconds(1f);
            }
            yield return null;
        }
        OnEnd();
    }
    private void OnEnd()
    {
        //print("End");
    }
   

    // Update is called once per frame
    void Update()
    {
        if (remainDuration <= 0 && gem < 8)
        {
            GameLose();
        }
    }

    public void GameWin()
    {
        bgmSound.Stop();
        // finishing game
        currentState = gamestate.gameover;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
        GameWinSound.Play();
        GameWinUI.SetActive(true);


    }

    public void GameLose()
    {
        bgmSound.Stop();
        //deathSound.Play();
        GameLoseSound.Play();
        currentState = gamestate.gameover;
        GameOverUI.SetActive(true);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
       
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Gem")
        {
            collectSound.Play();
            popSound.Play();
            Destroy(other.gameObject);
            gem = gem + 1;
            gemText.text = "Gem Count: " + gem.ToString();  //collect a gem plus 10seconds to timer
            remainDuration += 10;
            Instantiate(gemParticle, gameObject.transform.position, Quaternion.identity);
            Destroy(GameObject.FindGameObjectWithTag("Confetti"), 2);


        }

       
        if (other.gameObject.tag == "Goal")  //goal is the door to escape
        {
            if (gem >= 8)       //Win // Add another && OnCollision with end goal then win
            {
                GameWin();
                uiMessage.SetActive(false);

            }
            else
            {
                StartCoroutine(ExampleCoroutine());
                uiMessage.SetActive(true);
            }
        }
        if (other.gameObject.tag == "Gate")
        {
            if (gem >= 8)
            {
                GameWin();
            }
        }
        if (other.gameObject.tag == "TutorialDoor" && !TutorialKnightScript.TutorialKnight.IsActive)
        {
            SceneManager.LoadScene("EriLvl1Dialogue");
        }

        if (other.gameObject.tag == "Enemy" && other.GetComponent<BossScript>().PlayerInAttackRange && other.GetComponent<BossScript>().IsActive)
        {
            other.gameObject.SetActive(false);
            //deathSound.Play();
            GameLose();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (Input.GetMouseButtonDown(0)) //Left Click 
        {
            if (other.gameObject.tag == "Enemy")
            {
                if (!other.GetComponent<BossScript>().PlayerInSightRange && other.GetComponent<BossScript>().IsActive && !other.GetComponent<BossScript>().PlayerInAttackRange)
                {
                    other.GetComponent<BossScript>().IsActive = false;
                }
            }

            if (other.gameObject.tag == "Tutorial")
            {
                if (TutorialKnightScript.TutorialKnight.IsActive && !TutorialKnightScript.TutorialKnight.PlayerInSightRange && TutorialKnightScript.TutorialKnight != null)
                {
                    TutorialKnightScript.TutorialKnight.TutorialKnightKnockOut();
                }
            }
        }
    }



    IEnumerator ExampleCoroutine()
    {
        //Print the time of when the function is first called.
        Debug.Log("Started Coroutine at timestamp : " + Time.time);

        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(5);
        //After we have waited 5 seconds print the time again.
        Debug.Log("Finished Coroutine at timestamp : " + Time.time);
        uiMessage.gameObject.SetActive(false);
    }

    IEnumerator LoadToNextScene()
    {
        yield return new WaitForSeconds(3);  
        SceneManager.LoadScene("Lvl2");
    }


    void OnCollisionEnter(Collision otherObj)
    {
        if (otherObj.gameObject.tag == "Confetti")
        {
            Destroy(gameObject, .5f);
        }

    }

    public void OnClickReplayAgain()
    {
        uibuttonSound.Play();
        StartCoroutine(ReplayS());
    }

    IEnumerator ReplayS()
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("Lvl1");
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }

    public void GoNextLevelScene()
    {
        uibuttonSound.Play();
        SceneManager.LoadScene("Lvl2");
    }
}
