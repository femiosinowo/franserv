using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SignalGraphics.CMS;
using Ektron.Cms.Instrumentation;
using System.Text;

public partial class why_we_are_different : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            GetFranchiseDetails();
            GetWhyWeAreDiffContent();
            GetCenterEmployeesTeam();
        }
    }

    /// <summary>
    /// Gets the Franchise Data
    /// </summary>
    private void GetFranchiseDetails()
    {
        string fransid = FransDataManager.GetFranchiseId();
        var fd = FransDataManager.GetFransData(fransid);

        if (fd != null)
        {
            StringBuilder contactinfo = new StringBuilder();
            StringBuilder sbAdress = new StringBuilder();

            contactinfo.Append("<p class='location_address'>");
            contactinfo.Append(fd.Address1);

            contactinfo.Append("<br/>");
            contactinfo.Append(fd.City + "  " + fd.State + " " + fd.Zipcode);
            contactinfo.Append("</p>");
            litLocAddress.Text = contactinfo.ToString();

            contactinfo.Clear();
            contactinfo.Append("<ul>");
            if (!string.IsNullOrEmpty(fd.PhoneNumber))
                contactinfo.Append("<li class='location-icon-phone'><span>" + fd.PhoneNumber.Replace("-", ".") + "</span></li>");

            if (!string.IsNullOrEmpty(fd.FaxNumber))
                contactinfo.Append("<li class='location-icon-fax'><span>" + fd.FaxNumber.Replace("-", ".") + "</span></li>");

            if (!string.IsNullOrEmpty(fd.Email))
                contactinfo.Append("<li class='location-icon-email'><span><a href='mailto:" + fd.Email + "'>" + fd.Email + "</a></span></li>");

            contactinfo.Append("</ul>");
            litLocContact.Text = contactinfo.ToString();

            if (!string.IsNullOrEmpty(fd.HoursOfOperation))
            {
                contactinfo.Clear();
                contactinfo.Append("<p class=\"store_hours\">");
                contactinfo.Append("<span>Store Hours:</span>");
                string[] workingHours = fd.HoursOfOperation.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                if (workingHours != null)
                {
                    foreach (string h in workingHours)
                    {
                        contactinfo.Append(h + "<br/>");
                    }
                }
                contactinfo.Append("</p>");
                ltrWorkingHours.Text = contactinfo.ToString();
            }

            sbAdress.Append(fd.Address1);
            if (!string.IsNullOrEmpty(fd.Address2))
                sbAdress.Append(" " + fd.Address2);
            sbAdress.Append(" " + fd.City);
            sbAdress.Append(" " + fd.State);
            sbAdress.Append("-" + fd.Zipcode);

            string directionsLink = "https://www.google.com/maps?daddr=q={0}";
            hiddenCenterLat.Value = fd.Latitude;
            hiddenCenterLong.Value = fd.Longitude;
            string address = sbAdress.ToString().Replace(" ", "+");
            directions_lb.HRef = string.Format(directionsLink, address);
        }
    }

    private void GetWhyWeAreDiffContent()
    {
        string fransid = FransDataManager.GetFranchiseId();
        var data = FransDataManager.GetWhyWeAreDiffContent(fransid);
        if (data != null)
        {
            var nationalContent = SiteDataManager.GetNationalCompanyInfo();
            if (nationalContent != null)
            {
                ltrBannerTitle.Text = nationalContent.LocalCenterBannerTitle;
                ltrBannerSubTitle.Text = nationalContent.LocalCenterBannerSubTitle;
                ltrContentTitle.Text = nationalContent.LocalCenterContentTitle;
                ltrContentTagLine.Text = HttpUtility.HtmlDecode(nationalContent.LocalCenterContentTagLine);
            }

            ltrDescription.Text = HttpUtility.HtmlDecode(data.ContentDescription.Replace("''", "'"));
            if(!string.IsNullOrEmpty(data.VideoLink))
            {
                videoIframe.Src = data.VideoLink;
                videoIframe.Visible = true;
                videoSection.Visible = true;
                ImageSection.Visible = false;
            }
            else if (string.IsNullOrEmpty(data.VideoLink) && !string.IsNullOrEmpty(data.Image_Path))
            {
                mainImage.Src = data.Image_Path;
                videoSection.Visible = false;
                ImageSection.Visible = true;
            }
            ltrStatementText.Text = HttpUtility.HtmlDecode(data.VideoStatementText.Replace("''", "'"));
        }
    }

    private void GetCenterEmployeesTeam()
    {
        var centerEmployeeIds = FransDataManager.GetFransEmployeeIds();
        if (centerEmployeeIds != null && centerEmployeeIds.Any())
        {
            List<Employee> allEmployees = new List<Employee>();
            foreach (var id in centerEmployeeIds)
            {
                var employeeData = GetEmployeeById(id);
                if (employeeData != null)
                    allEmployees.Add(employeeData);
            }

          if(allEmployees.Any())
          {
              var dataList = from e in allEmployees
                             select new
                             {
                                 Name = e.FirstName + " " + e.LastName,
                                 Title = e.Title,
                                 Phone = string.IsNullOrEmpty(e.WorkPhone) ? "" : "<li class=\"phone\"><span>" + e.WorkPhone + "</span></li>",
                                 Mobile = string.IsNullOrEmpty(e.MobileNumber) ? "" : "<li class=\"mobile\"><span>" + e.MobileNumber + "</span></li>",
                                 Email = string.IsNullOrEmpty(e.Email) ? "" : "<li class=\"email\"><span><a href=\"mailto:" + e.Email + "\">" + e.Email + "</a></span></li>",
                                 Image = GetEmployeeImage(e)
                             };
              lvOurTeam.DataSource = dataList;
              lvOurTeam.DataBind();
          }
        }
    }

    private Employee GetEmployeeById(long employeeId)
    {
        Employee e = null;
        if (employeeId > 0)
        {
            return FransDataManager.GetEmployeeById(employeeId);
        }
        return e;
    }
    
    private string GetEmployeeImage(Employee data)
    {
        string image = "";
        if(data != null)
        {
            if (!string.IsNullOrEmpty(data.PicturePath))
                return data.PicturePath;
            if (data.Gender == "Male")
                image = "/images/photos/our-team-placeholder-male.png";
            if(data.Gender == "Female")
                image = "/images/photos/our-team-placeholder-female.png";
        }
        return image;
    }
}