using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public float enemyHP;
    private Image _image;
    private GameObject stageManager;
    public bool damageOverTimeDuration = false;

    private void Start()
    {
        _image = GetComponent<Image>();
        stageManager = GameObject.FindWithTag("Stage Manager");
    }

    private void OnEnable()
    {
        this.GetComponent<Image>().color = Color.white;
    }

    public void GetEnemyHP(float damage)
    {
        _image.DOColor(Color.red, 0.02f).SetLoops(2, LoopType.Yoyo);
        enemyHP -= damage;

        if (enemyHP <= 0)
            Death();
    }

    public void Debuff(float damageAmount, float duration, string debuffName)
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
            ApplyDamage(damage);

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

    private void ApplyDamage(float damage)
    {
        enemyHP -= damage;
        if (enemyHP <= 0)
            Death();
    }

    private void DisablingAllDebuffs()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }
    }

    void Death()
    {
        stageManager.GetComponent<GameRules>().ExperienceGain(1f);
        DisablingAllDebuffs();
        this.gameObject.SetActive(false);
    }
}