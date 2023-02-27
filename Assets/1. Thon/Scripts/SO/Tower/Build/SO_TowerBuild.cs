using Catze.Enum;
using System.Collections.Generic;
using UnityEngine;

namespace Catze
{
    [CreateAssetMenu(fileName = "SO_TowerBuild", menuName = "SO/RTD/Tower/Build", order = 0)]
    public class SO_TowerBuild : ScriptableObject
    {
        [SerializeField] private List<SO_TowerBuildInfuence> _soTowerBuildInfluences;

        public List<SO_TowerBuildInfuence> SOTowerBuildInfluences => _soTowerBuildInfluences;

        public SO_BuildTierInfo GetBuildTierInfo(TowerTier tier)
        {
            SO_TowerBuildInfuence buildInfuence = _soTowerBuildInfluences[Random.Range(0, _soTowerBuildInfluences.Count)];
            foreach (var buildTier in buildInfuence.SOTowerBuildInfuenceTiers)
            {
                if (buildTier.SOTierInfo.Tier == tier)
                {
                    return buildTier.SOTierInfo;
                }
            }
            
            return null;
        }

        public SO_TowerBuildInfuenceTier GetAcendTier(TowerTier tier)
        {
            if (tier == TowerTier.Eternity) return null;
            
            var soBuildInfluence = _soTowerBuildInfluences[Random.Range(0, _soTowerBuildInfluences.Count)];
            
            foreach(var buildTier in soBuildInfluence.SOTowerBuildInfuenceTiers)
            {
                if (buildTier.SOTierInfo.Tier == tier + 1)
                {
                    return buildTier;
                }
            }

            return null;
        }
    }
}