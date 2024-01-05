using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EnemyMove : MonoBehaviour
{
    public int currentIndex = 0;
    private Vector2 targetPosition;
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
        switch (currentIndex % 10)
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
                this.gameObject.SetActive(false);
                break;

        }
        currentIndex++;
    }

    private void OnDisable()
    {
        this.transform.position = new Vector2(-1000f, -200f);
    }
}