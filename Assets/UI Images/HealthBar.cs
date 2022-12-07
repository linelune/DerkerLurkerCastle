using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Image healthBarSprite;

    public void UpdateHealthBar(float maxHealth, float current)
    {
        healthBarSprite.fillAmount = current / maxHealth;
    }
}
