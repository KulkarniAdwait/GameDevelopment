using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace HUD
{
    internal class Partition
    {
        private Dictionary<int, HudObject> _hudObjects;
        private PartitionValue _partition;
        internal Partition(ContentManager game, PartitionValue cellPartition)
        {
            // TODO: Construct any child components here
            _hudObjects = new Dictionary<int, HudObject>();
            this._partition = cellPartition;
        }

        internal void Draw(SpriteBatch spriteBatch, float Scale)
        {
            spriteBatch.Begin();
            foreach (HudObject hudObj in _hudObjects.Values)
            {
                if (hudObj.isVisible)
                {
                    if (hudObj.type == HudObject.TEXT)
                    {
                        spriteBatch.DrawString(hudObj.font, hudObj.text
                            , new Vector2(hudObj.X, hudObj.Y)
                            , hudObj.color, 0.0f, Vector2.Zero, Scale, SpriteEffects.None, 0);
                        //spriteBatch.DrawString(hudObject.font, hudObject.text, new Vector2(hudObject.X, hudObject.Y), Color.Red);
                    }
                    else if (hudObj.type == HudObject.IMAGE)
                    {
                        spriteBatch.Draw(hudObj.image, new Vector2(hudObj.X, hudObj.Y)
                            , new Rectangle(0, 0, hudObj.image.Width, hudObj.image.Height)
                            , Color.White, 0.0f, Vector2.Zero, Scale, SpriteEffects.None, 0);
                        //spriteBatch.Draw(hudObject.image, new Rectangle(hudObject.X, hudObject.Y, hudObject.image.Width, hudObject.image.Height), Color.White);
                    }
                }
            }
            spriteBatch.End();
        }

        /// <summary>
        /// Adds items to the Cell
        /// </summary>
        /// <param name="item">item to add.</param>
        internal void AddItem(HudObject item)
        {
            _hudObjects.Add(item.id, item);
        }

        /// <summary>
        /// Finds the positions of all the objects relative toone another and the HUD bounds
        /// </summary>
        /// <param name="HudX">X co-ordinate of the HUD</param>
        /// <param name="HudY">Y co-ordinate of the HUD</param>
        /// <param name="HudWidth">Width of the HUD</param>
        /// <param name="HudHeight">Height of the HUD</param>
        /// <param name="Scale">Scaling factor of the HUD</param>
        internal void PlaceObjects(Rectangle Bounds, float Scale)
        {
            List<HudObject> hudObjects = this._hudObjects.Values.ToList();
            if (this._partition == PartitionValue.TopLeft)
            {
                PlaceTopLeft(Bounds, hudObjects, Scale);
            }
            else if (this._partition == PartitionValue.TopRight)
            {
                PlaceTopRight(Bounds, hudObjects, Scale);
            }
            else if (this._partition == PartitionValue.BottomLeft)
            {
                PlaceBottomLeft(Bounds, hudObjects, Scale);
            }
            else if (this._partition == PartitionValue.BottomRight)
            {
                PlaceBottomRight(Bounds, hudObjects, Scale);
            }
        }

        /// <summary>
        /// Returns if the partition contains an object with the particular Id
        /// </summary>
        /// <param name="id">id to be found</param>
        /// <returns></returns>
        internal bool ContainsObject(int id)
        {
            return (_hudObjects.ContainsKey(id));
        }

        /// <summary>
        /// Updates the text in the HudObject.
        /// </summary>
        /// <param name="id">id of the object</param>
        /// <param name="Text">text to be displayed by the object</param>
        /// <returns>true if update is successful</returns>
        internal bool UpdateHudText(int id, String text)
        {
            //is checked by the Hud class but still
            //doing a double check
            if (this.ContainsObject(id))
            {
                if (_hudObjects[id].type == HudObject.TEXT)
                {
                    _hudObjects[id].text = text;
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Updates the text in the HudObject.
        /// </summary>
        /// <param name="id">id of the object</param>
        /// <param name="Color">Color of the displayed Text</param>
        /// <returns>true if update is successful</returns>
        internal bool UpdateHudText(int id, Color Color)
        {
            //is checked by the Hud class but still
            //doing a double check
            if (this.ContainsObject(id))
            {
                if (_hudObjects[id].type == HudObject.TEXT)
                {
                    _hudObjects[id].color = Color;
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Tells if Hud object is set to be visible or not.
        /// </summary>
        /// <param name="id">id of target object</param>
        /// <returns>True if object is visible</returns>
        internal bool GetVisibility(int id)
        {
            //is checked by the Hud class but still
            //doing a double check
            if (this.ContainsObject(id))
            {
                return (_hudObjects[id].isVisible);
            }
            return false;
        }

        /// <summary>
        /// Sets the visibility of the Hud Object to the Value
        /// </summary>
        /// <param name="id">id of target object</param>
        /// <param name="Value">Visibility value to set</param>
        internal void SetVisibility(int id, bool Value)
        {
            //is checked by the Hud class but still
            //doing a double check
            if (this.ContainsObject(id))
            {
                _hudObjects[id].isVisible = Value;
            }
        }

        /// <summary>
        /// Calculates and places the objects in this partition
        /// </summary>
        /// <param name="Bounds">Bounds of the HUD</param>
        /// <param name="hudObjects">List of objects in this partition</param>
        /// <param name="Scale">Scaling factor of the objects</param>
        private void PlaceTopLeft(Rectangle Bounds, List<HudObject> hudObjects, float Scale)
        {
            foreach (HudObject hudObject in hudObjects)
            {
                //corner Object
                if (hudObject.isBeside == 0)
                {
                    hudObject.X = Bounds.X + (int)(hudObject.XOffset * Scale);
                    hudObject.Y = Bounds.Y + (int)(hudObject.YOffset * Scale);
                }
                else
                {
                    HudObject adjObject = _hudObjects[hudObject.isBeside];
                    hudObject.X = adjObject.X + (int)(adjObject.Width * Scale) + (int)(hudObject.XOffset * Scale);
                    hudObject.Y = Bounds.X + (int)(hudObject.YOffset * Scale);
                }
            }
        }
        /// <summary>
        /// Calculates and places the objects in this partition
        /// </summary>
        /// <param name="Bounds">Bounds of the HUD</param>
        /// <param name="hudObjects">List of objects in this partition</param>
        /// <param name="Scale">Scaling factor of the objects</param>
        private void PlaceTopRight(Rectangle Bounds, List<HudObject> hudObjects, float Scale)
        {
            foreach (HudObject hudObject in hudObjects)
            {
                //corner Object
                if (hudObject.isBeside == 0)
                {
                    hudObject.X = Bounds.Width - (int)(hudObject.Width * Scale) - (int)(hudObject.XOffset * Scale);
                    hudObject.Y = Bounds.Y + (int)(hudObject.YOffset * Scale);

                }
                else
                {
                    HudObject adjObject = _hudObjects[hudObject.isBeside];
                    hudObject.X = adjObject.X - (int)(hudObject.Width * Scale) - (int)(hudObject.XOffset * Scale);
                    hudObject.Y = Bounds.Y + (int)(hudObject.YOffset * Scale);
                }
            }
        }
        /// <summary>
        /// Calculates and places the objects in this partition
        /// </summary>
        /// <param name="Bounds">Bounds of the HUD</param>
        /// <param name="hudObjects">List of objects in this partition</param>
        /// <param name="Scale">Scaling factor of the objects</param>
        private void PlaceBottomLeft(Rectangle Bounds, List<HudObject> hudObjects, float Scale)
        {
            foreach (HudObject hudObject in hudObjects)
            {
                //corner Object
                if (hudObject.isBeside == 0)
                {
                    hudObject.X = Bounds.X - (int)(hudObject.XOffset * Scale);
                    hudObject.Y = Bounds.Height - (int)(hudObject.Height * Scale) - (int)(hudObject.YOffset * Scale);
                }
                else
                {
                    HudObject adjObject = _hudObjects[hudObject.isBeside];
                    hudObject.X = adjObject.X + (int)(adjObject.Width * Scale) + (int)(hudObject.XOffset * Scale);
                    hudObject.Y = Bounds.Height - (int)(hudObject.Height * Scale) - (int)(hudObject.YOffset * Scale);
                }
            }
        }
        /// <summary>
        /// Calculates and places the objects in this partition
        /// </summary>
        /// <param name="Bounds">Bounds of the HUD</param>
        /// <param name="hudObjects">List of objects in this partition</param>
        /// <param name="Scale">Scaling factor of the objects</param>
        private void PlaceBottomRight(Rectangle Bounds, List<HudObject> hudObjects, float Scale)
        {
            foreach (HudObject hudObject in hudObjects)
            {
                //corner Object
                if (hudObject.isBeside == 0)
                {
                    hudObject.X = Bounds.Width - (int)(hudObject.Width * Scale) - (int)(hudObject.XOffset * Scale);
                    hudObject.Y = Bounds.Height - (int)(hudObject.Height * Scale) - (int)(hudObject.YOffset * Scale);
                }
                else
                {
                    HudObject adjObject = _hudObjects[hudObject.isBeside];
                    hudObject.X = adjObject.X - (int)(hudObject.Width * Scale) - (int)(hudObject.XOffset * Scale);
                    hudObject.Y = Bounds.Height - (int)(hudObject.Height * Scale) - (int)(hudObject.YOffset * Scale);
                }
            }
        }

        
    }
}
