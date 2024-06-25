using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Nova;

namespace Bardent
{
    public class HoverToolTip : MonoBehaviour
    {
        public UIBlock tooltip;
        public TextBlock Name, Description;

        public string stuffName, stuffDescription;

        private void Start()
        {
            Name.Text = stuffName;
            Description.Text = stuffDescription;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "Player")
            {
                tooltip.gameObject.SetActive(true);
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "Player")
            {
                tooltip.gameObject.SetActive(false);
            }
        }
    }
}
