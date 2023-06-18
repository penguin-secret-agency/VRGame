using System.Collections;
using UnityEngine;

public class LightDamage : MonoBehaviour
{
    public Light lightSource;
    public float baseDamageMultiplier = 10f;
    public LayerMask targetMask;
    public LayerMask obstructionMask;
    [Range(0.1f, 1f)]
    public float damageTimer = 0.1f;
    void Start()
    {
        lightSource=gameObject.GetComponent<Light>();
        StartCoroutine(dealDamageRoutine());
    }

    private IEnumerator dealDamageRoutine() {
        WaitForSeconds wait = new WaitForSeconds(damageTimer);

        while(true) {
            dealDamage();
            yield return wait;
        }
    }

    private void dealDamage() {
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, lightSource.range, targetMask);

        if(rangeChecks.Length!=0) {
            Transform target = rangeChecks[0].transform;
            Vector3 directionToTarget = (target.position-transform.position).normalized;

            if(Vector3.Angle(transform.forward, directionToTarget)<(360/2)) {
                float distanceToTarget = Vector3.Distance(transform.position, target.position);

                if(!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstructionMask)) {
                    damage(target.gameObject);
                }
            }
        }
    }
    public void damage(GameObject gameObject) {
        HealthSystem enemyHealthSystem = gameObject.GetComponent<HealthSystem>();
        if(!enemyHealthSystem) {
            return;
        }
        float rawDamage = lightSource.intensity*baseDamageMultiplier;
        enemyHealthSystem.decreasedHealth(rawDamage*damageTimer);
    }
}
