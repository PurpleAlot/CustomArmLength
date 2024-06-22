using System;
using BepInEx;
using UnityEngine;
using Utilla;

namespace GorillaTagModTemplateProject
{
	/// <summary>
	/// This is your mod's main class.
	/// </summary>

	/* This attribute tells Utilla to look for [ModdedGameJoin] and [ModdedGameLeave] */
	[ModdedGamemode]
	[BepInDependency("org.legoandmars.gorillatag.utilla", "1.5.0")]
	[BepInPlugin(PluginInfo.GUID, PluginInfo.Name, PluginInfo.Version)]
	public class Plugin : BaseUnityPlugin
	{
		bool inRoom;

		void Start()
		{

			Utilla.Events.GameInitialized += OnGameInitialized;
		}

		void OnEnable()
		{

			HarmonyPatches.ApplyHarmonyPatches();
		}

		void OnDisable()
		{
			/* Undo mod setup here */
			/* This provides support for toggling mods with ComputerInterface, please implement it :) */
			/* Code here runs whenever your mod is disabled (including if it disabled on startup)*/

			HarmonyPatches.RemoveHarmonyPatches();
		}

		void OnGameInitialized(object sender, EventArgs e)
		{
			/* Code here runs after the game initializes (i.e. GorillaLocomotion.Player.Instance != null) */
		}

		void Update()
		{
            if (inRoom == true)
			{
                if (ControllerInputPoller.instance.leftControllerIndexFloat >= 0.1)
                {
                    GorillaLocomotion.Player.Instance.transform.localScale -= new Vector3(0.05f, 0.05f, 0.05f);
                }
                if (ControllerInputPoller.instance.rightControllerIndexFloat >= 0.1)
                {
                    GorillaLocomotion.Player.Instance.transform.localScale += new Vector3(0.05f, 0.05f, 0.05f);
                }
            }
        }

		[ModdedGamemodeJoin]
		public void OnJoin(string gamemode)
		{

			inRoom = true;
		}

		[ModdedGamemodeLeave]
		public void OnLeave(string gamemode)
		{
            
            inRoom = false;
            GorillaLocomotion.Player.Instance.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        }
	}
}
