using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using TMM.core;

namespace TMM.Cinematics
{
    public class CinematicsControlRemover : MonoBehaviour
    {
        private void Start()
        {
            GetComponent<PlayableDirector>().played += DisableControl; // adding method to list of callbacks
            GetComponent<PlayableDirector>().stopped += EnableControl;
        }
        //Observer Pattern: here we want to get notification when the cinematic is finished so we knoow when to enable and disable control
        // here we are observing when the cinematic is finished (dependency inversion), when director finishes playing it will call to observers without needing to know
        // who the observers are similar to interface previously implemented.
        void DisableControl(PlayableDirector cutscene)
        {
            GameObject player = GameObject.FindWithTag("Hero");
            print("Disable Control");
        }

        void EnableControl(PlayableDirector cutscene)
        {
            print("Enable Control");
        }
    }
}