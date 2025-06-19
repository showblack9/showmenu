using UnityEngine;
using System.Collections.Generic;

namespace ShowMenu;
public class MenuUI : MonoBehaviour
{

    public List<GroupInfo> groups = new List<GroupInfo>();
    private bool isDragging = false;
    private Rect windowRect = new Rect(10, 10, 1600, 800);
    private bool isGUIActive = false;
    private GUIStyle submenuButtonStyle;

    // Create all groups (buttons) and their toggles on start
    private void Start()
    {
        groups.Add(new GroupInfo("Player", false, new List<ToggleInfo>() {
            new ToggleInfo(" NoClip", () => CheatToggles.noClip, x => CheatToggles.noClip = x),
            new ToggleInfo(" SpeedHack", () => CheatToggles.speedBoost, x => CheatToggles.speedBoost = x),
            }, new List<SubmenuInfo> {
            new SubmenuInfo("Teleport", false, new List<ToggleInfo>() {
                new ToggleInfo(" to Cursor", () => CheatToggles.teleportCursor, x => CheatToggles.teleportCursor = x),
                new ToggleInfo(" to Player", () => CheatToggles.teleportPlayer, x => CheatToggles.teleportPlayer = x),
            }),
        }));

        groups.Add(new GroupInfo("ESP", false, new List<ToggleInfo>() {
            new ToggleInfo(" See Roles", () => CheatToggles.seeRoles, x => CheatToggles.seeRoles = x),
            new ToggleInfo(" See Ghosts", () => CheatToggles.seeGhosts, x => CheatToggles.seeGhosts = x),
            new ToggleInfo(" No Shadows", () => CheatToggles.fullBright, x => CheatToggles.fullBright = x),
            new ToggleInfo(" Reveal Votes", () => CheatToggles.revealVotes, x => CheatToggles.revealVotes = x),
        }, new List<SubmenuInfo> {
            new SubmenuInfo("Camera", false, new List<ToggleInfo>() {
                new ToggleInfo(" Zoom Out", () => CheatToggles.zoomOut, x => CheatToggles.zoomOut = x),
                new ToggleInfo(" Spectate", () => CheatToggles.spectate, x => CheatToggles.spectate = x),
                new ToggleInfo(" Freecam", () => CheatToggles.freecam, x => CheatToggles.freecam = x)
            }),
            new SubmenuInfo("Tracers", false, new List<ToggleInfo>() {
                new ToggleInfo(" Crewmates", () => CheatToggles.tracersCrew, x => CheatToggles.tracersCrew = x),
                new ToggleInfo(" Impostors", () => CheatToggles.tracersImps, x => CheatToggles.tracersImps = x),
                new ToggleInfo(" Ghosts", () => CheatToggles.tracersGhosts, x => CheatToggles.tracersGhosts = x),
                new ToggleInfo(" Dead Bodies", () => CheatToggles.tracersBodies, x => CheatToggles.tracersBodies = x),
                new ToggleInfo(" Color-based", () => CheatToggles.colorBasedTracers, x => CheatToggles.colorBasedTracers = x),
            }),
            new SubmenuInfo("Minimap", false, new List<ToggleInfo>() {
                new ToggleInfo(" Crewmates", () => CheatToggles.mapCrew, x => CheatToggles.mapCrew = x),
                new ToggleInfo(" Impostors", () => CheatToggles.mapImps, x => CheatToggles.mapImps = x),
                new ToggleInfo(" Ghosts", () => CheatToggles.mapGhosts, x => CheatToggles.mapGhosts = x),
                new ToggleInfo(" Color-based", () => CheatToggles.colorBasedMap, x => CheatToggles.colorBasedMap = x)
            }),
        }));

        groups.Add(new GroupInfo("Roles", false, new List<ToggleInfo>() {
            new ToggleInfo(" Set Fake Role", () => CheatToggles.changeRole, x => CheatToggles.changeRole = x),
        }, 
            new List<SubmenuInfo> {
                new SubmenuInfo("Impostor", false, new List<ToggleInfo>() {
                    new ToggleInfo(" Kill Reach", () => CheatToggles.killReach, x => CheatToggles.killReach = x),
                }),
                new SubmenuInfo("Shapeshifter", false, new List<ToggleInfo>() {
                    new ToggleInfo(" No Ss Animation", () => CheatToggles.noShapeshiftAnim, x => CheatToggles.noShapeshiftAnim = x),
                    new ToggleInfo(" Endless Ss Duration", () => CheatToggles.endlessSsDuration, x => CheatToggles.endlessSsDuration = x),
                }),
                new SubmenuInfo("Crewmate", false, new List<ToggleInfo>() {
                    new ToggleInfo(" Complete My Tasks", () => CheatToggles.completeMyTasks, x => CheatToggles.completeMyTasks = x)
                }),
                new SubmenuInfo("Tracker", false, new List<ToggleInfo>() {
                    new ToggleInfo(" Endless Tracking", () => CheatToggles.endlessTracking, x => CheatToggles.endlessTracking = x),
                    new ToggleInfo(" No Track Delay", () => CheatToggles.noTrackingDelay, x => CheatToggles.noTrackingDelay = x),
                    new ToggleInfo(" No Track Cooldown", () => CheatToggles.noTrackingCooldown, x => CheatToggles.noTrackingCooldown = x),
                }),
                new SubmenuInfo("Engineer", false, new List<ToggleInfo>() {
                    new ToggleInfo(" Endless Vent Time", () => CheatToggles.endlessVentTime, x => CheatToggles.endlessVentTime = x),
                    new ToggleInfo(" No Vent Cooldown", () => CheatToggles.noVentCooldown, x => CheatToggles.noVentCooldown = x),
                }),
                new SubmenuInfo("Scientist", false, new List<ToggleInfo>() {
                    new ToggleInfo(" Endless Battery", () => CheatToggles.endlessBattery, x => CheatToggles.endlessBattery = x),
                    new ToggleInfo(" No Vitals Cooldown", () => CheatToggles.noVitalsCooldown, x => CheatToggles.noVitalsCooldown = x),
                }),
            }));

        groups.Add(new GroupInfo("Ship", false, new List<ToggleInfo> {
            new ToggleInfo(" Unfixable Lights", () => CheatToggles.unfixableLights, x => CheatToggles.unfixableLights = x),
            new ToggleInfo(" Report Body", () => CheatToggles.reportBody, x => CheatToggles.reportBody = x),
            new ToggleInfo(" Close Meeting", () => CheatToggles.closeMeeting, x => CheatToggles.closeMeeting = x),
        }, new List<SubmenuInfo> {
            new SubmenuInfo("Sabotage", false, new List<ToggleInfo>() {
                new ToggleInfo(" Reactor", () => CheatToggles.reactorSab, x => CheatToggles.reactorSab = x),
                new ToggleInfo(" Oxygen", () => CheatToggles.oxygenSab, x => CheatToggles.oxygenSab = x),
                new ToggleInfo(" Lights", () => CheatToggles.elecSab, x => CheatToggles.elecSab = x),
                new ToggleInfo(" Comms", () => CheatToggles.commsSab, x => CheatToggles.commsSab = x),
                new ToggleInfo(" Doors", () => CheatToggles.doorsSab, x => CheatToggles.doorsSab = x),
                new ToggleInfo(" MushroomMixup", () => CheatToggles.mushSab, x => CheatToggles.mushSab = x),
            }),
            new SubmenuInfo("Vents", false, new List<ToggleInfo>() {
                new ToggleInfo(" Unlock Vents", () => CheatToggles.useVents, x => CheatToggles.useVents = x),
                new ToggleInfo(" Kick All From Vents", () => CheatToggles.kickVents, x => CheatToggles.kickVents = x),
                new ToggleInfo(" Walk In Vents", () => CheatToggles.walkVent, x => CheatToggles.walkVent = x)
            }),
        }));

      groups.Add(new GroupInfo("Updates", false, new List<ToggleInfo> {
    new ToggleInfo(" Join Discord", () => false, v =>
    {
        Application.OpenURL("https://discord.gg/z4WPDcu4fU");
    }),
    new ToggleInfo(" Visit YouTube", () => false, v =>
    {
        Application.OpenURL("https://youtube.com/@show_black_");
    }),
}, new List<SubmenuInfo>()));


        groups.Add(new GroupInfo("Chat", false, new List<ToggleInfo>() {
            new ToggleInfo(" Enable Chat", () => CheatToggles.alwaysChat, x => CheatToggles.alwaysChat = x),
            new ToggleInfo(" Unlock Textbox", () => CheatToggles.chatJailbreak, x => CheatToggles.chatJailbreak = x)
        }, new List<SubmenuInfo>()));

        // Host-Only cheats are temporarly disabled because of some bugs

        //groups.Add(new GroupInfo("Host-Only", false, new List<ToggleInfo>() {
        //    new ToggleInfo(" ImpostorHack", () => CheatSettings.impostorHack, x => CheatSettings.impostorHack = x),
        //    new ToggleInfo(" Godmode", () => CheatSettings.godMode, x => CheatSettings.godMode = x),
        //    new ToggleInfo(" EvilVote", () => CheatSettings.evilVote, x => CheatSettings.evilVote = x),
        //    new ToggleInfo(" VoteImmune", () => CheatSettings.voteImmune, x => CheatSettings.voteImmune = x)
        //}, new List<SubmenuInfo>()));

        // Console is temporarly disabled until we implement some features for it

        //groups.Add(new GroupInfo("Console", false, new List<ToggleInfo>() {
        //    new ToggleInfo(" ConsoleUI", () => ShowMenu.consoleUI.isVisible, x => ShowMenu.consoleUI.isVisible = x),
        //}, new List<SubmenuInfo>()));

        groups.Add(new GroupInfo("Host-Only", false, 
        new List<ToggleInfo>{
            new ToggleInfo(" Kill While Vanished", () => CheatToggles.killVanished, x => CheatToggles.killVanished = x),
            new ToggleInfo(" Kill Anyone", () => CheatToggles.killAnyone, x => CheatToggles.killAnyone = x),
            new ToggleInfo(" No Kill Cooldown", () => CheatToggles.zeroKillCd, x => CheatToggles.zeroKillCd = x),
        },
        new List<SubmenuInfo>{
            new SubmenuInfo("Murder", false, new List<ToggleInfo>() {
                new ToggleInfo(" Kill Player", () => CheatToggles.killPlayer, x => CheatToggles.killPlayer = x),
                //new ToggleInfo(" Telekill Player", () => CheatToggles.telekillPlayer, x => CheatToggles.telekillPlayer = x),
                new ToggleInfo(" Kill All Crewmates", () => CheatToggles.killAllCrew, x => CheatToggles.killAllCrew = x),
                new ToggleInfo(" Kill All Impostors", () => CheatToggles.killAllImps, x => CheatToggles.killAllImps = x),
                new ToggleInfo(" Kill Everyone", () => CheatToggles.killAll, x => CheatToggles.killAll = x),
            }),
        }));

        groups.Add(new GroupInfo("Passive", false, new List<ToggleInfo>() {
            new ToggleInfo(" Free Cosmetics", () => CheatToggles.freeCosmetics, x => CheatToggles.freeCosmetics = x),
            new ToggleInfo(" Avoid Penalties", () => CheatToggles.avoidBans, x => CheatToggles.avoidBans = x),
            new ToggleInfo(" Unlock Extra Features", () => CheatToggles.unlockFeatures, x => CheatToggles.unlockFeatures = x),
        }, new List<SubmenuInfo>()));
    }

    private void Update(){

        if (Input.GetKeyDown(Utils.stringToKeycode(ShowMenu.menuKeybind.Value)))
        {
            //Enable-disable GUI with DELETE key
            isGUIActive = !isGUIActive;

            //Also teleport the window to the mouse for immediate use
            Vector2 mousePosition = Input.mousePosition;
            windowRect.position = new Vector2(mousePosition.x, Screen.height - mousePosition.y);
        }

        //Passive cheats are always on to avoid problems
        CheatToggles.unlockFeatures = CheatToggles.freeCosmetics = CheatToggles.avoidBans = true;

        if(!Utils.isPlayer){
            CheatToggles.changeRole = CheatToggles.killAll = CheatToggles.telekillPlayer = CheatToggles.killAllCrew = CheatToggles.killAllImps = CheatToggles.teleportCursor = CheatToggles.teleportPlayer = CheatToggles.spectate = CheatToggles.freecam = CheatToggles.killPlayer = false;
        }

        if(!Utils.isHost && !Utils.isFreePlay){
            CheatToggles.killAll = CheatToggles.telekillPlayer = CheatToggles.killAllCrew = CheatToggles.killAllImps = CheatToggles.killPlayer = CheatToggles.zeroKillCd = CheatToggles.killAnyone = CheatToggles.killVanished = false;
        }

        //Host-only cheats are turned off if LocalPlayer is not the game's host
        //if(!CheatChecks.isHost){
        //    CheatToggles.voteImmune = CheatToggles.godMode = CheatToggles.impostorHack = CheatToggles.evilVote = false;
        //}

        //Some cheats only work if the ship is present, so they are turned off if it is not
        if(!Utils.isShip){
            CheatToggles.unfixableLights = CheatToggles.completeMyTasks = CheatToggles.kickVents = CheatToggles.reportBody = CheatToggles.closeMeeting = CheatToggles.reactorSab = CheatToggles.oxygenSab = CheatToggles.commsSab = CheatToggles.elecSab = CheatToggles.mushSab = CheatToggles.doorsSab = false;
        }
    }

    public void OnGUI()
    {

        if (!isGUIActive) return;

        if (submenuButtonStyle == null)
        {
            submenuButtonStyle = new GUIStyle(GUI.skin.button);

            submenuButtonStyle.normal.textColor = Color.gray;

            submenuButtonStyle.fontSize = 18;
            GUI.skin.toggle.fontSize = GUI.skin.button.fontSize = 20;

            submenuButtonStyle.normal.background = Texture2D.grayTexture;
            submenuButtonStyle.normal.background.Apply();
        }

        //Only change the window height while the user is not dragging it
        //Or else dragging breaks
        if (!isDragging)
        {
            int windowHeight = CalculateWindowHeight();
            windowRect.height = windowHeight;
        }

        Color uiColor;

        string configHtmlColor = ShowMenu.menuHtmlColor.Value;

        if (!ColorUtility.TryParseHtmlString(configHtmlColor, out uiColor))
        {
            if (!configHtmlColor.StartsWith("#"))
            {
                if (ColorUtility.TryParseHtmlString("#" + configHtmlColor, out uiColor))
                {
                    GUI.backgroundColor = uiColor;
                }
            }
        }
        else
        {
            GUI.backgroundColor = uiColor;
        }

        windowRect = GUI.Window(0, windowRect, (GUI.WindowFunction)WindowFunction, " . ");
    }

public void WindowFunction(int windowID)
{
    int toggleSpacing = 24;
    int submenuSpacing = 26;
    int groupButtonWidth = 100;
    int groupButtonHeight = 30;

    GUIStyle groupButtonStyle = new GUIStyle(GUI.skin.button);
    groupButtonStyle.fontSize = 12;
    groupButtonStyle.alignment = TextAnchor.MiddleCenter;
    groupButtonStyle.normal.textColor = Color.white;

    GUIStyle activeGroupButtonStyle = new GUIStyle(groupButtonStyle);
    activeGroupButtonStyle.normal.textColor = Color.green;

    GUIStyle titleStyle = new GUIStyle(GUI.skin.label);
    titleStyle.alignment = TextAnchor.MiddleCenter;
    titleStyle.fontSize = 14;
    titleStyle.normal.textColor = Color.cyan;

    GUIStyle toggleStyle = new GUIStyle(GUI.skin.toggle);
    toggleStyle.fontSize = 11;
    toggleStyle.normal.textColor = Color.white;

    GUIStyle submenuStyle = new GUIStyle(GUI.skin.button);
    submenuStyle.fontSize = 11;
    submenuStyle.normal.textColor = Color.cyan;

    GUIStyle borderOnlyStyle = new GUIStyle(GUI.skin.box);
    borderOnlyStyle.normal.background = Texture2D.grayTexture;
    borderOnlyStyle.normal.textColor = Color.clear;

    // Título centralizado no topo
    GUI.Label(new Rect(0, 5, windowRect.width, 20), "ShowMenu v" + ShowMenu.showVersion, titleStyle);

    // Scroll horizontal com roda do mouse
    float scrollSpeed = 20f;
    if (Event.current.type == EventType.ScrollWheel)
    {
        windowRect.x += Event.current.delta.y * scrollSpeed;
    }

    // Botões de grupo horizontais no topo
    for (int groupId = 0; groupId < groups.Count; groupId++)
    {
        GroupInfo group = groups[groupId];
        var style = group.isExpanded ? activeGroupButtonStyle : groupButtonStyle;

        group.isExpanded = GUI.Toggle(
            new Rect(10 + groupId * (groupButtonWidth + 8), 28, groupButtonWidth, groupButtonHeight),
            group.isExpanded,
            group.name,
            style
        );

        groups[groupId] = group;
    }

    int columnX = 10;
    int columnWidth = 200;
    int columnPadding = 20;
    int currentColumn = 0;

    for (int groupId = 0; groupId < groups.Count; groupId++)
    {
        GroupInfo group = groups[groupId];
        if (!group.isExpanded) continue;

        int x = columnX + currentColumn * (columnWidth + columnPadding);
        int y = 70;

        // Desenha apenas a borda branca (fundo transparente)
        GUI.color = Color.gray;
        GUI.Box(new Rect(x - 6, y - 8, columnWidth + 12, 900), GUIContent.none, borderOnlyStyle);

        // Reset GUI cor para elementos internos
        GUI.color = new Color(1, 1, 1, 1); 

        GUI.Label(new Rect(x, y, columnWidth, 20), group.name, groupButtonStyle);
        y += 24;

        foreach (var toggle in group.toggles)
        {
            bool currentState = toggle.getState();
            bool newState = GUI.Toggle(new Rect(x, y, columnWidth, 20), currentState, toggle.label, toggleStyle);
            if (newState != currentState)
                toggle.setState(newState);
            y += toggleSpacing;
        }

        for (int submenuId = 0; submenuId < group.submenus.Count; submenuId++)
        {
            var submenu = group.submenus[submenuId];

            if (GUI.Button(new Rect(x, y, columnWidth, 22), submenu.name, submenuStyle))
            {
                submenu.isExpanded = !submenu.isExpanded;
                group.submenus[submenuId] = submenu;
            }
            y += submenuSpacing;

            if (submenu.isExpanded)
            {
                foreach (var toggle in submenu.toggles)
                {
                    bool currentState = toggle.getState();
                    bool newState = GUI.Toggle(new Rect(x + 8, y, columnWidth - 8, 20), currentState, toggle.label, toggleStyle);
                    if (newState != currentState)
                        toggle.setState(newState);
                    y += toggleSpacing;
                }
            }
        }

        groups[groupId] = group;
        currentColumn++;
    }

    if (Event.current.type == EventType.MouseDrag)
        isDragging = true;
    if (Event.current.type == EventType.MouseUp)
        isDragging = false;

    GUI.DragWindow();
}


    // Dynamically calculate the window's height depending on
    // The number of toggles & group expansion
    private int CalculateWindowHeight()
    {
        int totalHeight = 70; // Base height for the window
        int groupHeight = 50; // Height for each group title
        int toggleHeight = 30; // Height for each toggle
        int submenuHeight = 40; // Height for each submenu title

        foreach (GroupInfo group in groups)
        {
            totalHeight += groupHeight; // Always add height for the group title

            if (group.isExpanded)
            {
                totalHeight += group.toggles.Count * toggleHeight; // Add height for toggles in the group

                foreach (SubmenuInfo submenu in group.submenus)
                {
                    totalHeight += submenuHeight; // Always add height for the submenu title

                    if (submenu.isExpanded)
                    {
                        totalHeight += submenu.toggles.Count * toggleHeight; // Add height for toggles in the expanded submenu
                    }
                }
            }
        }

        return totalHeight;
    }


    // Closes all expanded groups other than indexToKeepOpen
    private void CloseAllGroupsExcept(int indexToKeepOpen)
    {
        for (int i = 0; i < groups.Count; i++)
        {
            if (i != indexToKeepOpen)
            {
                GroupInfo group = groups[i];
                group.isExpanded = false;
                groups[i] = group;
            }
        }
    }

    private void CloseAllSubmenusExcept(GroupInfo group, int submenuIndexToKeepOpen)
    {
        for (int i = 0; i < group.submenus.Count; i++)
        {
            if (i != submenuIndexToKeepOpen)
            {
                var submenu = group.submenus[i];
                submenu.isExpanded = false;
                group.submenus[i] = submenu;
            }
        }
    }

}