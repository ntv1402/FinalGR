using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Nova;
using Bardent;

[System.Serializable]
public class NovaItemdes : ItemVisuals
{
    public TextBlock itemName;
    public TextBlock itemDescription;
    public UIBlock2D itemImage;

    public void Bind(ItemDescription data)
    {
        if (data == null)
        {
            itemName.Text = "Item Info";
            itemDescription.Text = "Item Description";
        }

            itemImage.SetImage(data.icon);
            itemName.Text = data.name;
            itemDescription.Text = data.description;


    }
}

