using System.Collections.Generic;

namespace KeepSlotGame
{
    public class PasteSlotMachineSettingsKeepGameOption : BaseMultiSelectiOption
    {
        public override string Name => "PasteSlotMachineSettingsKeepGameOption";

        public override void AddOption(UISelectedObjDialog dialog, HashSet<object> objects)
        {
            dialog.uitab.AddButton($"{I18n.GetFormat("UI.tools.MultiSelect.PasteGeneric", "", I18n.Get("UI.tools.MultiSelect.Games"))} (Keep Game)", () => {
                SlotMachine.SlotMachineData clipboard = SlotMachine.ClipboardMachine;
                if (clipboard == null)
                {
                    return;
                }

                foreach (object o in objects)
                {
                    if (IsPlaceable(o, out PlaceableObject po) && po.TryGetComponent(out SlotMachine sm))
                    {
                        sm.PlayCost = clipboard.PlayCost;
                        sm.HouseEdgeTarget = clipboard.TargetAdvantage;
                        sm.JackpotPrize = clipboard.JackpotPrize;
                        sm.RecalculateValues();
                    }
                }
            }, refresh: false);
        }

        public override bool CanAdd(object o)
        {
            if (IsPlaceable(o, out PlaceableObject po))
            {
                return po.GetComponent<SlotMachine>() != null && SlotMachine.ClipboardMachine != null;
            }

            return false;
        }
    }
}
