using System;
using System.IO;
using System.Text;

public class AdminPanelHtmlGenerator
{
    public static void GenerateHtml(AdminPanel adminPanel, string htmlFilePath)
    {
        if (adminPanel == null)
        {
            throw new ArgumentNullException(nameof(adminPanel));
        }

        // Generate the HTML content
        string htmlContent = GenerateHtmlContent(adminPanel);

        // Write the HTML content to the output file
        File.WriteAllText(htmlFilePath, htmlContent);
    }

    private static string GenerateHtmlContent(AdminPanel adminPanel)
    {
        StringBuilder html = new StringBuilder();
        html.AppendLine("<!DOCTYPE html>");
        html.AppendLine("<html>");
        html.AppendLine("<head>");
        html.AppendLine("<title>Admin Panel</title>");

        // Include a custom CSS file for styling
        html.AppendLine("<link rel='stylesheet' type='text/css' href='styles.css'>");

        html.AppendLine("</head>");
        html.AppendLine("<body>");

        // Generate the sidebar
        GenerateSidebar(html, adminPanel);

        html.AppendLine("</body>");
        html.AppendLine("</html>");

        return html.ToString();
    }

    private static void GenerateSidebar(StringBuilder html, AdminPanel adminPanel)
    {
        html.AppendLine("<nav class='sidebar'>");
        html.AppendLine("<ul>");

        // Generate sidebar menu based on AdminPanel data
        foreach (var menu in adminPanel.SidebarMenu.Menus)
        {
            html.AppendLine("<li class='menu-item'>");
            html.AppendLine($"<a href='#'>{menu.Name}</a>");
            html.AppendLine("</li>");

            html.AppendLine("<ul class='submenu'>");

            foreach (var submenu in menu.Submenus)
            {
                html.AppendLine("<li class='submenu-item'>");
                html.AppendLine($"<a href='{submenu.Link}'>{submenu.Name}</a>");
                html.AppendLine("</li>");
            }

            html.AppendLine("</ul>");
        }

        html.AppendLine("</ul>");
        html.AppendLine("</nav>");
    }
}
