<%@ Control Language="C#" AutoEventWireup="true" CodeFile="MultivariateSection.ascx.cs" Inherits="widgets_MultivariateSection" %>
<%@ Register Src="../Workarea/PageBuilder/PageControls/WidgetHost.ascx" TagPrefix="EktronUC" TagName="WidgetHost" %>

<asp:MultiView ID="uxUXSwitch" runat="server">
    <asp:View ID="uxOriginalView" runat="server">
        <div id="multivariate" runat="server" class="multivariate-section">
            <div id="slider" class="multivariate-buttons" runat="server">
                <asp:Button ID="btnAddVariation" Text="Add" runat="server" CssClass="add-variant-btn" OnClick="btnAddVariation_Click" />
    
                <div class="slider-container">
                    <span class="item-number">0</span>
                    <div class="add-variant" id="addVariant" alt="Add Variation" title="Add Variation" runat="server">+</div>
                    <div class="slider"></div>
                </div>
            </div>
            <asp:Literal ID="litDebugOutput" runat="server"></asp:Literal>
            <div class="columns-container">
                <asp:Repeater ID="repColumns" runat="server" OnItemDataBound="repColumns_ItemDataBound">
                    <ItemTemplate>
                        <div id="zone" class="PBColumn nested" style="display: none;" runat="server">
                            <ul class="columnwidgetlist" id="column" runat="server">
                                <li class="header" id="headerItem" runat="server">
                                    <a href="#" class="resizeColumn" onclick="Ektron.PageBuilder.WidgetHost.resizeColumn(this);return false;" runat="server" id="lbResizeColumn">
                                        <img alt="" id="imgresizecolumn" runat="server" class="PBeditbutton PB-UI-icon" src="#" />
                                    </a>
                                    <asp:LinkButton CssClass="remColumn" ID="btnDeleteColumn" runat="server">
                                        <img alt="" id="imgremcolumn" runat="server" class="PBclosebutton PB-UI-icon" src="#" />
                                    </asp:LinkButton>
                                </li>
                                <asp:Repeater ID="controlcolumn" runat="server">
                                    <ItemTemplate>
                                        <li class="PBItem">
                                            <EktronUC:WidgetHost ID="WidgetHost" runat="server" />
                                        </li>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </ul>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
                &nbsp;
            </div>              
        </div>
    </asp:View>
    <asp:View ID="uxUXView" runat="server">
        <div id="uxMultivariateSectionWrapper" class="multivariate-section" runat="server">
            <div id="uxSlider" class="multivariate-buttons" runat="server">
                <asp:Button Text="<%$ Resources:AddVariant %>" CssClass="add-variant-btn" runat="server" OnClick="btnAddVariation_Click" />
                <div class="slider-container">
                    <span class="item-number">0</span>
                    <div class="add-variant" id="uxAddVariant" alt="Add Variation" title="Add Variation" runat="server">+</div>
                    <div class="slider"></div>
                </div>
            </div>
            <div id="uxDropZone" runat="server" class="columns-container clearfix" data-ux-pagebuilder="DropZone">
                <p id="uxNoMultivariateExperimentWidgetErrorMessage" runat="server" visible="false" />
                <asp:Repeater ID="uxRepColumns" runat="server" OnItemDataBound="repColumns_ItemDataBound">
                    <ItemTemplate>
                        <div data-ux-pagebuilder="Column"<%# GetColumnData(Container, ((IList)((Repeater)Container.Parent).DataSource).Count) %>>
                            <asp:PlaceHolder runat="server" Visible="<%# this.IsEditMode  %>">
                                <div class="ektron-ux-reset columnHeader" data-bind="event: {mouseover: mouseoverHeader, mouseout: mouseoutHeader }">
                                    <h3 class="ux-app-siteApp-columnHeader" data-bind="click: select">
                                        <span class="columnLabel">
                                            <label><%# GetColumnLabel(Container) %></label>
                                        </span>
                                        <span class="actions" data-bind="foreach: Actions">
                                            <!-- ko ifnot:Action.toLowerCase() === "movecolumn" -->
                                            <a data-bind="attr: {href: Href, title: Title, 'class': Action.toLowerCase()}, html: icon, click: fireAction">
                                                    
                                            </a>
                                            <!-- /ko -->
                                        </span>
                                    </h3>
                                </div>
                            </asp:PlaceHolder>
                            <ul>
                                <asp:Repeater ID="uxControlColumn" runat="server">
                                    <ItemTemplate>
                                        <li>
                                            <EktronUC:WidgetHost ID="WidgetHost" runat="server" />
                                        </li>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </ul>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </div>
    </asp:View>
</asp:MultiView>