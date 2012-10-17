using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
namespace HUD
{
    /// <summary>
    /// Holds the details of the HUD objects.
    /// </summary>
    internal class HudObject
    {
        /// <summary>
        /// Constant defined to symbolize Text HudObject
        /// </summary>
        internal static String TEXT = "Text";
        /// <summary>
        /// Constant defined to symbolize Image HudObject
        /// </summary>
        internal static String IMAGE = "Image";

        //Necessary
        internal int id { get; set; }
        internal String type { get; set; }
        //Image
        internal String imageLocation { get; set; }
        internal Texture2D image { get; set; }
        //Text
        internal String fontLocation { get; set; }
        internal String text { get; set; }
        internal SpriteFont font { get; set; }
        internal Color color { get; set; }
        //optional
        internal int XOffset { get; set; }
        internal int YOffset { get; set; }
        internal Boolean isVisible { get; set; }
        internal int isBeside { get; set; }

        //calculated
        internal int X { get; set; }
        internal int Y { get; set; }
        internal int Width { get; set; }
        internal int Height { get; set; }

        /// <summary>
        /// Reads the objects from the settings XML and creates HUD objects.
        /// </summary>
        /// <param name="node">XML node from file. Must be of "HudObject" type</param>
        /// <param name="content">ContentManager of the host Game</param>
        internal HudObject(System.Xml.XmlNode node, ContentManager content)
        {
            if (node.Name == "HudObject")
            {
                //Necessary
                if (node.Attributes["id"] == null) { throw new Exception("id for Hud Object not found."); }
                else
                {
                    if (node.Attributes["id"].Value == "0") { throw new Exception("illegal id 0 found."); }
                    else { this.id = int.Parse(node.Attributes["id"].Value); }
                }
                //optional
                if (node.Attributes["XOffset"] == null) { this.XOffset = 0; }
                else { this.XOffset = int.Parse(node.Attributes["XOffset"].Value); }
                //optional
                if (node.Attributes["YOffset"] == null) { this.YOffset = 0; }
                else { this.YOffset = int.Parse(node.Attributes["YOffset"].Value); }
                //optional
                if (node.Attributes["isBeside"] == null) { this.isBeside = 0; }
                else { this.isBeside = int.Parse(node.Attributes["isBeside"].Value); }
                //Necessary
                if (node.Attributes["type"] == null) { throw new Exception("type for Hud Object not found."); }
                this.type = node.Attributes["type"].Value;
                try
                {
                    //Necessary
                    if (type == TEXT)
                    {
                        this.text = node.Attributes["text"].Value;
                        this.fontLocation = node.Attributes["fontLocation"].Value;
                        this.font = content.Load<SpriteFont>(fontLocation);
                        this.Width = (int)this.font.MeasureString(this.text).X;
                        this.Height = (int)this.font.MeasureString(this.text).Y;
                        this.color = Color.White;

                        SetTextColor(node);
                    }
                    else
                    {
                        this.imageLocation = node.Attributes["imageLocation"].Value;
                        this.image = content.Load<Texture2D>(imageLocation);
                        this.Width = this.image.Width;
                        this.Height = this.image.Height;
                    }

                    //optional
                    if (node.Attributes["isVisible"] == null) { this.isVisible = true; }
                    else { this.isVisible = Boolean.Parse(node.Attributes["isVisible"].Value); }
                }
                catch (Exception ex)
                {
                    throw (ex);
                }
            }
        }

        private void SetTextColor(System.Xml.XmlNode node)
        {
            try
            {
                bool overrideDefault = false;
                int red = 0, green = 0, blue = 0;
                if (node.Attributes["red"] != null) { red = int.Parse(node.Attributes["red"].Value); overrideDefault = true; }
                if (node.Attributes["green"] != null) { green = int.Parse(node.Attributes["green"].Value); overrideDefault = true; }
                if (node.Attributes["blue"] != null) { blue = int.Parse(node.Attributes["blue"].Value); overrideDefault = true; }

                if (overrideDefault)
                    this.color = new Color(red, green, blue);
                else
                    this.color = Hud.DefaultTextColor;
            }
            catch
            {
                this.color = Hud.DefaultTextColor;
            }
        }
    }
}