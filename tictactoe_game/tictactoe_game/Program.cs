using System;
using tictactoe_game;

Random random = new Random();

bool restart = true;

while (restart)
{
    string[] board = new string[9];

    string playerTag = "X";
    string message = "";

    int gameMode = 0;
    int difficulty = 0;
    int turn = 0;

    bool gameOver = false;

    resetBoard(board);

    while (gameMode == 0)
    {
        Console.Clear();
        Console.WriteLine("=== 틱택토 게임 ===");
        Console.WriteLine();
        Console.WriteLine("1. 두 사람 대전");
        Console.WriteLine("2. 컴퓨터 대전");
        Console.WriteLine();
        Console.Write("모드 선택: ");

        string input = Console.ReadLine();

        if (int.TryParse(input, out int number))
        {
            if (number == 1 || number == 2)
            {
                gameMode = number;
            }
        }
    }

    if (gameMode == 2)
    {
        while (difficulty == 0)
        {
            Console.Clear();
            Console.WriteLine("=== 난이도 선택 ===");
            Console.WriteLine();
            Console.WriteLine("1. 쉬움");
            Console.WriteLine("2. 어려움");
            Console.WriteLine();
            Console.Write("난이도 선택: ");

            string input = Console.ReadLine();

            if (int.TryParse(input, out int number))
            {
                if (number == 1 || number == 2)
                {
                    difficulty = number;
                }
            }
        }
    }

    while (!gameOver)
    {
        int selectNum = 0;

        printScreen(board, playerTag, message);

        message = "";

        if (gameMode == 2 && playerTag == "O")
        {
            if (difficulty == 1)
            {
                selectNum = Game.randomAI(board, random);
            }
            else
            {
                selectNum = Game.smartAI(board);
            }

            message = $"컴퓨터가 {selectNum}번을 선택했습니다.";
        }
        else
        {
            bool inputCheck = false;

            while (!inputCheck)
            {
                Console.Write("위치 선택 (1~9): ");

                string input = Console.ReadLine();

                if (!int.TryParse(input, out selectNum))
                {
                    message = "숫자를 입력해주세요.";
                }
                else if (selectNum < 1 || selectNum > 9)
                {
                    message = "1부터 9까지 입력해주세요.";
                }
                else if (board[selectNum - 1] != ".")
                {
                    message = "이미 사용한 위치입니다.";
                }
                else
                {
                    inputCheck = true;
                }

                if (!inputCheck)
                {
                    printScreen(board, playerTag, message);
                    message = "";
                }
            }
        }

        board[selectNum - 1] = playerTag;
        turn++;

        if (Game.IsSlotConnect(board, playerTag))
        {
            if (gameMode == 2 && playerTag == "O")
            {
                message = "컴퓨터 승리!";
            }
            else
            {
                message = $"플레이어 {playerTag} 승리!";
            }

            gameOver = true;
        }
        else if (turn == 9)
        {
            message = "무승부입니다!";
            gameOver = true;
        }
        else
        {
            if (playerTag == "X")
            {
                playerTag = "O";
            }
            else
            {
                playerTag = "X";
            }
        }
    }

    printScreen(board, playerTag, message);

    bool restartInput = false;
    string restartOutput = "";

    while (!restartInput)
    {
        Console.Clear();
        Console.WriteLine("=== 게임 결과 ===");
        Console.WriteLine();

        printBoard(board);

        Console.WriteLine(message);
        Console.WriteLine();

        if (restartOutput != "")
        {
            Console.WriteLine(restartOutput);
        }

        Console.Write("다시 플레이하시겠습니까? (Y/N): ");

        string input = Console.ReadLine();

        if (input == null)
        {
            input = "";
        }

        input = input.Trim().ToLower();

        if (input == "y" || input == "Y")
        {
            restart = true;
            restartInput = true;
        }
        else if (input == "n" || input == "N")
        {
            restart = false;
            restartInput = true;
        }
        else
        {
            restartOutput = "Y 또는 N을 입력해주세요.";
        }
    }
}

Console.Clear();
Console.WriteLine("게임을 종료합니다.");

//==================[Functions]======================

static void resetBoard(string[] board)
{
    for (int i = 0; i < board.Length; i++)
    {
        board[i] = ".";
    }
}
static void printScreen(string[] board, string player, string message)
{
    Console.Clear();

    Console.WriteLine("=== 틱택토 게임 ===");
    Console.WriteLine();

    printNum();
    printBoard(board);

    Console.WriteLine($"현재 차례: {player}");
    Console.WriteLine();

    if (message != "")
    {
        Console.WriteLine(message);
        Console.WriteLine();
    }
}

static void printNum()
{
    Console.WriteLine("위치 번호:");
    Console.WriteLine("-------------");
    Console.WriteLine("| 1 | 2 | 3 |");
    Console.WriteLine("-------------");
    Console.WriteLine("| 4 | 5 | 6 |");
    Console.WriteLine("-------------");
    Console.WriteLine("| 7 | 8 | 9 |");
    Console.WriteLine("-------------");
    Console.WriteLine();
}

static void printBoard(string[] board)
{
    Console.WriteLine("현재 보드:");

    string format = "| {0} | {1} | {2} |";

    Console.WriteLine("-------------");
    Console.WriteLine(string.Format(format, board[0], board[1], board[2]));
    Console.WriteLine("-------------");
    Console.WriteLine(string.Format(format, board[3], board[4], board[5]));
    Console.WriteLine("-------------");
    Console.WriteLine(string.Format(format, board[6], board[7], board[8]));
    Console.WriteLine("-------------");
    Console.WriteLine();
}