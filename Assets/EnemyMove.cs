using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EnemyMove : MonoBehaviour
{
    [SerializeField] private float[] pathTime;
    private Coroutine p;

    private void OnEnable()
    {
        p = StartCoroutine(EnemyMoving());
    }

    private IEnumerator EnemyMoving()
    {
        this.transform.DOLocalMoveX(150f, pathTime[0]).SetEase(Ease.Linear);
        yield return new WaitForSeconds(pathTime[0]);
        this.transform.DOLocalMoveY(100f, pathTime[1]).SetEase(Ease.Linear);
        yield return new WaitForSeconds(pathTime[1]);
        this.transform.DOLocalMoveX(-50f, pathTime[2]).SetEase(Ease.Linear);
        yield return new WaitForSeconds(pathTime[2]);
        this.transform.DOLocalMoveY(0f, pathTime[3]).SetEase(Ease.Linear);
        yield return new WaitForSeconds(pathTime[3]);
        this.transform.DOLocalMoveX(50f, pathTime[4]).SetEase(Ease.Linear);
        yield return new WaitForSeconds(pathTime[4]);
        this.transform.DOLocalMoveY(-100f, pathTime[5]).SetEase(Ease.Linear);
        yield return new WaitForSeconds(pathTime[5]);
        this.transform.DOLocalMoveX(-150f, pathTime[6]).SetEase(Ease.Linear);
        yield return new WaitForSeconds(pathTime[6]);
        this.transform.DOLocalMoveY(200f, pathTime[7]).SetEase(Ease.Linear);
        yield return new WaitForSeconds(pathTime[7]);
        this.transform.DOLocalMoveX(1000f, pathTime[8]).SetEase(Ease.Linear);
        yield return new WaitForSeconds(pathTime[8]);

        this.gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        StopCoroutine(p);
        this.transform.position = new Vector2(-1000f, -200f);
    }
}