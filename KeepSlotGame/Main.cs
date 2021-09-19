using System.Reflection;
using HarmonyLib;
using SimCasino.Modding;

namespace KeepSlotGame
{
    public class Main : BaseMod
    {
        public override string InternalName => "root.KeepSlotGame";

        public override void OnLoad(GameEnvironment gameState)
        {
            Harmony harmony = new Harmony(InternalName);
            harmony.PatchAll(Assembly.GetExecutingAssembly());
        }
    }
}
