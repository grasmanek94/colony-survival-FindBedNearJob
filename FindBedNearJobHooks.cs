using BlockEntities.Implementations;
using Harmony;
using NPC;
using Pipliz;

namespace grasmanek94.FindBedNearJob
{
	[HarmonyPatch(typeof(NPCBase))]
	[HarmonyPatch("CalculateGoalLocation")]
	public class NPCBaseHookNPCGoal
	{
		static bool Prefix(NPCBase __instance, ref Vector3Int __result, NPCBase.NPCGoal goal)
		{
			if(__instance == null || __instance.Colony == null || __instance.Colony.BedTracker == null)
			{
				return true;
			}

			switch (goal)
			{
				case NPCBase.NPCGoal.Bed:
				{
					if (__instance.UsedBed != null && __instance.UsedBed.IsValid)
					{
						return true;
					}

					if(__instance.Job == null)
					{
						return true;
					}

					Vector3Int jobLocation;
					BedTracker.Bed bed;
					if (__instance.Colony.BedTracker.TryGetClosestUnused(__instance.Job.GetJobLocation(), out jobLocation, out bed, 200))
					{
						Log.WriteWarning("Redirected NPC Goal");

						ClassUtility.Call(__instance, "ClearBed", new object[] { });
						ClassUtility.SetProperty(__instance, "UsedBed", bed);
						ClassUtility.SetProperty(__instance, "UsedBedLocation", jobLocation);
						bed.SetUseState(jobLocation, true);
						__result =  jobLocation;
						return false;
					}
					break;
				}
			}

			return true;
		}
	}
}
