using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace QuestPrototype
{
    class Program
    {
        static void Main(string[] args)
        {
            XDocument doc = XDocument.Load(@"K:\Capstone\Titan_game_project\Assets\XmlTest.xml");
            XElement Item = doc.Element("Item");
            Item.Add(new XElement("Student",
           new XElement("FirstName", "Bob"),
           new XElement("LastName", "Smith")));
            doc.Save(@"K:\Capstone\Titan_game_project\Assets\XmlTest.xml");
        }

        
    }


}
