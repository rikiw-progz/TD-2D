using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SplashBullet : MonoBehaviour
{
    public float damage = 50f;
    public float _bulletSpeed = 10f;
    public float _splashRadius = 5f;

    public Transform target;
    private float step;
    private GameObject splashEffect;
    private RectTransform splashEffectRectTransform;

    private void Start()
    {
        splashEffect = transform.GetChild(0).gameObject;
        splashEffectRectTransform = splashEffect.GetComponent<RectTransform>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            splashEffect.SetActive(true);
            SetSplashEffectSize();
            SplashAttack();
        }
    }

    private void SetSplashEffectSize()
    {
        splashEffectRectTransform.sizeDelta = new Vector2(_splashRadius * 250f, _splashRadius * 250f);
    }

    private void SplashAttack()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, _splashRadius);
        
        foreach (var collider in colliders)
        {
            if (collider.CompareTag("Enemy"))
            {
                collider.gameObject.GetComponent<EnemyHealth>().GetEnemyHP(damage);
                StartCoroutine(SplashEffectDisable());
            }
        }
    }

    IEnumerator SplashEffectDisable()
    {
        yield return new WaitForSeconds(0.1f);
        splashEffect.SetActive(false);
        this.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (target != null && target.gameObject.activeInHierarchy)
        {
            MoveTowardsTarget();
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    private void MoveTowardsTarget()
    {
        step = _bulletSpeed * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, target.position, step);
    }
}