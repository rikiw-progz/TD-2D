using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityTriggerRange : MonoBehaviour
{
    public readonly List<GameObject> abilityTriggerEnemyList = new();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            abilityTriggerEnemyList.Add(collision.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            abilityTriggerEnemyList.Remove(collision.gameObject);
        }
    }
}