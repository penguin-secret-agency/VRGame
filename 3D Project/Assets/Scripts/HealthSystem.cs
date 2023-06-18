using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour {
    public float health = 100;
    public GameManagerScript gameManager;
    public void addHealth(float amount) {
        health+=amount;
    }
    public void decreasedHealth(float amount) {
        bool willDie = health>0&&health-amount<=0;
        if(willDie) {
            health=0;
            gameManager.GameOver();
            disablePlayer();
        } else if(health > 0){
            health-=amount;
        }
    }

    private void disablePlayer() {
        PlayerLook look = gameObject.GetComponent<PlayerLook>();
        PlayerMovement movement = gameObject.GetComponent<PlayerMovement>();
        if(look!= null) {
            look.enabled=false;
        }
        if(movement!=null) {
            movement.enabled=false;
        }
    }

}