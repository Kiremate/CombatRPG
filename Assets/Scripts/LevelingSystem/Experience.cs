using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LevelingSystem
{
    namespace CharacterExperience
    {
        public abstract class Experience
        {
            //Actual level of the character
            private int _actualLevel;
            //Max level reachable for the character
            private const int MAX_LEVEL = 20;
            //Actual experience
            private int _actualExperience;
            //Experience neccesary for the next level
            private int _experienceRequiredForNextLevel;
            //If the actual experience 

            public int ActualLevel
            {
                get { return this._actualLevel; }
                set { this._actualLevel = value; }
            }

            public int ActualExperience
            {
                get { return this._actualExperience; }
                set { this._actualExperience = value; }
            }

            public int ExperienceNextLevel
            {
                get { return this._experienceRequiredForNextLevel; }
                set { this._experienceRequiredForNextLevel = value; }
            }

            public void IncrementLevel()
            {
                if (this._actualLevel < 20)
                {
                    this._actualLevel++;
                }
                else Debug.LogWarning("Se esta intentando superar el nivel maximo que es: " + MAX_LEVEL);
            }

        }

    }

}
