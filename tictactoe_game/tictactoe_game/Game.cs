using System;

namespace tictactoe_game
{
    internal class Game
    {
        public static bool IsSlotConnect(string[] board, string player)
        {
            // 가로 세자리 연속 일때
            for (int i = 0; i < 9; i += 3)
            {
                if (board[i] == player && board[i + 1] == player && board[i + 2] == player)
                {
                    return true;
                }
            }

            // 세로 세자리 연속 일때
            for (int i = 0; i < 3; i++)
            {
                if (board[i] == player && board[i + 3] == player && board[i + 6] == player)
                {
                    return true;
                }
            }

            // =====================대각선======================
            // 1차원 배열 고정자리 검사
            // 좌상단 우하단 대각 연속 일때
            if (board[0] == player && board[4] == player && board[8] == player)
            {
                return true;
            }

            // 우상단 좌하단 대각 연속 일때
            if (board[2] == player && board[4] == player && board[6] == player)
            {
                return true;
            }

            return false;
        }

        public static int randomAI(string[] board, Random random)
        {
            int selectNum = 0;
            bool isSelect = false;

            //컴퓨터 선택 중복 체크
            while (!isSelect)
            {
                selectNum = random.Next(1, 10);

                if (board[selectNum - 1] == ".")
                {
                    isSelect = true;
                }
            }

            return selectNum;
        }

        public static int smartAI(string[] board)
        {
            int selectNum = 0;

            //컴퓨터 승리 체크
            selectNum = WinCheck(board, "O");

            if (selectNum != 0)
            {
                return selectNum;
            }

            //플레이어 승리 체크 및 방어
            selectNum = WinCheck(board, "X");

            if (selectNum != 0)
            {
                return selectNum;
            }

            //가운데 5번자리 체크
            if (board[4] == ".")
            {
                return 5;
            }

            //모서리 체크
            int[] corners = { 0, 2, 6, 8 };

            for (int i = 0; i < corners.Length; i++)
            {
                if (board[corners[i]] == ".")
                {
                    return corners[i] + 1;
                }
            }

            //남은 빈칸 체크
            for (int i = 0; i < board.Length; i++)
            {
                if (board[i] == ".")
                {
                    return i + 1;
                }
            }

            return 0;
        }

        public static int WinCheck(string[] board, string player)
        {
            for (int i = 0; i < board.Length; i++)
            {
                if (board[i] == ".")
                {
                    board[i] = player; //임시 위치 할당

                    bool isWinCheck = IsSlotConnect(board, player); //임시 위치 할당된 플레이어 승리 체크

                    board[i] = ".";

                    if (isWinCheck)
                    {
                        return i + 1;
                    }
                }
            }

            return 0;
        }
    }    

}
