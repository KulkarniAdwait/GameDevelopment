using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;


namespace HUD
{
    /// <summary>
    /// Holds and displays the various hud objects (text and images) that are specified in the settingsFile.
    /// All the objects do not have an "isBeside" attribute are placed to corner of the specified partition.
    /// All objects are placed beside other objects by specifying the partition and the "isBeside" attribute 
    /// to point to the id of the target object that you want the current object to be placed beside.
    /// Objects in the TopLeft and BottomLeft are placed from left to right.
    /// Objects in the TopRight and BottomRight are placed from right to left.
    /// 
    /// 1.Import DLL into your source project.
    /// 2.Create a Hud object.
    /// 3.Instantiate the Hud with an XML file as parameter and an optional textColor parameter.
    ///     All the text in the Hud will default to White or to the specified textColor.
    ///     Details of XML settings file are given below.
    /// 4.XML file must contain one "HudSettings" node.
    ///     a.The node must contain the attributes X, Y, Width and Height which specify 
    ///     the X co-ordintate, Y co-ordinate, Width and Height of the Hud.
    /// 5.The details of the Hud Objects must be specified in the "HudObject" nodes. One node per object.
    /// The attributes of HudObjects are
    ///     Compulsory attributes:
    ///         a."id" - Specify the id of the object. Must be an integer value and unique in the XML file.
    ///         b."partition" - Specify the partition of the HUd in which this object must reside.
    /// 	        TopLeft, TopRight, BottomLeft, BottomRight
    ///         c."type" - Specify the type of the object.
    /// 	        Image, Text
    /// 	        c.1 If type is Image then the "imageLocation" attribute must be 
    /// 	            set to location of the image in the source programs content folder.
    /// 	        c.2 If type is text then two attributs must be set
    /// 	            c.2.1 "fontLocation" attribute must be set to the location of the font
    /// 	                in the source programs content folder.
    /// 	            c.2.2 "text" attribute must be set to the text that is to be displayed.
    ///     Optional attributes:
    ///         a."isBeside" - Specify the id of the object beside which this object must reside. Default = 0 i.e. corner
    ///         b."isVisible" - Specify if the object is visible or not. Default = true
    ///         c."XOffset" - Specify the integer value by which you want the object to be offset by on the X axis
    ///             relative to the object that it is beside.
    ///         d."YOffset" - Specify the integer value by which you want the object to be offset by on the Y axis
    ///             relative to the object that it is beside.
    ///         e."red" - Spcify the red component of text color.
    ///         f."blue" - Specify the blue component of the text color.
    ///         e."green" - Specify the green component of the text color.
    /// </summary>
    public class Hud
    {
        internal const int NUMER_OF_CELLS = 4;
        internal Rectangle Bounds;
        /// <summary>
        /// Keeps track of the original Height of the HUD
        /// It is used to track the Scale of the Hud objects.
        /// </summary>
        private int _originalHeight;
        private float _scale;
        private List<Partition> _hud_partitions;
        private String _settingsFile;
        internal static Color DefaultTextColor;

        /// <summary>
        /// Creates a new HUD. The HUD has 4 partitions. HUD supports display of text and images in these partitions.
        /// Sets the default color of the text as White.
        /// </summary>
        /// <param name="settingsFile">XML file containing the settings for the HUD.</param>
        public Hud(String settingsFile)
        {
            _hud_partitions = new List<Partition>(NUMER_OF_CELLS);
            _settingsFile = settingsFile;
            //by default the dimensions in the XML file gives the original scale
            this._scale = 1.0f;
            DefaultTextColor = Color.White;
        }

        /// <summary>
        /// Creates a new HUD. The HUD has 4 partitions. HUD supports display of text and images in these partitions.
        /// Sets the default color of the text in the hud
        /// </summary>
        /// <param name="settingsFile">XML file containing the settings for the HUD.</param>
        /// <param name="textColor">Color for all the text objects in the Hud.</param>
        public Hud(String settingsFile, Color textColor)
        {
            _hud_partitions = new List<Partition>(NUMER_OF_CELLS);
            _settingsFile = settingsFile;
            //by default the dimensions in the XML file gives the original scale
            this._scale = 1.0f;
            DefaultTextColor = textColor;
        }

        /// <summary>
        /// Loads all the HUD Objects described in the XML file and places them in their respective position.
        /// </summary>
        /// <param name="content">Content manager of calling program</param>
        public void LoadContent(ContentManager content)
        {
            HudXmlReader reader = new HudXmlReader();
            int X, Y, Width, Height;
            _hud_partitions = reader.GetSettings(_settingsFile, content, out X, out Y, out Width, out Height);
            Bounds = new Rectangle(X, Y, Width, Height);
            this._originalHeight = Height;
            PlaceObjects();
        }

        /// <summary>
        /// Draws the Hud to the given spritebatch.
        /// </summary>
        /// <param name="spriteBatch">SpriteBatch to draw Hud to.</param>
        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Partition cell in _hud_partitions)
            {
                cell.Draw(spriteBatch, this._scale);
            }
        }

        /// <summary>
        /// ReSizes and scales the HUD to the given parameters.
        /// Assumes that the new dimensions maintain the same aspect ratio as the previous values.
        /// </summary>
        /// <param name="X">X co-ordinate of the new HUD</param>
        /// <param name="Y">Y co-ordinate of the new HUD</param>
        /// <param name="Width">Width of the new HUD</param>
        /// <param name="Height">Height of the new HUD</param>
        public void ReSize(int X, int Y, int Width, int Height)
        {
            this._scale = (float)Height / (float)this._originalHeight;
            Bounds = new Rectangle(X, Y, Width, Height);
            foreach (Partition cell in _hud_partitions)
            {
                cell.PlaceObjects(Bounds, this._scale);
            }
        }

        /// <summary>
        /// Updates the text in the HudObject.
        /// </summary>
        /// <param name="id">id of the object</param>
        /// <param name="Text">text to be displayed by the object</param>
        /// <returns>true if update is successful</returns>
        public bool UpdateText(int id, String Text)
        {
            try
            {
                foreach (Partition cell in _hud_partitions)
                {
                    if (cell.ContainsObject(id))
                    {
                        return (cell.UpdateHudText(id, Text));
                    }
                }
            }
            catch
            {
                return false;
            }
            return false;
        }

        /// <summary>
        /// Updates the text in the HudObject.
        /// </summary>
        /// <param name="id">id of the object</param>
        /// <param name="Color">Color of the displayed Text</param>
        /// <returns>true if update is successful</returns>
        public bool UpdateText(int id, Color Color)
        {
            try
            {
                foreach (Partition cell in _hud_partitions)
                {
                    if (cell.ContainsObject(id))
                    {
                        return (cell.UpdateHudText(id, Color));
                    }
                }
            }
            catch
            {
                return false;
            }
            return false;
        }

        /// <summary>
        /// Tells if Hud object is set to be visible or not.
        /// </summary>
        /// <param name="id">id of object</param>
        /// <returns>True if object is visible</returns>
        public bool GetVisibility(int id)
        {
            try
            {
                foreach (Partition cell in _hud_partitions)
                {
                    if (cell.ContainsObject(id))
                    {
                        return (cell.GetVisibility(id));
                    }
                }
            }
            catch
            {
                return false;
            }
            return false;
        }

        /// <summary>
        /// Sets the visibility of the Hud Object to the Value
        /// </summary>
        /// <param name="id">id of target object</param>
        /// <param name="Value">Visibility value to set</param>
        public void SetVisibility(int id, bool Value)
        {
            try
            {
                foreach (Partition cell in _hud_partitions)
                {
                    if (cell.ContainsObject(id))
                    {
                        cell.SetVisibility(id, Value);
                    }
                }
            }
            catch
            {
                return;
            }
        }

        /// <summary>
        /// Places the objects relative to each other and the bounds of the HUD.
        /// </summary>
        private void PlaceObjects()
        {
            foreach (Partition cell in _hud_partitions)
            {
                cell.PlaceObjects(Bounds, this._scale);
            }
        }

    }
}
