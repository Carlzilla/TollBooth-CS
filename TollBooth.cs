using System;
using ICities;
using ColossalFramework;
using UnityEngine;

namespace TollBooth
{

    public class TollBooth : IUserMod
    {
        public string Description
        {
            get
            {
                return "Adds the ability for toll booth assets to collect money.";
            }
        }

        public string Name
        {
            get
            {
                return "Working toll booths";
            }
        }

        public TollBooth()
        {
        }
    }

    public class TollBoothAI : PlayerBuildingAI
    {
        private VehicleManager vehicleManager = Singleton<VehicleManager>.instance;

        [CustomizableProperty("EntertainmentAccumulation")]
        public int m_entertainmentAccumulation = -300;

        [CustomizableProperty("EntertainmentRadius")]
        public float m_entertainmentRadius = 400f;

        [CustomizableProperty("UneducatedWorkers", "Workers")]
        public int m_workPlaceCount0 = 3;

        [CustomizableProperty("TollRate")]
        public int m_tollRate = 1;

        [CustomizableProperty("HighwayUsagePercent", "percent")]
        public int m_highwayRate = 20;

        public int m_workPlaceCount1 = 0;
        public int m_workPlaceCount2 = 0;
        public int m_workPlaceCount3 = 0;

    public TollBoothAI()
    {
    }

    public override int GetMaintenanceCost()
    {
        if (this.m_highwayRate > 100)
        {
            this.m_highwayRate = 100;
        }

        int mTolls = Convert.ToInt32(Math.Floor(this.vehicleManager.m_vehicleCount * (this.m_highwayRate /100) * (this.m_tollRate * 6.25) * -1));
        return mTolls;
    }

    public override void GetImmaterialResourceRadius(ushort buildingID, ref Building data, out ImmaterialResourceManager.Resource resource1, out float radius1, out ImmaterialResourceManager.Resource resource2, out float radius2)
    {
        if (this.m_entertainmentAccumulation == 0)
        {
            resource1 = ImmaterialResourceManager.Resource.None;
            radius1 = 0f;
        }
        else
        {
            resource1 = ImmaterialResourceManager.Resource.Entertainment;
            radius1 = this.m_entertainmentRadius;
        }
        resource2 = ImmaterialResourceManager.Resource.None;
        radius2 = 0f;
    }
    
    protected override void HandleWorkAndVisitPlaces(ushort buildingID, ref Building buildingData, ref Citizen.BehaviourData behaviour, ref int aliveWorkerCount, ref int totalWorkerCount, ref int workPlaceCount, ref int aliveVisitorCount, ref int totalVisitorCount, ref int visitPlaceCount)
    {
        workPlaceCount = workPlaceCount + this.m_workPlaceCount0 + this.m_workPlaceCount1 + this.m_workPlaceCount2 + this.m_workPlaceCount3;
        base.GetWorkBehaviour(buildingID, ref buildingData, ref behaviour, ref aliveWorkerCount, ref totalWorkerCount);
        base.HandleWorkPlaces(buildingID, ref buildingData, this.m_workPlaceCount0, this.m_workPlaceCount1, this.m_workPlaceCount2, this.m_workPlaceCount3, ref behaviour, aliveWorkerCount, totalWorkerCount);
    }
  }
}
