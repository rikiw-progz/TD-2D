using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerHandler : MonoBehaviour
{
    //[SerializeField] private GameObject towerStats;
    [SerializeField] private GameObject[] towerBuilder;

    public void OnClick()
    {
        // Tower stats
        //towerStats.SetActive(true);
        this.GetComponent<Button>().enabled = false;

        foreach(GameObject g in towerBuilder)
        {
            g.GetComponent<Button>().enabled = true;
            g.GetComponent<TowerBuilder>().towerPositionToBuild = this.transform.position;
        }
    }
}