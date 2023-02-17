using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialScript : MonoBehaviour
{
    public Text NarratorText;
    public GameObject TutorialKnight;
    // Start is called before the first frame update
    void Start()
    {
        NarratorText.text = "Hello Timmy II! It seems you are in prison, like usual.";
        Invoke("KnightShowUpText", 3.5f);
    }

    void KnightShowUpText()
    {
        NarratorText.text = "Oh Look! The knight here to give your your daily lunch.";
        TutorialKnightScript.TutorialKnightCode.KnightWalksToDestination();
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
