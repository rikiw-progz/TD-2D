using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EnemyMove : MonoBehaviour
{
    private int currentIndex = 0;
    private Vector2 targetPosition;
    public float speed = 5f;

    void Start()
    {
        MoveToNextPosition();
    }

    void Update()
    {
        float step = speed * Time.deltaTime;

        transform.position = Vector3.MoveTowards(transform.position, targetPosition, step);

        if (Vector2.Distance(transform.localPosition, targetPosition) < 1f)
        {
            Debug.Log(0);
            MoveToNextPosition();
        }
    }

    void MoveToNextPosition()
    {
        switch (currentIndex % 9)
        {
            case 0:
                targetPosition = new Vector2(150f, transform.position.y);
                break;
            case 1:
                targetPosition = new Vector2(transform.position.x, 100f);
                break;
            case 2:
                targetPosition = new Vector2(-50f, transform.position.y);
                break;
            case 3:
                targetPosition = new Vector2(transform.position.x, 0f);
                break;
            case 4:
                targetPosition = new Vector2(50f, transform.position.y);
                break;
            case 5:
                targetPosition = new Vector2(transform.position.x, -100f);
                break;
            case 6:
                targetPosition = new Vector2(-150f, transform.position.y);
                break;
            case 7:
                targetPosition = new Vector2(transform.position.x, 200f);
                break;
            case 8:
                targetPosition = new Vector2(1000f, transform.position.y);
                break;

        }
        currentIndex++;
    }

    private void OnDisable()
    {
        this.transform.position = new Vector2(-1000f, -200f);
    }
}