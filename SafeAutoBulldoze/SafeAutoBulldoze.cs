using System;
using System.Runtime.InteropServices.WindowsRuntime;
using ColossalFramework;
using ColossalFramework.Plugins;
using ICities;
using UnityEngine;
using Object = UnityEngine.Object;

namespace SafeAutoBulldoze
{
    public class SafeAutoBulldozeMod : IUserMod
    {
        public string Name => "SafeAutoBulldoze";

        public string Description => "Periodically removes abandoned and burned down buildings by automatically bulldozing them. Note that " +
                                     "here will be no sound or animation that plays, but a message will be printed to the debug console (press " +
                                     "F7 to toggle its visibility.";
    }

    public class SafeAutoBulldozeThread : ThreadingExtensionBase
    {
        private float _timeAccumulator = 0;
        private const float AutoBulldozeInterval = 5;
        public override void OnUpdate(float realTimeDelta, float simulationTimeDelta)
        {
            _timeAccumulator += simulationTimeDelta;
            if (_timeAccumulator > AutoBulldozeInterval)
            {
                DemolishAbandonedAndBurnedDown();
                _timeAccumulator = 0;
            }
        }
        private static void DemolishAbandonedAndBurnedDown()
        {
            var buildingManager = Singleton<BuildingManager>.instance;
            var buildings = buildingManager.m_buildings.m_buffer;
            var numBulldozed = 0;

            // Iterate through the array of buildings. Notes that not every element in the array represents a 
            // real building (via experimentation, Building.Flags.Created flags a building that exists in the
            // world.
            for (var i = 0; i < buildingManager.m_buildings.m_buffer.Length; ++i)
            {
                var building = buildings[i];
                if ((building.m_flags & Building.Flags.Created) != Building.Flags.None
                    &&
                    (building.m_flags & (Building.Flags.Abandoned | Building.Flags.BurnedDown)) !=
                    Building.Flags.None)
                {
                    // Check if the building is "bulldozeable".
                    if (building.Info.m_buildingAI.CheckBulldozing((ushort)i, ref building) ==
                        ToolBase.ToolErrors.None)
                    {
                        // This immedately removes a building from the world. No animation/sound is played, it just disappears . . .
                        buildingManager.ReleaseBuilding((ushort)i);
                        numBulldozed++;
                    }
                }
            }

            dbg.Message("SafeAutoBulldoze: Just demolished " + numBulldozed + " building(s).");
        }
    }

    internal class dbg
    {
        public static void Message(String message)
        {
            DebugOutputPanel.AddMessage(PluginManager.MessageType.Message, message);
        }
    }
}

