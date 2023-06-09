using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{

    public float health = 100;

    public void addHealth(float amount)
    {
        health += amount;
    }

    public void decreasedHealth(float amount)
    {
        health -= amount;
        if (health <= 0)
        {
            health = 0;
        }
    }
}

