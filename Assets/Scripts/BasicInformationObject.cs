using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicInformationObject{

    private string _name;
    private string _description;
    private Sprite _icon;

    public BasicInformationObject(string name)
    {
        _name = name;
    }

    public BasicInformationObject(string name, string description)
    {
        _name = name;
        _description = description;
    }

    public BasicInformationObject(string name, string description, Sprite icon)
    {
        _name = name;
        _description = description;
        _icon = icon;
    }

    public string Name
    {

        get
        {
            return _name;
        }
    }

    public string Description
    {

        get
        {
            return _description;
        }
 
    }

    public Sprite Icon
    {

        get
        {
            return _icon;
        }
    }
}
