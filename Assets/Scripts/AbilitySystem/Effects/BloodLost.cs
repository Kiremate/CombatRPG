using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AbilitySystem.AbilityBehavior;
using AbilitySystem.Abilities;

public class BloodLost : AbilityBehaviors {
    private const string effectName = "Blood Lost";
    private const string effectDescription = "You're bleeding!";
    private const BehaviourStartTimes startTime = BehaviourStartTimes.End; // On impact??/
                                                                           //TODO LOAD ICON
    private int timesTicked = 0;
    private const int MAX_TURNOS = 2;
    private const int BASE_DAMAGE = 20; 
    private bool haveTickedThisTurn = false;

    public BloodLost(int max_turnos) : base(new BasicInformationObject(effectName, effectDescription), startTime, MAX_TURNOS)
    {
        this.MaxTurnos = max_turnos;
        this.timesTicked = 0;
        this.haveTickedThisTurn = false;
    }

    public override void ActivateBehaviour(Character enemyCharacter, Ability ability)
    {
        Debug.Log("Behaviour Activated! ");
        this.Activated = true;
        enemyCharacter.StartCoroutine(BLEEDING(enemyCharacter, ability));
    }


    private IEnumerator BLEEDING(Character enemyCharacter, Ability ability)
    {
        Debug.Log("BLEEDING function called");
        Debug.Log("Quedan: " + this.TurnsLast.ToString() + " turnos para expirar");
        while (Activated)
        {
            haveTickedThisTurn = false;
            yield return new WaitWhile(() => enemyCharacter.CurrentEnemy.MyTurn);
            if (!haveTickedThisTurn && TurnsLast > 0)
            {
                timesTicked++;  //Tickeado
                TurnsLast--;    //Turnos --
                Debug.Log("Ticked!" + timesTicked + " times");
                Debug.Log(enemyCharacter.CharacterName);
                enemyCharacter.StartCoroutine(enemyCharacter.TakeDamage((int)( BASE_DAMAGE)));
                haveTickedThisTurn = true;
            }
            else if (TurnsLast == 0)
                this.Activated = false;
        }
        Debug.Log("Coroutine ended");
        yield return null;
    }
}
