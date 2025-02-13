using UnityEngine;
using System.Collections.Generic;
using System;

public class TowerMergeHandler : MonoBehaviour
{
    public enum MergeResult
    {
        FireEarth,                  // Fire + Earth
        FireNature,                 // Fire + Nature
        FireDarkness,               // Fire + Darkness
        FireThunder,                // Fire + Thunder

        EarthNature,                // Earth + Nature
        EarthDarkness,              // Earth + Darkness
        EarthThunder,               // Earth + Thunder

        NatureDarkness,             // Nature + Darkness
        NatureThunder,              // Nature + Thunder

        DarknessThunder,            // Darkness + Thunder


        FireEarthNature,            // Fire + Earth + Nature
        FireEarthDarkness,          // Fire + Earth + Darkness
        FireEarthThunder,           // Fire + Earth + Thunder
        FireNatureDarkness,         // Fire + Nature + Darkness
        FireNatureThunder,          // Fire + Nature + Thunder
        FireDarknessThunder,        // Fire + Darkness + Thunder
        EarthNatureDarkness,        // Earth + Nature + Darkness
        EarthNatureThunder,         // Earth + Nature + Thunder
        EarthDarknessThunder,       // Earth + Darkness + Thunder
        NatureDarknessThunder,      // Nature + Darkness + Thunder

        Unknown // Default case for invalid or unhandled combinations
    }

    public MergeResult GetMergeResult(TowerBase.ElementType element1, TowerBase.ElementType element2)
    {
        if ((element1 == TowerBase.ElementType.Fire && element2 == TowerBase.ElementType.Earth) ||
                 (element1 == TowerBase.ElementType.Earth && element2 == TowerBase.ElementType.Fire))
        {
            return MergeResult.FireEarth;
        }
        else if ((element1 == TowerBase.ElementType.Fire && element2 == TowerBase.ElementType.Nature) ||
                 (element1 == TowerBase.ElementType.Nature && element2 == TowerBase.ElementType.Fire))
        {
            return MergeResult.FireNature;
        }
        else if ((element1 == TowerBase.ElementType.Fire && element2 == TowerBase.ElementType.Darkness) ||
                 (element1 == TowerBase.ElementType.Darkness && element2 == TowerBase.ElementType.Fire))
        {
            return MergeResult.FireDarkness;
        }
        else if ((element1 == TowerBase.ElementType.Fire && element2 == TowerBase.ElementType.Thunder) ||
                 (element1 == TowerBase.ElementType.Thunder && element2 == TowerBase.ElementType.Fire))
        {
            return MergeResult.FireThunder;
        }
        else if ((element1 == TowerBase.ElementType.Earth && element2 == TowerBase.ElementType.Nature) ||
                 (element1 == TowerBase.ElementType.Nature && element2 == TowerBase.ElementType.Earth))
        {
            return MergeResult.EarthNature;
        }
        else if ((element1 == TowerBase.ElementType.Earth && element2 == TowerBase.ElementType.Darkness) ||
                 (element1 == TowerBase.ElementType.Darkness && element2 == TowerBase.ElementType.Earth))
        {
            return MergeResult.EarthDarkness;
        }
        else if ((element1 == TowerBase.ElementType.Earth && element2 == TowerBase.ElementType.Thunder) ||
                 (element1 == TowerBase.ElementType.Thunder && element2 == TowerBase.ElementType.Earth))
        {
            return MergeResult.EarthThunder;
        }
        else if ((element1 == TowerBase.ElementType.Nature && element2 == TowerBase.ElementType.Darkness) ||
                 (element1 == TowerBase.ElementType.Darkness && element2 == TowerBase.ElementType.Nature))
        {
            return MergeResult.NatureDarkness;
        }
        else if ((element1 == TowerBase.ElementType.Nature && element2 == TowerBase.ElementType.Thunder) ||
                 (element1 == TowerBase.ElementType.Thunder && element2 == TowerBase.ElementType.Nature))
        {
            return MergeResult.NatureThunder;
        }
        else if ((element1 == TowerBase.ElementType.Darkness && element2 == TowerBase.ElementType.Thunder) ||
                 (element1 == TowerBase.ElementType.Thunder && element2 == TowerBase.ElementType.Darkness))
        {
            return MergeResult.DarknessThunder;
        }

        // If no match is found, return Unknown
        return MergeResult.Unknown;
    }

    public MergeResult GetMergeResult(TowerBase.ElementType element1, TowerBase.ElementType element2, TowerBase.ElementType element3)
    {
        // Three-element merge logic
        HashSet<TowerBase.ElementType> elements = new HashSet<TowerBase.ElementType> { element1, element2, element3 };

        if (elements.SetEquals(new HashSet<TowerBase.ElementType> { TowerBase.ElementType.Fire, TowerBase.ElementType.Earth, TowerBase.ElementType.Nature }))
            return MergeResult.FireEarthNature;
        if (elements.SetEquals(new HashSet<TowerBase.ElementType> { TowerBase.ElementType.Fire, TowerBase.ElementType.Earth, TowerBase.ElementType.Darkness }))
            return MergeResult.FireEarthDarkness;
        if (elements.SetEquals(new HashSet<TowerBase.ElementType> { TowerBase.ElementType.Fire, TowerBase.ElementType.Earth, TowerBase.ElementType.Thunder }))
            return MergeResult.FireEarthThunder;
        if (elements.SetEquals(new HashSet<TowerBase.ElementType> { TowerBase.ElementType.Fire, TowerBase.ElementType.Nature, TowerBase.ElementType.Darkness }))
            return MergeResult.FireNatureDarkness;
        if (elements.SetEquals(new HashSet<TowerBase.ElementType> { TowerBase.ElementType.Fire, TowerBase.ElementType.Nature, TowerBase.ElementType.Thunder }))
            return MergeResult.FireNatureThunder;
        if (elements.SetEquals(new HashSet<TowerBase.ElementType> { TowerBase.ElementType.Fire, TowerBase.ElementType.Darkness, TowerBase.ElementType.Thunder }))
            return MergeResult.FireDarknessThunder;
        if (elements.SetEquals(new HashSet<TowerBase.ElementType> { TowerBase.ElementType.Earth, TowerBase.ElementType.Nature, TowerBase.ElementType.Darkness }))
            return MergeResult.EarthNatureDarkness;
        if (elements.SetEquals(new HashSet<TowerBase.ElementType> { TowerBase.ElementType.Earth, TowerBase.ElementType.Nature, TowerBase.ElementType.Thunder }))
            return MergeResult.EarthNatureThunder;
        if (elements.SetEquals(new HashSet<TowerBase.ElementType> { TowerBase.ElementType.Earth, TowerBase.ElementType.Darkness, TowerBase.ElementType.Thunder }))
            return MergeResult.EarthDarknessThunder;
        if (elements.SetEquals(new HashSet<TowerBase.ElementType> { TowerBase.ElementType.Nature, TowerBase.ElementType.Darkness, TowerBase.ElementType.Thunder }))
            return MergeResult.NatureDarknessThunder;

        return MergeResult.Unknown;
    }

    public GameObject MergeTowers(TowerBase tower1, TowerBase tower2, Vector2 towerLocation)
    {
        // If tower1 or tower2 is already a merged tower
        if (tower1.towerElement.ToString().Contains("_") || tower2.towerElement.ToString().Contains("_"))
        {
            return MergeTowersThreeElements(tower1, tower2, towerLocation);
        }
        else
        {
            return MergeTowersTwoElements(tower1, tower2, towerLocation);
        }
    }

    public GameObject MergeTowersTwoElements(TowerBase tower1, TowerBase tower2, Vector2 towerLocation)
    {
        // Get the merge result
        var mergeResult = GetMergeResult(tower1.towerElement, tower2.towerElement);

        // Example: Create a new tower based on the merge result
        switch (mergeResult)
        {
            case MergeResult.FireEarth:
                return CreateTower("MagmaCreature", towerLocation);
            case MergeResult.FireNature:
                return CreateTower("Flamewood", towerLocation);
            case MergeResult.FireDarkness:
                return CreateTower("Nightflame", towerLocation);
            case MergeResult.FireThunder:
                return CreateTower("Stormblazer", towerLocation);
            case MergeResult.EarthNature:
                return CreateTower("Stonewood", towerLocation);
            case MergeResult.EarthDarkness:
                return CreateTower("Dreadstone", towerLocation);
            case MergeResult.EarthThunder:
                return CreateTower("Stormcrusher", towerLocation);
            case MergeResult.NatureDarkness:
                return CreateTower("Gravenight", towerLocation);
            case MergeResult.NatureThunder:
                return CreateTower("Wildstorm", towerLocation);
            case MergeResult.DarknessThunder:
                return CreateTower("Nightstorm", towerLocation);
            default:
                Debug.Log("Invalid merge!");
                return null;
        }
    }

    public GameObject MergeTowersThreeElements(TowerBase tower1, TowerBase tower2, Vector2 towerLocation)
    {
        TowerBase.ElementType elementA, elementB, elementC;

        // If tower1 is already a merged tower, extract its elements
        if (tower1.towerElement.ToString().Contains("_"))
        {
            string[] elements = tower1.towerElement.ToString().Split('_');
            elementA = (TowerBase.ElementType)Enum.Parse(typeof(TowerBase.ElementType), elements[0]);
            elementB = (TowerBase.ElementType)Enum.Parse(typeof(TowerBase.ElementType), elements[1]);
            elementC = tower2.towerElement;
        }
        // If tower2 is already a merged tower, extract its elements
        else if (tower2.towerElement.ToString().Contains("_"))
        {
            string[] elements = tower2.towerElement.ToString().Split('_');
            elementA = (TowerBase.ElementType)Enum.Parse(typeof(TowerBase.ElementType), elements[0]);
            elementB = (TowerBase.ElementType)Enum.Parse(typeof(TowerBase.ElementType), elements[1]);
            elementC = tower1.towerElement;
        }
        else
        {
            Debug.Log("Invalid merge attempt! One tower must already be merged.");
            return null;
        }
        
        var mergeResult = GetMergeResult(elementA, elementB, elementC);

        switch (mergeResult)
        {
            case MergeResult.FireEarthNature:
                return CreateTower("MagmaGolem", towerLocation);
            case MergeResult.FireEarthDarkness:
                return CreateTower("InfernalWarrior", towerLocation);
            case MergeResult.FireEarthThunder:
                return CreateTower("Stormcrusher", towerLocation);
            case MergeResult.FireNatureDarkness:
                return CreateTower("Hellhound", towerLocation);
            case MergeResult.FireNatureThunder:
                return CreateTower("Voltree", towerLocation);
            case MergeResult.FireDarknessThunder:
                return CreateTower("Hellstorm", towerLocation);
            case MergeResult.EarthNatureDarkness:
                return CreateTower("Shadow", towerLocation);
            case MergeResult.EarthNatureThunder:
                return CreateTower("Thunderbark", towerLocation);
            case MergeResult.EarthDarknessThunder:
                return CreateTower("Shadowquake", towerLocation);
            case MergeResult.NatureDarknessThunder:
                return CreateTower("Thornshroud", towerLocation);
            default:
                Debug.Log("Invalid merge!");
                return null;
        }
    }

        private GameObject CreateTower(string prefabName, Vector2 towerLocation)
    {
        // Replace with your actual tower creation logic
        GameObject towerPrefab = PoolBase.instance.GetObject(prefabName, towerLocation);
        if (towerPrefab != null)
        {
            return towerPrefab;
        }
        return null;
    }
}
