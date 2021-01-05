using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AbilitySystem.AbilityBehavior;
using AbilitySystem.Abilities;
public class HealingPerTurn : AbilityBehaviors {

    private const string effectName = "Healing per turn";
    private const string effectDescription = "You're being healed!";
    private const BehaviourStartTimes startTime = BehaviourStartTimes.End; // On impact??/
                                                                           //TODO LOAD ICON
    private int timesTicked = 0;
    private const int MAX_TURNOS = 2;
    private const float PERCENT_HEALING = 0.05f;
    private bool haveTickedThisTurn = false;

    public HealingPerTurn(int max_turnos) : base(new BasicInformationObject(effectName, effectDescription), startTime, MAX_TURNOS)
    {
        this.MaxTurnos = max_turnos;
        this.timesTicked = 0;
        this.haveTickedThisTurn = false;
    }


    public override void ActivateBehaviour(Character sourceCharacter, Ability ability)
    {
        Debug.Log("Behaviour Activated! ");
        this.Activated = true;
        sourceCharacter.StartCoroutine(HOT(sourceCharacter, ability));
    }


    private IEnumerator HOT(Character sourceCharacter, Ability ability)
    {
        Debug.Log("DOT function called");
        Debug.Log("Quedan: " + this.TurnsLast.ToString() + " turnos para expirar");
        while (Activated)
        {
            haveTickedThisTurn = false;
            yield return new WaitWhile(() => sourceCharacter.CurrentEnemy.MyTurn);
            if (!haveTickedThisTurn && TurnsLast > 0)
            {
                timesTicked++;  //Tickeado
                TurnsLast--;    //Turnos --
                Debug.Log("Ticked!" + timesTicked + " times");
                sourceCharacter.StartCoroutine(sourceCharacter.HealDamage((int)(sourceCharacter.MaxHP * PERCENT_HEALING)));
                haveTickedThisTurn = true;
            }
            else if (TurnsLast == 0)
                this.Activated = false;
        }
        Debug.Log("Coroutine ended");
        yield return null;
    }

}
