using System.Collections;
using System.Collections.Generic;
using TMM.Combat;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    public Image playerHealthBar;
    public Health playerHealth;
    public PlayerCombat player;
    public Image projectileCooldown;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float _healthPercentage = playerHealth.GetNormalizedHealth();
        playerHealthBar.fillAmount = _healthPercentage;  // turn current health into a % value so it can be used from 0-1 in accordance with fillAmount
        projectileCooldown.fillAmount = player.GetProjectileTime();
    }
}
