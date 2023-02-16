using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialScript : MonoBehaviour
{
    public Text NarratorText;
    public GameObject TutorialKnight;
    public GameObject FoodTray;
    // Start is called before the first frame update
    void Start()
    {
        FoodTray.active = false;
        NarratorText.text = "Hello Timmy II! It seems you are in prison, like usual.";
        Invoke("KnightShowUpText", 3.0f);
    }

    void KnightShowUpText()
    {
        NarratorText.text = "Oh Look the knight here to give your your daily lunch.";
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
