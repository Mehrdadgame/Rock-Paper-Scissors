using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Helpers.GameEnums;

public class RandomBotStrategy : IBotStrategy
{
    public GameChoice MakeChoice()
    {
        int randomChoice = UnityEngine.Random.Range(1, 4);
        return (GameChoice)randomChoice;
    }
}