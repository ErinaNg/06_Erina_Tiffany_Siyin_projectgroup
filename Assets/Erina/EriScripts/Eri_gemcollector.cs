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

    public GameObject GameOverUI;
    public GameObject GameWinUI;
    public GameObject PauseMenu;
    public GameObject uiMessage;
    public AudioClip GameWinSound;
    public AudioClip GameLoseSound;
    private AudioSource audioSource;
    public GameObject replay;
    private Animator anim;
    // Start is called before the first frame update


    void Awake()
    {
        GameOverUI.SetActive(false);
        GameWinUI.SetActive(false);
        PauseMenu.SetActive(false);
        replay.SetActive(false);
        audioSource = GetComponent<AudioSource>();
    }
    private void Start()
    {
        Being(Duration);
        anim = GetComponentInChildren<Animator>();
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
        if (isGameOver)
            return;

        //Timer = Timer + Time.deltaTime;
        //TimerSeconds = Mathf.FloorToInt(Timer % 60);
        //TimerText.text = "Timer: " + TimerSeconds.ToString();
        if (remainDuration <= 0 && gem < 8)
        {
            GameOverUI.SetActive(true);
            replay.SetActive(true);
            // SceneManager.LoadScene("GameLose"); use UI
        }
    }

    public void SetGameOver(bool isWin)
    {
        isGameOver = true;

        if (isWin)
        {

            audioSource.PlayOneShot(GameWinSound);
            GameOverUI.SetActive(false);
            PauseMenu.SetActive(false);
            GameWinUI.SetActive(true);
            IsWinShown = true;
        }
        else
        {
            if (!GameOverIsPlayed)
            {
                audioSource.PlayOneShot(GameLoseSound);

                GameOverIsPlayed = true;
            }
            GameOverUI.SetActive(true);
            GameWinUI.SetActive(false);
            PauseMenu.SetActive(false);
        }
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

        if (other.gameObject.tag == "Tutorial")
        {
            deathSound.Play();
            if (isGameOver)
            {
                SetGameOver(true);
            }
            SceneManager.LoadScene("GameLose");
        }


        if (other.gameObject.tag == "Boss")
        {
            deathSound.Play();
            if (isGameOver)
            {
                SetGameOver(true);
            }
            SceneManager.LoadScene("GameLose");
        }

        if (other.gameObject.tag == "Goal")  //goal is the door to escape
        {
            if (gem >= 8)       //Win // Add another && OnCollision with end goal then win
            {
                anim.SetTrigger("Win");
                GameWinUI.SetActive(true);
                replay.SetActive(true);
                uiMessage.SetActive(false);
                StartCoroutine(LoadToNextScene());

            }
            else
            {
                StartCoroutine(ExampleCoroutine());
                uiMessage.SetActive(true);
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
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene("Lvl2");
    }

    void OnCollisionEnter(Collision otherObj)
    {
        if (otherObj.gameObject.tag == "Confetti")
        {
            Destroy(gameObject, .5f);
        }
    }
}
