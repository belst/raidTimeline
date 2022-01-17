﻿using raidTimelineLogic.Helper;
using raidTimelineLogic.Models;
using System.Linq;
using System.Web;

namespace raidTimelineLogic.Mechanics.Strategies
{
	internal class KeepConstructMechanics : BaseMechanics
	{
		public KeepConstructMechanics()
		{
			EncounterIcon = "https://wiki.guildwars2.com/images/e/ea/Mini_Keep_Construct.png";
		}

		public override string CreateHtml(RaidModel model)
		{
			var top = "";
			top += @"<table class=""mechanicsTable"" style=""display: none;"">";
			top += @"<tr style=""color: #aaa"">
						<th>Player</th>
						<th title=""Hail of Fury (Falling Debris)"">Debris</th>
						<th title=""Phantasmal Blades (rotating Attack)"">Pizza</th>
						<th title=""Tower Drop (KC Jump)"">Jump</th>
					</tr>";

			foreach (var player in model.Players.OrderByDescending(i => i.CombinedMechanics.Sum(j => j.Value)).Take(3))
			{
				var mid = $@"
					<tr style=""color: #aaa"">
						<td>{HttpUtility.HtmlEncode(player.AccountName)}</td>
						<td>{player.CombinedMechanics["kc_debris"]}</td>
						<td>{player.CombinedMechanics["kc_pizza"]}</td>
						<td>{player.CombinedMechanics["kc_jump"]}</td>
					</tr>";
				top += mid;
			}

			top += "</table>";

			return top;
		}

		public override void Parse(dynamic logData, PlayerModel playerModel)
		{
			PrepareParsing(logData, playerModel);

			var debris = playerModel.Mechanics.GetOrDefault("Debris");
			var pizza = playerModel.Mechanics.GetOrDefault("Pizza");
			var jump = playerModel.Mechanics.GetOrDefault("Jump");

			playerModel.CombinedMechanics.Add("kc_debris", debris);
			playerModel.CombinedMechanics.Add("kc_pizza", pizza);
			playerModel.CombinedMechanics.Add("kc_jump", jump);
		}
	}
}