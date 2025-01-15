using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    public int currentIndex = 0;
    private Vector2 targetPosition;
    private float _speed;
    public float speed = 1f;
    private const float speedCellConst = 0.695f;
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
        float step = speedCellConst * speed * Time.deltaTime;

        transform.position = Vector2.MoveTowards(transform.position, targetPosition, step);
        
        if (Vector2.Distance(transform.position, targetPosition) < 0.01f)
        {
            MoveToNextPosition();
        }
    }

    void MoveToNextPosition()
    {
        if (currentIndex < enemyPath.Count)
        {
            // Set the target position to the current path point
            targetPosition = enemyPath[currentIndex].position;
        }
        else
        {
            // When the index exceeds the path length, deactivate the object
            this.gameObject.SetActive(false);
        }

        // Increment currentIndex to move to the next path point
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
        //this.transform.position = new Vector2(-1000f, -200f);
    }
}