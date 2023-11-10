using System.Diagnostics;
using System.Runtime.CompilerServices;
using ConsoleTables;
using System.Linq;
using System.Threading;
using static System.Net.Mime.MediaTypeNames;

internal class Program
{
    private static Character player; //플레이어
    private static List<Monster> monsters = new List<Monster>(); //몬스터
    private static List<Item> Inventory = new List<Item>(); //인벤토리
    private static List<Item> SortInventory = new List<Item>(); //정렬된 인벤토리
    private static List<Item> Shop = new List<Item>();  //상점 아이템 목록
    private static List<Item> Armor = new List<Item>(); //방어구 아이템 목록
    private static List<Item> Weapon = new List<Item>();//무기 아이템 목록
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

    static void DisplayGameIntro()
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
                DisplayDungeon();
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
        Console.WriteLine("\n[아이템 목록]");;
        for(int i = 0; i<Inventory.Count;i++)
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
            table.AddRow(i+1+". "+Inventory[i].Name, Inventory[i].Effect ,Inventory[i].Descriptions);
        }
        table.Write();
        Console.WriteLine();
        Console.WriteLine("0. 나가기");
        Console.WriteLine();
        Console.WriteLine("원하시는 행동을 입력해주세요.");
        Console.Write(">>");
        int input = CheckValidInput(0,Inventory.Count+1);
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
                table.AddRow(Shop[i].Name, Shop[i].Effect, Shop[i].Descriptions, Shop[i].Gold+" G");
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
    }   //상점 기본 화면

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
                table.AddRow((i+1)+". "+Shop[i].Name, Shop[i].Effect, Shop[i].Descriptions, "구매완료");
            }
            else
            {
                table.AddRow((i + 1) + ". " + Shop[i].Name, Shop[i].Effect, Shop[i].Descriptions, Shop[i].Gold +" G");
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
        for(int i = 0;i< Inventory.Count; i++)
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
                    Inventory.Remove(Inventory[input-1]);
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
    }   //아이템 판매 화면

    static void DisplayDungeon()
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

        int input = CheckValidInput(0, 3);
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
                DisplayGameIntro();
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
            int input = CheckValidInput(1, 3);
            Random random = new Random();
            int randomvalue = random.Next(4,7);
            switch (randomvalue%(input+1))
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
        int select = CheckValidInput(0,1);
        switch(select)
        {
            case 0:
                DisplayDungeon();
                break;
            case 1:
                Rest();
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
            int input = CheckValidInput(1, 3);
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
        int select = CheckValidInput(0, 1);
        switch (select)
        {
            case 0:
                DisplayDungeon();
                break;
            case 1:
                Rest();
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
            if(i<6)
            {
                Console.WriteLine("당신의 앞에는 총 세개의 방이 있습니다.\n들어갈 방을 선택해주세요.");
                int input = CheckValidInput(1, 3);
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
        int select = CheckValidInput(0, 1);
        switch (select)
        {
            case 0:
                DisplayDungeon();
                break;
            case 1:
                Rest();
                break;
        }
    }//어려운 던전 입장

    static void Battle(int grade)
    {
        Console.Clear();
        Random random = new Random();
        int monstervalue = 0;
        int monsterHp = 0;
        switch(grade)
        {
            case 0:
                monstervalue = random.Next(0,2); 
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
        if(grade == 3)
        {
            monstervalue = 6;
            monsterHp = monsters[monstervalue].Hp;
            Console.WriteLine();
            Console.WriteLine("던전의 수호자 해골기사가 나타났습니다.");
            while (true)
            {
                Console.WriteLine("-------------------------------------------");
                Console.WriteLine();
                Console.WriteLine("플레이어의 체력 : "+player.Hp);
                Console.WriteLine(monsters[monstervalue].Name + "의 체력:" + monsterHp);
                Console.WriteLine("1. 공격   2. 방어   3. 도망");
                Console.WriteLine("행동을 선택해 주세요.");
                Console.Write(">>");
                int input = CheckValidInput(1, 3);
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
                    int damage = player.Atk - monsters[monstervalue].Def;
                    if (player.Atk <= monsters[monstervalue].Def)
                        damage = 1;
                    Console.WriteLine(damage + " 의 피해를 입혔습니다.");
                    monsterHp -= damage;
                    monsters[monstervalue].MonsterDoing(player, player.Def / 2);
                }
                else if (input == 2)
                {
                    monsters[monstervalue].MonsterDoing(player, player.Def / 2);
                }
                if (monsterHp <= 0)
                {
                    break;
                }
                else if (player.Hp <= 0)
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
            Console.WriteLine(monsters[monstervalue].Exp + "의 경험치와 " + monsters[monstervalue].Gold + "의 골드를 획득했습니다.");
            for(int i = 0;i<Inventory.Count;i++)
            {
                if (Inventory[i].Name.Contains("해골기사"))
                {
                    Inventory.Add(Weapon[4]);
                    Inventory.Add(Armor[4]);
                    Console.WriteLine("해골기사의 검과 해골기사의 갑옷을 획득했습니다.");
                    break;
                }
            } //하드 던전 보스 보상이 없으면 보상이 인벤토리에 추가
            player.Exp += monsters[monstervalue].Exp;
            player.Gold += monsters[monstervalue].Gold;
            Thread.Sleep(2000);
        }
        else
        {
            Console.WriteLine(monsters[monstervalue].Name + "이(가) 나타났습니다.");
            monsterHp = monsters[monstervalue].Hp;
            while (true)
            {
                Console.WriteLine("-------------------------------------------");
                Console.WriteLine();
                Console.WriteLine("플레이어의 체력 : " + player.Hp);
                Console.WriteLine(monsters[monstervalue].Name + " 현재 체력:" + monsterHp);
                Console.WriteLine("1. 공격   2. 방어   3. 도망");
                Console.WriteLine("행동을 선택해 주세요.");
                Console.Write(">>");
                int input = CheckValidInput(1, 3);
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
                    int damage = player.Atk - monsters[monstervalue].Def;
                    if (player.Atk <= monsters[monstervalue].Def)
                        damage = 1;
                    Console.WriteLine(damage + " 의 피해를 입혔습니다.");
                    monsterHp -= damage;
                    if (monsterHp <= 0)
                    {
                        break;
                    }
                    monsters[monstervalue].MonsterDoing(player, player.Def / 2);
                }
                else if (input == 2)
                {
                    monsters[monstervalue].MonsterDoing(player, player.Def / 2);
                }
                if (player.Hp <= 0)
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
            Console.WriteLine(monsters[monstervalue].Exp + "의 경험치와 " + monsters[monstervalue].Gold + "의 골드를 획득했습니다.");
            player.Exp += monsters[monstervalue].Exp;
            player.Gold += monsters[monstervalue].Gold;
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
        player.Gold += gold;
        Console.WriteLine("현재 Gold : " + player.Gold);
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
        player.Hp += hp;
        if (player.Hp > 100)
            player.Hp = 100;
        Console.WriteLine("현재 체력 : " + player.Hp);
        Thread.Sleep(3000);
    }//던전 빈방 입장
    static void Rest()
    {
        while(true)
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
    static int CheckValidInput(int min, int max)
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
        Exp += exp;
        while(Exp > Level*5)
        {
            Exp -= Level * 5;
            Level++;
            Atk += Level;
            Def += Level;
        }
    }//플레이어 경험치 획득 메서드
}

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
    public Item(string name,int level, int atk, int def, string effect, int hp,int gold, string descriptions)
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
        if(weapon.Contains(this))//this는 장착할 아이템, 장착 아이템이 웨폰인지 확인
        {
            for(int i = 0; i < inventory.Count; i++)
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
        else if(armor.Contains(this)) 
        {
            for (int i = 0; i < inventory.Count; i++)
            {
                if(character.EquipArmor)
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
        int damage = this.Atk - player.Def- damagerule;
        if( damage < 0 )
        {
            damage = 1;
        }
        if(rand == 0)
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