using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using TMM.core;
using TMM.Control;
using TMM.Combat;
using UnityEngine.InputSystem;

namespace TMM.Cinematics
{
    public class CinematicsControlRemover : MonoBehaviour
    {
        //Observer Pattern: here we want to get notification when the cinematic is finished so we knoow when to enable and disable control
        // here we are observing when the cinematic is finished (dependency inversion), when director finishes playing it will call to observers without needing to know
        // who the observers are similar to interface previously implemented.

        private void OnEnable()
        {
            GetComponent<PlayableDirector>().played += DisableControl; // adding method to list of callbacks
            GetComponent<PlayableDirector>().stopped += EnableControl;
        }
        void DisableControl(PlayableDirector cutscene)
        {
            GameObject player = GameObject.FindWithTag("Hero");
            player.GetComponent<PlayerInput>().enabled = false;
            player.GetComponent<PlayerCombat>().enabled = false;
            print("Disable Control");
        }

        void EnableControl(PlayableDirector cutscene)
        {
            GameObject player = GameObject.FindWithTag("Hero");
            player.GetComponent<PlayerInput>().enabled = true;
            player.GetComponent<PlayerCombat>().enabled = true;
            print("Enable Control");
        }

        private void OnDisable()
        {
             GetComponent<PlayableDirector>().played -= DisableControl; // adding method to list of callbacks
            GetComponent<PlayableDirector>().stopped -= EnableControl;
        }
    }
}