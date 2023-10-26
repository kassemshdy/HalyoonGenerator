
//Input XML file
string xmlString = File.ReadAllText("xml/admin-panel.xml");

//Output HTML file
string indexPath = "html/index.html";
string solutionDirectory = AppDomain.CurrentDomain.BaseDirectory.Replace("bin\\Debug\\net7.0\\","");
string fullIndexPath = Path.Combine(solutionDirectory, indexPath);



//Parse XML
AdminPanel adminPanel = AdminPanelXmlParser.ParseXml(xmlString);

//Generate Html
AdminPanelHtmlGenerator.GenerateHtml(adminPanel, fullIndexPath);