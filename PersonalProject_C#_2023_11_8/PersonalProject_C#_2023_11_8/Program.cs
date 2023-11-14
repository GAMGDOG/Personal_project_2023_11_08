using System.Diagnostics;
using System.Runtime.CompilerServices;
using ConsoleTables;
using System.Linq;
using System.Threading;
using static System.Net.Mime.MediaTypeNames;

namespace TextDungeon
{
    internal class Program
    {
        public static Character player; //플레이어
        public static List<Monster> monsters = new List<Monster>(); //몬스터
        public static List<Item> Inventory = new List<Item>(); //인벤토리
        private static List<Item> SortInventory = new List<Item>(); //정렬된 인벤토리
        private static List<Item> Shop = new List<Item>();  //상점 아이템 목록
        public static List<Item> Armor = new List<Item>(); //방어구 아이템 목록
        public static List<Item> Weapon = new List<Item>();//무기 아이템 목록
        static void Main(string[] args)
        {
            GameDataSetting();
            DisplayGameIntro();
        }
        static void GameDataSetting()
        {
            // 캐릭터 정보 세팅(이름, 직업, 레벨, 공격력, 방어력, 체력, 골드)
            player = new Character("Chad", "전사", 1, 10, 5, 100, 1500);

            // 아이템 정보 세팅(이름, 레벨, 공격력, 방어력, 효과, 체력, 골드, 설명)
            Item leather_armor = new Item("가죽갑옷", 1, 0, 2, "방어력 +2", 0, 50, "가죽으로 만든 기본적인 갑옷");
            Item iron_armor = new Item("무쇠갑옷", 3, 0, 5, "방어력 +5", 0, 100, "무쇠로 만들어져 튼튼한 갑옷");
            Item steel_armor = new Item("강철갑옷", 5, 0, 10, "방어력 +10", 0, 200, "강철로 만들어진 병사의 갑옷");
            Item knight_armor = new Item("기사갑옷", 10, 0, 15, "방어력 +15", 0, 500, "기사들이 입는 튼튼한 갑옷");
            Item skulknight_armor = new Item("해골기사의 갑옷", 15, 0, 25, "방어력 +25", 0, 1000, "던전의 수호자 해골 기사의 갑옷");
            Armor.Add(leather_armor);
            Armor.Add(iron_armor);
            Armor.Add(steel_armor);
            Armor.Add(knight_armor);
            Armor.Add(skulknight_armor);

            Item old_sword = new Item("낡은 검", 1, 2, 0, "공격력 +2", 0, 50, "쉽게 볼 수 있는 낡은 검");
            Item short_sword = new Item("숏소드", 3, 5, 0, "공격력 +5", 0, 100, "병사들이 지급받는 기본적인 검");
            Item long_sword = new Item("롱소드", 5, 10, 0, "공격력 +10", 0, 200, "검사들이 사용하는 균형잡힌 검");
            Item knight_sword = new Item("기사의 검", 10, 15, 0, "공격력 +15", 0, 500, "기사들이 사용하는 강철검");
            Item skulknight_sword = new Item("해골기사의 검", 15, 25, 0, "공력력 +25", 0, 1000, "던전의 수호자 해골 기사의 검");
            Weapon.Add(old_sword);
            Weapon.Add(short_sword);
            Weapon.Add(long_sword);
            Weapon.Add(knight_sword);
            Weapon.Add(skulknight_sword);

            Inventory.Add(Armor[0]);    //기본 아이템 인벤토리 추가
            Inventory.Add(Weapon[0]);

            for (int i = 1; i < Weapon.Count - 1; i++)
            {
                Shop.Add(Weapon[i]);//상점 판매 아이템 추가
            }
            for (int i = 1; i < Armor.Count - 1; i++)
            {
                Shop.Add(Armor[i]);//상점 판매 아이템 추가
            }

            //몬스터 정보 세팅(이름, 공격력, 방어력, 체력, 경험치, 골드)
            Monster slime = new Monster("슬라임", 3, 5, 10, 3, 30);
            Monster goblin = new Monster("고블린", 5, 2, 15, 5, 45);
            Monster kobold = new Monster("코볼트", 10, 10, 25, 10, 75);
            Monster orc = new Monster("오크", 20, 15, 40, 30, 150);
            Monster skeleton = new Monster("해골병사", 12, 8, 40, 20, 100);
            Monster zombie = new Monster("좀비", 8, 12, 40, 20, 100);
            Monster skeletonKnight = new Monster("해골기사", 20, 30, 100, 50, 300);

            monsters.Add(slime);
            monsters.Add(goblin);
            monsters.Add(kobold);
            monsters.Add(orc);
            monsters.Add(zombie);
            monsters.Add(skeleton);
            monsters.Add(skeletonKnight);
        }   //게임 데이터 설정
        public static void DisplayGameIntro()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("스파르타 마을에 오신 여러분 환영합니다.");
            Console.ResetColor();
            Console.WriteLine("이곳에서 던전으로 들어가기 전 활동을 할 수 있습니다.");
            Console.WriteLine();
            Console.WriteLine("1. 상태보기");
            Console.WriteLine("2. 인벤토리");
            Console.WriteLine("3. 상점");
            Console.WriteLine("4. 던전 입장");
            Console.WriteLine("5. 휴식하기");
            Console.WriteLine();
            Console.WriteLine("원하시는 행동을 입력해주세요.");
            Console.Write(">>");

            int input = CheckValidInput(1, 5);
            switch (input)
            {
                case 1:
                    DisplayMyInfo();
                    break;
                case 2:
                    DisplayInventory();
                    break;
                case 3:
                    DisplayShop();
                    break;
                case 4:
                    Dungeon.DisplayDungeon();
                    break;
                case 5:
                    Rest();
                    break;
            }
        }   //게임 시작 화면
        static void DisplayMyInfo()
        {
            Console.Clear();

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("상태보기");
            Console.ResetColor();
            Console.WriteLine("캐릭터의 정보를 표시합니다.");
            Console.WriteLine();
            Console.WriteLine($"Lv.{player.Level}");
            Console.WriteLine($"{player.Name}({player.Job})");
            Console.WriteLine($"공격력 :{player.Atk}");
            Console.WriteLine($"방어력 : {player.Def}");
            Console.WriteLine($"체력 : {player.Hp}");
            Console.WriteLine($"Gold : {player.Gold} G");
            Console.WriteLine();
            Console.WriteLine("0. 나가기");
            Console.WriteLine();
            Console.WriteLine("원하시는 행동을 입력해주세요.");
            Console.Write(">>");

            int input = CheckValidInput(0, 0);
            switch (input)
            {
                case 0:
                    DisplayGameIntro();
                    break;
            }
        }   //플레이어 정보 확인
        static void DisplayInventory()
        {
            ConsoleTable table = new ConsoleTable("아이템 이름", "효과", "설명");
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("인벤토리");
            Console.ResetColor();
            Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.");
            Console.WriteLine("\n[아이템 목록]"); ;
            for (int i = 0; i < Inventory.Count; i++)
            {
                table.AddRow(Inventory[i].Name, Inventory[i].Effect, Inventory[i].Descriptions);
            }
            table.Write();
            Console.WriteLine();
            Console.WriteLine("1. 장착 관리");
            Console.WriteLine("2. 아이템 정렬");
            Console.WriteLine("0. 나가기");
            Console.WriteLine();
            Console.WriteLine("원하시는 행동을 입력해주세요.");
            Console.Write(">>");

            int input = CheckValidInput(0, 2);
            switch (input)
            {
                case 0:
                    DisplayGameIntro();
                    break;
                case 1:
                    DisplayItemManagement();
                    break;
                case 2:
                    DisplaySortInventory(Inventory);
                    break;
            }
        }   //인벤토리 정보 확인
        static void DisplayItemManagement()
        {
            ConsoleTable table = new ConsoleTable("아이템 이름", "효과", "설명");
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("인벤토리 - 아이템 장착");
            Console.ResetColor();
            Console.WriteLine("보유 중인 아이템을 장착할 수 있습니다.");
            Console.WriteLine("\n[아이템 목록]");
            for (int i = 0; i < Inventory.Count; i++)
            {
                table.AddRow(i + 1 + ". " + Inventory[i].Name, Inventory[i].Effect, Inventory[i].Descriptions);
            }
            table.Write();
            Console.WriteLine();
            Console.WriteLine("0. 나가기");
            Console.WriteLine();
            Console.WriteLine("원하시는 행동을 입력해주세요.");
            Console.Write(">>");
            int input = CheckValidInput(0, Inventory.Count + 1);
            if (input == 0)
            {
                DisplayGameIntro();
            }
            else
            {
                if (Inventory[input - 1].Name.Contains("[E]"))
                {
                    Inventory[input - 1].UnEquip(player, Weapon, Armor);
                    DisplayItemManagement();
                }
                else
                {
                    Inventory[input - 1].Equip(player, Inventory, Weapon, Armor);
                    DisplayItemManagement();
                }
            }
        }   //아이템 장착 관리
        static void DisplaySortInventory(List<Item> Inventory)
        {
            ConsoleTable table = new ConsoleTable("아이템 이름", "효과", "설명");
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("인벤토리 - 아이템 정렬");
            Console.ResetColor();
            Console.WriteLine("보유 중인 아이템을 정렬할 수 있습니다.");
            Console.WriteLine("\n[아이템 목록]");
            for (int i = 0; i < Inventory.Count; i++)
            {
                table.AddRow(Inventory[i].Name, Inventory[i].Effect, Inventory[i].Descriptions);
            }
            table.Write();
            Console.WriteLine();
            Console.WriteLine("1. 이름 순으로");
            Console.WriteLine("2. 공격력 순으로");
            Console.WriteLine("3. 방어력 순으로");
            Console.WriteLine("0. 나가기");
            Console.WriteLine();
            Console.WriteLine("원하시는 행동을 입력해주세요.");
            Console.Write(">>");
            int input = CheckValidInput(0, 3);
            switch (input)
            {
                case 0:
                    DisplayInventory();
                    break;
                case 1:
                    var sortName = Inventory.OrderBy(x => x.Name);
                    SortInventory = sortName.ToList<Item>();
                    DisplaySortInventory(SortInventory);
                    break;
                case 2:
                    var sortAtk = Inventory.OrderByDescending(x => x.Atk);
                    SortInventory = sortAtk.ToList<Item>();
                    DisplaySortInventory(SortInventory);
                    break;
                case 3:
                    var sortDef = Inventory.OrderByDescending(x => x.Def);
                    SortInventory = sortDef.ToList<Item>();
                    DisplaySortInventory(SortInventory);
                    break;
            }
        }  //아이템 목록 정렬
        static void DisplayShop()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("상점");
            Console.ResetColor();
            Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.");
            Console.WriteLine("[보유 골드]");
            Console.WriteLine(player.Gold + " G\n");
            Console.WriteLine("[아이템 목록]");
            var table = new ConsoleTable("아이템 목록", "능력치", "설명", "가격");
            for (int i = 0; i < Shop.Count; i++)
            {
                if (Inventory.Contains(Shop[i]))
                {
                    table.AddRow(Shop[i].Name, Shop[i].Effect, Shop[i].Descriptions, "구매완료");
                }
                else
                {
                    table.AddRow(Shop[i].Name, Shop[i].Effect, Shop[i].Descriptions, Shop[i].Gold + " G");
                }
            } //테이블 추가
            table.Write(); //테이블 출력(아이템 목록)
            Console.WriteLine("1. 아이템 구매");
            Console.WriteLine("2. 아이템 판매");
            Console.WriteLine("0. 나가기");
            Console.WriteLine();
            Console.WriteLine("원하시는 행동을 입력해주세요.");
            Console.Write(">>");
            int input = CheckValidInput(0, 2);
            switch (input)
            {
                case 0:
                    DisplayGameIntro();
                    break;
                case 1:
                    DisplayBuy();
                    break;
                case 2:
                    DisplaySell();
                    break;
            }
        }  //상점 기본 화면
        static void DisplayBuy()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("상점 - 아이템 구매");
            Console.ResetColor();
            Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.");
            Console.WriteLine("[보유 골드]");
            Console.WriteLine(player.Gold + " G\n");
            Console.WriteLine("[아이템 목록]");
            var table = new ConsoleTable("아이템 목록", "능력치", "설명", "가격");
            for (int i = 0; i < Shop.Count; i++)
            {
                if (Inventory.Contains(Shop[i]))
                {
                    table.AddRow((i + 1) + ". " + Shop[i].Name, Shop[i].Effect, Shop[i].Descriptions, "구매완료");
                }
                else
                {
                    table.AddRow((i + 1) + ". " + Shop[i].Name, Shop[i].Effect, Shop[i].Descriptions, Shop[i].Gold + " G");
                }
            } //테이블 추가
            table.Write(); //테이블 출력(아이템 목록);
            Console.WriteLine("0. 나가기");

            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("원하시는 행동을 입력해주세요.");
                Console.Write(">>");
                int input = CheckValidInput(0, Shop.Count);
                if (input == 0)
                {
                    DisplayShop();
                }
                else
                {
                    if (Inventory.Contains(Shop[input - 1]))
                    {
                        Console.WriteLine("이미 구매한 아이템입니다.");
                    }
                    else if (player.Gold >= Shop[input - 1].Gold)
                    {
                        player.Gold -= Shop[input - 1].Gold;
                        Inventory.Add(Shop[input - 1]);
                        Console.WriteLine("구매를 완료했습니다.");
                        Thread.Sleep(1000);
                        DisplayBuy();
                    }
                    else if (player.Gold < Shop[input - 1].Gold)
                    {
                        Console.WriteLine("Gold가 부족합니다.");
                    }
                }
            }
        }   //아이템 구매 화면
        static void DisplaySell()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("상점 - 아이템 판매");
            Console.ResetColor();
            Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.");
            Console.WriteLine("[보유 골드]");
            Console.WriteLine(player.Gold + " G\n");
            Console.WriteLine("[아이템 목록]");
            var table = new ConsoleTable("아이템 목록", "능력치", "설명", "가격");
            for (int i = 0; i < Inventory.Count; i++)
            {
                table.AddRow(i + 1 + ". " + Inventory[i].Name, Inventory[i].Effect, Inventory[i].Descriptions, Inventory[i].Gold);
            }
            table.Write();
            Console.WriteLine();
            Console.WriteLine("0.나가기");
            while (true) //판매할 아이템 선택
            {
                Console.WriteLine();
                Console.WriteLine("원하시는 행동을 입력해주세요.");
                Console.Write(">>");
                int input = CheckValidInput(0, Inventory.Count);
                if (input == 0)
                {
                    DisplayShop();
                }
                else
                {
                    if (Inventory[input - 1].Name.Contains("[E]")) //장착중인 아이템일때
                    {
                        Inventory[input - 1].UnEquip(player, Weapon, Armor);
                        player.Gold += Inventory[input - 1].Gold * 85 / 100;
                        Shop.Add(Inventory[input - 1]);
                        Inventory.Remove(Inventory[input - 1]);
                        Console.WriteLine("판매 완료되었습니다.");
                        Thread.Sleep(1000);
                        DisplaySell();
                    }
                    else
                    {
                        player.Gold += Inventory[input - 1].Gold * 85 / 100;
                        Shop.Add(Inventory[input - 1]);
                        Inventory.Remove(Inventory[input - 1]);
                        Console.WriteLine("판매 완료되었습니다.");
                        Thread.Sleep(1000);
                        DisplaySell();
                    }
                }
            }
        }  //아이템 판매 화면
        
        public static void Rest()
        {
            while (true)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine("휴식하기");
                Console.ResetColor();
                Console.WriteLine("500 G 를 내면 체력을 회복할 수 있습니다. (보유 골드 : " + player.Gold + " G)");
                Console.WriteLine("1. 휴식하기");
                Console.WriteLine("0. 나가기");
                Console.WriteLine();
                Console.WriteLine("원하시는 행동을 입력해주세요.");
                Console.Write(">>");

                int input = CheckValidInput(0, 1);
                switch (input)
                {
                    case 0:
                        DisplayGameIntro();
                        break;
                    case 1:
                        if (player.Gold >= 500)
                        {
                            player.Hp = 100; player.Gold -= 500;
                            Console.WriteLine("휴식을 완료했습니다.");
                        }
                        else
                        {
                            Console.WriteLine("Gold 가 부족합니다.");
                        }
                        Thread.Sleep(1000);
                        break;
                }
            }
        }//휴식하기 화면
        public static int CheckValidInput(int min, int max)
        {
            while (true)
            {
                string input = Console.ReadLine();

                bool parseSuccess = int.TryParse(input, out var ret);
                if (parseSuccess)
                {
                    if (ret >= min && ret <= max)
                        return ret;
                }
                Console.WriteLine("잘못된 입력입니다.");
            }
        }
    }
}