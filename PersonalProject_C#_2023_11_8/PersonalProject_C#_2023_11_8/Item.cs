using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextDungeon
{
    public class Item
    {
        public string Name { get; set; }
        public int Level { get; }
        public int Atk { get; }
        public int Def { get; }
        public string Effect { get; }
        public int Hp { get; }
        public int Gold { get; }
        public string Descriptions { get; }
        public Item(string name, int level, int atk, int def, string effect, int hp, int gold, string descriptions)
        {
            Name = name;
            Level = level;
            Atk = atk;
            Def = def;
            Effect = effect;
            Hp = hp;
            Gold = gold;
            Descriptions = descriptions;
        }

        public void Equip(Character character, List<Item> inventory, List<Item> weapon, List<Item> armor)
        {
            if (weapon.Contains(this))//this는 장착할 아이템, 장착 아이템이 웨폰인지 확인
            {
                for (int i = 0; i < inventory.Count; i++)
                {
                    if (character.EquipWeapon)
                    {
                        if (inventory[i].Name.Contains("[E]") && weapon.Contains(inventory[i]))
                        {
                            UnEquip(character, inventory[i], weapon, armor);
                            break;
                        }
                    }
                }
                character.EquipWeapon = true;
                Name = "[E]" + Name;
                character.Atk += Atk;
                character.Def += Def;
            }
            else if (armor.Contains(this))
            {
                for (int i = 0; i < inventory.Count; i++)
                {
                    if (character.EquipArmor)
                    {
                        if (inventory[i].Name.Contains("[E]") && armor.Contains(inventory[i]))
                        {
                            UnEquip(character, inventory[i], weapon, armor);
                            break;
                        }
                    }
                }
                character.EquipArmor = true;
                Name = "[E]" + Name;
                character.Atk += Atk;
                character.Def += Def;
            }
        }//아이템 장착 메서드

        public void UnEquip(Character character, Item item, List<Item> weapon, List<Item> armor)
        {
            if (weapon.Contains(item))
            {
                character.EquipWeapon = false;
                item.Name = item.Name.Replace("[E]", "");
                character.Atk -= item.Atk;
                character.Def -= item.Def;
            }
            else if (armor.Contains(item))
            {
                character.EquipArmor = false;
                item.Name = item.Name.Replace("[E]", "");
                character.Atk -= item.Atk;
                character.Def -= item.Def;
            }
        }//아이템 장착 해제 메서드

        public void UnEquip(Character character, List<Item> weapon, List<Item> armor)
        {
            if (weapon.Contains(this))
            {
                character.EquipWeapon = false;
                Name = Name.Replace("[E]", "");
                character.Atk -= Atk;
                character.Def -= Def;
            }
            else if (armor.Contains(this))
            {
                character.EquipArmor = false;
                Name = Name.Replace("[E]", "");
                character.Atk -= Atk;
                character.Def -= Def;
            }
        }//아이템 장착 해제 메서드 오버로딩
    }
}
