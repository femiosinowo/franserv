using SirSpeedy.CMS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AdminTool_Templates_Manage_Mega_Menu : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!Page.IsPostBack)
        {
            if (Request.QueryString.HasKeys())
            {
                if (string.IsNullOrEmpty(Request.QueryString["centerid"]))
                {
                    Response.Redirect("/admin/templates/AllCenters.aspx");
                }

                if (!string.IsNullOrEmpty(Request.QueryString["tab"]))
                {
                    string tab = Request.QueryString["tab"];
                    hdnSelectedTab.Value = tab;
                }

                string centerId = Request.QueryString["centerid"];
                ltrBreadcrumb.Text = "<a href=\"/admintool/index.aspx\">Home</a> >> <a href=\"/admintool/templates/AllCenters.aspx\">All Centers</a> >> <a href=\"/admintool/templates/Center.aspx?centerid=" + centerId + "\">Center</a> >> Manage Mega Menu";
              
                this.GetOurTeam(centerId);
                this.GetFlickrData(centerId);
            }
        }
    }
      
    protected void btnOurTeam_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            string centerId = Request.QueryString["centerId"] != null ? Request.QueryString["centerId"] : "";
            if (hddnSelectedOurTeam.Value != "" && centerId != "")
            {
                AdminToolManager.UpdateM3OurTeamIds(centerId, hddnSelectedOurTeam.Value);
                Response.Redirect("/AdminTool/Templates/Manage-Mega-Menu.aspx?centerid=" + centerId + "&tab=1");
            }
        }
    }
    
    protected void btnFlickr_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            string centerId = Request.QueryString["centerId"] != null ? Request.QueryString["centerId"] : "";
            if (hddnSelectedProfilePic.Value != "" && centerId != "")
            {
                AdminToolManager.UpdateM3FlickrPhotoSets(centerId, hddnSelectedProfilePic.Value);
                m3DataSaveMsg.Visible = true;
                Response.Redirect("/AdminTool/Templates/Manage-Mega-Menu.aspx?centerid=" + centerId + "&tab=1");
            }
        }
    }
      
    private void GetOurTeam(string centerId)
    {
        var getAllEmployees = FransDataManager.GetAllEmployee(centerId, true);
        if(getAllEmployees != null && getAllEmployees.Any())
        {
            var currentCenterEmps = getAllEmployees.Where(x => x.FransId == centerId).ToList();
            if(currentCenterEmps != null && currentCenterEmps.Any())
            {
                 var m3Data = FransDataManager.GetFransM3TData(centerId, true);
                 if (m3Data != null && m3Data.OurTeamEmployeeIds!=null && m3Data.OurTeamEmployeeIds.Any())
                 {
                     var existingList = m3Data.OurTeamEmployeeIds;
                     var empList = from c in currentCenterEmps
                                       select new
                                       {
                                           AvailableEmployees = existingList.Exists(x => x == c.EmployeeId) ? "" : "<div class=\"drag t1\"><span  id='" + c.EmployeeId + "'>" + c.FirstName + " " + c.LastName + "<br/>" + c.Roles + "</span></div>",
                                           SelectedEmployees = existingList.Exists(x => x == c.EmployeeId) ? "<div class=\"drag t1\"><span  id='" + c.EmployeeId + "'>" + c.FirstName +" "+c.LastName +"<br/>"+ c.Roles + "</span></div>" : ""
                                       };
                     lvEmployees.DataSource = empList;
                     lvEmployees.DataBind();
                 }
                 else
                 {
                     var empList = from c in currentCenterEmps
                                       select new
                                       {
                                           AvailableEmployees = "<div class=\"drag t1\"><span  id='" + c.EmployeeId + "'>" + c.FirstName +" "+c.LastName +"<br/>"+ c.Roles + "</span></div>",
                                           SelectedEmployees = ""
                                       };
                     lvEmployees.DataSource = empList;
                     lvEmployees.DataBind();
                 }
            }
        }
        else
        {
            noEmployeesInTeam.Visible = true;
            pnlOurTeam.Visible = false;
        }
    }

    private void GetFlickrData(string centerId)
    {
        var thirdPartyData = FransDataManager.GetFransThirdPartyData(centerId, true);
        if(thirdPartyData != null)
        {
            var flickrUserId = thirdPartyData.FlickrUserId;
            if (!string.IsNullOrEmpty(flickrUserId))
            {
                var flickrSets = FlickrManager.GetFlickrPhotoSets(flickrUserId, 50);
                if (flickrSets != null && flickrSets.Any())
                {
                    if (thirdPartyData.SelectedPhotoSetIds != null && thirdPartyData.SelectedPhotoSetIds.Any())
                    {
                        var existingList = thirdPartyData.SelectedPhotoSetIds;
                        var flickrDataList = from c in flickrSets
                                          select new
                                          {
                                              AvailableFlickr = existingList.Exists(x => x == c.PhotosetId) ? "" : "<div class=\"drag t1\"><span  id='" + c.PhotosetId + "'><a target=\"_blank\" href=\"/AdminTool/Templates/FlickrPhotoDetail.aspx?id=" + c.PhotosetId + "&userId="+ flickrUserId+ "\">" + c.Title + "</a></span></div>",
                                              SelectedFlickr = existingList.Exists(x => x == c.PhotosetId) ? "<div class=\"drag t1\"><span id='" + c.PhotosetId + "'><a target=\"_blank\" href=\"/AdminTool/Templates/FlickrPhotoDetail.aspx?id=" + c.PhotosetId + "&userId=" + flickrUserId + "\">" + c.Title + "</a></span></div>" : ""
                                          };
                        lvFlickr.DataSource = flickrDataList;
                        lvFlickr.DataBind();
                    }
                    else
                    {
                        var flickrDataList = from c in flickrSets
                                             select new
                                             {
                                                 AvailableFlickr = "<div class=\"drag t1\"><span  id='" + c.PhotosetId + "'><a target=\"_blank\" href=\"/AdminTool/Templates/FlickrPhotoDetail.aspx?id=" + c.PhotosetId + "&userId=" + flickrUserId + "\">" + c.Title + "</a></span></div>",
                                                 SelectedFlickr = ""
                                             };
                        lvFlickr.DataSource = flickrDataList;
                        lvFlickr.DataBind();
                    }
                }
            }
        }
        else
        {
            pnlNoThirdPartyData.Visible = true;           
            pnlFlickr.Visible = false;
        }
    }

    private string GetContentTitle(long id)
    {
        string title = "Content";
        var cData =ContentHelper.GetContentById(id, false);
        if (cData != null)
            title = cData.Title;

        return title;
    }
}