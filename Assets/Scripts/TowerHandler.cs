using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerHandler : MonoBehaviour
{
    //[SerializeField] private GameObject towerStats;
    [SerializeField] private GameObject towerBuilder;

    public void OnClick()
    {
        // Tower stats
        //towerStats.SetActive(true);

        towerBuilder.GetComponent<Button>().enabled = true;
        towerBuilder.GetComponent<TowerBuilder>().towerPositionToBuild = this.transform.position;
    }
}