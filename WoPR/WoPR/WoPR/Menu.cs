using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WoPR
{
    class Menu
    {
        private int selected;
        private List<string> menuItems;

        public Menu()
        {
            selected = -1;
            menuItems = new List<string>();
        }

        private bool changeSelectionUpdate(button d)
        {
            if (d == button.up)
            {
                if (selected > 0)
                {
                    selected--;
                    return true;
                }
                else return false;
            }
            if (d == button.down)
            {
                if (selected < menuItems.Count - 2)
                {
                    selected++;
                    return true;
                }
                else return false;
            }
            return false;


        }

        private bool changeSelectionNew(button d)
        {
            if (d == button.up)
            {
                selected = menuItems.Count - 1;
                return true;
            }
            if (d == button.down)
            {
                selected = 0;
                return true;
            }
            return false;
        }

        public bool changeSelection(button d)
        {
            if (selected != -1) return changeSelectionUpdate(d);
            return changeSelectionNew(d);
        }

        public KeyValuePair<int, string> getSelected()
        {
            return new KeyValuePair<int, string>(selected, menuItems[selected]);
        }

        public bool removeItem(string s)
        {
            return menuItems.Remove(s);
        }

        public bool removeItem(int index)
        {
            if (index < 0 || index >= menuItems.Count)
            {
                return false;
            }
            menuItems.RemoveAt(index);
            return true;
        }
    }
}
