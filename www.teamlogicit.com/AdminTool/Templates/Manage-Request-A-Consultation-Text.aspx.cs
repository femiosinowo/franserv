using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TeamLogic.CMS;

public partial class AdminTool_Templates_Manage_Request_A_Consultation_Text : System.Web.UI.Page
{
    private RequestConsultationWording rcitem;
    private RequestConsultationWording rcitem2;
    private string fransid = FransDataManager.GetFranchiseId();

    protected void Page_Load(object sender, EventArgs e)
    {
        List<RequestConsultationWording> list = AdminToolManager.GetAllRequestConsultationText(fransid);
        if (list.Any())
        {
            rcitem = list[0];
            rcitem2 = list[1];
        }
        if (!IsPostBack)
        {
            txtContentFreeConsultation.InnerText = rcitem2.ConsultationText;
            txtTitleContentFreeConsultation.Text = rcitem2.Title;
            txtTitleRequestConsultation.Text = rcitem.Title;
            txtContentRequestConsultation.InnerText = rcitem.ConsultationText;
        }
    }


    protected void SaveButton1_Click(object sender, EventArgs e)
    {
        string title = txtTitleRequestConsultation.Text;
        string text = txtContentRequestConsultation.InnerText;
        string title2 = txtTitleContentFreeConsultation.Text;
        string text2 = txtContentFreeConsultation.InnerText;

       bool status= AdminToolManager.UpdateConsultationWording(string.Empty, rcitem.Consultation_Id, text, title);
        bool status2= AdminToolManager.UpdateConsultationWording(string.Empty, rcitem2.Consultation_Id, text2, title2);

        if (status && status2)
            statusLabel1.Text = "Data saved successfully";
        else
        {
            if (!status) statusLabel1.Text = "There was a problem saving the first item";
            if (!status2) statusLabel1.Text = "There was a problem saving the second item";
        }

    }
}