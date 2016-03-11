<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SubscribeLocal.ascx.cs"
    Inherits="UserControls_subscribeLocal" %>
<%@ Register TagPrefix="cms" Namespace="Ektron.Cms.Controls" Assembly="Ektron.Cms.Controls" %>

<style type="text/css">
    .SubscribeChkType {
        float: left;
    }
    .requiredField{
        border: 3px solid red !important;
    }
</style>
<script type="text/javascript">
    $(document).ready(function () {
        $('.btnSubscribe').click(function () {
            $('.safWaitImg').show();
            var fieldsStatus = false;
            $('.formSubscribe input.required').each(function () {
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
                SaveSubscribeData();
                //return false;
            }
            else {
                $("#subscribe_form").scrollTop(100);
                $('.safWaitImg').hide();
                return false;
            }
        });

        function SaveSubscribeData() {
            //save form data
            //call Handler  
            var emailAddress = $('.txtEmail').val();
            var companyName = $('.txtCompany').val();
            var firstName = $('.txtFirstName').val();
            var lastName = $('.txtLastName').val();
            var address = $('.txtAddress').val();
            var city = $('.txtCity').val();
            var selectedStateVal = $('.subscribe_state ul.transformSelect li').eq('0').find('span:first').text();
            var state = '';
            if (selectedStateVal != 'State')
                state = selectedStateVal;
            var zipCode = $('.txtZipCode').val();
            var phoneNumber = $('.txtPhone').val();
            var centerId = $('.hddnCenterId').val();

            var responseData = '';
            $.ajax({
                type: "POST",
                url: "/Handlers/SaveSubscribeData.ashx",
                data: '{"firstName": "' + firstName + '","lastName":"' + lastName + '","emailAddress":"' + emailAddress + '","companyName":"' + companyName + '" ,"phoneNumber":"' + phoneNumber + '" ,"address":"' + address + '" ,"city":"' + city + '" ,"state":"' + state + '" ,"zipCode":"' + zipCode + '","centerId":"' + centerId + '"  }',
                contentType: "application/json; charset=utf-8",                
                async: false,
                cache: false,
                success: function (response) {
                    //console.log(response);
                    responseData = response;
                },
                error: function (response, status, error) {
                    alert('error' + error);
                    $('.safWaitImg').hide();
                    return false;
                }
            });

            if (responseData != '') {
                alert(responseData);
                $('.safWaitImg').hide();
                return false;
            }
            else {
                var thankYouPage = '/thank-you/?type=subscribe&centerId=';
                var localSiteCenterId = $('.hddnCenterId').val();
                if (localSiteCenterId != '')
                    thankYouPage = '/' + localSiteCenterId + thankYouPage + localSiteCenterId;
                else
                    thankYouPage = thankYouPage + selectedCenter;
                window.location.href = thankYouPage;
            }
        }
    });
</script>

<!-- mmmmmmmmmmmm SUBSCRIBE (LOCAL) mmmmmmmmmmmmmm -->
<div id="subscribe_close" class="container_24">
    <div class="grid_24">
        <div id="subscribe_form">
            <div class="subscribe_header clearfix">
                <div class="grid_23">
                    <div class="subscribe_logo">
                        <img src="/images/subscribe_logo.png" alt="subscribe_logo" width="320" height="107" />
                    </div>
                </div>
                <div class="grid_1 close_subscribe"><a id="close_x" class="close" href="#">X</a></div>
            </div>
            <!--//.subscribe_header-->
            <cms:ContentBlock ID="cbDescription" runat="server" DoInitFill="false" />
            <asp:Panel ID="pnlForm" runat="server">
                <div class="form formSubscribe" id="form_subscribe">
                    <fieldset>
                        <span>
                            <asp:TextBox ID="txtEmail" TextMode="Email" placeholder="Email Address*" CssClass="required txtEmail" runat="server"></asp:TextBox>
                        </span>
                        <span>
                            <asp:TextBox ID="txtCompany" placeholder="Company*" CssClass="required txtCompany" runat="server"></asp:TextBox>
                        </span>
                        <span>
                            <asp:TextBox ID="txtFirstName" placeholder="First Name*" CssClass="required txtFirstName" runat="server"></asp:TextBox>
                         </span>
                        <span>
                            <asp:TextBox ID="txtLastName" placeholder="Last Name*" CssClass="required txtLastName" runat="server"></asp:TextBox>
                         </span>
                        <span>
                            <asp:TextBox ID="txtAddress" placeholder="Address" CssClass="txtAddress" runat="server"></asp:TextBox>
                        </span>
                        <span>
                            <asp:TextBox ID="txtCity" placeholder="City" CssClass="txtCity" runat="server"></asp:TextBox>
                        </span>
                        <span class="subscribe_state">
                             <asp:DropDownList ID="ddlState" class="custom-select" runat="server">                                
                                <asp:ListItem Selected="True" Value="">State</asp:ListItem>
                                <asp:ListItem Value="AL">Alabama</asp:ListItem>
                                <asp:ListItem Value="AK">Alaska</asp:ListItem>
                                <asp:ListItem Value="AZ">Arizona</asp:ListItem>
                                <asp:ListItem Value="AR">Arkansas</asp:ListItem>
                                <asp:ListItem Value="CA">California</asp:ListItem>
                                <asp:ListItem Value="CO">Colorado</asp:ListItem>
                                <asp:ListItem Value="CT">Connecticut</asp:ListItem>
                                <asp:ListItem Value="DE">Delaware</asp:ListItem>
                                <asp:ListItem Value="DC">District of Columbia</asp:ListItem>
                                <asp:ListItem Value="FL">Florida</asp:ListItem>
                                <asp:ListItem Value="GA">Georgia</asp:ListItem>
                                <asp:ListItem Value="HI">Hawaii</asp:ListItem>
                                <asp:ListItem Value="ID">Idaho</asp:ListItem>
                                <asp:ListItem Value="IL">Illinois</asp:ListItem>
                                <asp:ListItem Value="IN">Indiana</asp:ListItem>
                                <asp:ListItem Value="IA">Iowa</asp:ListItem>
                                <asp:ListItem Value="KS">Kansas</asp:ListItem>
                                <asp:ListItem Value="KY">Kentucky</asp:ListItem>
                                <asp:ListItem Value="LA">Louisiana</asp:ListItem>
                                <asp:ListItem Value="ME">Maine</asp:ListItem>
                                <asp:ListItem Value="MD">Maryland</asp:ListItem>
                                <asp:ListItem Value="MA">Massachusetts</asp:ListItem>
                                <asp:ListItem Value="MI">Michigan</asp:ListItem>
                                <asp:ListItem Value="MN">Minnesota</asp:ListItem>
                                <asp:ListItem Value="MS">Mississippi</asp:ListItem>
                                <asp:ListItem Value="MO">Missouri</asp:ListItem>
                                <asp:ListItem Value="MT">Montana</asp:ListItem>
                                <asp:ListItem Value="NE">Nebraska</asp:ListItem>
                                <asp:ListItem Value="NV">Nevada</asp:ListItem>
                                <asp:ListItem Value="NH">New Hampshire</asp:ListItem>
                                <asp:ListItem Value="NJ">New Jersey</asp:ListItem>
                                <asp:ListItem Value="NM">New Mexico</asp:ListItem>
                                <asp:ListItem Value="NY">New York</asp:ListItem>
                                <asp:ListItem Value="NC">North Carolina</asp:ListItem>
                                <asp:ListItem Value="ND">North Dakota</asp:ListItem>
                                <asp:ListItem Value="OH">Ohio</asp:ListItem>
                                <asp:ListItem Value="OK">Oklahoma</asp:ListItem>
                                <asp:ListItem Value="OR">Oregon</asp:ListItem>
                                <asp:ListItem Value="PA">Pennsylvania</asp:ListItem>
                                <asp:ListItem Value="RI">Rhode Island</asp:ListItem>
                                <asp:ListItem Value="SC">South Carolina</asp:ListItem>
                                <asp:ListItem Value="SD">South Dakota</asp:ListItem>
                                <asp:ListItem Value="TN">Tennessee</asp:ListItem>
                                <asp:ListItem Value="TX">Texas</asp:ListItem>
                                <asp:ListItem Value="UT">Utah</asp:ListItem>
                                <asp:ListItem Value="VT">Vermont</asp:ListItem>
                                <asp:ListItem Value="VA">Virginia</asp:ListItem>
                                <asp:ListItem Value="WA">Washington</asp:ListItem>
                                <asp:ListItem Value="WV">West Virginia</asp:ListItem>
                                <asp:ListItem Value="WI">Wisconsin</asp:ListItem>
                                <asp:ListItem Value="WY">Wyoming</asp:ListItem>
                             </asp:DropDownList> 
                        </span>                        
                        <span class="subscribe_zip">
                            <asp:TextBox ID="txtZipCode" placeholder="Zip Code" CssClass="txtZipCode" runat="server"></asp:TextBox>
                         </span>
                        <span>
                            <asp:TextBox ID="txtPhone" TextMode="Phone" CssClass="txtPhone" placeholder="Phone" runat="server"></asp:TextBox>
                        </span>
                        <div class="clear"></div>                       
                            <div class="square_button">        
                                <input type="button" value="Subscribe" id="btnSubscribe" class="btnSubscribe" />
                            </div>
                            <span class="safWaitImg" style="display: none;" id="safWaitImg">
                                <img alt="ajax-loader" src="/Workarea/images/application/ajax-loader_circle_lg.gif" />
                            </span>
                    </fieldset>
                </div>
            </asp:Panel>
        </div>
        <!--//#subscribe_form-->

    </div>
    <!--//.grid_24-->
</div>
<!--//.container_24-->
<div class="clear"></div>
