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

    private void Start()
    {
        _image = GetComponent<Image>();
        stageManager = GameObject.FindWithTag("Stage Manager");
    }

    private void OnEnable()
    {
        this.GetComponent<Image>().color = Color.white;
    }

    public void GetEnemyHP(float f)
    {
        _image.DOColor(Color.red, 0.1f).SetLoops(2, LoopType.Yoyo);
        enemyHP -= f;

        if (enemyHP <= 0)
            Death();
    }

    void Death()
    {
        stageManager.GetComponent<GameRules>().ExperienceGain(1f);
        this.gameObject.SetActive(false);
    }
}