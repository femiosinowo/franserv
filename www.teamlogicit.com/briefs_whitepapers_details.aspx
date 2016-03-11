<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Secondary.master" AutoEventWireup="true"
    CodeFile="briefs_whitepapers_details.aspx.cs" Inherits="briefs_whitepapers_details" %>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">
    <div class="clear">
    </div>
    <div class="bwdetails_content_wrapper resources clearfix">
        <div class="container_24 brief clearfix">
            <div class="grid_24 whitePaperDetail">
                <asp:Repeater runat="server" ID="UxBriefsWhitepapers">
                    <ItemTemplate>
                        <div class="grid_15">
                            <h2 class="headline"><a href="#"><%#Eval("title") %></a></h2>
                            <div class="grid_14 brief_details_content">
                                <h3 class="sub_headline">
                                    <%#Eval("abstract") %>
                                </h3>
                                <%#Eval("content") %>
                                <div class="bw_meta social_share">
                                    <ul>
                                        <!--Commented out as per client request -->
                                        <!--<li class="news_print"><a class="print" href="javascript:void('0')">
                                            <img src="/images/black_print_icon.png" alt="black_printer_icon" /></a></li>-->
                                        <li class="news_email"><span class='st_email'><a href="javascript:void('0')">
                                            <img src="/images/black_mail_icon.png" alt="black_email_icon" /></a></span></li>
                                        <!--<li class="news_share"><a href="quicklink">Share</a></li>-->
                                        <li class="news_share"><span class="st_sharethis_custom"><a href="javascript:void('0')">Share</a></span></li>
                                    </ul>
                                </div>
                                <!--//.bw_meta -->
                            </div>
                            <!--//.grid_10-->
                            <div class="grid_10 thumbnail">
                                <img src="<%#Eval("imgSRC") %>" alt="<%#Eval("title") %>">
                            </div>
                        </div>
                        <!--// end left content -->
                    </ItemTemplate>
                </asp:Repeater>
                <!-- DOWNLOAD FORM -->
                <div class="grid_8 push_1 free_guide_form">
                    <h4 class="sidebar_title">Get Your Free Guide</h4>
                    <div class="form" id="download_brief">
                        <fieldset>
                            <asp:TextBox ID="fname" runat="server" required="required" placeholder="First Name*"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfv1" runat="server" ControlToValidate="fname"
                                CssClass="required" ValidationGroup="Whitepapers" ForeColor="Red">* Required</asp:RequiredFieldValidator>
                            <asp:TextBox ID="lname" required="required" runat="server" placeholder="Last Name*"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfv2" runat="server" ControlToValidate="lname"
                                CssClass="required" ValidationGroup="Whitepapers" ForeColor="Red">* Required</asp:RequiredFieldValidator>
                            <asp:TextBox ID="emailAdd" required="required" runat="server" placeholder="Email*" TextMode="Email"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfv3" runat="server" ControlToValidate="emailAdd"
                                CssClass="required" ValidationGroup="Whitepapers" ForeColor="Red">* Required</asp:RequiredFieldValidator>
                            <asp:TextBox ID="url" required="required" runat="server" placeholder="Website URL*"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfv4" runat="server" ControlToValidate="url"
                                CssClass="required" ValidationGroup="Whitepapers" ForeColor="Red">* Required</asp:RequiredFieldValidator>
                             <asp:TextBox ID="txtZipCode" required="required" runat="server" placeholder="ZipCode*"></asp:TextBox>
                             <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtZipCode"
                                  CssClass="required" ValidationGroup="Whitepapers" ForeColor="Red">* Required</asp:RequiredFieldValidator>
                        </fieldset>
                        <%--<label>
                                <input type="checkbox" name="subscribe" value="Subscribe_IT_Inflections">
                                Subscribe to IT Inflections
                            </label>--%>
                        <fieldset>
                            <label for="ddlPrimaryBusiness">
                                What is your primary mode of business? *</label>
                            <asp:DropDownList ID="ddlPrimaryBusiness" CssClass="custom-select" runat="server" AppendDataBoundItems="true">
                                <asp:ListItem Text="- Please Select One -" Value="0" />
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfv5" runat="server" ControlToValidate="ddlPrimaryBusiness"
                                CssClass="required" InitialValue="0" ValidationGroup="Whitepapers" ForeColor="Red">* Required</asp:RequiredFieldValidator>
                        </fieldset>
                        <fieldset>
                            <label for="ddlMarketingEmployee">
                                Number of Employees? *</label>
                            <asp:DropDownList ID="ddlMarketingEmployee" CssClass="custom-select" runat="server" AppendDataBoundItems="true">
                                <asp:ListItem Text="- Please Select One -" Value="0" />
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfv6" runat="server" ControlToValidate="ddlMarketingEmployee"
                                CssClass="required" InitialValue="0" ValidationGroup="Whitepapers" ForeColor="Red">* Required</asp:RequiredFieldValidator>
                        </fieldset>
                        <!-- commented out the fields here are client requested to match sirspeedy site form -->
                        <!-- <fieldset>
                            <label for="ddlSalesReprestative">
                                Number of Sales Representatives? *</label>
                            <asp:DropDownList ID="ddlSalesReprestative" CssClass="custom-select" runat="server">
                                <asp:ListItem Text="- Please Select One -" Value="0" />
                                <asp:ListItem Text="Item 1" Value="Item 1" />
                                <asp:ListItem Text="Item 2" Value="Item 2" />
                                <asp:ListItem Text="Item 3" Value="Item 3" />
                                <asp:ListItem Text="Item 4" Value="Item 4" />
                            </asp:DropDownList>                            
                        </fieldset>
                        <fieldset>
                            <label for="ddlBusinessUseCRMType">
                                What CRM does your business use? *</label>
                            <asp:DropDownList ID="ddlBusinessUseCRMType" CssClass="custom-select" runat="server">
                                <asp:ListItem Text="- Please Select One -" Value="0" />
                                <asp:ListItem Text="Item 1" Value="Item 1" />
                                <asp:ListItem Text="Item 2" Value="Item 2" />
                                <asp:ListItem Text="Item 3" Value="Item 3" />
                                <asp:ListItem Text="Item 4" Value="Item 4" />
                            </asp:DropDownList>                           
                        </fieldset>
                        <fieldset>
                            <label for="ddlYourLocation">
                                Where are you located? *</label>
                            <asp:DropDownList ID="ddlYourLocation" CssClass="custom-select" runat="server">
                                <asp:ListItem Text="- Please Select One -" Value="0" />
                                <asp:ListItem Text="Item 1" Value="Item 1" />
                                <asp:ListItem Text="Item 2" Value="Item 2" />
                                <asp:ListItem Text="Item 3" Value="Item 3" />
                                <asp:ListItem Text="Item 4" Value="Item 4" />
                            </asp:DropDownList>                            
                        </fieldset>
                        <fieldset>
                            <label for="marketingChallenge">What is your biggest marketing challenge?</label>
                            <textarea id="marketingChallenge" runat="server" rows="5" cols="5"></textarea>
                        </fieldset> -->
                        <!-- end select_group -->
                        <div class="square_button">
                            <asp:LinkButton ID="download_now" runat="server" Text="GET YOUR FREE DOWNLOAD" OnClick="btnDownloadNow_Click" ValidationGroup="Whitepapers" />
                        </div>
                    </div>
                </div>
                <!--//.thumbnail -->
            </div>
            <!--// .grid_24-->
        </div>
        <!--// .brief-->
    </div>
    <asp:HiddenField ID="hddnWhitePaperTitle" runat="server" Value="" />
    <div class="clear">
    </div>
    <!-- mmm How Can We Help You (loc) mmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmm How Can We Help You (loc) mmm -->
    <div class="img-holder" id="how_we_can_help_Img" runat="server" data-image="/images/how_we_can_help_bkg.jpg" data-width="1600" data-height="670"></div>
    <div class="how_we_can_help clearfix">
        <div class="container_24">
            <div class="grid_24">
                <asp:Literal ID="ltrSupplementOutSourcing" runat="server"></asp:Literal>
            </div>
            <!-- grid 24-->
        </div>
        <!--container 24-->
    </div>
    <!-- how_we_can_help-->
    <!--how_we_can_help_wrapper-->
    <div class="clear"></div>
</asp:Content>
