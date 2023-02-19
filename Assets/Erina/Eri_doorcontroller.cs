using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eri_doorcontroller : MonoBehaviour
{
    public int id;
    private void Start()
    {
        GameEvents.current.onDoorwayTriggerEnter += OnDoorwayOpen; //acess action //declare action as events
        GameEvents.current.onDoorwayTriggerEnter += OnDoorwayClose;
    }

    private void OnDoorwayClose(int id)
    {
        if (id == this.id)
        {
            LeanTween.moveLocalY(gameObject, .75f, 1f).setEaseInQuad();
        }

    }
    private void OnDoorwayOpen(int id)
    {
        if (id == this.id)
        {
            LeanTween.moveLocalY(gameObject, 1.6f, 1f).setEaseOutQuad();
        }
           
    }

   
}
