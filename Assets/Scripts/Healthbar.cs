using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    public Image healthState;
    public void DisplayHealth(int maxHealth, int health)
    {
        healthState.fillAmount = (float)health/(float)maxHealth;
    }
}
