using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bardent
{
    public class InventoryManager : MonoBehaviour
    {
        public static InventoryManager instance;
        public List<ItemDescription> itemDescriptions = new List<ItemDescription>();

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }

            for (int i = 0; i < 30; i++)
            {
                itemDescriptions.Add(null);
            }
        }

        public void AddItem(ItemDescription item)
        {
            for (int i = 0; i < itemDescriptions.Count; i++)
            {
                if (itemDescriptions[i] == null)
                {
                    itemDescriptions[i] = item;
                    break; // Stop after adding the item
                }
            }
        }

        public void ClearItems()
        {
            itemDescriptions.Clear();
            for (int i = 0; i < 30; i++)
            {
                itemDescriptions.Add(null);
            }
        }
    }
}
