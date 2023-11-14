using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextDungeon
{
    public class Character
    {
        public string Name { get; }
        public string Job { get; }
        public int Level { get; set; }
        public int Exp { get; set; }
        public int Atk { get; set; }
        public int Def { get; set; }
        public int Hp { get; set; }
        public int Gold { get; set; }
        public bool EquipArmor { get; set; }
        public bool EquipWeapon { get; set; }
        public Character(string name, string job, int level, int atk, int def, int hp, int gold)
        {
            Name = name;
            Job = job;
            Level = level;
            Atk = atk;
            Def = def;
            Hp = hp;
            Gold = gold;
            Exp = 0;
            EquipArmor = false;
            EquipWeapon = false;
        }

        public void GetExp(int exp)
        {
            this.Exp += exp;
            while (Exp > Level * 5)
            {
                this.Exp -= Level * 5;
                this.Level++;
                this.Atk += 1;
                this.Def += 1;
            }
        }//플레이어 경험치 획득 메서드
    }
}
