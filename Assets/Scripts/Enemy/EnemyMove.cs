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

    [Header("Slow")]
    private int appliedSlowAmount = 0;
    private List<string> _slowDebuffNames = new();
 
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

    public void ApplyMovementSlow(float slowPercent,float slowDuration, string slowDebuffName)
    {
        if (this.gameObject.activeInHierarchy)
            StartCoroutine(SlowCountdown(slowPercent, slowDuration, slowDebuffName));
    }

    public IEnumerator SlowCountdown(float slowPercent,float slowDuration, string slowDebuffName)
    {
        appliedSlowAmount++;

        if (appliedSlowAmount == 1)
            _speed = speed;

        GameObject debuffSlowGO = PoolBase.instance.GetObject(slowDebuffName, this.transform.position);        // maybe check if it was debuffed before then just activate gameobject not pool
        debuffSlowGO.transform.SetParent(this.transform);
        debuffSlowGO.transform.localPosition = Vector2.zero;

        if (!_slowDebuffNames.Contains(slowDebuffName))
        {
            // Apply the slow effect
            speed *= (1 - slowPercent / 100);
            Debug.Log($"Speed after applying {slowDebuffName}: {speed}");

            // Add the debuff to the list of active debuffs
            _slowDebuffNames.Add(slowDebuffName);
        }

        yield return new WaitForSeconds(slowDuration);
        appliedSlowAmount--;
        if(appliedSlowAmount <= 0)
        {
            speed = _speed;
            debuffSlowGO.SetActive(false);
        }
    }

    private void OnDisable()
    {
        
    }
}