using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombat : MonoBehaviour
{
    public enum EnemyType { GroundMelee = 0, GroundRange = 1, Air = 2 /*rightSpecial = 3*/ }
    public EnemyType currentType;
    GameObject player;
    [SerializeField] public GameObject enemyProjectile;

    void Start()
    {
        player = GameObject.FindWithTag("Hero");
    }

    // Update is called once per frame
    void Update()
    {

       // CombatAttackTypeCheck();
        
    }

    public void CombatAttackTypeCheck()
    {
        if (currentType == EnemyType.GroundMelee)
        {
            GroundMeleeCombatBehaviour();
        }
        if (currentType == EnemyType.GroundRange)
        {
            GroundRangeCombatBehaviour();
        }
        if (currentType == EnemyType.Air)
        {
            AirCombatBehaviour();
        }
    }

    public void AirCombatBehaviour()
    {
        EnemyProjectile();
        GetComponent<Animator>().SetTrigger("Attack");
        
      //  Debug.Log("aerial type");

    }

    public void GroundRangeCombatBehaviour()
    {
        GetComponent<Animator>().SetTrigger("Attack");
        EnemyProjectile();
       // Debug.Log("ground range type");
    }

    public void GroundMeleeCombatBehaviour()
    {
        GetComponent<Animator>().SetTrigger("Attack");
      //  Debug.Log("Melee type");
    }


    public void EnemyProjectile()
    {
        Instantiate(enemyProjectile, this.transform.position, player.transform.rotation);
    }
}
