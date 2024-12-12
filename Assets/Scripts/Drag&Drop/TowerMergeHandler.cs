using UnityEngine;

public class TowerMergeHandler : MonoBehaviour
{
    public enum MergeResult
    {
        FireWater,   // Fire + Water
        FireEarth,   // Fire + Earth
        FireNature,  // Fire + Nature
        FireDarkness, // Fire + Darkness
        FireThunder,  // Fire + Thunder

        WaterEarth,  // Water + Earth
        WaterNature, // Water + Nature
        WaterDarkness, // Water + Darkness
        WaterThunder, // Water + Thunder

        EarthNature, // Earth + Nature
        EarthDarkness, // Earth + Darkness
        EarthThunder, // Earth + Thunder

        NatureDarkness, // Nature + Darkness
        NatureThunder, // Nature + Thunder

        DarknessThunder, // Darkness + Thunder

        Unknown // Default case for invalid or unhandled combinations
    }

    public MergeResult GetMergeResult(TowerBase.ElementType element1, TowerBase.ElementType element2)
    {
        // Ensure consistent ordering for comparisons
        if ((element1 == TowerBase.ElementType.Fire && element2 == TowerBase.ElementType.Water) ||
            (element1 == TowerBase.ElementType.Water && element2 == TowerBase.ElementType.Fire))
        {
            return MergeResult.FireWater;
        }
        else if ((element1 == TowerBase.ElementType.Fire && element2 == TowerBase.ElementType.Earth) ||
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
        else if ((element1 == TowerBase.ElementType.Water && element2 == TowerBase.ElementType.Earth) ||
                 (element1 == TowerBase.ElementType.Earth && element2 == TowerBase.ElementType.Water))
        {
            return MergeResult.WaterEarth;
        }
        else if ((element1 == TowerBase.ElementType.Water && element2 == TowerBase.ElementType.Nature) ||
                 (element1 == TowerBase.ElementType.Nature && element2 == TowerBase.ElementType.Water))
        {
            return MergeResult.WaterNature;
        }
        else if ((element1 == TowerBase.ElementType.Water && element2 == TowerBase.ElementType.Darkness) ||
                 (element1 == TowerBase.ElementType.Darkness && element2 == TowerBase.ElementType.Water))
        {
            return MergeResult.WaterDarkness;
        }
        else if ((element1 == TowerBase.ElementType.Water && element2 == TowerBase.ElementType.Thunder) ||
                 (element1 == TowerBase.ElementType.Thunder && element2 == TowerBase.ElementType.Water))
        {
            return MergeResult.WaterThunder;
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

    public GameObject MergeTowers(TowerBase tower1, TowerBase tower2, Vector2 towerLocation)
    {
        // Get the merge result
        var mergeResult = GetMergeResult(tower1.towerElement, tower2.towerElement);

        // Example: Create a new tower based on the merge result
        switch (mergeResult)
        {
            case MergeResult.FireWater:
                Debug.Log("Merged into a Steam Tower!");
                return CreateTower("SteamTower", towerLocation);
            case MergeResult.FireEarth:
                Debug.Log("Merged into a Lava Tower!");
                return CreateTower("LavaTower", towerLocation);
            case MergeResult.WaterEarth:
                Debug.Log("Merged into a Mud Tower!");
                return CreateTower("MudTower", towerLocation);
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
