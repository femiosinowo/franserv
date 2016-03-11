using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SirSpeedy.CMS;
using Ektron.Cms.Instrumentation;
using System.Text;

public partial class why_we_are_different : PageBase
{

    /// <summary>
    /// page init event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Init(object sender, EventArgs e)
    {
        cbWhyWeAreDifferentSideContent.DefaultContentID = ConfigHelper.GetValueLong("WhyWeAreDifferentSideContentID");
        cbWhyWeAreDifferentSideContent.CacheInterval = ConfigHelper.GetValueLong("longCacheInterval");
        cbWhyWeAreDifferentSideContent.Fill();

        cbWhyWeAreDifferentSideQuotesContent.DefaultContentID = ConfigHelper.GetValueLong("WhyWeAreDifferentSideQuotesContentID");
        cbWhyWeAreDifferentSideQuotesContent.CacheInterval = ConfigHelper.GetValueLong("longCacheInterval");
        cbWhyWeAreDifferentSideQuotesContent.Fill();

        cbWhyWeAreDifferentOurTeamSideContentId.DefaultContentID = ConfigHelper.GetValueLong("WhyWeAreDifferentOurTeamSideContentID");
        cbWhyWeAreDifferentSideQuotesContent.CacheInterval = ConfigHelper.GetValueLong("longCacheInterval");
        cbWhyWeAreDifferentSideQuotesContent.Fill();

        cbWhyweDiffImg1.DefaultContentID = ConfigHelper.GetValueLong("WhyweAreDiffImg1CbId");
        cbWhyweDiffImg1.CacheInterval = ConfigHelper.GetValueLong("longCacheInterval");
        cbWhyweDiffImg1.Fill();

        cbWhyweDiffImg2.DefaultContentID = ConfigHelper.GetValueLong("WhyweAreDiffImg2CbId");
        cbWhyweDiffImg2.CacheInterval = ConfigHelper.GetValueLong("longCacheInterval");
        cbWhyweDiffImg2.Fill();
    }
    
    protected void Page_Load(object sender, EventArgs e)
    {
        Master.BodyClass += " why-different "; 
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

            contactinfo.Append("<ul class='location_address'>");
            contactinfo.Append("<li>" + fd.Address1 + "</li>");
            contactinfo.Append("<li>" + fd.City + ", " + fd.State + " " + fd.Zipcode + "</li>");
            contactinfo.Append("</ul>");
            litLocAddress.Text = contactinfo.ToString();

            contactinfo.Clear();
            contactinfo.Append("<ul class='location_contact'>");
            if (!string.IsNullOrEmpty(fd.PhoneNumber))
                contactinfo.Append("<li><span>P: " + fd.PhoneNumber.Replace("-", ".") + "</span></li>");

            if (!string.IsNullOrEmpty(fd.FaxNumber))
                contactinfo.Append("<li><span>F: " + fd.FaxNumber.Replace("-", ".") + "</span></li>");

            if (!string.IsNullOrEmpty(fd.Email))
                contactinfo.Append("<li><span><a href='" + fd.Email + "'>" + fd.Email + "</a></span></li>");

            contactinfo.Append("</ul>");
            litLocContact.Text = contactinfo.ToString();

            if (!string.IsNullOrEmpty(fd.HoursOfOperation))
            {
                contactinfo.Clear();
                contactinfo.Append("<ul class=\"location_hours\">");
                contactinfo.Append("<li>Store Hours:</li>");
                string[] workingHours = fd.HoursOfOperation.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                if (workingHours != null)
                {
                    foreach (string h in workingHours)
                    {
                        contactinfo.Append("<li>" + h + "</li>");
                    }
                }
                contactinfo.Append("</ul>");
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
                //itrBannerTitle.Text = nationalContent.LocalCenterBannerTitle;
                //ltrBannerSubTitle.Text = nationalContent.LocalCenterBannerSubTitle;
                //ltrContentTitle.Text = nationalContent.LocalCenterContentTitle;
                ltrContentTagLine.Text = nationalContent.LocalCenterContentTagLine;
            }
            ltrDescription.Text = data.ContentDescription;
            if (!string.IsNullOrEmpty(data.VideoLink))
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
            //ltrStatementText.Text = data.VideoStatementText;
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
                                 Phone = e.WorkPhone,
                                 Mobile = e.MobileNumber,
                                 Email = e.Email,
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