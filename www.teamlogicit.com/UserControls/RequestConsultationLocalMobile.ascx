<%@ Control Language="C#" AutoEventWireup="true" CodeFile="RequestConsultationLocalMobile.ascx.cs"
    Inherits="UserControls_RequestConsultationLocal" %>
<%@ Register TagPrefix="cms" Namespace="Ektron.Cms.Controls" Assembly="Ektron.Cms.Controls" %>

<style type="text/css">
    .requestconsultationChkType {
        float: left;
    }
    .requiredField{
        border: 3px solid red !important;
    }
</style>
<script type="text/javascript">
    $(document).ready(function () {
        $('.btnrequestconsultation').click(function () {
            document.getElementById('<%=consultationWaitImg.ClientID%>').style.display = 'block';
            var fieldsStatus = false;
            $('.form_requestconsultation input.required').each(function () {
                var fieldId = $(this).attr('id');
                var fieldVal = document.getElementById(fieldId).value;
                if (fieldVal != '') {
                    fieldsStatus = true;
                    $(this).removeClass('requiredField');
                }
                else {
                    fieldsStatus = false;
                    $(this).addClass('requiredField');
                }
            });

            if (fieldsStatus) {
                SaveConsultationData();
                //return false;
            }
            else {
                $("#form_requestconsultation").scrollTop(100);
                document.getElementById('<%=consultationWaitImg.ClientID%>').style.display = 'none';
                return false;
            }
        });

        function SaveConsultationData() {
            //save form data
            //call Handler             
            var firstName = document.getElementById('<%=txtFirstName.ClientID%>').value;
            var lastName = document.getElementById('<%=txtLastName.ClientID%>').value; 
            var emailAddress = document.getElementById('<%=txtEmail.ClientID%>').value; 
            var phoneNumber = document.getElementById('<%=txtPhone.ClientID%>').value; 
            var companyName = document.getElementById('<%=txtCompany.ClientID%>').value; 
            var howCanweHelp = document.getElementById('<%=howcanwehelpText.ClientID%>').value; 
            var signUp = $("input.signup").is(':checked');
            var centerId = $('.hddnCenterId').val();

            var responseData = '';
            $.ajax({
                type: "POST",
                url: "/Handlers/SaveConsultationData.ashx",
                data: '{"firstName": "' + firstName + '","lastName":"' + lastName + '","emailAddress":"' + emailAddress + '","companyName":"' + companyName + '" ,"phoneNumber":"' + phoneNumber + '" ,"howCanweHelp":"' + howCanweHelp + '" ,"signUp":"' + signUp + '","centerId":"' + centerId + '"  }',
                contentType: "application/json; charset=utf-8",
                async: false,
                cache: false,
                success: function (response) {
                    //console.log(response);
                    responseData = response;
                },
                error: function (response, status, error) {
                    alert('error' + error);
                    document.getElementById('<%=consultationWaitImg.ClientID%>').style.display = 'none';
                    return false;
                }
            });

            if (responseData != '') {
                alert(responseData);
                document.getElementById('<%=consultationWaitImg.ClientID%>').style.display = 'none';
                return false;
            }
            else {
                document.getElementById('<%=consultationWaitImg.ClientID%>').style.display = 'none';
                document.getElementById('<%=pnlForm.ClientID%>').style.display = 'none';
                document.getElementById('<%=pnlThanks.ClientID%>').style.display = 'block';
                $("html, body").animate({ scrollTop: 0 }, "slow");           
                return false;
            }
        }
    });
</script>

<!-- mmmmmmmmmmmm requestconsultation (LOCAL) mmmmmmmmmmmmmm -->
<div id="requestconsultation_close" class="container_24">
    <div class="grid_24">
        <div id="requestconsultation_form">
            <div class="requestconsultation_header clearfix">
                <div class="grid_23">
                    <div class="requestconsultation_logo">
                        <img src="/images/subscribe_logo.png" alt="subscribe_logo" width="320" height="107" />
                    </div>
                </div>
                
            </div>
            <!--//.requestconsultation_header-->
            <div id="pnlForm" class="pnlForm" runat="server">
                <h2>
                    <asp:Label runat="server" ID="TitleLabel1" /></h2>
                <asp:Literal ID="DescriptionLiteral1" runat="Server" />
                <div class="form form_requestconsultation" id="form_requestconsultation">
                    <fieldset>
                        <span>
                            <asp:TextBox ID="txtFirstName" placeholder="First Name*" CssClass="required txtFirstName" runat="server"></asp:TextBox>
                        </span>
                        <span>
                            <asp:TextBox ID="txtLastName" placeholder="Last Name*" CssClass="required txtLastName" runat="server"></asp:TextBox>
                        </span>
                        <span>
                            <asp:TextBox ID="txtEmail" TextMode="Email" placeholder="Email Address*" CssClass="required txtEmail" runat="server"></asp:TextBox>
                        </span>
                        <span>
                            <asp:TextBox ID="txtPhone" TextMode="Phone" placeholder="Phone" runat="server" CssClass="required txtPhone"></asp:TextBox>
                        </span>
                        <span>
                            <asp:TextBox ID="txtCompany" placeholder="Company*" CssClass="required txtCompany" runat="server"></asp:TextBox>                           
                        </span>
                        <span>
                            <textarea id="howcanwehelpText" CssClass="required howcanwehelpText" runat="server" placeholder="How Can We Help?" onblur="if(this.value == '') { this.value = 'How Can We Help?'; }" onfocus="if(this.value == 'Message*') { this.value = ''; }" cols="62" rows="6"></textarea>
                        </span>
                        <span>
                            <input type="checkbox" class="signup" id="signup" runat="server" checked="checked" style="-webkit-appearance: checkbox;" />
                            <label for="signup" class="small-text">Sign up for TeamLogic IT emails and promos</label>
                        </span>
                        <div class="clear"></div>
                        <div class="square_button">
                            <input type="button" value="Request Consultation" id="btnrequestconsultation" class="btnrequestconsultation" />                           
                        </div>
                        <span class="consultationWaitImg" runat="server" style="display: none;" id="consultationWaitImg">
                            <img alt="ajax-loader" src="/Workarea/images/application/ajax-loader_circle_lg.gif" />
                        </span>
                    </fieldset>
                </div>
            </div>
            <div id="pnlThanks" cssclass="pnlThanks" style="display: none;" runat="server"> 
                <cms:ContentBlock runat="server" DoInitFill="false" ID="ThankYouCB" />
            </div>
        </div>
        <!--//#requestconsultation_form-->
    </div>
    <!--//.grid_24-->
</div>
<!--//.container_24-->
<div class="clear"></div>
