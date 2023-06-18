using UnityEngine;
using UnityEngine.Events;
public class HealthSystem : MonoBehaviour {
    public float health = 100;
    public UnityEvent onDeath = new UnityEvent();
    public void addHealth(float amount) {
        health+=amount;
    }
    public void decreasedHealth(float amount) {
        bool willDie = health>0&&health-amount<=0;
        if(willDie) {
            health=0;
            onDeath.Invoke();
        } else if(health > 0){
            health-=amount;
        }
    }

}