using Nova;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bardent
{
    public class IngameUI : MonoBehaviour
    {
        public static IngameUI Instance { get; private set; }
        public ItemView itemview, invenItemView;
        public UIBlock2D pauseMenu, blur, blur2;
        public ItemView inventoryView;
        public UIBlock inventory;
        public List<UIBlock2D> Allothermenus;
        private bool isPaused;
        // Start is called before the first frame update
        private void Start()
        {
            Instance = this;
        }

        void Update()
        {
            if (Time.timeScale == 0)
            { isPaused = true; }
            else
            { isPaused = false; }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (!isPaused)
                {
                    GameManager.instance.Pause();
                    pauseMenu.gameObject.SetActive(true);
                    blur.gameObject.SetActive(true);
                    itemview.gameObject.SetActive(false);
                    blur2.gameObject.SetActive(false);
                    //play pause sound
                    AudioManager.Instance.PlaySound2D("InvenOpen");
                }
                else
                {
                    ResumeGame();
                }
            }
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                if (!isPaused)
                {
                    GameManager.instance.Pause();
                    inventory.gameObject.SetActive(true);
                    blur.gameObject.SetActive(true);
                    itemview.gameObject.SetActive(false);
                    blur2.gameObject.SetActive(false);

                    //play inventory sound
                    AudioManager.Instance.PlaySound2D("InvenOpen");
                }
                else
                {
                    ResumeGame();

                }
            }    
        }

        public void ResumeGame()
        {
            GameManager.instance.Resume();
            pauseMenu.gameObject.SetActive(false);
            inventory.gameObject.SetActive(false);
            blur.gameObject.SetActive(false);
            //set active allothermenus
            foreach (UIBlock2D menu in Allothermenus)
            {
                menu.gameObject.SetActive(false);
            }

            //play resume sound
            AudioManager.Instance.PlaySound2D("InvenClose");    
        }
        public void InitiateitemUI(ItemDescription item)
        {
            itemview.gameObject.SetActive(true);
            blur2.gameObject.SetActive(true);

            NovaItemdes itemdes = itemview.Visuals as NovaItemdes;
            itemdes.itemName.Text = item.Itemname;
            itemdes.itemDescription.Text = item.description;
            itemdes.itemImage.SetImage(item.icon);
            AudioManager.Instance.PlaySound2D("ItemPickUp");
            //disable after 1 second
            //this is where we handle exit animation/sfxs etc
            Invoke(nameof(DisableItemView), 3f);
        }

        private void DisableItemView()
        {
            itemview.gameObject.SetActive(false);
            blur2.gameObject.SetActive(false);
        }

        public void QuitToMain()
        {
            GameManager.instance.QUitToMain();
        }

        public void QuitToLibrary()
        {
            GameManager.instance.QuitToLibrary();
        }

        public void Resume()
        {
            GameManager.instance.Resume();
        }

        public void InventoryItemSlotHover(ItemDescription item)
        {
            NovaItemdes itemdes = invenItemView.Visuals as NovaItemdes;
            itemdes.itemName.Text = item.Itemname;
            itemdes.itemDescription.Text = item.description;
            itemdes.itemImage.SetImage(item.icon);

        }

        public void HoverSound()
        {
            AudioManager.Instance.PlaySound2D("ButtonHover");
        }

    }
}
