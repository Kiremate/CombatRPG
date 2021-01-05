using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AbilitySystem.AbilityBehavior;
using AbilitySystem.Abilities;

namespace AbilitySystem
{
    namespace Effects
    {
        public class SkeletonSummoning : AbilityBehaviors
        {
            private const string effectName = "Skeleton Summoning";
            private const string effectDescription = "Skeleton Sumonned!";
            private const BehaviourStartTimes startTime = BehaviourStartTimes.End; // On impact??/
            //TODO LOAD ICON
            private int timesTicked = 0;
            private const int MAX_TURNOS = 3;
            private const int BASE_DAMAGE = 30; 
            private const float INTELLIGENCE_SCALE_RATE = 0.1f; //Scale rate
            private bool haveAttackedThisTurn = false;

            public SkeletonSummoning() : base(new BasicInformationObject(effectName, effectDescription), startTime, MAX_TURNOS)
            {

            }

            public override void ActivateBehaviour(Character enemyCharacter, Ability ability)
            {
                //On development, create a npc, have a turn have 4 spells etc
            }


        }
    }
}