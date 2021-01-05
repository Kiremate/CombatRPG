using System.Collections.Generic;
using UnityEngine;
using Enums.Ability;
using AbilitySystem.AbilityBehavior;
namespace AbilitySystem
{
    namespace Abilities
    {
        /*Poner una referencia a un boton para que sea la propia habilidad la que gestione el cd
         habilitar el boton etc etc etc... preguntar por si acaso, que seria la mejor practica.*/

        public class Ability
        {
            BasicInformationObject informationObject;
            private int _id; //0
            private EAbilityType _type; //Shadow etc
            private int _coolDownTurns; //CD
            //This could be on the effect list
            private int _amount; // Could be heal or damage
            private int _manaCost; //Cost from most of the spells
            private Character _characterSource;
            private List<AbilityBehaviors> behaviors = new List<AbilityBehaviors>();
            private int levelRestriction = 0;


            //Full Constructor
            public Ability(BasicInformationObject informationObject, int id, EAbilityType type, int coolDownTurns, int amount, int manaCost, Character characterSource, List<AbilityBehaviors> behaviors)
            {
                this.informationObject = informationObject;
                _id = id;
                _type = type;
                _coolDownTurns = coolDownTurns;
                _amount = amount;
                _manaCost = manaCost;
                _characterSource = characterSource;
                this.behaviors = behaviors;
            }
            //Second Constructor
            public Ability(BasicInformationObject informationObject, int id, EAbilityType type, int coolDownTurns)
            {
                this.informationObject = informationObject;
                _id = id;
                _type = type;
                _coolDownTurns = coolDownTurns;
            }
            //Third Constructor
            public Ability(BasicInformationObject informationObject, int id, EAbilityType type, int coolDownTurns, int amount, int manaCost, Character characterSource)
            {
                this.informationObject = informationObject;
                _id = id;
                _type = type;
                _coolDownTurns = coolDownTurns;
                _amount = amount;
                _manaCost = manaCost;
                _characterSource = characterSource;
            }
            //BULLSHIT CONSTRUCTOR -- TEMPORAL -- REMOVE THIS AFTER TESTING!!!!
            public Ability(Character characterSource)
            {
                this.informationObject = new BasicInformationObject("AbilityName");
                _characterSource = characterSource;
            }

            public BasicInformationObject InformationObject
            {
                get { return this.informationObject; }
                set { this.informationObject = value; }
            }

            public Character CharacterSource
            {
                get { return this._characterSource; }
                set { this._characterSource = value; }
            }

            public List<AbilityBehaviors> AbilityEffects
            {
                get { return this.behaviors; }
                set { this.behaviors = value; }
            }

            public int Id
            {
                get { return this._id; }
                set { this._id = value; }
            }
            public EAbilityType Type
            {
                get { return this._type; }
                set { this._type = value; }
            }
            public int CoolDownTurns
            {
                get { return this._coolDownTurns; }
                set { this._coolDownTurns = value; }
            }
            public int Amount
            {
                get { return this._amount; }
                set { this._amount = value; }
            }

            public int ManaCost
            {
                get { return this._manaCost; }
                set { this._manaCost = value; }
            }
            public int LevelRestriction
            {
                get { return this.levelRestriction; }
                set { this.levelRestriction = value; }
            }
            public virtual string UseAbility()
            {
                _characterSource.MyTurn = false;
                _characterSource.CurrentEnemy.StartCoroutine(_characterSource.CurrentEnemy.TakeDamage(this._amount));
                _characterSource.StartCoroutine(_characterSource.ConsumeMana(this._manaCost));
                return this.informationObject.Name;
            }

        }

    }
}