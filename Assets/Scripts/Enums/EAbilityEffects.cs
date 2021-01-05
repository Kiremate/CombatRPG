using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enums
{   
    namespace AbilityEffects
    {
        public enum EAbilityEffects
        {
            NONE,
            POISONED, //DEALS DAMAGE PER TURN
            BURNED, // DEALS DAMAGE PER TURN
            CURSED, // THE CURSED EFFECT DON'T LET YOU USE A RANDOM ABILITY
            FROST, //THE FROST EFFECT DON'T LET YOU PLAY TILL THE END OF THE EFFECT
            BLEEDING, // DEALS DAMAGE PER TURN
            KNOCKED //THE KNOCKED EFFECT DON'T LET YOU PLAY TILL THE NEXT TURN
        }
    }
  
}

