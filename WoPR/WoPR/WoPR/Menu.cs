using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace WoPR
{
    public class Menu
    {
        private Vector2 position, size;
        private int selected;
        private List<string> menuItems;
        private float depth;

        public Menu()
        {
            initialize(Vector2.Zero, new Vector2(100, 100), 1);
        }

        public Menu(Vector2 position, Vector2 size)
        {
            initialize(position, size, 1);
        }

        public Menu(Vector2 position, Vector2 size, float depth)
        {
            initialize(position, size, depth);
        }

        private void initialize(Vector2 position, Vector2 size, float depth)
        {
            this.position = position;
            this.size = size;
            this.depth = depth;

            selected = -1;
            menuItems = new List<string>();
        }

        // Add an item to the menu at the end
        public void addItem(string title)
        {
            addItem(title, menuItems.Count);
        }

        // Add an item to the menu at the specified index
        public void addItem(string title, int index)
        {
            menuItems.Insert(index, title);
        }

        // Use a button input to change the selected menu item
        public bool changeSelection(button d)
        {
            if (selected != -1) return changeSelectionUpdate(d);
            return changeSelectionNew(d);
        }

        // If an item was selected before, the selection is updated with the direction
        private bool changeSelectionUpdate(button d)
        {
            Debug.Print("Changing Selection");
            if (d == button.up)
            {
                Debug.Print("Moving Up" + selected);
                if (selected > 0)
                {
                    selected--;
                    Debug.Print(selected.ToString());
                    return true;
                }
                else return false;
            }
            if (d == button.down)
            {
                Debug.Print("Moving Down");
                if (selected < menuItems.Count - 1)
                {
                    selected++;
                    Debug.Print(selected.ToString());
                    return true;
                }
                else return false;
            }
            return false;


        }

        // If no item was selected, selected is set to the appropriate end of the list
        private bool changeSelectionNew(button d)
        {
            Debug.Print("Setting Selection");
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

        // Return the index and the title currently selected
        public KeyValuePair<int, string> getSelected()
        {
            // Special case for if nothing has been selected to automatically select the first option
            if (selected == -1) return new KeyValuePair<int, string>(0, menuItems[0]);
            return new KeyValuePair<int, string>(selected, menuItems[selected]);
        }

        // Remove an item based on title
        public bool removeItem(string s)
        {
            return menuItems.Remove(s);
        }

        // Remove an item based on index
        public bool removeItem(int index)
        {
            if (index < 0 || index >= menuItems.Count)
            {
                return false;
            }
            menuItems.RemoveAt(index);
            return true;
        }

        // Draw the menu items
        public void draw(SpriteBatch batch, SpriteFont font)
        {
            // Initialize and declare some starting vectors
            Vector2 itemSize = new Vector2(size.X, size.Y / menuItems.Count);
            Vector2 fullSize, scale;
            Vector2 offset = Vector2.Zero;

            // Cycle through each menu item to draw
            for (int i = 0; i < menuItems.Count; i++)
            {
                // Select the color to use
                Color color;
                if(selected == i) color = Color.Black;
                else color = Color.Yellow;

                // Set the scale to render the full title in alloted space
                fullSize = font.MeasureString(menuItems[i]);
                scale = itemSize / fullSize;
                scale.X = Math.Min(scale.X, scale.Y);
                scale.Y = scale.X;

                // Draw the string
                batch.DrawString(font, menuItems[i], position + offset, color, 0, Vector2.Zero, scale, SpriteEffects.None, depth);

                // Update the offset for the next item
                offset.Y += itemSize.Y;
            }
        }
    }
}
