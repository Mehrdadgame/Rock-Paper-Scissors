using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Helpers
{
    public static class EnumsHelper
    {
        public enum Choice
        {
            Rock,
            Paper,
            Scissors
        }

        public enum GameResult
        {
            Win,
            Lose,
            Draw
        }
    }
}