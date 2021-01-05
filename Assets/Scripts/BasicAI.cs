using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums.BattleStates;

public class BasicAI{
    //This is gonna be the npc that will control the AI
    Character mob = GameController.enemy;
    //This is the actual enemy of the mob/AI/NPC
    Class player = GameController.player;
    //CurrentState
    EBattleStates currentState = GameController.currentState;
   

    public void Play()
    {
        
        mob.Abilities[Random.Range(0,4)].UseAbility();

    }

}
