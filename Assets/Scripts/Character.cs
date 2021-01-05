using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System;


using System.Xml;
using System.Xml.Serialization;
using System.IO;

using Enums.Stats;
using Enums.AbilityEffects;
using LevelingSystem.CharacterExperience;


using AbilitySystem.Abilities;
    public class Character : MonoBehaviour
    {
        string _characterName; //Name of the character
        int _characterID; // ID number
        int _healthPoints; // HealthPoints
        int _maxHealthPoints; //MaxManaPoints;
        int _maxManaPoints; //MaxManaPoints
        int _manaPoints; //ManaPoints
        Character _currentEnemy; //CurrentEnemy
        Dictionary<EStats, Stat> _statDictionary; //Dictionary with stats and stats name
        bool _myTurn; //The character can play if is it's turn
        List<Ability> _abilities;
        EAbilityEffects _actualState = EAbilityEffects.NONE;
        Sprite _characterImage;
        Experience _experience;

        public Sprite ActualImage
    {
        get { return this._characterImage; }
        set { this._characterImage = value; }
    }

        public List<Ability> Abilities
        {
            get { return this._abilities; }
            protected set { this._abilities = value; }
        }

        public int MaxHP
        {
            get { return this._maxHealthPoints; }
            set { this._maxHealthPoints = value; }
        }

        public int MaxMana
        {
            get { return this._maxManaPoints; }
            set { this._maxManaPoints = value; }
        }

        public EAbilityEffects ActualState
        {
            get { return this._actualState; }
            set { this._actualState = value; }
        }

        public Character(string characterName, int characterID, int healthPoints, int manaPoints)
        {
            this._characterName = characterName;
            this._characterID = characterID;
            this._healthPoints = healthPoints;
            this._manaPoints = manaPoints;
            this._myTurn = false;
        }
        //Default constructor
        public Character()
        {
            this._characterID = 0;
            this._characterName = "";
            this._maxHealthPoints = 1;
            this._maxManaPoints = 1;
            this._healthPoints = 1;
            this._manaPoints = 0;
            _statDictionary = new Dictionary<EStats, Stat>();
            this._myTurn = false;
            //GetAbilitiesFromXML();
            FillStats();
        }
        public Character CurrentEnemy
        {
            get { return this._currentEnemy; }
            set { this._currentEnemy = value; }
        }

        public bool MyTurn
        {
            get { return this._myTurn; }
            set { this._myTurn = value; }
        }

        public Dictionary<EStats, Stat> Stats
        {
            get { return this._statDictionary; }
            protected set { this._statDictionary = value; }
        }
        public string CharacterName
        {
            get { return this._characterName; }
            set { this._characterName = value; }
        }
        protected int CharacterID
        {
            get { return this._characterID; }
            set { this._characterID = value; }
        }
        public int HealthPoints
        {
            get { return this._healthPoints; }
            set { this._healthPoints = value; }
        }

        public int ManaPoints
        {
            get { return this._manaPoints; }
            set { this._manaPoints = value; }
        }

        //Castea la habilidad, los parametros son el origen del casteo de la habilidad, a quien va dirigida la habilidad, y que habilidad se ha usado
        public bool CastAbility(Character from, Character to, Ability abilityCasted)
        {
            bool resultado = false;





            return resultado;
        }

    public IEnumerator TakeDamage(int value)
    {
        int actualDamage = 0;
        while(actualDamage < value && IsAlive())
        {
            actualDamage++;
            this._healthPoints--;
            SimpleHealthBar.UpdateBar(this.name + "_hp", this.HealthPoints, this.MaxHP);
            yield return new WaitForEndOfFrame();

        }
        yield return null;
    }

    public IEnumerator ConsumeMana(int value)
    {
        int actualMana = 0;
        while (actualMana < value && this._manaPoints > 0)
        {
            actualMana++;
            this._manaPoints--;
            SimpleHealthBar.UpdateBar(this.name + "_hp", this.HealthPoints, this.MaxHP);
            yield return new WaitForEndOfFrame();

        }
        yield return null;
    }

    public IEnumerator HealDamage(int value)
    {
        int actualHealing = 0;
        while (actualHealing < value)
        {
            actualHealing++;
            this._healthPoints++;
            SimpleHealthBar.UpdateBar(this.name + "_hp", this.HealthPoints, this.MaxHP);
            yield return new WaitForEndOfFrame();
        }
        yield return null;
    }


        //Carga las habilidades de un character desde un XML
        /*
        public bool GetAbilitiesFromXML()
        { 
            bool resultado = false;
            XmlSerializer serializer = new XmlSerializer(typeof(List<Ability>));
            FileStream stream = new FileStream(Application.dataPath + "/StreamingAssets/XMLDatabases/Abilities/"+this.CharacterName+"_abilities.xml",FileMode.OpenOrCreate);
            this.Abilities = serializer.Deserialize(stream) as List<Ability>;
            if(this.Abilities != null)
            {
                return true;
            }

            return resultado;

    }
        */

        public virtual Character GetCharacter()//Devuelve el personaje actual
        {
            return this;
        }

        private void FillStats() //Rellena el diccionario de stats con todas las stats existentes en el enumerado y las inicializa a 0
        {
            foreach (EStats statType in Enum.GetValues(typeof(EStats)))
            {
                if (statType != EStats.NONE)
                {
                    Stat stat = new Stat(statType, 0);
                    this._statDictionary.Add(statType, stat);
                }
            }

        }

        public Sprite ChargeImageFromResource(string nombreImg)
    {
        try
        {
          return Resources.Load<Sprite>("Classes-Images/" + nombreImg);
        } catch(FileNotFoundException e)
        {
            Debug.Log(e.Message);
            Debug.LogError("Image Asset called " + nombreImg + " not found, null returned");
            return null;
        }
    }
  

        public bool IsAlive()
        {
            if (this.HealthPoints <= 0)
            {
                return false;
            }
            else return true;
        }

    }




