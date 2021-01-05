using System.Collections.Generic;
using System;
using Enums.Ability;
using Enums.Classes;
using Enums.PyromancerAbilities;
using AbilitySystem.Abilities;
using Enums.Stats;
using UnityEngine;

    public class Pyromancer : Class
    {

        private const string SPRITE_PATH = "Resources/Classes-Images/Pyromancer.jpg";
        private const int _characterID = 2;
        Dictionary<EStats, Stat> _statDictionary; //Dictionary with stats and stats name
        List<Ability> _abilities;


        //Class const values

        void Awake()
        {
            base.ClassId = 1;
            base.ClassName = EClasses.PYROMANCER;
            base.CharacterName = "Pyromancer";
            base.MaxHP = 500;
            base.MaxMana = 300;
            base.HealthPoints = MaxHP;
            base.ManaPoints = MaxMana;
            base.Abilities = getClassAbilities();
            base.ActualImage = base.ChargeImageFromResource(CharacterName);
        }


        public Pyromancer()
        {

        }

        protected override List<Ability> getClassAbilities()
        {
            List<Ability> resultado = new List<Ability>();
            //Posible mejora meter un identificador enumerado de los hechizos para poder hacer un foreach e iterar en
            //vez de meter forzadamente habilidad por habilidad, agilizaria el progreso
            foreach (EPyromancerAbilities piroAbility in Enum.GetValues(typeof(EPyromancerAbilities)))
            {
                switch (piroAbility)
                {
                    case EPyromancerAbilities.NONE:
                        //does nothing
                        break;
                    case EPyromancerAbilities.FIRE_TOUCH:
                        resultado.Add(new FireTouch(GetCharacter()));

                        break;
                    case EPyromancerAbilities.FIREBLAST:
                        resultado.Add(new FireBlast(GetCharacter()));
                        break;
                    case EPyromancerAbilities.FENIX:
                        resultado.Add(new Fenix(GetCharacter()));
                        break;
                    case EPyromancerAbilities.PYROBLAST:
                        resultado.Add(new Pyroblast(GetCharacter()));
                        break;
                }
            }
            return resultado;
        }

        public class FireTouch : Ability
        {
            public FireTouch(Character character) : base(character)
            {
                this.InformationObject = new BasicInformationObject("FireTouch");
                this.Id = 1;
                this.Type = EAbilityType.FIRE;
                this.CoolDownTurns = 0;
                this.Amount = 20;
                this.ManaCost = 20;
            }
        }

        public class FireBlast : Ability
        {
            public FireBlast(Character character) : base(character)
            {
                this.InformationObject = new BasicInformationObject("Fireblast");
                this.Id = 2;
                this.Type = EAbilityType.FIRE;
                this.CoolDownTurns = 0;
                this.Amount = 20;
                this.ManaCost = 20;
            }
        }


        public class Fenix : Ability
        {
            public Fenix(Character character) : base(character)
            {
            this.InformationObject = new BasicInformationObject("Fenix");
            this.Id = 3;
                this.Type = EAbilityType.FIRE;
                this.CoolDownTurns = 0;
                this.Amount = 20;
                this.ManaCost = 20;
            }
        }


        public class Pyroblast : Ability
        {
            public Pyroblast(Character character) : base(character)
            {
            this.InformationObject = new BasicInformationObject("Pyroblast");
            this.Id = 4;
                this.Type = EAbilityType.FIRE;
                this.CoolDownTurns = 0;
                this.Amount = 20;
                this.ManaCost = 20;
            }
        }
    }


