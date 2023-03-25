using System;
using UnityEngine;

namespace Cards
{
    public abstract class MonsterCard : PlayerCard
    {
        public int basePower;
        public MonsterTypes monsterType;
    }
}