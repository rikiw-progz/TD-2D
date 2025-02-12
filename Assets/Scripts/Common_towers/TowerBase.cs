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

        Fire_Earth,
        Fire_Thunder,
        Fire_Darkness,
        Fire_Nature,
        Earth_Nature,
        Earth_Darkness,
        Earth_Thunder,
        Nature_Darkness,
        Nature_Thunder,
        Darkness_Thunder,

        Synergy
    }

    public HashSet<ElementType> elements = new();

    public void SetElements(params ElementType[] newElements)
    {
        elements.Clear();
        foreach (var element in newElements)
        {
            elements.Add(element);
        }
    }

    [Header("Tower Properties")]
    public ElementType towerElement; // Allows setting the element in Inspector or dynamically

    public readonly List<GameObject> enemyList = new();
    private bool canAttack = false;

    [Header("Projectile")]
    public int projectileAmount = 1;
    public string projectileName = "Name of your bullet here";
    public bool projectileFinishTrigger = false;
    public bool projectileFinishEffect = false;
    [HideInInspector] public GameObject projectileGO;
    [HideInInspector] public GameObject triggerProjectileGO;

    [Header("Tower stats")]
    [SerializeField] public float projectileSpeed = 5f;
    [HideInInspector] public float fireCountdown = 0f;
    public float fireCooldown = 1f;
    public float towerDamage = 10f;
    public float towerRadius = 1f;
    public float chancePercentage = 30f;
    public float abilityDamage = 10f;
    [SerializeField] public float triggerProjectileSpeed = 3f;
    private CircleCollider2D myCircleCollider;

    private GameObject stageManager;
    private DamageTextHandler _damageTextHandler;

    private float triggerRandomValue;
    private float missAttackRandomValue;
    public float missAttackBaseChancePercentage = 0f;

    public virtual void Start()
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

        stageManager = GameObject.FindWithTag("Stage Manager");
        _damageTextHandler = stageManager.GetComponent<DamageTextHandler>();
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
        triggerRandomValue = Random.Range(0f, 100f);

        if (triggerRandomValue < chancePercentage)
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
                if(projectileFinishTrigger)
                    ProjectileFinish(target);

                DoDamage(target, towerDamage);
                go.SetActive(false);
            }

            yield return null;
        }

        // Return the projectile to the pool or handle deactivation
        go.SetActive(false);
    }
    
    public virtual IEnumerator TriggerProjectileCoroutine(GameObject go, GameObject target)
    {
        while (target != null && go.activeInHierarchy)
        {
            float step = triggerProjectileSpeed * Time.deltaTime;
            go.transform.position = Vector2.MoveTowards(go.transform.position, target.transform.position, step);

            if (Vector2.Distance(go.transform.position, target.transform.position) < 0.1f)
            {
                DoTriggerDamage(target, abilityDamage);
                    go.SetActive(false);

                if (projectileFinishEffect)
                    TriggerProjectileFinishEffect(target);
            }

            yield return null;
        }

        // Return the projectile to the pool or handle deactivation
        go.SetActive(false);
    }

    public virtual IEnumerator LineRendererProjectileCoroutine(GameObject go, GameObject target)
    {
        if (target != null && go.activeInHierarchy)
        {
            go.GetComponent<CustomLineRenderer>().CustomSetUpLine(this.transform.position, target.transform.position);
            DoDamage(target, towerDamage);

            yield return new WaitForSeconds(0.15f);
            go.SetActive(false);

            if (projectileFinishTrigger)
                ProjectileFinish(target);

            yield return null;
        }

        // Return the projectile to the pool or handle deactivation
        go.SetActive(false);
    }

    public void DoDamage(GameObject target, float damage)
    {
        missAttackRandomValue = Random.Range(0f, 100f);

        if (missAttackRandomValue > missAttackBaseChancePercentage)
        {
            target.GetComponent<EnemyHealth>().GetEnemyHP(damage);
        }
        else
        {
            _damageTextHandler.MissTextEnable(target.transform);
        }
    }
    
    public void DoTriggerDamage(GameObject target, float damage)
    {
        target.GetComponent<EnemyHealth>().GetEnemyHP(damage);
    }

    public virtual void ProjectileFinish(GameObject target)
    {
        float randomValue = Random.Range(0f, 100f);

        if (randomValue < chancePercentage)
        {
            // Execute your action here
            ProjectileFinishTrigger(target);
        }
    }

    public virtual void ProjectileFinishTrigger(GameObject target)
    {
        // Implement your action logic here
    }

    public virtual void TriggerProjectileFinishEffect(GameObject target)
    {
        // write your effects here
    }

    // Adds targetRadius amount to the radius
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

    private void OnDisable()
    {
        if (projectileGO != null)
            projectileGO.SetActive(false);

        if (triggerProjectileGO != null)
            triggerProjectileGO.SetActive(false);
    }
}