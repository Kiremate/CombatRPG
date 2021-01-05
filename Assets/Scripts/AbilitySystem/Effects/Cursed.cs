using AbilitySystem.AbilityBehavior;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AbilitySystem.Abilities;

namespace AbilitySystem
{
    namespace Effects
    {
        public class Cursed : AbilityBehaviors
        {
            private const string effectName = "Cursed";
            private const string effectDescription = "You've been cursed!";
            private const BehaviourStartTimes startTime = BehaviourStartTimes.End; // On impact??/
            //TODO LOAD ICON
            private int timesTicked = 0;
            private const int MAX_TURNOS = 2;
            private const float BASE_DAMAGE = 0.1f; //It's gonna be based on the enemy's max hp
            private const float INTELLIGENCE_SCALE_RATE = 0.1f; //Scale rate
            private bool haveTickedThisTurn = false;

            public Cursed() : base(new BasicInformationObject(effectName, effectDescription), startTime, MAX_TURNOS)
            {
               
            }

            public override void ActivateBehaviour(Character c, Ability ability)
            {   //Le llega el enemigo al cual se le activa el dot y la habilidad que lo aplica
                //dosth
                Debug.Log("Behaviour Activated! ");
                this.Activated = true;
                c.StartCoroutine(DOT(c,ability));
            }


            private IEnumerator DOT(Character enemyCharacter, Ability ability)
            {
                Debug.Log("DOT function called");
                Debug.Log("Quedan: " + this.TurnsLast.ToString() + " turnos para expirar");
                while (Activated)
                {
                    //@Author
                    //Debugado y sugerido por Jose Manuel Gomez Espasandín
                    haveTickedThisTurn = false;
                    yield return new WaitWhile(() => enemyCharacter.MyTurn);
                    if (!haveTickedThisTurn && TurnsLast > 0)
                    {
                        timesTicked++;  //Tickeado
                        TurnsLast--;    //Turnos --
                        Debug.Log("Ticked!" + timesTicked + " times");
                        enemyCharacter.StartCoroutine(enemyCharacter.TakeDamage((int)((enemyCharacter.HealthPoints * BASE_DAMAGE) 
                            + (enemyCharacter.CurrentEnemy.Stats[Enums.Stats.EStats.INTELLIGENCE].Value * INTELLIGENCE_SCALE_RATE))));
                        haveTickedThisTurn = true;
                    }
                    else if(TurnsLast == 0)
                        this.Activated = false;
                }
                ResetAtributes();
                Debug.Log("Coroutine ended");
                yield return null;
            }

            private void ResetAtributes()
            {
                this.TurnsLast = this.MaxTurnos;
                this.timesTicked = 0;
                this.haveTickedThisTurn = false;
            }

        }
    }
  
}

