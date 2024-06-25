using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Nova;

namespace Bardent
{
    public class Inventory : MonoBehaviour
    {
        public List<ItemDescription> itemDescriptions;
        public List<ItemSlot> itemSlots;
        public GridView itemgrid = null;
        public int maxItems = 30;

        private void Start()
        {
            itemDescriptions = InventoryManager.instance.itemDescriptions;

            itemgrid.AddDataBinder<ItemDescription, NovaItemdes>(BindItem);

            itemgrid.SetSliceProvider(ProvideSlice);

            itemgrid.SetDataSource(itemDescriptions);
        }
        private void OnEnable()
        {
            itemDescriptions = InventoryManager.instance.itemDescriptions;

            itemgrid.AddDataBinder<ItemDescription, NovaItemdes>(BindItem);

            itemgrid.SetSliceProvider(ProvideSlice);

            itemgrid.SetDataSource(itemDescriptions);
        }

        private void ProvideSlice(int sliceIndex, GridView gridView, ref GridSlice2D gridSlice)
        {
            gridSlice.Layout.AutoSize.Y = AutoSize.Shrink;
            gridSlice.Layout.AutoSize.X = AutoSize.Expand;
            gridSlice.AutoLayout.AutoSpace = true;

        }


        private void BindItem(Data.OnBind<ItemDescription> evt, NovaItemdes visuals, int index) => visuals.Bind(evt.UserData);
    }
}
