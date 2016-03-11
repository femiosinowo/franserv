using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using SirSpeedy.CMS;
using Ektron.Cms.Instrumentation;

public partial class UserControls_subscribeLocal : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        cbLogo.DefaultContentID = ConfigHelper.GetValueLong("NonTransparentLogoCId");
        cbLogo.CacheInterval = ConfigHelper.GetValueLong("longCacheInterval");
        cbLogo.Fill();

        cbSubscribe.DefaultContentID = ConfigHelper.GetValueLong("LocalCenterSubscribeTextCId");
        cbSubscribe.CacheInterval = ConfigHelper.GetValueLong("longCacheInterval");
        cbSubscribe.Fill();
    }

    protected void btnSubscribeFrm_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            try
            {
                string centerId = FransDataManager.GetFranchiseId();
                string onlineType = "";
                string printType = "";

                if (centerId == string.Empty || chkOnLineTypeList.SelectedIndex < 0 || chkPrintTypeList.SelectedIndex < 0 || txtEmail.Text == string.Empty ||
                    txtFirstName.Text == string.Empty || txtLastName.Text == string.Empty || txtAddress.Text == string.Empty ||
                    txtCity.Text == string.Empty || txtState.Text == string.Empty || txtZipCode.Text == string.Empty)
                {
                    return;
                }


                if (chkOnLineTypeList.SelectedIndex > -1)
                {
                    for (int i = 0; i < chkOnLineTypeList.Items.Count; i++)
                    {
                        if (chkOnLineTypeList.Items[i].Selected)
                        {
                            if (onlineType == "")
                                onlineType += chkOnLineTypeList.Items[i].Value + ", ";
                            else
                                onlineType += chkOnLineTypeList.Items[i].Value;
                        }
                    }
                }

                if (chkPrintTypeList.SelectedIndex > -1)
                {
                    for (int i = 0; i < chkPrintTypeList.Items.Count; i++)
                    {
                        if (chkPrintTypeList.Items[i].Selected)
                        {
                            if (printType == "")
                                printType += chkPrintTypeList.Items[i].Value + ", ";
                            else
                                printType += chkPrintTypeList.Items[i].Value;
                        }
                    }
                }

                FransDataManager.SaveUserSubscribeData(centerId, printType, onlineType, txtFirstName.Text, txtLastName.Text,
                    txtEmail.Text, txtAddress.Text, txtCity.Text, txtState.Text, txtZipCode.Text);

                if (!string.IsNullOrEmpty(centerId))
                    Response.Redirect("/" + centerId + "/Thank-You/?type=subscribe&centerId=" + centerId);
                else
                    Response.Redirect("/Thank-You/?type=subscribe&centerId=" + centerId);

            }
            catch (Exception ex)
            {
                Log.WriteError(ex);
            }
        }
    }
}