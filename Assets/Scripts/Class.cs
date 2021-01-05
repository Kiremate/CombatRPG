using System.Collections.Generic;
using UnityEngine.Events;
using Enums.Classes;
using AbilitySystem.Abilities;
    //Usually the class character is designed for players not AI npc's
    public abstract class Class : Character
    {
        Ability _lastClicked; //Last Ability Clicked by the player
        Ability[] _asignedAbilities = new Ability[4]; //Actual 4 Abilities asigned to use in combat
        EClasses _className; //Name of the class which is playing the player
        int _classId; //ID of the class itself

        public Class()
        {

        }

        public Ability[] AsignedAbilities
        {
            get { return this._asignedAbilities; }
            set { this._asignedAbilities = value; }
        }

        public Class(EClasses className, int classId)
        {
            this._className = className;
            this._classId = classId;
        }
        public Ability LastClicked
        {
            get { return this._lastClicked; }
            set { this._lastClicked = value; }
        }

        protected abstract List<Ability> getClassAbilities();


        public EClasses ClassName
        {
            get { return this._className; }
            protected set { this._className = value; }
        }
        public int ClassId
        {
            get { return this._classId; }
            protected set { this._classId = value; }
        }

        public int ExecuteAsignedAbility(int numAbility)//Execute the ability asigned on the AbilityList
        {
            this._lastClicked = this.Abilities[numAbility];
            this.Abilities[numAbility].UseAbility();
            return numAbility;
        }

        //In a future try to automatice this, idk how, but this is bullshit...
        public void ExecuteFirstAbility()
        {
            this.Abilities[0].UseAbility();
        }
        public void ExecuteSecondAbility()
        {
            this.Abilities[1].UseAbility();
        }
        public void ExecuteThirdAbility()
        {
            this.Abilities[2].UseAbility();
        }
        public void ExecuteFourthAbility()
        {
            this.Abilities[3].UseAbility();
        }

    }
