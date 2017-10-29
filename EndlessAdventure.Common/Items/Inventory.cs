﻿using System;
using System.Collections.Generic;
using EndlessAdventure.Common.Characters;

namespace EndlessAdventure.Common.Items
{
	public class Inventory
	{
		public Dictionary<ItemType, Item> Equipped { get; private set; }
		public List<Item> Equippables { get; private set; }
		public List<Item> Consumables { get; private set; }
		public List<Item> Miscellaneous { get; private set; }

		public Inventory(Dictionary<ItemType, Item> equipped,
		                 List<Item> equippables,
										 List<Item> consumables,
										 List<Item> miscellaneous) {
			Equipped = equipped ?? new Dictionary<ItemType, Item>();
			Equippables = equippables ?? new List<Item>();
			Consumables = consumables ?? new List<Item>();
			Miscellaneous = miscellaneous ?? new List<Item>();
		}

		public void Equip(Item item, Character character) {
			if (item.Type == ItemType.Consumable || item.Type == ItemType.Miscellaneous) {
				throw new ArgumentException();
			}

			// add/remove buffs depending on what was equipped/unequipped
			List<ABuff> buffsToAdd = null;
			List<ABuff> buffsToRemove = null;
			if (Equipped.TryGetValue(item.Type, out Item equipped)) {
				Equipped.Remove(item.Type);
				Equipped.Add(item.Type, item);
				Equippables.Add(equipped);

				buffsToAdd = item.Buffs;
				buffsToRemove = equipped.Buffs;
			}
			else {
				Equippables.Remove(item);

				Equipped.Add(item.Type, item);
				buffsToAdd = item.Buffs;
			}

			if (buffsToAdd != null) {
				foreach (ABuff buff in buffsToAdd) {
					character.AddBuff(buff);
				}
			}
			if (buffsToRemove != null) {
				foreach (ABuff buff in buffsToRemove) {
					character.RemoveBuff(buff);
				}
			}
		}

		public void Unequip(Item equipment, Character character) {
			if (!Equipped.TryGetValue(equipment.Type, out Item equipped)) {
				throw new ArgumentException();
			}

			// remove buffs depending on what was unequipped
			List<ABuff> buffsToRemove = null;
			if (equipped == equipment) {
				Equipped.Remove(equipment.Type);
				Equippables.Add(equipped);
				buffsToRemove = equipped.Buffs;
			}
			if (buffsToRemove != null) {
				foreach (ABuff buff in buffsToRemove) {
					character.RemoveBuff(buff);
				}
			}
		}

		public void Consume(Item item, Character character) {
			if (item.Type != ItemType.Consumable) throw new ArgumentException();
			if (!Consumables.Contains(item)) return;

			Consumables.Remove(item);
			item.Consume(character);
		}

		public void Add(Item item) {
			if (item.Type == ItemType.Consumable) {
				Consumables.Add(item);
			}
			else if (item.Type == ItemType.Miscellaneous) {
				Miscellaneous.Add(item);
			}
			else {
				Equippables.Add(item);
			}
		}

	}
}