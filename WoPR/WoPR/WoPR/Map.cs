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

        public bool move(HexCoord a, HexCoord b)
        {
            Tile tempA;
            Tile tempB;
            if (tiles.TryGetValue(a, out tempA))
            {
                if(tiles.TryGetValue(b, out tempB))
                {
                    if(tempA.getUnit() != null)
                    {
                        if (tempB.getUnit() == null)
                        {
                            tempB.unit = tempA.unit;
                            tempA.unit = null;
                        }
                    }
                }
            }
            return false;
        }

        public List<Tile> getLegalMoves(Tile t)
        {
            List<Tile> temp = new List<Tile>();
            Unit u = t.getUnit();
            int i = 0;
            if (u != null)
            {
                i = u.getMoveSpeed();
            }
            
            Unit.moveType m = u.getMoveType();
            temp = getLegalMoves(temp, m, t, i);
            return temp;
        }

        public List<Tile> getLegalMoves(List<Tile> list, Unit.moveType m, Tile t, int movePoints)
        {
            List<HexCoord> adjacent = t.getPosition().neighbors();
            Tile temp;
            int[] moveCosts;

            foreach (HexCoord h in adjacent)
            {
                tiles.TryGetValue(h, out temp);
                if (temp != null)
                {
                    moveCosts = temp.getMoveCosts();
                }
                else
                {
                    moveCosts = new int[] { -1, -1, -1 };
                }
                switch(m)
                {
                    case Unit.moveType.foot:
                        if (moveCosts[0] != -1 && (movePoints - moveCosts[0]) >= 0)
                        {
                            if (!list.Contains(temp))
                            {
                                list.Add(temp);
                            }
                            getLegalMoves(list, m, temp, movePoints - moveCosts[0]);
                        }
                        break;
                    case Unit.moveType.tread:
                        if (moveCosts[1] != -1 && (movePoints - moveCosts[1]) >= 0)
                        {
                            if (!list.Contains(temp))
                            {
                                list.Add(temp);
                            }
                            getLegalMoves(list, m, temp, movePoints - moveCosts[1]);
                        }
                        break;
                    case Unit.moveType.air:
                        if (moveCosts[2] != -1 && (movePoints - moveCosts[2]) >= 0)
                        {
                            if (!list.Contains(temp))
                            {
                                list.Add(temp);
                            }
                            getLegalMoves(list, m, temp, movePoints - moveCosts[2]);
                        }
                        break;
                        
                }
                
            }
            return list;
        }

        public List<Tile> getLegalAttacks(Tile t, bool isPrimaryAttack)
        {
            List<Tile> temp = new List<Tile>();
            List<HexCoord> adjacent = t.getPosition().neighbors();
            Tile current;
            Attack a;

            if (t.getUnit() != null)
            {
                if (isPrimaryAttack)
                {
                    a = t.getUnit().getPrimaryAttack();
                }
                else
                {
                    a = t.getUnit().getSecondaryAttack();
                }
            }
            else
            {
                return temp;
            }

            if (a.getRange() == 0)
            {
                foreach (HexCoord h in adjacent)
                {
                    tiles.TryGetValue(h, out current);
                    if (current != null)
                    {
                        if (current.getUnit() != null)
                        {
                            if (current.getUnit().attackable(a))
                            {
                                if(t.unit.getOwner() != current.unit.getOwner())
                                temp.Add(current);
                            }
                        }
                    }
                }
            }
            else
            {
                temp = findRangedTargets(temp, a.getRange(), t);
            }
            
            return temp;
        }

        public List<Tile> findRangedTargets(List<Tile> temp, int rangeRemaining, Tile t)
        {
            rangeRemaining--;            
            List<HexCoord> adjacent = t.getPosition().neighbors();
            if(!temp.Contains(t))
            {
                temp.Add(t);
            }
            if(rangeRemaining > 0)
            {
                foreach (HexCoord h in adjacent)
                {
                    tiles.TryGetValue(h, out t);
                    if (t != null)
                    {
                        temp = findRangedTargets(temp, rangeRemaining, t);
                    }
                }
            }
            return temp;
        }
    }
}
