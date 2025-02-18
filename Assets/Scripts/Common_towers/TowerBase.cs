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

    [Header("Tower Properties")]
    public ElementType towerElement; // Allows setting the element in Inspector or dynamically

    public readonly List<GameObject> enemyList = new();
    private bool canAttack = false;

    [Header("Projectile")]
    public int projectileAmount = 1;
    public string projectileName = "Name of your bullet here";
    public bool projectileFinishTrigger = false;
    public bool abilityProjectileFinishEffect = false;
    [HideInInspector] public GameObject projectileGO;
    [HideInInspector] public GameObject triggerProjectileGO;

    [Header("Tower stats")]
    [SerializeField] public float projectileSpeed = 5f;
    [HideInInspector] public float fireCountdown = 0f;
    public float fireCooldown = 1f;
    public float towerDamage = 10f;
    public float towerRadius = 1f;
    public float chancePercentage = 30f;
    public int abilityProjectileAmount = 1;
    public float abilityDamage = 10f;
    [SerializeField] public float abilityProjectileSpeed = 3f;
    public float abilityFinishChancePercentage = 20f;
    private CircleCollider2D myCircleCollider;

    private GameObject stageManager;
    private DamageTextHandler _damageTextHandler;

    private float triggerRandomValue;
    protected float abilityTriggerRandomValue;
    private float missAttackRandomValue;
    public float missAttackBaseChancePercentage = 0f;

    [SerializeField] private int killCount;
    private bool isDead;

    [Header("Attack Damage Buff")]
    private Dictionary<string, float> _activeAttackDamageBuffs = new();
    private Dictionary<string, GameObject> _activeAttackDamageBuffGO = new();
    private Dictionary<string, Coroutine> _activeAttackDamageBuffCoroutines = new();
    private GameObject buffAttackDamageGO;

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
                if (projectileFinishTrigger)
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
            float step = abilityProjectileSpeed * Time.deltaTime;
            go.transform.position = Vector2.MoveTowards(go.transform.position, target.transform.position, step);

            if (Vector2.Distance(go.transform.position, target.transform.position) < 0.1f)
            {
                DoTriggerDamage(target, abilityDamage);
                go.SetActive(false);

                if (abilityProjectileFinishEffect)
                    AbilityProjectileFinishEffect(target);
            }

            yield return null;
        }

        // Return the projectile to the pool or handle deactivation
        go.SetActive(false);
    }

    public virtual IEnumerator LineRendererProjectileCoroutine(GameObject go, GameObject target, float damage)
    {
        if (target != null && go.activeInHierarchy)
        {
            go.GetComponent<CustomLineRenderer>().CustomSetUpLine(this.transform.position, target.transform.position);
            DoDamage(target, damage);

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
            DamageAndCheckFate(target, damage);
        }
        else
        {
            _damageTextHandler.MissTextEnable(target.transform);
        }
    }

    public void DoTriggerDamage(GameObject target, float damage)
    {
        DamageAndCheckFate(target, damage);
    }

    private void DamageAndCheckFate(GameObject target, float damage)
    {
        EnemyHealth enemyHealth = target.GetComponent<EnemyHealth>();
        if (enemyHealth != null)
        {
            isDead = enemyHealth.GetEnemyHP(damage);
            if (isDead)
            {
                IncreaseTowerKillCount();
                TowerKillTrigger(target);
            }
        }
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

    public virtual void AbilityProjectileFinishEffect(GameObject target)
    {
        // write your effects here
        abilityTriggerRandomValue = Random.Range(0f, 100f);

        if (abilityTriggerRandomValue < chancePercentage)
        {
            // Execute your action here
            AbilityFinishEffectAction(target);
        }
        else
        {
            // Action did not occur
        }
    }

    public virtual void AbilityFinishEffectAction(GameObject target)
    {

    }

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

    public void IncreaseTowerKillCount()
    {
        killCount++;
        isDead = false;
    }

    public virtual void TowerKillTrigger(GameObject target)
    {

    }

    public void ApplyAttackDamageBuff(float attackDamagePercent, float attackDamageBuffDuration, string attackDamageBuffName)
    {
        if (this.gameObject.activeInHierarchy)
        {
            if (_activeAttackDamageBuffCoroutines.ContainsKey(attackDamageBuffName))
            {
                StopCoroutine(_activeAttackDamageBuffCoroutines[attackDamageBuffName]);
            }

            _activeAttackDamageBuffCoroutines[attackDamageBuffName] = StartCoroutine(AttackDamageBuffCountdown(attackDamagePercent, attackDamageBuffDuration, attackDamageBuffName));
        }
    }

    public IEnumerator AttackDamageBuffCountdown(float attackDamagePercent, float attackDamageBuffDuration, string attackDamageBuffName)
    {
        if (!_activeAttackDamageBuffs.ContainsKey(attackDamageBuffName))
        {
            buffAttackDamageGO = PoolBase.instance.GetObject(attackDamageBuffName, this.transform.position);
            buffAttackDamageGO.transform.SetParent(this.transform);
            buffAttackDamageGO.transform.localPosition = new Vector2(0f, 0f);
            _activeAttackDamageBuffGO.Add(attackDamageBuffName, buffAttackDamageGO);

            // Apply the slow effect
            towerDamage *= (1 + attackDamagePercent / 100);

            // Add the debuff to the list of active debuffs
            _activeAttackDamageBuffs.Add(attackDamageBuffName, attackDamagePercent);
        }

        yield return new WaitForSeconds(attackDamageBuffDuration);

        towerDamage *= 1 / ((1 + _activeAttackDamageBuffs[attackDamageBuffName] / 100));

        _activeAttackDamageBuffs.Remove(attackDamageBuffName);
        _activeAttackDamageBuffGO[attackDamageBuffName].SetActive(false);
        _activeAttackDamageBuffGO.Remove(attackDamageBuffName);
        _activeAttackDamageBuffCoroutines.Remove(attackDamageBuffName);

        // Change parent instead of destroying?
    }

    protected void OnDisable()
    {
        if (projectileGO != null)
            projectileGO.SetActive(false);

        if (triggerProjectileGO != null)
            triggerProjectileGO.SetActive(false);

        StopAllCoroutines();
    }
}