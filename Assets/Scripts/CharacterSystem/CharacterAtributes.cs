using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CharacterSystem
{
    namespace Atributes
    {
        public abstract class CharacterAtributes
        {
            private Color _hpColor;
            private Color _resColor;
            private int _actualHP;
            private int _maxHP;
            private int _actualResource; //Could be mana/energy/fury
            private int _maxActualResource;


            public Color HealthColor
            {
                get { return this._hpColor; }
                set { this._hpColor = value; }
            }

            public Color ResourceColor
            {
                get { return this._resColor; }
                set { this._resColor = value; }
            }

            public int ActualHP
            {
                get { return this._actualHP; }
                set { this._actualHP = value; }
            }

            public int MaxHP
            {
                get { return this._maxHP; }
                set { this._maxHP = value; }
            }
            public int ActualResource
            {
                get { return this._maxActualResource; }
                set { this._maxActualResource = value; }
            }
            public int MaxActualResource
            {
                get { return this._maxActualResource; }
                set { this._maxActualResource = value; }
            }

        }

    }
   
}

