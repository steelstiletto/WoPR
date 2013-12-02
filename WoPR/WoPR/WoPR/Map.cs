using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Xml;
using Microsoft.Xna.Framework.Graphics;

namespace WoPR
{
    public class Map
    {
        private Dictionary<HexCoord, Tile> tiles;

        public Map()
        {
            tiles = new Dictionary<HexCoord, Tile>();
            //writeBasicMap();
            parseMapXML("Content/MapData.xml");
        }

        private void writeBasicMap()
        {
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            settings.CloseOutput = true;

            XmlWriter writer = XmlWriter.Create("Content/MapData.xml", settings);
            writer.WriteStartDocument();
            writer.WriteComment("Test Map Data");
            writer.WriteStartElement("map");
            writer.WriteStartElement("terrain");
            writer.WriteAttributeString("type", "sand");
            writer.WriteStartElement("coord");
            writer.WriteAttributeString("x", "0");
            writer.WriteAttributeString("y", "0");
            writer.WriteAttributeString("z", "0");
            writer.WriteEndElement();
            writer.WriteStartElement("coord");
            writer.WriteAttributeString("x", "1");
            writer.WriteAttributeString("y", "-1");
            writer.WriteAttributeString("z", "0");
            writer.WriteEndElement();
            writer.WriteEndElement();
            writer.WriteStartElement("terrain");
            writer.WriteAttributeString("type", "road");
            writer.WriteStartElement("coord");
            writer.WriteAttributeString("x", "1");
            writer.WriteAttributeString("y", "2");
            writer.WriteAttributeString("z", "-3");
            writer.WriteEndElement();
            writer.WriteStartElement("coord");
            writer.WriteAttributeString("x", "-1");
            writer.WriteAttributeString("y", "1");
            writer.WriteAttributeString("z", "0");
            writer.WriteEndElement();
            writer.WriteEndElement();
            //writer.WriteStartElement("p", "person", "urn:person");
            //writer.WriteStartElement("name", "");
            //writer.WriteString("joebob");
            //writer.WriteEndElement();
            writer.WriteEndDocument();
            writer.Flush();
            writer.Close();
        }

        private void parseMapXML(string fileName)
        {
            XmlReader reader = XmlReader.Create(fileName);
            //while (reader.Read())
            //{
            //    if (reader.IsStartElement())
            //    {
            //        if (reader.IsEmptyElement)
            //            Console.WriteLine("<{0}/>", reader.Name);
            //        else
            //        {
            //            Console.Write("<{0}> ", reader.Name);
            //            reader.Read(); // Read the start tag. 
            //            if (reader.IsStartElement())  // Handle nested elements.
            //                Console.Write("\r\n<{0}>", reader.Name);
            //            //Console.WriteLine(reader.ReadString());  //Read the text content of the element.
            //        }
            //    }
            //}
            while (reader.Read())
            {
                if (!reader.IsStartElement()) continue;
                if (reader.Name != "terrain") continue;
                string terrainType = reader.GetAttribute("type");
                Tile currentTile = new Tile(terrainType);
                reader.ReadToDescendant("coord");
                do
                {
                    reader.MoveToNextAttribute();
                    int X = Convert.ToInt32(reader.Value);
                    reader.MoveToNextAttribute();
                    int Y = Convert.ToInt32(reader.Value);
                    reader.MoveToNextAttribute();
                    int Z = Convert.ToInt32(reader.Value);
                    Debug.Print(X + " " + Y + " " + Z);
                    HexCoord position = new HexCoord(X, Y, Z);
                    tiles.Add(position, currentTile);
                } while (reader.ReadToNextSibling("coord"));
            }
            foreach (KeyValuePair<HexCoord, Tile> pair in tiles)
            {
                Debug.Print(pair.Value.type + " " + pair.Key);
            }
        }

        public void draw(SpriteBatch batch)
        {
        }
    }
}
