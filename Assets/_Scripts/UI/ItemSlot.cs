using Nova;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bardent
{
    public class ItemSlot : MonoBehaviour
    {
        public ItemView itemDescription;
        public ItemView InventoryView;

        public string itemname;
        public string itemdescription;
        public Sprite itemimage;
        private void OnEnable()
        {
            InventoryView = IngameUI.Instance.inventoryView;

            itemDescription = GetComponent<ItemView>();
            NovaItemdes itemdes = itemDescription.Visuals as NovaItemdes;

            itemimage = itemdes.itemImage.Sprite;
            itemname = itemdes.itemName.Text;
            itemdescription = itemdes.itemDescription.Text;
        }

        public void HoverSlot()
        {
            NovaItemdes itemdes = InventoryView.Visuals as NovaItemdes;

            itemdes.itemName.Text = itemname;
            itemdes.itemDescription.Text = itemdescription;
            itemdes.itemImage.SetImage(itemimage);
        }
    }
}
