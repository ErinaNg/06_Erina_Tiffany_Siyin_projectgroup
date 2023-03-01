using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EriDialogue: MonoBehaviour
{
    public Text textComponent;
    public string[] sentences;
    public float textSpeed;
    private int index;
    private AudioSource audioSource;
    [SerializeField] private AudioSource typeSound;
    //public string name;
    //[TextArea(3, 10)]
    // Start is called before the first frame update

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        textComponent.text = string.Empty;
        StartDialogue();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            if(textComponent.text == sentences[index])
            {
                NextLine();
            }
            else
            {
                StopAllCoroutines();
     
                textComponent.text = sentences[index];
            }
        }
    }

    void StartDialogue()
    {
        index = 0;
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        foreach(char c in sentences[index].ToCharArray()) //break string into char array
        {
            typeSound.Play();
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }

    void NextLine()
    {
        if(index < sentences.Length - 1)
        {
            index++;
            textComponent.text = string.Empty;
            StartCoroutine(TypeLine());
      
        }
        else
        {
            gameObject.SetActive(false);
            Cursor.lockState = CursorLockMode.Confined;
        }
    }

}
