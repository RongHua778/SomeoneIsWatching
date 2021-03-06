using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    public int ID { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }

    public string Icon { get; private set; }

    public List<string> AttentionStates { get; private set; }
    public Item(int id, string name,string description,string icon,List<string> attentionstates)
    {
        this.ID = id;
        this.Name = name;
        this.Description = description;
        this.Icon = icon;
        this.AttentionStates = attentionstates;
    }

    
}
