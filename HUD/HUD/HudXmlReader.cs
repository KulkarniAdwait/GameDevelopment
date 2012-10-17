using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace HUD
{
    internal class HudXmlReader
    {
        internal List<Partition> GetSettings(String filePath, ContentManager content, out int HudX, out int HudY, out int HudWidth, out int HudHeight)
        {
            List<Partition> hudDetails = new List<Partition>(4);
            for (int i = 0; i < Hud.NUMER_OF_CELLS; i++)
            {
                hudDetails.Add(new Partition(content, (PartitionValue)i));
            }
            if (filePath != null && filePath != String.Empty)
            {
                try
                {
                    // open and read the XML file
                    XmlDocument reader = new XmlDocument();
                    reader.Load(filePath);

                    //from the read in - create an XmlNodeList 
                    XmlNodeList allNodes = reader.ChildNodes;

                    // iterate through the list, looking for the root tag, aka <HUDSettings>
                    foreach (XmlNode rootNode in allNodes)
                    {
                        if (rootNode.Name == "HUDSettings")
                        {
                            HudX = int.Parse(rootNode.Attributes["X"].Value);
                            HudY = int.Parse(rootNode.Attributes["Y"].Value);
                            HudWidth = int.Parse(rootNode.Attributes["Width"].Value);
                            HudHeight = int.Parse(rootNode.Attributes["Height"].Value);
                            // make another XmlNodeList - this time pulling all the childnodes nested in <HUDSettings>
                            XmlNodeList rootChildren = rootNode.ChildNodes;
                            foreach (XmlNode node in rootChildren)
                            {
                                if (node.Name == "HudObject")
                                {
                                    string partition = node.Attributes["partition"].Value;
                                    PartitionValue part = (PartitionValue)Enum.Parse(typeof(PartitionValue), partition);
                                    hudDetails[(int)part].AddItem(new HudObject(node, content));
                                }
                            }
                            return hudDetails;
                        }

                    }
                }
                catch (Exception ex)
                {
                    throw (ex);
                }
            }
            HudX = HudY = HudWidth = HudHeight = 0;
            return null;

        }
    }
}
