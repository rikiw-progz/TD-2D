using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EnemyMove : MonoBehaviour
{
    public int currentIndex = 0;
    private Vector2 targetPosition;
    private float _speed;
    public float speed = 5f;
    public List<Transform> enemyPath = new();
    public bool pathAdded = false;

    void Start()
    {
        MoveToNextPosition();
    }

    private void OnEnable()
    {
        if(enemyPath.Count > 0)
        {
            currentIndex = 0;

            MoveToNextPosition();
        }
    }

    void Update()
    {
        float step = speed * Time.deltaTime;

        transform.position = Vector2.MoveTowards(transform.position, targetPosition, step);
        
        if (Vector2.Distance(transform.position, targetPosition) < 0.01f)
        {
            MoveToNextPosition();
        }
    }

    void MoveToNextPosition()
    {
        switch (currentIndex % 14)
        {
            case 0:
                targetPosition = enemyPath[0].position;
                break;
            case 1:
                targetPosition = enemyPath[1].position;
                break;
            case 2:
                targetPosition = enemyPath[2].position;
                break;
            case 3:
                targetPosition = enemyPath[3].position;
                break;
            case 4:
                targetPosition = enemyPath[4].position;
                break;
            case 5:
                targetPosition = enemyPath[5].position;
                break;
            case 6:
                targetPosition = enemyPath[6].position;
                break;
            case 7:
                targetPosition = enemyPath[7].position;
                break;
            case 8:
                targetPosition = enemyPath[8].position;
                break;
            case 9:
                targetPosition = enemyPath[9].position;
                break;
            case 10:
                targetPosition = enemyPath[10].position;
                break;
            case 11:
                targetPosition = enemyPath[11].position;
                break;
            case 12:
                targetPosition = enemyPath[12].position;
                break;
            case 13:
                this.gameObject.SetActive(false);
                break;

        }
        currentIndex++;
    }

    public void Stun(float stunDuration)
    {
        if(this.gameObject.activeInHierarchy)
            StartCoroutine(StunCountdown(stunDuration));
    }

    public IEnumerator StunCountdown(float stunDuration)
    {
        _speed = speed;
        speed = 0f;
        yield return new WaitForSeconds(stunDuration);
        speed = _speed;
    }
    
    private void OnDisable()
    {
        this.transform.position = new Vector2(-1000f, -200f);
    }
}