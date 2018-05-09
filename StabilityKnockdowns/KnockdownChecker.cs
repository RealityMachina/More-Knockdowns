using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Harmony;
using BattleTech;
using System.Configuration;

namespace StabilityKnockdowns
{
    class KnockdownChecker
    {
        [HarmonyPatch(typeof(BattleTech.Mech))]
        [HarmonyPatch("AddInstability")]
        public static class BattleTech_Pilot_CanPilot_Prefix
        {
            static void Prefix(Mech __instance, float amt, StabilityChangeSource source, string sourceGuid)
            {
                if (amt > 0f)
                {
                    float currentStability = __instance.CurrentStability + amt;
                    float maxStability = __instance.MaxStability;

                    if(currentStability / maxStability >= 1.25f)
                    {
                        //doing 25% stability damage over the maximum will result in a knockdown
                        __instance.FlagForKnockdown();
                    }
                }
            }
        }
    }
}
