﻿using System;
using EndlessAdventure.Common.Characters;

namespace EndlessAdventure.Common.Equipments.Effects {
	public class WeaponUnequipEffect : IEquipmentEffect {

		public string Name { get; private set; }
		public int Value { get; private set; }

		public WeaponUnequipEffect(string name, int value) {
			Name = name ?? throw new ArgumentException();
			Value = value;
		}

		public void Affect(Character character) {
			character.Stats.TryGetValue(StatType.Attack, out Stat attack);
			attack.Current -= Value;
		}
	}
}