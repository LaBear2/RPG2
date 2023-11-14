namespace RPG2
{
    using System;
    using System.Collections.Generic;

    internal class Program
    {
        private static Character player;
        private static List<Item> inventory = new List<Item>();
        private static List<Item> startingItems = new List<Item>
    {
        new Item("무쇠갑옷", "방어력 +5, 무쇠로 만들어져 튼튼한 갑옷입니다."),
        new Item("낡은 검", "공격력 +2, 쉽게 볼 수 있는 낡은 검입니다.")
    };

        static void Main(string[] args)
        {
            GameDataSetting();
            DisplayGameIntro();
        }

        static void GameDataSetting()
        {
            // 캐릭터 정보 세팅
            player = new Character("Chad", "전사", 1, 10, 5, 100, 1500);

            // 처음에 인벤토리에 아이템 추가
            inventory.AddRange(startingItems);
        }

        static void DisplayGameIntro()
        {
            Console.Clear();

            Console.WriteLine("스파르타 마을에 오신 여러분 환영합니다.");
            Console.WriteLine("이곳에서 들어가기 전 활동을 할 수 있습니다.");
            Console.WriteLine();
            Console.WriteLine("1. 상태보기");
            Console.WriteLine("2. 인벤토리");
            Console.WriteLine();
            Console.WriteLine("원하시는 행동을 입력해주세요.");

            int input = CheckValidInput(1, 2);
            switch (input)
            {
                case 1:
                    DisplayMyInfo();
                    break;

                case 2:
                    DisplayInventory();
                    break;

                // 이외의 행동을 하면 출력
                default:
                    Console.WriteLine("잘못된 입력입니다.");
                    break;
            }
        }

        static void DisplayMyInfo()
        {
            Console.Clear();

            Console.WriteLine("상태보기");
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

            int input = CheckValidInput(0, 0);
            switch (input)
            {
                case 0:
                    DisplayGameIntro();
                    break;
            }
        }

        static void DisplayInventory()
        {
            Console.Clear();

            Console.WriteLine("인벤토리");
            Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.");
            Console.WriteLine();

            for (int i = 0; i < inventory.Count; i++)
            {
                string equippedIndicator = inventory[i].Equipped ? "[E] " : "";
                Console.WriteLine($"{i + 1}. {equippedIndicator}{inventory[i].Name} | {inventory[i].Description}");
            }

            Console.WriteLine("0. 나가기");
            Console.WriteLine();
            Console.Write("원하는 아이템을 선택해주세요: ");

            int input = CheckValidInput(0, inventory.Count);
            if (input == 0)
            {
                // 메뉴로 돌아가기
                DisplayGameIntro();
                return;
            }

            Item selected = inventory[input - 1];
            ToggleEquipStatus(selected);
        }

        static void ToggleEquipStatus(Item item)
        {
            if (item.Equipped)
            {
                // 이미 장착 중인 아이템을 선택하면 장착 해제
                item.Equipped = false;
                Console.WriteLine($"[{item.Name}]를 장착 해제했습니다.");
            }
            else
            {
                // 아직 장착 중이지 않은 아이템을 선택하면 장착
                item.Equipped = true;
                Console.WriteLine($"[{item.Name}]를 장착했습니다.");
            }
        }

        public class Item
        {
            public string Name { get; }
            public string Description { get; }
            public bool Equipped { get; set; }

            public Item(string name, string description)
            {
                Name = name;
                Description = description;
                Equipped = false; // 아이템은 처음에 장착되지 않은 상태로 생성
            }
        }

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
        public int Level { get; }
        public int Atk { get; }
        public int Def { get; }
        public int Hp { get; }
        public int Gold { get; }

        public Character(string name, string job, int level, int atk, int def, int hp, int gold)
        {
            Name = name;
            Job = job;
            Level = level;
            Atk = atk;
            Def = def;
            Hp = hp;
            Gold = gold;
        }
    }
}
