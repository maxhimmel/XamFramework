using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* 
 * Enable me at: [ Project Settings > Player > Scripting Define Symbols ]
 * ( Don't forget semi-colons between defines! )
 * 
 * OR
 * 
 * Enable me at: [ Xam.asmdef > Define Constraints ]
 */
#if REWIRED
using Rewired;

namespace Xam.Utility.Extensions
{
	public static class RewiredExtensions
	{
		private const string k_enabledControlsRuleSetTag = "Controls";

		#region Layout
		public static void CreateLayoutActivationRules( this Player player )
		{
			player.controllers.maps.layoutManager.ruleSets.Clear();

			IEnumerable<ControllerMap> allMaps = player.controllers.maps.GetAllMaps();
			foreach ( ControllerMap map in allMaps )
			{
				ControllerMapLayoutManager.RuleSet enabledControlsRuleSet = new ControllerMapLayoutManager.RuleSet()
				{
					enabled = true,
					tag = $"{k_enabledControlsRuleSetTag}_{map.categoryId}_{map.layoutId}",
					rules = {
						new ControllerMapLayoutManager.Rule() {
							layoutId = map.layoutId,
							categoryId = map.categoryId,
							controllerSetSelector = ControllerSetSelector.SelectControllerType( ControllerType.Joystick )
						}
					}
				};

				player.controllers.maps.layoutManager.ruleSets.Add( enabledControlsRuleSet );
			}

			player.controllers.maps.layoutManager.Apply();
		}

		public static void SetAllLayoutsActive( this Player player, bool isActive )
		{
			foreach ( ControllerMapLayoutManager.RuleSet ruleSet in player.controllers.maps.layoutManager.ruleSets )
			{
				ruleSet.enabled = isActive;
			}

			player.controllers.maps.layoutManager.Apply();
		}

		public static void SetLayoutActive( this Player player, bool isActive, int categoryId, int layoutId )
		{
			ControllerMapLayoutManager.RuleSet enabledControlsRuleSet = player.controllers.maps.layoutManager.ruleSets.Find(
				queryRuleSet => queryRuleSet.tag == $"{k_enabledControlsRuleSetTag}_{categoryId}_{layoutId}"
			);

			enabledControlsRuleSet.enabled = isActive;

			player.controllers.maps.layoutManager.Apply();
		}
		#endregion

		#region Map
		public static void CreateMapActivationRules( this Player player )
		{
			ControllerMapEnabler.RuleSet enabledControlsRuleSet = new ControllerMapEnabler.RuleSet()
			{
				enabled = true,
				tag = k_enabledControlsRuleSetTag,
				rules = new List<ControllerMapEnabler.Rule>()
			};

			IEnumerable<ControllerMap> allMaps = player.controllers.maps.GetAllMaps();
			foreach ( ControllerMap map in allMaps )
			{
				enabledControlsRuleSet.rules.Add( new ControllerMapEnabler.Rule()
				{
					enable = map.enabled,
					categoryId = map.categoryId,
					controllerSetSelector = ControllerSetSelector.SelectAll()
				} );
			}

			player.controllers.maps.mapEnabler.ruleSets.Clear();
			player.controllers.maps.mapEnabler.ruleSets.Add( enabledControlsRuleSet );
			player.controllers.maps.mapEnabler.Apply();
		}

		public static void SetAllControlsActive( this Player player, bool isActive )
		{
			ControllerMapEnabler.RuleSet enabledControlsRuleSet = player.controllers.maps.mapEnabler.ruleSets.Find(
				queryRuleSet => queryRuleSet.tag == k_enabledControlsRuleSetTag
			);

			foreach ( ControllerMapEnabler.Rule rule in enabledControlsRuleSet.rules )
			{
				rule.enable = isActive;
			}

			player.controllers.maps.mapEnabler.Apply();
		}

		public static void SetControlsActive( this Player player, int categoryId, bool isActive )
		{
			ControllerMapEnabler.RuleSet enabledControlsRuleSet = player.controllers.maps.mapEnabler.ruleSets.Find( 
				queryRuleSet => queryRuleSet.tag == k_enabledControlsRuleSetTag 
			);
			
			foreach ( ControllerMapEnabler.Rule rule in enabledControlsRuleSet.rules )
			{
				if ( rule.categoryId == categoryId )
				{
					rule.enable = isActive;
				}
			}

			player.controllers.maps.mapEnabler.Apply();
		}
		#endregion
	}
}
#endif