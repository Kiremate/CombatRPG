using System.Collections.Generic;
using System;
using Enums.Ability;
using Enums.Stats;
using Enums.NecromancerAbilities;
using Enums.Classes;
using AbilitySystem.Abilities;
using AbilitySystem.Effects;
using AbilitySystem.AbilityBehavior;
public class Necromancer : Class {
     void Awake()
    {
        base.ClassId = 2;
        base.ClassName = EClasses.NECROMANCER;
        base.CharacterName = "Necromancer";
        base.MaxHP = 500;
        base.MaxMana = 300;
        base.HealthPoints = MaxHP;
        base.ManaPoints = MaxMana;
        base.Abilities = getClassAbilities();
        //Trasladar esto a la clase base Character y automatizar el pillar el nombre de los resource con el CharacterName
        //Como se puede observar
        base.ActualImage = base.ChargeImageFromResource(CharacterName);
        //TESTING TURN BASED COMBAT
        base.Stats[EStats.DEXTERITY].Value = 10; //Dex to 10 in order to test turn based gaem
                                                 //Delete this after tests
    }

    protected override List<Ability> getClassAbilities()
    {
        List<Ability> resultado = new List<Ability>();
        //Posible mejora meter un identificador enumerado de los hechizos para poder hacer un foreach e iterar en
        //vez de meter forzadamente habilidad por habilidad, agilizaria el progreso
        foreach (ENecromancerAbilities necroAbility in Enum.GetValues(typeof(ENecromancerAbilities)))
        {
            switch (necroAbility)
            {
                case ENecromancerAbilities.NONE:
                    break;
                case ENecromancerAbilities.CURSE:
                    resultado.Add(new Curse(GetCharacter()));
                    break;
                case ENecromancerAbilities.DECOMPOSE:
                    resultado.Add(new Decompose(GetCharacter()));
                    break;
                case ENecromancerAbilities.BONE_BREAKER:
                    resultado.Add(new BoneBreaker(GetCharacter()));
                    break;
                case ENecromancerAbilities.CONSUME_CORPSE:
                    resultado.Add(new ConsumeCorpse(GetCharacter()));
                    break;
                case ENecromancerAbilities.RAISE_UNDEAD:
                    resultado.Add(new RaiseUndead(GetCharacter()));
                    break;
                case ENecromancerAbilities.PARASYTE_BREATH:
                    resultado.Add(new ParasyteBreath(GetCharacter()));
                    break;
                case ENecromancerAbilities.FROZEN_TOUCH:
                    resultado.Add(new FrozenTouch(GetCharacter()));
                    break;
                case ENecromancerAbilities.CORPSE_RAIN:
                    resultado.Add(new CorpseRain(GetCharacter()));
                    break;
            }
        }
        return resultado;
    }

    //CURSE SPELL IN
    public class Curse : Ability
    {
       public Curse(Character character) : base(character)
        {
         this.InformationObject = new BasicInformationObject("Curse");
         this.Id = 1;
         this.Type = EAbilityType.SHADOW;
         this.CoolDownTurns = 0;
         this.Amount = 40;
         this.ManaCost = 20;
         this.AbilityEffects.Add(new Cursed());
        }

        public override string UseAbility()
        {
            foreach(AbilityBehaviors ab in this.AbilityEffects)
            {
                if (!ab.Activated)
                {
                    ab.ActivateBehaviour(this.CharacterSource.CurrentEnemy, this);
                } 
            }
            this.CharacterSource.StartCoroutine(this.CharacterSource.ConsumeMana(this.ManaCost));
            this.CharacterSource.MyTurn = false;
            //base para pasar de turno automaticamente
            return this.InformationObject.Name;
        }

    }
    //CURSE SPELL END

    public class Decompose : Ability
    {
        public Decompose(Character character) : base(character)
        {
            this.InformationObject = new BasicInformationObject("Decompose");
            this.Id = 2;
            this.Type = EAbilityType.ACID;
            this.Amount = 40;
            this.ManaCost = 40;
            this.CoolDownTurns = 0;
        }

        public override string UseAbility()
        {
            this.CharacterSource.StartCoroutine(this.CharacterSource.CurrentEnemy.TakeDamage(this.Amount));
            this.CharacterSource.StartCoroutine(this.CharacterSource.ConsumeMana(this.ManaCost));
            this.CharacterSource.MyTurn = false;
            //base para pasar de turno automaticamente
            return this.InformationObject.Name;
        }

    }

    //RAISE UNDEAD SPELL IN --- ON DEVELOPMENT! ---
    public class RaiseUndead : Ability
    {
        public RaiseUndead(Character character) : base(character)
        {
            this.InformationObject = new BasicInformationObject("Raise Undead");
            this.Id = 3;
            this.Type = EAbilityType.SHADOW;
            this.Amount = 0;
            this.ManaCost = 200;
            this.CoolDownTurns = 3;
        }

        public override string UseAbility()
        {
            foreach (AbilityBehaviors ab in this.AbilityEffects)
            { //Activa todos los comportamientos del hechizo
                if (!ab.Activated)
                {
                    ab.ActivateBehaviour(this.CharacterSource.CurrentEnemy, this);
                }
            }
            this.CharacterSource.StartCoroutine(this.CharacterSource.ConsumeMana(this.ManaCost));
            this.CharacterSource.MyTurn = false;
            //base para pasar de turno automaticamente
            return this.InformationObject.Name;
        }

    }
    //RAISE UNDEAD END

    //BONE BREARKER IN
    public class BoneBreaker : Ability
    {
        public BoneBreaker(Character character) : base(character)
        {
            this.InformationObject = new BasicInformationObject("BoneBreaker");
            this.Id = 4;
            this.Type = EAbilityType.PHYSICAL;
            this.CoolDownTurns = 2;
            this.Amount = 50;
            this.ManaCost = 120;
            this.AbilityEffects.Add(new BloodLost(4)); //4 turnos de sangrado
        }

        public override string UseAbility()
        {
            this.CharacterSource.StartCoroutine(this.CharacterSource.CurrentEnemy.TakeDamage(this.Amount));
            foreach (AbilityBehaviors ab in this.AbilityEffects)
            { //Activa todos los comportamientos del hechizo
                if (!ab.Activated)
                {
                    ab.ActivateBehaviour(this.CharacterSource.CurrentEnemy, this);
                }
            }
            this.CharacterSource.StartCoroutine(this.CharacterSource.ConsumeMana(this.ManaCost));
            this.CharacterSource.MyTurn = false;
            //base para pasar de turno automaticamente
            return this.InformationObject.Name;
        }
    }
    //BONE BREAKER END

    //PARASYTE BREATH IN
    public class ParasyteBreath : Ability
    {
        public ParasyteBreath(Character character): base(character)
        {
            this.InformationObject = new BasicInformationObject("ParasyteBreath");
            this.Id = 5;
            this.Type = EAbilityType.PHYSICAL;
            this.CoolDownTurns = 1;
            this.Amount = 20;
            this.ManaCost = 80;
        }
        public override string UseAbility()
        {
            this.CharacterSource.StartCoroutine(this.CharacterSource.CurrentEnemy.TakeDamage(this.Amount));
            foreach (AbilityBehaviors ab in this.AbilityEffects)
            { //Activa todos los comportamientos del hechizo
                if (!ab.Activated)
                {
                    ab.ActivateBehaviour(this.CharacterSource.CurrentEnemy, this);
                }
            }
            this.CharacterSource.StartCoroutine(this.CharacterSource.ConsumeMana(this.ManaCost));
            this.CharacterSource.MyTurn = false;
            //base para pasar de turno automaticamente
            return this.InformationObject.Name;
        }

    }
    //PARASYTE BREATH END

    //CONSUME CORPSE IN
    public class ConsumeCorpse : Ability
    {
        const float healingPercent = 0.1f;


        public ConsumeCorpse(Character character) : base(character)
        {
            this.InformationObject = new BasicInformationObject("ConsumeCorpse");
            this.Id = 6;
            this.Type = EAbilityType.PHYSICAL;
            this.CoolDownTurns = 4;
            this.Amount = 0;
            this.ManaCost = 80;
            this.AbilityEffects.Add(new HealingPerTurn(2));
        }

        public override string UseAbility()
        {
            this.CharacterSource.StartCoroutine(this.CharacterSource.HealDamage((int)(this.CharacterSource.MaxHP * healingPercent)));
            foreach (AbilityBehaviors ab in this.AbilityEffects)
            { //Activa todos los comportamientos del hechizo
                if (!ab.Activated)
                {
                    ab.ActivateBehaviour(this.CharacterSource.CurrentEnemy, this);
                }
            }
            this.CharacterSource.StartCoroutine(this.CharacterSource.ConsumeMana(this.ManaCost));
            this.CharacterSource.MyTurn = false;
            //base para pasar de turno automaticamente
            return this.InformationObject.Name;
        }
    }
    // CONSUME CORPSE END

    //FROZEN TOUCH IN
    public class FrozenTouch : Ability
    {
        //Try to reuse this
        public FrozenTouch(Character character) : base(character)
        {
            this.InformationObject = new BasicInformationObject("FrozenTouch");
            this.Id = 7;
            this.Type = EAbilityType.COLD;
            this.CoolDownTurns = 2;
            this.Amount = 20;
            this.ManaCost = 80;
            this.AbilityEffects.Add(new FreezeEnemy(1));
        }

        public override string UseAbility()
        {
            this.CharacterSource.StartCoroutine(this.CharacterSource.CurrentEnemy.TakeDamage(this.Amount));
            foreach (AbilityBehaviors ab in this.AbilityEffects)
            { //Activa todos los comportamientos del hechizo
                if (!ab.Activated)
                {
                    ab.ActivateBehaviour(this.CharacterSource.CurrentEnemy, this);
                }
            }
            this.CharacterSource.StartCoroutine(this.CharacterSource.ConsumeMana(this.ManaCost));
            this.CharacterSource.MyTurn = false;
            //base para pasar de turno automaticamente
            return this.InformationObject.Name;
        }
    }
    //FROZEN TOUCH END

    public class CorpseRain : Ability 
    {
        //Try to reuse this
        public CorpseRain(Character character) : base(character)
        {
            this.InformationObject = new BasicInformationObject("Corpse Rain!");
            this.Id = 8;
            this.Type = EAbilityType.COLD;
            this.CoolDownTurns = 4;
            this.Amount = 300;
            this.ManaCost = 50; //50 %
        }

        public override string UseAbility()
        {
            this.CharacterSource.StartCoroutine(this.CharacterSource.CurrentEnemy.TakeDamage(this.Amount));
            this.CharacterSource.StartCoroutine(this.CharacterSource.ConsumeMana((int)(this.CharacterSource.MaxMana * this.ManaCost)/100));
            this.CharacterSource.MyTurn = false;
            //base para pasar de turno automaticamente
            return this.InformationObject.Name;
        }
    }

}