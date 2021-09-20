using System.Collections.Generic;
using HarmonyLib;

namespace KeepSlotGame
{
    [HarmonyPatch]
    public class GameHooks
    {
        [HarmonyPostfix]
        [HarmonyPatch(typeof(MultiSelectOptions), MethodType.Constructor)]
        public static void AddButtonToMultiSelectOptions(List<IMultiSelectOption> ___Options)
        {
            int index = ___Options.FindIndex(option => option.Name == "PasteSlotMachineSettingsOption");

            if (index == -1)
            {
                ___Options.Add(new PasteSlotMachineSettingsKeepGameOption());
            }
            else
            {
                ___Options.Insert(index + 1, new PasteSlotMachineSettingsKeepGameOption());
            }
        }

        [HarmonyPostfix]
        [HarmonyPatch(typeof(SlotMachine), "ISelectable_SelectedEnter")]
        public static void AddButtonToOptions(SlotMachine __instance, UITab tab)
        {
            SlotMachine.SlotMachineData clipboard = SlotMachine.ClipboardMachine;
            if (clipboard == null)
            {
                return;
            }

            tab.AddButton($"{I18n.GetFormat("UI.tools.MultiSelect.PasteGeneric", "", I18n.Get("UI.tools.MultiSelect.Games"))} (Keep Game)", () =>
            {
                __instance.PlayCost = clipboard.PlayCost;
                __instance.HouseEdgeTarget = clipboard.TargetAdvantage;
                __instance.JackpotPrize = clipboard.JackpotPrize;
                __instance.RecalculateValues();
            }, refresh: false);
        }
    }
}
