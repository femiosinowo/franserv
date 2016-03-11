using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TeamLogic.CMS;
using Ektron.Cms.Instrumentation;
using System.Text;

public partial class why_we_are_different : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {            
            GetWhyWeAreDiffContent();
            GetCompanyLocationContent();
            GetOurTeamInfo();
        }
    }

    private void GetWhyWeAreDiffContent()
    {
        string fransid = FransDataManager.GetFranchiseId();
        var data = FransDataManager.GetWhyWeAreDiffContent(fransid);
        if (data != null)
        {
            //itrBannerTitle.Text = data.BannerTitle;
            //ltrBannerSubTitle.Text = data.BannerSubTitle;
            ltrContentTitle.Text = data.ContentTitle;
            ltrContentTagLine.Text = data.ContentTagLine;
            ltrDescription.Text = data.ContentDescription;
            if (!string.IsNullOrEmpty(data.VideoLink))
            {
                videoIframe.Src = data.VideoLink;
                videoIframe.Visible = true;
            }
            ltrStatementText.Text = data.VideoStatementText;
        }
    }

    private void GetCompanyLocationContent()
    {
        var fd = FransDataManager.GetFransData();
        if (fd != null)
        {
            StringBuilder contactinfo = new StringBuilder();
            contactinfo.Append("<li class=\"address\">");
            contactinfo.Append(fd.Address1);
            if (!string.IsNullOrEmpty(fd.Address2))
            {
                contactinfo.Append(", ");
                contactinfo.Append(fd.Address2);
            }
            contactinfo.Append(", ");
            contactinfo.Append(fd.City + ",  " + fd.State + " " + fd.Zipcode);
            contactinfo.Append("</li>");
            litLocAddress.Text = contactinfo.ToString();

            ltrPhoneNumber.Text = "<li class=\"telephone\"><a href=\"tel:+" + fd.PhoneNumber + "\">" + fd.PhoneNumber + "</a></li>";
            ltrEmailAddress.Text = "<li class=\"email\"><a href='mailto:" + fd.Email + "'>" + fd.Email + "</a></li>";

            var hours = fd.HoursOfOperation.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            string hoursDisplay = string.Empty;
            if (hours != null && hours.Length > 0)
                hoursDisplay = "<p>" + hours[0] + "</p>";
            if (hours != null && hours.Length > 1)
                hoursDisplay = hoursDisplay + "<p>" + hours[1] + "</p>";
            ltrHour.Text = hoursDisplay;

            hiddenCenterLat.Value = fd.Latitude;
            hiddenCenterLong.Value = fd.Longitude;
        }
    }

    private void GetOurTeamInfo()
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

            int count = 0;
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i <= allEmployees.Count / 4; i++)
            {
                sb.Append("<div id=\"our_team_tabs_row" + (i + 1) + "\" class=\"our_team grid_block_container\">");
                sb.Append("<div class=\"container_24\">");
                sb.Append("<ul class=\"grid_24\">");
                var filteredEmployeeList = allEmployees.Skip(i * 4).Take(4).ToList();

                for (int k = 0; k < filteredEmployeeList.Count(); k++)
                {
                    count++;
                    sb.Append("<li id=\"meet_team_" + count + "\">");
                    sb.Append(this.GetMemberBasicInfo(filteredEmployeeList[k], count));
                    sb.Append("</li>");
                }
                sb.Append("</ul>");
                sb.Append("</div>");
                sb.Append("<div class=\"clear\"></div>");
                foreach (var e in filteredEmployeeList)
                {
                    if (!string.IsNullOrEmpty(e.Bio))
                        sb.Append(this.GetMemberFullInfo(e));
                }
                sb.Append("</div>");
            }
            ltrOurTeam.Text = sb.ToString();
        }
    }

    private string GetMemberBasicInfo(Employee employee, int count)
    {
        StringBuilder sbHtml = new StringBuilder();
        if (!string.IsNullOrEmpty(employee.Bio))
        {
            sbHtml.Append("<div class=\"meet_team_div fade meet_team_" + count + "\">");
            sbHtml.Append("<div class=\"square_button opened\"><a href=\"#" + employee.EmployeeId + "\">More</a></div>");
            sbHtml.Append("<div class=\"caption\">");
        }
        else
        {
            sbHtml.Append("<div class=\"meet_team_div meet_team_" + count + "\">");
            sbHtml.Append("<div class=\"caption no_details\">");
        }
        sbHtml.Append("<div class=\"caption_content\">");
        sbHtml.Append("<h3>" + employee.FirstName + " " + employee.LastName + "<br/><span>" + employee.Title + "</span></h3>");
        sbHtml.Append("<ul>");
        sbHtml.Append("<li class=\"telephone\"><a href=\"tel:" + employee.WorkPhone + "\">" + employee.WorkPhone + "</a></li>");
        sbHtml.Append("<li class=\"mobile\"><a href=\"tel:" + employee.MobileNumber + "\">" + employee.MobileNumber + "</a></li>");
        sbHtml.Append("<li class=\"email\"><a href=\"mailto:" + employee.Email + "\">" + employee.Email + "</a></li>");
        sbHtml.Append("</ul>");
        sbHtml.Append("</div>");
        sbHtml.Append("</div>");
        string employeeImage = this.GetEmployeeImage(employee);
        sbHtml.Append("<img src=\"" + employeeImage + "\" />");
        sbHtml.Append("</div>");

        return sbHtml.ToString();
    }

    private string GetMemberFullInfo(Employee employee)
    {
        StringBuilder sbHtml = new StringBuilder();
        sbHtml.Append("<div class=\"our_team_details\" id=\"" + employee.EmployeeId + "\">");
        sbHtml.Append("<div class=\"container_24\">");
        sbHtml.Append("<div class=\"grid_24\">");
        sbHtml.Append("<a class=\"close\" href=\"#hide\"><span></span></a>");
        sbHtml.Append("<div class=\"grid_11 team_meta\">");
        sbHtml.Append("<h2 class=\"headline\">" + employee.FirstName + " " + employee.LastName + "</h2>");
        sbHtml.Append("<h2 class=\"sub_headline\">" + employee.Title + "</h2>");
        sbHtml.Append("<div class=\"team_member_intro\">");
        sbHtml.Append("<p>" + employee.Bio + "</p>");
        sbHtml.Append("<ul>");
        sbHtml.Append("<li class=\"telephone\"><a href=\"tel:" + employee.WorkPhone + "\">" + employee.WorkPhone + "</a></li>");
        sbHtml.Append("<li class=\"mobile\"><a href=\"tel:" + employee.MobileNumber + "\">" + employee.MobileNumber + "</a></li>");
        sbHtml.Append("<li class=\"email\"><a href=\"mailto:" + employee.Email + "\">" + employee.Email + "</a></li>");
        sbHtml.Append("</ul>");
        sbHtml.Append("</div>");
        sbHtml.Append("</div>");
        sbHtml.Append("<div class=\"team_detail grid_12 push_2 clearfix\">");
        sbHtml.Append("<div class=\"grid_18 omega team_text\">");
        sbHtml.Append("<div class=\"team_text_inner\">" + this.GetAboutTeamLogic() + "</div>");
        sbHtml.Append("</div>");
        sbHtml.Append("</div>");
        sbHtml.Append("</div>");
        sbHtml.Append("</div>");
        sbHtml.Append("<div class=\"clear\"></div>");
        sbHtml.Append("</div>");

        return sbHtml.ToString();
    }

    private string GetAboutTeamLogic()
    {
        string companyInfo = "";
        if (cbOurTeamCompnayInfo != null && cbOurTeamCompnayInfo.EkItem != null)
        {
            companyInfo = cbOurTeamCompnayInfo.EkItem.Html;
        }
        return companyInfo;
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
        if (data != null)
        {
            if (!string.IsNullOrEmpty(data.PicturePath))
                return data.PicturePath;
            if (data.Gender == "Male")
                image = "/images/photos/our-team-placeholder-male.png";
            if (data.Gender == "Female")
                image = "/images/photos/our-team-placeholder-female.png";
        }
        return image;
    }
}