using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace WoPR
{
    class Map
    {
        private Dictionary<HexCoord, Tile> tiles;

        public Map()
        {
            tiles = new Dictionary<HexCoord, Tile>();
            doXMLStuff();
        }

        private void doXMLStuff()
        {
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            settings.CloseOutput = true;

            XmlWriter writer = XmlWriter.Create("test.xml", settings);
            writer.WriteStartDocument();
            writer.WriteComment("Test Map Data");
            writer.WriteStartElement("map");
            writer.WriteStartElement("node");
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
            writer.WriteStartElement("node");
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
        }

        private void parseMapXML(string fileName)
        {

        }
    }
}
