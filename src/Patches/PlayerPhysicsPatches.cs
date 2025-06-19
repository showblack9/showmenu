using HarmonyLib;
using UnityEngine;

namespace ShowMenu;

[HarmonyPatch(typeof(PlayerPhysics), nameof(PlayerPhysics.LateUpdate))]
public static class PlayerPhysics_LateUpdate
{
    public static void Postfix(PlayerPhysics __instance)
    {

        ShowESP.playerNametags(__instance);
        ShowESP.seeGhostsCheat(__instance);

        ShowCheats.noClipCheat();
        ShowCheats.speedBoostCheat();
        ShowCheats.killAllCheat();
        ShowCheats.killAllCrewCheat();
        ShowCheats.killAllImpsCheat();
        ShowCheats.teleportCursorCheat();
        ShowCheats.completeMyTasksCheat();

        ShowPPMCheats.spectatePPM();
        ShowPPMCheats.killPlayerPPM();
        //ShowPPMCheats.telekillPlayerPPM();
        ShowPPMCheats.teleportPlayerPPM();
        ShowPPMCheats.changeRolePPM();

        //if (ShowPPMCheats.teleKillWaitFrames == 0){
        //    KillAnimation.SetMovement(PlayerControl.LocalPlayer, true);
        //    PlayerControl.LocalPlayer.NetTransform.RpcSnapTo(ShowPPMCheats.teleKillPosition);
        //}

        //ShowPPMCheats.teleKillWaitFrames--;

        TracersHandler.drawPlayerTracer(__instance);

        GameObject[] bodyObjects = GameObject.FindGameObjectsWithTag("DeadBody");
        foreach(GameObject bodyObject in bodyObjects) // Finds and loops through all dead bodies
        {
            DeadBody deadBody = bodyObject.GetComponent<DeadBody>();

            if (deadBody){
                if (!deadBody.Reported){ // Only draw tracers for unreported dead bodies
                    TracersHandler.drawBodyTracer(deadBody);
                }
            }
        }
    }
}