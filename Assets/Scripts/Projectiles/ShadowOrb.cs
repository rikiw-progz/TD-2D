using UnityEngine;

public class ShadowOrb : MonoBehaviour
{
    public float shadowOrbDurationTime = 3f;
    public float shadowOrbSpeed = 3f;
    public float shadowOrbDamage = 10f;
    private float sphereRadius = 0.08f;

    private Vector3 randomDirection;

    private void OnEnable()
    {
        // Generate a random direction vector when the object is instantiated
        randomDirection = Random.insideUnitCircle.normalized;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<EnemyHealth>().GetEnemyHP(shadowOrbDamage);
        }
    }

    private void Update()
    {
        transform.position += shadowOrbSpeed * Time.deltaTime * randomDirection;

        shadowOrbDurationTime -= Time.deltaTime;

        //CheckForTargets();

        if (shadowOrbDurationTime <= 0)
            this.gameObject.SetActive(false);
    }

    private void CheckForTargets()
    {
        RaycastHit[] hits = Physics.SphereCastAll(transform.position, sphereRadius, transform.forward);
        
        // Iterate through the hits and apply damage to enemies
        foreach (RaycastHit hit in hits)
        {
            Debug.Log(1);
            if (hit.collider.CompareTag("Enemy"))
            {
                Debug.Log(2);
                hit.collider.GetComponent<EnemyHealth>().GetEnemyHP(shadowOrbDamage);
            }
        }
    }

    private void OnDrawGizmos()
    {
        // Draw a wire sphere in the Scene view to visualize the sphere radius
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position + transform.forward * sphereRadius, sphereRadius);
    }
}