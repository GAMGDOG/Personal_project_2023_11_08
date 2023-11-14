using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace TextDungeon
{
    class Dungeon
    {
        public static void DisplayDungeon()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("던전입장");
            Console.ResetColor();
            Console.WriteLine("이곳에서 던전으로 들어가기전 활동을 할 수 있습니다.\n");
            Console.WriteLine("1. 쉬운 던전");
            Console.WriteLine("2. 일반 던전");
            Console.WriteLine("3. 어려운 던전");
            Console.WriteLine("0. 나가기");

            Console.WriteLine();
            Console.WriteLine("원하시는 행동을 입력해주세요.");
            Console.Write(">>");

            int input = Program.CheckValidInput(0, 3);
            switch (input)
            {
                case 1:
                    EasyDungeon();
                    break;
                case 2:
                    NormalDungeon();
                    break;
                case 3:
                    HardDungeon();
                    break;
                case 0:
                    Program.DisplayGameIntro();
                    break;
            }
        }//던전 입장 화면
        static void EasyDungeon()
        {
            for (int i = 0; i < 3; i++)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("쉬운 던전");
                Console.ResetColor();
                Console.WriteLine();
                Console.WriteLine("현재 위치 : " + (i + 1) + "층");
                Console.WriteLine("당신의 앞에는 총 세개의 방이 있습니다.\n들어갈 방의 번호를 입력해주세요.");
                Console.Write(">>");
                int input = Program.CheckValidInput(1, 3);
                Random random = new Random();
                int randomvalue = random.Next(4, 7);
                switch (randomvalue % (input + 1))
                {
                    case 0:
                        Battle(0);
                        break;
                    case 1:
                        TreasureRoom();
                        break;
                    case 2:
                        EmptyRoom();
                        break;
                }
            }
            Console.Clear();
            Console.WriteLine("쉬운 던전을 클리어했습니다.");
            Console.WriteLine("1. 휴식");
            Console.WriteLine("0. 나가기");
            Console.WriteLine();
            Console.WriteLine("원하시는 행동을 입력해주세요.");
            Console.Write(">>");
            int select = Program.CheckValidInput(0, 1);
            switch (select)
            {
                case 0:
                    DisplayDungeon();
                    break;
                case 1:
                    Program.Rest();
                    break;
            }
        }//쉬운 던전 입장
        static void NormalDungeon()
        {
            for (int i = 0; i < 5; i++)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("일반 던전");
                Console.ResetColor();
                Console.WriteLine();
                Console.WriteLine("현재 위치 : " + (i + 1) + "층");
                Console.WriteLine("당신의 앞에는 총 세개의 방이 있습니다.\n들어갈 방을 선택해주세요.");
                Console.Write(">>");
                int input = Program.CheckValidInput(1, 3);
                Random random = new Random();
                int randomvalue = random.Next(4, 7);
                switch (randomvalue % (input + 1))
                {
                    case 0:
                        Battle(1);
                        break;
                    case 1:
                        TreasureRoom();
                        break;
                    case 2:
                        EmptyRoom();
                        break;
                }
            }
            Console.Clear();

            Console.WriteLine("일반 던전을 클리어했습니다.");
            Console.WriteLine("1. 휴식");
            Console.WriteLine("0. 나가기");
            Console.WriteLine();
            Console.WriteLine("원하시는 행동을 입력해주세요.");
            Console.Write(">>");
            int select = Program.CheckValidInput(0, 1);
            switch (select)
            {
                case 0:
                    DisplayDungeon();
                    break;
                case 1:
                    Program.Rest();
                    break;
            }
        }//일반 던전 입장
        static void HardDungeon()
        {
            for (int i = 0; i < 7; i++)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("어려운 던전");
                Console.ResetColor();
                Console.WriteLine();
                Console.WriteLine("현재 위치 : " + (i + 1) + "층");
                if (i < 6)
                {
                    Console.WriteLine("당신의 앞에는 총 세개의 방이 있습니다.\n들어갈 방을 선택해주세요.");
                    int input = Program.CheckValidInput(1, 3);
                    Random random = new Random();
                    int randomvalue = random.Next(4, 7);
                    switch (randomvalue % (input + 1))
                    {
                        case 0:
                            Battle(2);
                            break;
                        case 1:
                            TreasureRoom();
                            break;
                        case 2:
                            EmptyRoom();
                            break;
                    }
                }
                else
                {
                    Battle(3);
                }
            }
            Console.Clear();

            Console.WriteLine("어려운 던전을 클리어했습니다.");
            Console.WriteLine("1. 휴식");
            Console.WriteLine("0. 나가기");
            Console.WriteLine();
            Console.WriteLine("원하시는 행동을 입력해주세요.");
            Console.Write(">>");
            int select = Program.CheckValidInput(0, 1);
            switch (select)
            {
                case 0:
                    DisplayDungeon();
                    break;
                case 1:
                    Program.Rest();
                    break;
            }
        }//어려운 던전 입장
        static void Battle(int grade)
        {
            Console.Clear();
            Random random = new Random();
            int monstervalue = 0;
            int monsterHp = 0;
            switch (grade)
            {
                case 0:
                    monstervalue = random.Next(0, 2);
                    break;//쉬운 던전 몬스터
                case 1:
                    monstervalue = random.Next(2, 4);
                    break;//일반 던전 몬스터
                case 2:
                    monstervalue = random.Next(4, 6);
                    break;//어려운 던전 몬스터
                case 3:
                    break;//보스 몬스터의 경우
            }
            if (grade == 3)
            {
                monstervalue = 6;
                monsterHp = Program.monsters[monstervalue].Hp;
                Console.WriteLine();
                Console.WriteLine("던전의 수호자 해골기사가 나타났습니다.");
                while (true)
                {
                    Console.WriteLine("-------------------------------------------");
                    Console.WriteLine();
                    Console.WriteLine("플레이어의 체력 : " + Program.player.Hp);
                    Console.WriteLine(Program.monsters[monstervalue].Name + "의 체력:" + monsterHp);
                    Console.WriteLine("1. 공격   2. 방어   3. 도망");
                    Console.WriteLine("행동을 선택해 주세요.");
                    Console.Write(">>");
                    int input = Program.CheckValidInput(1, 3);
                    if (input == 3)
                    {
                        int run = random.Next(0, 1);
                        switch (run)
                        {
                            case 0:
                                Console.WriteLine("도망쳤습니다.");
                                Thread.Sleep(1000);
                                DisplayDungeon();
                                break;
                            case 1:
                                Console.WriteLine("도망에 실패했습니다.");
                                break;
                        }
                    }
                    else if (input == 1)
                    {
                        int damage = Program.player.Atk - Program.monsters[monstervalue].Def;
                        if (Program.player.Atk <= Program.monsters[monstervalue].Def)
                            damage = 1;
                        Console.WriteLine(damage + " 의 피해를 입혔습니다.");
                        monsterHp -= damage;
                        Program.monsters[monstervalue].MonsterDoing(Program.player, Program.player.Def / 2);
                    }
                    else if (input == 2)
                    {
                        Program.monsters[monstervalue].MonsterDoing(Program.player, Program.player.Def / 2);
                    }
                    if (monsterHp <= 0)
                    {
                        break;
                    }
                    else if (Program.player.Hp <= 0)
                    {
                        Console.WriteLine("당신은 쓰러졌습니다.");
                        Thread.Sleep(1000);
                        Environment.Exit(0);
                    }
                }   //하드 던전 보스 전투 시나리오
                Console.WriteLine();
                Console.WriteLine("몬스터가 쓰러졌습니다.");
                Console.WriteLine("승리했습니다.");
                Console.WriteLine();
                Program.player.GetExp(Program.monsters[monstervalue].Exp);
                Program.player.Gold += Program.monsters[monstervalue].Gold;
                Console.WriteLine(Program.monsters[monstervalue].Exp + "의 경험치와 " + Program.monsters[monstervalue].Gold + "의 골드를 획득했습니다.");
                for (int i = 0; i < Program.Inventory.Count; i++)
                {
                    if (Program.Inventory[i].Name.Contains("해골기사"))
                    {
                        Program.Inventory.Add(Program.Weapon[4]);
                        Program.Inventory.Add(Program.Armor[4]);
                        Console.WriteLine("해골기사의 검과 해골기사의 갑옷을 획득했습니다.");
                        break;
                    }
                } //하드 던전 보스 보상이 없으면 보상이 인벤토리에 추가
                Thread.Sleep(2000);
            }
            else
            {
                Console.WriteLine(Program.monsters[monstervalue].Name + "이(가) 나타났습니다.");
                monsterHp = Program.monsters[monstervalue].Hp;
                while (true)
                {
                    Console.WriteLine("-------------------------------------------");
                    Console.WriteLine();
                    Console.WriteLine("플레이어의 체력 : " + Program.player.Hp);
                    Console.WriteLine(Program.monsters[monstervalue].Name + " 현재 체력:" + monsterHp);
                    Console.WriteLine("1. 공격   2. 방어   3. 도망");
                    Console.WriteLine("행동을 선택해 주세요.");
                    Console.Write(">>");
                    int input = Program.CheckValidInput(1, 3);
                    if (input == 3)
                    {
                        int run = random.Next(0, 1);
                        switch (run)
                        {
                            case 0:
                                Console.WriteLine("도망쳤습니다.");
                                Thread.Sleep(1000);
                                DisplayDungeon();
                                break;
                            case 1:
                                Console.WriteLine("도망에 실패했습니다.");
                                break;
                        }
                    }
                    else if (input == 1)
                    {
                        int damage = Program.player.Atk - Program.monsters[monstervalue].Def;
                        if (Program.player.Atk <= Program.monsters[monstervalue].Def)
                            damage = 1;
                        Console.WriteLine(damage + " 의 피해를 입혔습니다.");
                        monsterHp -= damage;
                        if (monsterHp <= 0)
                        {
                            break;
                        }
                        Program.monsters[monstervalue].MonsterDoing(Program.player, Program.player.Def / 2);
                    }
                    else if (input == 2)
                    {
                        Program.monsters[monstervalue].MonsterDoing(Program.player, Program.player.Def / 2);
                    }
                    if (Program.player.Hp <= 0)
                    {
                        Console.WriteLine("당신은 쓰러졌습니다.");
                        Thread.Sleep(1000);
                        Environment.Exit(0);
                    }

                } //전투 시나리오
                Console.WriteLine();
                Console.WriteLine("몬스터가 쓰러졌습니다.");
                Console.WriteLine("승리했습니다.");
                Console.WriteLine();
                Program.player.GetExp(Program.monsters[monstervalue].Exp);
                Program.player.Gold += Program.monsters[monstervalue].Gold;
                Console.WriteLine(Program.monsters[monstervalue].Exp + "의 경험치와 " + Program.monsters[monstervalue].Gold + "의 골드를 획득했습니다.");
                Thread.Sleep(2000);
            }
        }//몬스터 전투 메서드
        static void TreasureRoom()
        {
            Console.Clear();
            Console.WriteLine("보물 상자가 있는 방입니다.");
            Console.WriteLine("상자에는 동전이 몇개 남아있습니다.");
            Console.WriteLine("당신은 동전을 챙기고 방을 나왔습니다.");

            Random random = new Random();
            int gold = random.Next(10, 50);
            Program.player.Gold += gold;
            Console.WriteLine("현재 Gold : " + Program.player.Gold);
            Thread.Sleep(3000);
        }//던전 보물방 입장
        static void EmptyRoom()
        {
            Console.Clear();
            Console.WriteLine("비어 있는 방입니다.");
            Console.WriteLine("당신은 휴식을 취했습니다.");
            Console.WriteLine("당신의 체력이 약간 회복했습니다.");
            Random random = new Random();
            int hp = random.Next(10, 30);
            Program.player.Hp += hp;
            if (Program.player.Hp > 100)
                Program.player.Hp = 100;
            Console.WriteLine("현재 체력 : " + Program.player.Hp);
            Thread.Sleep(3000);
        }//던전 빈방 입장
    }
}
