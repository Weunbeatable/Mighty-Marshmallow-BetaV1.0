using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables; // related to playable director.

namespace TMM.Cinematics
{
    public class CinematicTrigger : MonoBehaviour
    {
       bool isTriggered = false;
        
        private void OnTriggerEnter2D(Collider2D collision)
        {
            
         
            if (collision.gameObject.tag == "Hero" && !isTriggered)
            {
                isTriggered = true;
                GetComponent<PlayableDirector>().Play();
            }                  
        }
    }

}