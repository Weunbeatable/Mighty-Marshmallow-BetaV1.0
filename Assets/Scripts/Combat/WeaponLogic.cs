using System.Collections;
using System.Collections.Generic;
using TMM.Combat;
using Unity.VisualScripting;
using UnityEngine;

public class WeaponLogic : MonoBehaviour
{
    public Collider2D weaponCollider;
    [SerializeField] int swordDamage = 25;
    private void OnEnable()
    {
/*        PlayerCombat.onActivateWeapon += PlayerCombat_onActivateWeapon;
        PlayerCombat.onDeactivateWeapon += PlayerCombat_onDeactivateWeapon;*/
    }
    private void PlayerCombat_onActivateWeapon()
    {
        weaponCollider.enabled = true;
        Debug.Log("Turned on Weapon");
    }
    private void PlayerCombat_onDeactivateWeapon()
    {
        weaponCollider.enabled = false;
        Debug.Log("Turned off weapon ");
    }
    private void OnDisable()
    {
   /*     PlayerCombat.onActivateWeapon -= PlayerCombat_onActivateWeapon;
        PlayerCombat.onDeactivateWeapon -= PlayerCombat_onDeactivateWeapon;*/
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!weaponCollider.enabled) { return; }
        if (collision.gameObject.TryGetComponent<Health>(out Health health) && collision.gameObject.tag != "Hero")
        {
            health.TakeDamage(swordDamage);
            print("Sword Hit You ");
        }
    
    }
}
