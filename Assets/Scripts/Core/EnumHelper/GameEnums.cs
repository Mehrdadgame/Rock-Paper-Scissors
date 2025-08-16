using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Helpers
{
    public static class GameEnums
    {
        public enum GameChoice
        {
            None,
            Rock,
            Paper,
            Scissors
        }

        public enum GameResult
        {
            None,
            PlayerWin,
            BotWin,
            Draw
        }
    }
}

