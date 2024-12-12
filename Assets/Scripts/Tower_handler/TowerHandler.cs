using UnityEngine;
using TMPro;

public class TowerHandler : MonoBehaviour
{
    public int towerAmount;

    public void TowerAmount(int amount)
    {
        towerAmount += amount;

        this.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = towerAmount.ToString();
    }
}