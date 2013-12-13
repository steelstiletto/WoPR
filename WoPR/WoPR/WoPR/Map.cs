﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Xml;
using Microsoft.Xna.Framework.Graphics;

/* CHANGED*/using Microsoft.Xna.Framework;/* CHANGED*/

namespace WoPR
{
    public class Map
    {
        public Dictionary<HexCoord, Tile> tiles;
        
        /* CHANGED*/private WoPR WoPR;/* CHANGED*/

        public Map(Game game)
        {
            tiles = new Dictionary<HexCoord, Tile>();

            /* CHANGED*/WoPR = (WoPR)game; //CHANGED

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
            writer.WriteEndDocument();
            writer.Flush();
            writer.Close();
        }

        private void parseMapXML(string fileName)
        {
            XmlReader reader = XmlReader.Create(fileName);
            while (reader.Read())
            {
                if (!reader.IsStartElement()) continue;
                if (reader.Name != "terrain") continue;
                string terrainType = reader.GetAttribute("type");
                Tile.TileType terrainEnum = (Tile.TileType) Enum.Parse(typeof(Tile.TileType), terrainType);
                Tile currentTile;
                reader.ReadToDescendant("coord");
                do
                {
                    reader.MoveToNextAttribute();
                    int X = Convert.ToInt32(reader.Value);
                    reader.MoveToNextAttribute();
                    int Y = Convert.ToInt32(reader.Value);
                    reader.MoveToNextAttribute();
                    int Z = Convert.ToInt32(reader.Value);
                    string ownerString = reader.GetAttribute("owner");
                    Player owner = null;
                    if (ownerString == "1") owner = WoPR.players[0];
                    if (ownerString == "2") owner = WoPR.players[1];
                    Debug.Print(X + " " + Y + " " + Z);
                    HexCoord position = new HexCoord(X, Y, Z);

                    /* CHANGED*/currentTile = new Tile(WoPR, terrainEnum, position, owner);/* CHANGED*/

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
            /*CHANGED CHANGED CHANGED CHANGED*/
            foreach (KeyValuePair<HexCoord, Tile> pair in tiles)
            {
                pair.Value.Draw(batch);
            }
            /*CHANGED CHANGED CHANGED CHANGED*/
        }
    }
}
