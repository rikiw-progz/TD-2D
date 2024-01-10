using UnityEngine;

public class ShadowOrb : MonoBehaviour
{
    public float shadowOrbDurationTime = 3f;
    public float shadowOrbSpeed = 3f;
    public float shadowOrbDamage = 10f;
    private float sphereRadius = 0.1f;

    private Vector3 randomDirection;

    private void OnEnable()
    {
        // Generate a random direction vector when the object is instantiated
        randomDirection = Random.insideUnitCircle.normalized;
    }

    private void Update()
    {
        transform.position += shadowOrbSpeed * Time.deltaTime * randomDirection;

        shadowOrbDurationTime -= Time.deltaTime;

        CheckForTargets();

        if (shadowOrbDurationTime <= 0)
            this.gameObject.SetActive(false);
    }

    private void CheckForTargets()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, sphereRadius);

        // Iterate through the hits and apply damage to enemies
        foreach (var hit in hits)
        {
            if (hit.CompareTag("Enemy"))
            {
                hit.GetComponent<EnemyHealth>().GetEnemyHP(shadowOrbDamage);
            }
        }
    }
}