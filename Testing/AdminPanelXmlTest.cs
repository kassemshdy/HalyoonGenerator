


namespace Test
{
    [TestClass]
    public class AdminPanelXmlTest
    {

        [TestMethod]
        public void CAN_READ_XML_AND_PARSE()
        {
            string xmlString = File.ReadAllText("xml/admin-panel.xml");

            AdminPanel adminPanel = XmlParser.ParseXml(xmlString);

            Assert.IsNotNull(adminPanel);

            Assert.IsTrue(adminPanel.SidebarMenu.Menus.Count > 0);
        }
    }
}