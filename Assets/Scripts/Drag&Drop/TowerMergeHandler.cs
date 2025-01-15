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

    public GameObject MergeTowers(TowerBase tower1, TowerBase tower2, Vector2 towerLocation)
    {
        // Get the merge result
        var mergeResult = GetMergeResult(tower1.towerElement, tower2.towerElement);

        // Example: Create a new tower based on the merge result
        switch (mergeResult)
        {
            case MergeResult.FireEarth:
                return CreateTower("MagmaGolemTower", towerLocation);
            case MergeResult.WaterEarth:
                return CreateTower("MudLeviathanTower", towerLocation);
            case MergeResult.FireNature:
                return CreateTower("FireTreantTower", towerLocation);
            case MergeResult.FireDarkness:
                return CreateTower("DarkFireRevenantTower", towerLocation);
            case MergeResult.FireThunder:
                return CreateTower("FireThunderCloudbringerTower", towerLocation);
            case MergeResult.EarthNature:
                return CreateTower("ThornbackBehemoth", towerLocation);
            case MergeResult.EarthDarkness:
                return CreateTower("GraveColossus", towerLocation);
            case MergeResult.EarthThunder:
                return CreateTower("Stormcrusher", towerLocation);
            case MergeResult.NatureDarkness:
                return CreateTower("RotfiendBloom", towerLocation);
            case MergeResult.NatureThunder:
                return CreateTower("Stormwood", towerLocation);
            case MergeResult.DarknessThunder:
                return CreateTower("Blackstorm", towerLocation);
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
