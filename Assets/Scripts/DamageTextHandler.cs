using UnityEngine;
using TMPro;
using System.Collections;
using DG.Tweening;

public class DamageTextHandler : MonoBehaviour
{
    private float damageTextFadeTime = 0.15f;

    public void DamageTextEnable(int damageAmount, Transform pos)
    {
        GameObject textDamageGO = PoolBase.instance.GetObject("Damage text", pos.position);
        textDamageGO.GetComponent<TextMeshProUGUI>().text = (damageAmount).ToString();
        textDamageGO.transform.DOScale(new Vector2(1.5f, 1.5f), damageTextFadeTime);
        textDamageGO.transform.DOLocalMoveY(pos.localPosition.y + 15f, damageTextFadeTime);
        StartCoroutine(TextDamageDeactivation(textDamageGO));
    }

    IEnumerator TextDamageDeactivation(GameObject textGO)
    {
        yield return new WaitForSeconds(damageTextFadeTime);
        textGO.SetActive(false);
    }
}