using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;

public class EnemyHealth : MonoBehaviour
{
    public EnemyType enemyType;

    public enum EnemyType
    {
        Simple,
        Boss_1,
        Boss_2,
        Boss_3,
        Unknown
    }
    public float enemyHP;
    private Image _image;
    private Color _color;
    private bool colored = false;
    private GameObject stageManager;
    public bool damageOverTimeDuration = false;
    private bool experienceGained = false;
    private DamageTextHandler _damageTextHandler;
    private GameRules _gameRules;
    private EnemyPathing _enemyPath;
    private bool isDead;
    [SerializeField] private Image healthFill;
    private float currentHealth;
    [HideInInspector] public float maxHealth;

    [Header("Armor")]
    public float armor = 0f;
    private float debuffArmor = 0f;
    public float coefficient = 0.04f;
    private float damageReduction;
    private float damageAmount;

    private void Start()
    {
        _image = GetComponent<Image>();
        _color = _image.color;
        stageManager = GameObject.FindWithTag("Stage Manager");
        _damageTextHandler = stageManager.GetComponent<DamageTextHandler>();
        _gameRules = stageManager.GetComponent<GameRules>();
        _enemyPath = stageManager.GetComponent<EnemyPathing>();
    }

    private void OnEnable()
    {
        this.GetComponent<Image>().color = Color.white;
        experienceGained = false;
        colored = false;
        isDead = false;
    }

    public bool GetEnemyHP(float damage)
    {
        if(this.gameObject.activeInHierarchy)
            StartCoroutine(DamageColorReaction());

        // Calculate and log the damage reduction
        damageReduction = CalculateDamageReduction(armor - debuffArmor, coefficient);

        damageAmount = damageReduction * damage;

        if (enemyHP > 0)
            _damageTextHandler.DamageTextEnable((int)damageAmount, this.transform);         // damage text

        enemyHP -= damageAmount;

        SetHealth(enemyHP, maxHealth);

        if (enemyHP <= 0 && !isDead)
        {
            isDead = true;
            Death();
            return true;
        }

        return false;
    }

    public void SetHealth(float health, float maxHealth)
    {
        if (health == currentHealth) return; // Prevent unnecessary updates
        this.currentHealth = health;
        this.maxHealth = maxHealth;
        healthFill.fillAmount = health / maxHealth; // Efficient scaling
    }

    // Method to calculate damage reduction
    private float CalculateDamageReduction(float armorValue, float coeff)
    {
        return (1 - (armorValue * coeff) / (1 + armorValue * coeff));
    }

    IEnumerator DamageColorReaction()
    {
        if (colored) yield break;

        colored = true;
        _image.color = Color.red;
        yield return new WaitForSeconds(0.05f);
        _image.color = _color;
        colored = false;
    }

    public void DebuffDamage(float damageAmount, float duration, string debuffName)
    {
        if (this.gameObject.activeInHierarchy && damageOverTimeDuration == false)
            StartCoroutine(ApplyDamageOverTime(damageAmount, duration, debuffName));
    }

    private IEnumerator ApplyDamageOverTime(float damage, float duration, string debuffName)
    {
        damageOverTimeDuration = true;

        GameObject debuffGO = PoolBase.instance.GetObject(debuffName, this.transform.position);
        debuffGO.transform.SetParent(this.transform);
        debuffGO.transform.localPosition = Vector2.zero;

        while (damageOverTimeDuration == true)
        {
            GetEnemyHP(damage);

            // Wait for one second before applying damage again
            yield return new WaitForSeconds(1f);
            duration -= 1f;
            if (duration <= 0)
            {
                damageOverTimeDuration = false;
                // disable debuff effects
                if (transform.childCount > 0)
                    DisablingAllDebuffs();
            }
        }
    }

    public void DebuffArmor(float armorAmount, float duration, string debuffName)
    {
        if (this.gameObject.activeInHierarchy)
            StartCoroutine(ApplyArmorDebuff(armorAmount, duration, debuffName));
    }

    private IEnumerator ApplyArmorDebuff(float armorAmount, float duration, string debuffName)
    {
        GameObject debuffArmorGO = PoolBase.instance.GetObject(debuffName, this.transform.position);        // maybe check if it was debuffed before then just activate gameobject not pool
        debuffArmorGO.transform.SetParent(this.transform);
        debuffArmorGO.transform.localPosition = Vector2.zero;

        debuffArmor = armorAmount;

        while (duration > 0f)
        {
            yield return new WaitForSeconds(1f);
            duration -= 1f;
        }

        debuffArmor = 0f;
        debuffArmorGO.SetActive(false);
    }

    private void DisablingAllDebuffs()
    {
        for (int i = 1; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }
    }

    public void Death()
    {
        if(experienceGained == false)
        {
            if(enemyType == EnemyType.Simple)
                _gameRules.ExperienceGain(1f);
            else if (enemyType == EnemyType.Boss_1)
            {
                _gameRules.ExperienceGain(2f);
                _gameRules.GetFirstEssence(1);
            }
            else if (enemyType == EnemyType.Boss_2)
            {
                _gameRules.ExperienceGain(3f);
                _gameRules.GetSecondEssence(1);
            }
            else if (enemyType == EnemyType.Boss_3)
            {
                _gameRules.ExperienceGain(5f);
                _gameRules.GetThirdEssence(1);
            }

            experienceGained = true;

            //_enemyPath.EnemyCount(-1);
        }

        DisablingAllDebuffs();
        this.gameObject.SetActive(false);
    }
}