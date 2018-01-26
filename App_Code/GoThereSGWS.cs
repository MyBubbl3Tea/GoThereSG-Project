using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Xml;

/// <summary>
/// Summary description for GoThereSGWS
/// </summary>
[WebService(Namespace = "http://www.tpifc.com/TPWebServices")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class GoThereSGWS : System.Web.Services.WebService
{

    public GoThereSGWS()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod(Description = "Get all MRT Stations' Code and Name")]
    public XmlDocument getMRTStations()
    {
        XmlDocument xmlDoc = new XmlDocument();
        string xmlPath = @"C:\Users\Shawn Lee\source\repos\GoThereSG\GoThereSG";
        xmlDoc.Load(xmlPath + "/MRT.xml");
        return xmlDoc;
    }

    [WebMethod(Description = "Get Emergency numbers and Taxi hotlines in Singapore")]
    public string[] getHotlines(string Category)
    {
        string[] hotline;
        XmlDocument xmlDoc = new XmlDocument();
        string xmlPath = @"C:\Users\Shawn Lee\source\repos\GoThereSG\GoThereSG";
        xmlDoc.Load(xmlPath + "/Hotline.xml");
        XmlNodeList xnlType = null;
        XmlNodeList xnlNumber = null;
        if (Category == "")
        {
            xnlType = xmlDoc.SelectNodes("//Hotline/Type");
            xnlNumber = xmlDoc.SelectNodes("//Hotline/Number");
        }
        else if (Category == "Emergency")
        {
            xnlType = xmlDoc.SelectNodes("//Hotline[@Category='" + Category + "']/Type");
            xnlNumber = xmlDoc.SelectNodes("//Hotline[@Category='" + Category + "']/Number");
        }
        else if (Category == "Taxi")
        {
            xnlType = xmlDoc.SelectNodes("//Hotline[@Category='Taxi']/Type");
            xnlNumber = xmlDoc.SelectNodes("//Hotline[@Category='Taxi']/Number");
        }
        int size = xnlNumber.Count;
        hotline = new string[size];
        for(int i = 0; i<xnlNumber.Count; i++)
        {
            XmlNode xnType = xnlType.Item(i), xnNumber = xnlNumber.Item(i);
            hotline[i] = xnType.InnerText + "     " + xnNumber.InnerText;
        }
        return hotline;
    }



}
