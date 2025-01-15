using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public abstract class TowerBase : MonoBehaviour
{
    public enum ElementType
    {
        Fire,
        Earth,
        Thunder,
        Darkness,
        Nature,
        Synergy
    }

    [Header("Tower Properties")]
    public ElementType towerElement; // Allows setting the element in Inspector or dynamically

    public readonly List<GameObject> enemyList = new();
    private bool canAttack = false;

    [Header("Bullet")]
    public int projectileAmount = 1;
    public string projectileName = "Name of your bullet here";
    public bool projectileFinishEffect = false;

    [Header("Tower stats")]
    public float projectileSpeed = 5f;
    [HideInInspector] public float fireCountdown = 0f;
    public float fireCooldown = 1f;
    public float towerDamage = 10f;
    public float towerRadius = 1f;
    public float chancePercentage = 30f;
    private CircleCollider2D myCircleCollider;

    private void Start()
    {
        myCircleCollider = GetComponent<CircleCollider2D>();

        if (myCircleCollider != null)
        {
            // Set the new radius
            myCircleCollider.radius = towerRadius * 50f + 25f;
        }
        else
        {
            Debug.LogError("No CircleCollider2D component found on this GameObject!");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            enemyList.Add(collision.gameObject);
        }
    }

    public virtual void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            canAttack = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            enemyList.Remove(collision.gameObject);
            canAttack = false;
        }
    }

    public virtual void Update()
    {
        if (fireCountdown <= 0f && canAttack)
        {
            Shoot();
            fireCountdown = fireCooldown;
        }
        fireCountdown -= Time.deltaTime;
    }

    public virtual void Shoot()
    {
        float randomValue = Random.Range(0f, 100f);

        if (randomValue < chancePercentage)
        {
            // Execute your action here
            TowerTrigger();
        }
        else
        {
            // Action did not occur
        }
    }

    public virtual void TowerTrigger()
    {
        // Implement your action logic here
        //Debug.Log("Triggered!");
    }

    public virtual IEnumerator ProjectileCoroutine(GameObject go, GameObject target)
    {
        while (target != null && go.activeInHierarchy)
        {
            float step = projectileSpeed * Time.deltaTime;
            go.transform.position = Vector2.MoveTowards(go.transform.position, target.transform.position, step);

            if (Vector2.Distance(go.transform.position, target.transform.position) < 0.1f)
            {
                float randomValue = Random.Range(0f, 100f);

                if (randomValue < chancePercentage)
                {
                    // Execute your action here
                    ProjectileTrigger(target);
                }
                else
                {
                    // Action did not occur
                }

                if(projectileFinishEffect)
                    ProjectileFinish(target);

                DoDamage(target, towerDamage);
                go.SetActive(false);
            }

            yield return null;
        }

        // Return the projectile to the pool or handle deactivation
        go.SetActive(false);
    }

    public void DoDamage(GameObject target, float damage)
    {
        target.GetComponent<EnemyHealth>().GetEnemyHP(damage);
        GameObject textDamageGO = PoolBase.instance.GetObject("Damage text", target.transform.position);
        textDamageGO.GetComponent<TextMeshProUGUI>().text = this.towerDamage.ToString();
        StartCoroutine(TextDamageDeactivation(textDamageGO));
    }

    public virtual void ProjectileTrigger(GameObject target)
    {

    }

    public virtual void ProjectileFinish(GameObject target)
    {
        
    }

    IEnumerator TextDamageDeactivation(GameObject textGO)
    {
        yield return new WaitForSeconds(0.1f);
        textGO.SetActive(false);
    }

    // Adds targerRadius amount to the radius
    public void ChangeTowerRadius(float radiusAmount)
    {
        GetComponent<CircleCollider2D>().radius += radiusAmount;
        towerRadius = GetComponent<CircleCollider2D>().radius;
    }

    public void ChangeAttackSpeed(float speedAmount)
    {
        // Convert speedAmount to a percentage (e.g., 10.0 will become 0.1)
        float reductionPercentage = speedAmount / 100f;

        // Calculate the amount to reduce fireCooldown by percentage
        float reductionAmount = fireCooldown * reductionPercentage;

        // Apply the reduction to fireCooldown
        fireCooldown -= reductionAmount;
    }
}