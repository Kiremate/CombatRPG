using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AbilitySystem.AbilityBehavior;
using AbilitySystem.Abilities;

public class FreezeEnemy : AbilityBehaviors {

    private const string effectName = "Freeze enemy";
    private const string effectDescription = "You're frozen!";
    private const BehaviourStartTimes startTime = BehaviourStartTimes.End; // On impact??/
                                                                           //TODO LOAD ICON
    private const int MAX_TURNOS = 1;

    public FreezeEnemy(int max_turnos) : base(new BasicInformationObject(effectName, effectDescription), startTime, MAX_TURNOS)
    {
        this.MaxTurnos = max_turnos;
    }

    public override void ActivateBehaviour(Character enemyCharacter, Ability ability)
    {
        
        Debug.Log("Behaviour Activated! ");
        this.Activated = true;
    }

    private IEnumerator UNABLE(Character enemyCharacter, Ability ability)
    {
        Debug.Log("UNABLE function called");
        while (Activated)
        {
           yield return new WaitWhile(() => enemyCharacter.MyTurn);
           enemyCharacter.MyTurn = false; //Salto turno al enemigo
           Debug.Log("Enemy Turn skipped");
           this.Activated = false;
        }

        yield return null;
    }
}
