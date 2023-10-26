using System;
using System.Collections.Generic;
using System.Xml;

public class AdminPanel
{
    public SidebarMenu SidebarMenu { get; set; }
    public List<Menu> Menus { get; set; }
}

public class SidebarMenu
{
    public List<Menu> Menus { get; set; }
}

public class Menu
{
    public string Name { get; set; }
    public List<Submenu> Submenus { get; set; }
}

public class Submenu
{
    public string Name { get; set; }
    public string Link { get; set; }
    public List<Field> Fields { get; set; }
    public Form Form { get; set; }
    public List<MenuItem> MenuItems { get; set; }
}

public class Field
{
    public string Name { get; set; }
    public string Label { get; set; }
}

public class Form
{
    public string Display { get; set; }
    public List<Field> Fields { get; set; }
}

public class MenuItem
{
    public string Name { get; set; }
    public string Link { get; set; }
}

public static class AdminPanelXmlParser
{
    public static AdminPanel ParseXml(string xmlString)
    {
        try
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xmlString);

            AdminPanel adminPanel = new AdminPanel
            {
                SidebarMenu = new SidebarMenu(),
                Menus = new List<Menu>()
            };

            // Parse sidebar menu
            XmlNode sidebarMenuNode = xmlDoc.SelectSingleNode("/admin-panel/menus/sidebar-menu");
            if (sidebarMenuNode != null)
            {
                adminPanel.SidebarMenu.Menus = ParseMenus(sidebarMenuNode);
            }

            // Parse other menus
            XmlNodeList menuNodes = xmlDoc.SelectNodes("/admin-panel/menus/menu");
            foreach (XmlNode menuNode in menuNodes)
            {
                adminPanel.Menus.Add(new Menu
                {
                    Name = menuNode.Attributes["name"].Value,
                    Submenus = ParseSubmenus(menuNode)
                });
            }

            return adminPanel;
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error: " + ex.Message);
            return null;
        }
    }

    private static List<Menu> ParseMenus(XmlNode menuNode)
    {
        List<Menu> menus = new List<Menu>();

        XmlNodeList menuNodes = menuNode.SelectNodes("menu");
        foreach (XmlNode node in menuNodes)
        {
            menus.Add(new Menu
            {
                Name = node.Attributes["name"].Value,
                Submenus = ParseSubmenus(node)
            });
        }

        return menus;
    }

    private static List<Submenu> ParseSubmenus(XmlNode menuNode)
    {
        List<Submenu> submenus = new List<Submenu>();

        XmlNodeList submenuNodes = menuNode.SelectNodes("submenu");
        foreach (XmlNode node in submenuNodes)
        {
            Submenu submenu = new Submenu
            {
                Name = node.Attributes["name"].Value,
                Link = node.Attributes["link"].Value,
                Fields = ParseFields(node),
                Form = ParseForm(node),
                MenuItems = ParseMenuItems(node)
            };

            submenus.Add(submenu);
        }

        return submenus;
    }

    private static List<Field> ParseFields(XmlNode submenuNode)
    {
        List<Field> fields = new List<Field>();
        XmlNode fieldsNode = submenuNode.SelectSingleNode("list/fields");

        if (fieldsNode != null)
        {
            XmlNodeList fieldNodes = fieldsNode.SelectNodes("field");
            foreach (XmlNode node in fieldNodes)
            {
                fields.Add(new Field
                {
                    Name = node.Attributes["name"].Value,
                    Label = node.Attributes["label"].Value
                });
            }
        }

        return fields;
    }

    private static Form ParseForm(XmlNode submenuNode)
    {
        XmlNode formNode = submenuNode.SelectSingleNode("form");

        if (formNode != null)
        {
            return new Form
            {
                Display = formNode.Attributes["display"].Value,
                Fields = ParseFields(formNode)
            };
        }

        return null;
    }

    private static List<MenuItem> ParseMenuItems(XmlNode submenuNode)
    {
        List<MenuItem> menuItems = new List<MenuItem>();
        XmlNodeList menuItemNodes = submenuNode.SelectNodes("menu");

        foreach (XmlNode node in menuItemNodes)
        {
            menuItems.Add(new MenuItem
            {
                Name = node.Attributes["name"].Value,
                Link = node.Attributes["link"].Value
            });
        }

        return menuItems;
    }
}
