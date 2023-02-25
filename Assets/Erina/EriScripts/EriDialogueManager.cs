using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EriDialogueManager : MonoBehaviour
{
//    public Text nameUI;
//    public Text contentUI;
//    private Queue<string> sentences; //Fifo collections //restricted
//    // Start is called before the first frame update
//    void Start()
//    {
//        sentences = new Queue<string>();
//    }

//    public void StartDialogue (EriDialogue eriDialogue)
//    {
//        Debug.Log("Starting conversation with" + eriDialogue.name);
//        sentences.Clear();
//        nameUI.text = eriDialogue.name;
//        foreach (string sentence in eriDialogue.sentences)
//        {
//            sentences.Enqueue(sentence);
//        }
//        DisplayNextSentence();
//    }

//    public void DisplayNextSentence()
//    {
//        if(sentences.Count == 0)
//        {
//            EndDialogue();
//            return;
//        }

//        string sentence = sentences.Dequeue();
//        contentUI.text = sentence;
//        //Debug.Log(sentence);
//    }

//    void EndDialogue()
//    {
//        Debug.Log("End of conversation.");
//    }
}
