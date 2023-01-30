using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMM.Control;
using TMM.core;

namespace TMM.Combat
{
    public class PlayerCombat : MonoBehaviour
    {
        //Components
        Animator playerAnimations;
        [SerializeField] public GameObject generatedPlayerProjectile;
        [SerializeField] public Transform swordSlashTip;
        Fighting currentProjectile;

        //Player Status
        bool isAlive = true;

        //Basic Combat
        [Header("AttackMechanics")]
        public float attackRate = 1f;
        float nextATtackTime = 0f;
        int attackCounter = 0;
        void Start()
        {
            InputValue value;
            playerAnimations = GetComponent<Animator>();
            currentProjectile = GetComponent<Fighting>();
        }

        // Update is called once per frame
        void Update()
        {

        }
        void OnFire(InputValue value)
        {
            if (!isAlive) { return; }
            if (Time.time > nextATtackTime)
            {
                if (value.isPressed)
                {

                    playerAnimations.SetTrigger("Attack");
                    Instantiate(generatedPlayerProjectile, swordSlashTip.position, Quaternion.identity);
                    // damagedealt.GetDamage();
                    nextATtackTime = Time.time + .7f / attackRate;
                }
            }
        }
        public Vector3 getSwordTip()
        {
            return swordSlashTip.position;
        }

        public GameObject getProjectile()
        {
            return generatedPlayerProjectile;
        }
    }
}
