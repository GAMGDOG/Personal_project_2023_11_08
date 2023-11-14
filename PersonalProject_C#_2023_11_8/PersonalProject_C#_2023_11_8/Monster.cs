using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextDungeon
{
    public class Monster
    {
        public string Name { get; }
        public int Atk { get; }
        public int Def { get; }
        public int Hp { get; }
        public int Exp { get; }
        public int Gold { get; }

        public Monster(string name, int atk, int def, int hp, int exp, int gold)
        {
            Name = name;
            Atk = atk;
            Def = def;
            Hp = hp;
            Exp = exp;
            Gold = gold;
        }

        public void MonsterDoing(Character player, int damagerule)
        {
            Random random = new Random();
            int rand = random.Next(0, 2);
            int damage = this.Atk - player.Def - damagerule;
            if (damage < 0)
            {
                damage = 1;
            }
            if (rand == 0)
            {
                Console.WriteLine("몬스터의 공격");
                Console.WriteLine(damage + " 의 피해를 입었습니다.");
                player.Hp -= damage;
            }
            else
            {
                Console.WriteLine("몬스터는 상황을 살피고 있습니다.");
            }
        }//몬스터 행동 메서드
    }
}
