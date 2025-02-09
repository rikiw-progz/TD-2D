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
    private GameObject debuffSlowGO;

    [Header("Stun")]
    private bool isStunned;
    private GameObject debuffStunGO;
    private Dictionary<string, GameObject> _activeStunDebuffGO = new();
    private Dictionary<string, Coroutine> _activeStunDebuffCoroutines = new();

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
        if(!isStunned)
        {
            float step = speedCellConst * speed * Time.deltaTime;

            transform.position = Vector2.MoveTowards(transform.position, targetPosition, step);

            if (Vector2.Distance(transform.position, targetPosition) < 0.01f)
            {
                MoveToNextPosition();
            }
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
            currentIndex = -1;
            // When the index exceeds the path length, deactivate the object
            //this.gameObject.SetActive(false);
        }

        // Increment currentIndex to move to the next path point
        currentIndex++;
    }

    public void ApplyMovementSlow(float slowPercent,float slowDuration, string slowDebuffName)
    {
        if (this.gameObject.activeInHierarchy)
        {
            if (_activeSlowDebuffCoroutines.ContainsKey(slowDebuffName))
            {
                StopCoroutine(_activeSlowDebuffCoroutines[slowDebuffName]);
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
            debuffSlowGO.transform.localPosition = new Vector2(0f, 10f);
            _activeSlowDebuffGO.Add(slowDebuffName, debuffSlowGO);

            // Apply the slow effect
            speed *= (1 - slowPercent / 100);

            // Add the debuff to the list of active debuffs
            _activeSlowDebuffs.Add(slowDebuffName, slowPercent);
        }
        
        yield return new WaitForSeconds(slowDuration);

        speed *= 1/((1 - _activeSlowDebuffs[slowDebuffName] / 100));

        _activeSlowDebuffs.Remove(slowDebuffName);
        _activeSlowDebuffGO[slowDebuffName].SetActive(false);
        _activeSlowDebuffGO.Remove(slowDebuffName);
        _activeSlowDebuffCoroutines.Remove(slowDebuffName);
        
        // Change parent instead of destroying?
    }

    public void ApplyStun(string stunDebuffName, float stunDuration)
    {
        if (this.gameObject.activeInHierarchy)
        {
            if (_activeStunDebuffCoroutines.ContainsKey(stunDebuffName))
            {
                StopCoroutine(_activeStunDebuffCoroutines[stunDebuffName]);
            }

            _activeStunDebuffCoroutines[stunDebuffName] = StartCoroutine(StunCountdown(stunDebuffName, stunDuration));
        }
    }

    public IEnumerator StunCountdown(string stunDebuffName, float stunDuration)
    {
        if (!_activeStunDebuffGO.ContainsKey(stunDebuffName))
        {
            debuffStunGO = PoolBase.instance.GetObject(stunDebuffName, this.transform.position);
            debuffStunGO.transform.SetParent(this.transform);
            debuffStunGO.transform.localPosition = new Vector2(0f, 10f);
            _activeStunDebuffGO.Add(stunDebuffName, debuffStunGO);

            isStunned = true;
        }

        yield return new WaitForSeconds(stunDuration);

        isStunned = false;

        _activeStunDebuffGO[stunDebuffName].SetActive(false);
        _activeStunDebuffGO.Remove(stunDebuffName);
        _activeStunDebuffCoroutines.Remove(stunDebuffName);

        // Change parent instead of destroying?
    }

    private void DisableAllDebuffs()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }
    }

    private void OnDisable()
    {
        DisableAllDebuffs();
    }
}