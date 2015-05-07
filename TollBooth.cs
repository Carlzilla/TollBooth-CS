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
        public int m_tollRate = 16;

    public TollBoothAI()
    {
    }

    public override int GetMaintenanceCost()
    {
        int mFees = Convert.ToInt32(Math.Floor(this.vehicleManager.m_vehicleCount * 0.2 * m_tollRate * -1));
        return mFees;
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
  }
}
