using UnityEngine;
using UnityEngine.Events;
public class HealthSystem : MonoBehaviour {
    public float health = 100;
    private float totalHealth;
    private GameObject attacker;
    public UnityEvent onDeath = new UnityEvent();
    public UnityEvent onDamageTaken = new UnityEvent();

    private void Start() {
        totalHealth=health;
    }
    public float getTotalHealth() {
        return totalHealth;
    }
    public void addHealth(float amount) {
        health+=amount;
    }
    public GameObject getAttacker() {
        return this.attacker;
    }
    public void decreasedHealth(float amount, GameObject attacker) {
        if(health<=0) {
            return;   
        }
        bool willDie = health-amount<=0;
        this.attacker=attacker;
        if(willDie) {
            health=0;
            onDeath.Invoke();
        } else {
            health-=amount;
            onDamageTaken.Invoke();
        }
    }

}