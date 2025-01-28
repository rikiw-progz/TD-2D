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
    private Dictionary<string, float> _activeSlowDebuffs = new();
    private Dictionary<string, GameObject> _activeSlowDebuffGO = new();
    private Dictionary<string, Coroutine> _activeSlowDebuffCoroutines = new();
    private Coroutine _coroutine;
    private GameObject debuffSlowGO;
 
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
        {
            if (_activeSlowDebuffCoroutines.ContainsKey(slowDebuffName))
            {
                StopCoroutine(_activeSlowDebuffCoroutines[slowDebuffName]);
                Debug.Log("Coroutine cancelled");
            }

            _activeSlowDebuffCoroutines[slowDebuffName] = StartCoroutine(SlowCountdown(slowPercent, slowDuration, slowDebuffName));

        }
    }

    public IEnumerator SlowCountdown(float slowPercent,float slowDuration, string slowDebuffName)
    {
        if(!_activeSlowDebuffs.ContainsKey(slowDebuffName))
        {
            debuffSlowGO = PoolBase.instance.GetObject(slowDebuffName, this.transform.position);
            debuffSlowGO.transform.SetParent(this.transform);
            debuffSlowGO.transform.localPosition = Vector2.zero;
            _activeSlowDebuffGO.Add(slowDebuffName, debuffSlowGO);
        }
        
        if (!_activeSlowDebuffs.ContainsKey(slowDebuffName))
        {
            // Apply the slow effect
            speed *= (1 - slowPercent / 100);
            Debug.Log($"Speed after applying {slowDebuffName}: {speed}");

            // Add the debuff to the list of active debuffs
            _activeSlowDebuffs.Add(slowDebuffName, slowPercent);
        }
        
        yield return new WaitForSeconds(slowDuration);

        speed *= 1/((1 - _activeSlowDebuffs[slowDebuffName] / 100));
        Debug.Log($"Speed after slow expired {slowDebuffName}: {speed}");
        _activeSlowDebuffs.Remove(slowDebuffName);
        _activeSlowDebuffGO[slowDebuffName].SetActive(false);
        _activeSlowDebuffGO.Remove(slowDebuffName);
    }

    private void DisablingAllDebuffs()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }
    }

    private void OnDisable()
    {
        DisablingAllDebuffs();
    }
}