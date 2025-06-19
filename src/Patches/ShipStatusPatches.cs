using HarmonyLib;

namespace ShowMenu;

[HarmonyPatch(typeof(ShipStatus), nameof(ShipStatus.FixedUpdate))]
public static class ShipStatus_FixedUpdate
{
    public static void Postfix(ShipStatus __instance)
    {
        ShowCheats.sabotageCheat(__instance);
        ShowCheats.closeMeetingCheat();
        ShowCheats.walkInVentCheat();
        ShowCheats.kickVentsCheat();

        ShowPPMCheats.reportBodyPPM();
    }
}