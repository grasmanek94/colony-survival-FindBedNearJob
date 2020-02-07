using Harmony;
using static ModLoader;

namespace grasmanek94.FindBedNearJob
{
    [ModManager]
    public static class FindBedNearJob
    {
        [ModCallback(EModCallbackType.OnAssemblyLoaded, "grasmanek94.FindBedNearJob.OnAssemblyLoaded")]
        static void OnAssemblyLoaded(string assemblyPath)
        {
            var harmony = HarmonyInstance.Create("grasmanek94.FindBedNearJob");
            harmony.PatchAll();
        }
    }
}
