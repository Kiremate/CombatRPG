using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums.Classes;
using Enums.Stats;
public class Stat{

    private EStats eStats; //PRESCINDIBLE?
    private int value;

    public EStats EStats
    {
        get { return this.eStats; }
        set { this.eStats = value; }
    }

    public int Value
    {
        get { return this.value; }
        set { this.value = value; }
    }

    public Stat(EStats eStats, int value)
    {
        this.eStats = eStats;
        this.value = value;
    }
   
    public Stat()
    {
        this.eStats = EStats.NONE;
        this.value = 0;
    }

}
