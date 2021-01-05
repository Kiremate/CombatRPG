using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AbilitySystem.Abilities;
namespace AbilitySystem
{
namespace AbilityBehavior
    {
        public class AbilityBehaviors
        {
            public enum BehaviourStartTimes
            {
                None,
                Begin,
                Middle,
                End
            }


            private BasicInformationObject objectInfo;
            private BehaviourStartTimes startTime;
            private int maxTurnos;
            private int turnsLast;
            private bool activated = false;

            //Constructor
            public AbilityBehaviors(BasicInformationObject basicInfo, BehaviourStartTimes startTimes, int maxTurnos)
            {
                this.objectInfo = basicInfo;
                this.startTime = startTimes;
                this.maxTurnos = maxTurnos;
                this.turnsLast = this.maxTurnos;
            }

            public virtual void ActivateBehaviour(Character enemyCharacter, Ability ability)
            {
                //dosth
                Debug.LogWarning("BEHAVIOUR NOT DEVELOPED! ");
            }

            public int MaxTurnos
            {
                get { return this.maxTurnos; }
                set { this.maxTurnos = value; }
            }

            public int TurnsLast
            {
                get { return this.turnsLast; }
                set { this.turnsLast = value; }
            }

            public BasicInformationObject AbilityBehaviourInfo
            {
                get { return objectInfo; }
            }

            public bool Activated
            {
                get { return this.activated; }
                set { this.activated = value; }
            }

            public BehaviourStartTimes AbilityBehaviourStartTime
            {
                get { return startTime; }
            }
        }

    }
}
