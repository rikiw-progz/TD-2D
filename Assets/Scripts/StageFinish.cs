using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageFinish : MonoBehaviour
{
    [SerializeField] private int life = 20;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Enemy"))
        {
            life -= 1;
            if (life <= 0)
                Debug.Log("You LOST");
        }
    }
}