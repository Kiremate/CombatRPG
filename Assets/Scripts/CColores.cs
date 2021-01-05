using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Enums.Races;
using Enums.Classes;
public abstract class CColores{
    static Dictionary<EClasses, ColorBlock> classColorDictionary = FillClassColorDictionary();
    static Dictionary<ERaces, ColorBlock> racesColorDictionary = null; //TODO fillRaceColorDictionary

    public static Dictionary<EClasses, ColorBlock> ClassColorDictionary
    {
        get { return classColorDictionary; }
    }

    public static Dictionary<ERaces, ColorBlock> RacesColorDictionary
    {
        get { return racesColorDictionary; }
    }

    private static Dictionary<EClasses,ColorBlock> FillClassColorDictionary()
    {   
        //3 Color instantiations, normal, highlighted, pressed --> bad
        //I'm not going to create another Color to fill the pressed variable, back to normal then...
        //TODO improve this, holy sh*t, well better rewrite everything...
        //Not good, better keep colors on a XML || JSON?
        Dictionary<EClasses, ColorBlock> dictionary = new Dictionary<EClasses, ColorBlock>();
        //10 Classes
        // Necromancer color set
        ColorBlock colorBlockNecromancer = new ColorBlock();
        Color normalColorNecromancer = new Color(0.065f, 0.017f, 0.086f);
        Color highlightColorNecromancer = new Color(0.048f, 0.016f, 0.086f);
        colorBlockNecromancer.normalColor = normalColorNecromancer;
        colorBlockNecromancer.highlightedColor = highlightColorNecromancer;
        colorBlockNecromancer.pressedColor = normalColorNecromancer;
        colorBlockNecromancer.colorMultiplier = 5;
        dictionary.Add(EClasses.NECROMANCER, colorBlockNecromancer);
        // Pyromancer color set
        ColorBlock colorBlockPyromancer = new ColorBlock();
        Color normalColorPyromancer = new Color(0.224f, 0.094f, 0.002f); 
        Color highlightColorPyromancer = new Color(0.188f, 0.100f, 0.011f);
        colorBlockPyromancer.normalColor = normalColorPyromancer;
        colorBlockPyromancer.highlightedColor = highlightColorPyromancer;
        colorBlockPyromancer.pressedColor = normalColorPyromancer;
        colorBlockPyromancer.colorMultiplier = 5;
        dictionary.Add(EClasses.PYROMANCER, colorBlockPyromancer);
        // WitheredChampion color set
        ColorBlock colorBlockWitheredChampion = new ColorBlock();
        Color normalColorWitheredChampion = new Color(0.116f, 0.110f, 0.132f);
        Color highlightColorWitheredChampion = new Color(0.044f, 0.042f, 0.051f);
        colorBlockWitheredChampion.normalColor = normalColorWitheredChampion;
        colorBlockWitheredChampion.highlightedColor = highlightColorWitheredChampion;
        colorBlockWitheredChampion.pressedColor = normalColorWitheredChampion;
        colorBlockWitheredChampion.colorMultiplier = 5;
        dictionary.Add(EClasses.WITHERED_CHAMPION, colorBlockWitheredChampion);
        // Hunter color set
        ColorBlock colorBlockHunter = new ColorBlock();
        Color normalColorHunter = new Color(0.014f, 0.160f, 0.041f);
        Color highlightColorHunter = new Color(0, 0.068f, 0.012f);
        colorBlockHunter.normalColor = normalColorHunter;
        colorBlockHunter.highlightedColor = highlightColorHunter;
        colorBlockHunter.pressedColor = normalColorHunter;
        colorBlockHunter.colorMultiplier = 5;
        dictionary.Add(EClasses.HUNTER, colorBlockHunter);
        // Berserker color set
        ColorBlock colorBlockBerserker = new ColorBlock();
        Color normalColorBerserker = new Color(0.160f, 0, 0);
        Color highlightColorBerserker = new Color(0.091f, 0, 0);
        colorBlockBerserker.normalColor = normalColorBerserker;
        colorBlockBerserker.highlightedColor = highlightColorBerserker;
        colorBlockBerserker.pressedColor = normalColorBerserker;
        colorBlockBerserker.colorMultiplier = 5;
        dictionary.Add(EClasses.BERSERKER, colorBlockBerserker);
        // Shaman color set
        ColorBlock colorBlockShaman = new ColorBlock();
        Color normalColorShaman = new Color(0.035f, 0.056f, 0.086f);
        Color highlightColorShaman = new Color(0.063f, 0.117f, 0.193f);
        colorBlockShaman.normalColor = normalColorShaman;
        colorBlockShaman.highlightedColor = highlightColorShaman;
        colorBlockShaman.pressedColor = normalColorShaman;
        colorBlockShaman.colorMultiplier = 5;
        dictionary.Add(EClasses.SHAMAN, colorBlockShaman);
        // AshKnight color set
        ColorBlock colorBlockAshKnight = new ColorBlock();
        Color normalColorAshKnight = new Color(0.178f, 0.050f, 0);
        Color highlightColorAshKnight = new Color(0.109f, 0.020f, 0);
        colorBlockAshKnight.normalColor = normalColorAshKnight;
        colorBlockAshKnight.highlightedColor = highlightColorAshKnight;
        colorBlockAshKnight.pressedColor = normalColorAshKnight;
        colorBlockAshKnight.colorMultiplier = 5;
        dictionary.Add(EClasses.ASH_KNIGHT, colorBlockAshKnight);
        // Wizard color set
        ColorBlock colorBlockWizard = new ColorBlock();
        Color normalColorWizard = new Color(0, 0.255f, 0.242f);
        Color highlightColorWizard = new Color(0.001f, 0.140f, 0.133f);
        colorBlockWizard.normalColor = normalColorWizard;
        colorBlockWizard.highlightedColor = highlightColorWizard;
        colorBlockWizard.pressedColor = normalColorWizard;
        colorBlockWizard.colorMultiplier = 5;
        dictionary.Add(EClasses.WIZARD, colorBlockWizard);
        // Snooper color set
        ColorBlock colorBlockSnooper = new ColorBlock();
        Color normalColorSnooper = new Color(0.204f, 0.214f, 0.014f);
        Color highlightColorSnooper = new Color(0.188f, 0.193f, 0.034f);
        colorBlockSnooper.normalColor = normalColorSnooper;
        colorBlockSnooper.highlightedColor = highlightColorSnooper;
        colorBlockSnooper.pressedColor = normalColorSnooper;
        colorBlockSnooper.colorMultiplier = 5;
        dictionary.Add(EClasses.SNOOPER, colorBlockSnooper);
        // Paladin color set
        ColorBlock colorBlockPaladin = new ColorBlock();
        Color normalColorPaladin = new Color(0.255f, 0, 0.144f);
        Color highlightColorPaladin = new Color(0.155f, 0.003f, 0.071f);
        colorBlockPaladin.normalColor = normalColorPaladin;
        colorBlockPaladin.highlightedColor = highlightColorPaladin;
        colorBlockPaladin.pressedColor = normalColorPaladin;
        colorBlockPaladin.colorMultiplier = 5;
        dictionary.Add(EClasses.PALADIN, colorBlockPaladin);
        return dictionary;
    }

    private void fillRaceColorDictionary()
    {
        //TODO FIRST DO THE CLASS RACE THEN DO THE COLORS












    }
}
