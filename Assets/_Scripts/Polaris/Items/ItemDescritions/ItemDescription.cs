using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "ItemDescription")]
public class ItemDescription : ScriptableObject 
{
    public string Itemname;
    public Sprite icon;
    public string description;

    public List<PowerUps> powerupEffects;

}

