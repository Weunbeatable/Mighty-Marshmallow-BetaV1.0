using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMM.Control;
using TMM.core;
using System;

namespace TMM.Combat
{
    public class PlayerCombat : MonoBehaviour
    {
        public static event Action onActivateWeapon;
        public static event Action onDeactivateWeapon;
        //Components
        Animator playerAnimations;
        [SerializeField] public GameObject generatedPlayerProjectile;
        [SerializeField] public Transform swordSlashTip;
        private bool firedProjectile;
        Fighting currentProjectile;

        //Player Status
        bool isAlive = true;

        //Basic Combat
        [Header("AttackMechanics")]
        public float attackRate = 1f;
        float nextATtackTime = 0f;
        float nextProjectileAttackTime = 0f;
        public float setNextProjectileTime = 3f;
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

            if (firedProjectile == true)
            {
                nextProjectileAttackTime += Time.deltaTime;
            }
               
            if (nextProjectileAttackTime >= setNextProjectileTime) {
                firedProjectile = false;
            }
            Debug.Log("next attack time is " +  nextProjectileAttackTime);
            Debug.Log("is projectile fired " + firedProjectile);
            if (!playerAnimations.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))
            {
                onDeactivateWeapon?.Invoke();
            }
        }
        void OnFire(InputValue value)
        {
            if (!isAlive) { return; }
            playerAnimations.SetTrigger("Attack");
            onActivateWeapon?.Invoke();
           
            if (firedProjectile == true) { return; }
            else if (firedProjectile == false)
            {
                Instantiate(generatedPlayerProjectile, swordSlashTip.position, Quaternion.identity);
                firedProjectile = true;
                nextProjectileAttackTime = 0;
                // damagedealt.GetDamage();
        /*        nextATtackTime = Time.time + .7f / attackRate;*/

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


       public float GetProjectileTime() { return nextProjectileAttackTime / setNextProjectileTime; }

    }
}
