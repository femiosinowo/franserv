<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ColumnDisplay.ascx.cs" Inherits="ColumnDisplay" %>
<%@ Register Src="~/Workarea/PageBuilder/PageControls/WidgetHost.ascx" TagPrefix="EktronUC" TagName="WidgetHost" %>

<asp:MultiView ID="uxUXSwitch" runat="server">
    <asp:View ID="uxOriginalView" runat="server">
        <div class="columns-container clearfix">
            <asp:Repeater ID="repColumns" runat="server" OnItemDataBound="repColumns_ItemDataBound">
                <ItemTemplate>
                    <div id="zone" class="PBColumn nested" runat="server">
                        <ul class="columnwidgetlist" id="column" runat="server">
                            <li class="header" id="headerItem" runat="server">
					            <asp:Label ID="HeaderCaption" CssClass="columnwidgetcaption" EnableViewState="False" runat="server" meta:resourcekey="HeaderCaptionResource1" />
                                <a href="#" class="resizeColumn" onclick="Ektron.PageBuilder.WidgetHost.resizeColumn(this);return false;" runat="server" id="lbResizeColumn">
                                    <img id="imgresizecolumn" runat="server" class="PBeditbutton PB-UI-icon" src="#" />
                                &nbsp;</a><asp:LinkButton CssClass="remColumn" ID="btnDeleteColumn" runat="server" meta:resourcekey="btnDeleteColumnResource1">
                                    
                                    <span class="targetedContentConditionDeleteIcon"></span>
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
        </div>
    </asp:View>
    <asp:View ID="uxUXView" runat="server">
        <div id="uxTargetedContentWrapper" runat="server">
            <div id="uxDropZone" runat="server" class="columns-container clearfix" data-ux-pagebuilder="DropZone">
                <asp:Repeater ID="uxRepColumns" runat="server" OnItemDataBound="repColumns_ItemDataBound">
                    <ItemTemplate>
                        <div data-ux-pagebuilder="Column"<%# GetColumnData(Container) %>>
                            <div class="ektron-ux-reset columnHeader" data-bind="event: {mouseover: mouseoverHeader, mouseout: mouseoutHeader }">
                                <h3 class="ux-app-siteApp-columnHeader" data-bind="click: select">
                                    <span class="columnLabel">
                                        <label><%# GetColumnLabel(Container) %></label>
                                        <asp:DropDownList ID="uxConditionPosition" runat="server" CssClass="ux-condition-change"></asp:DropDownList>
                                    </span>
                                    <span class="actions" data-bind="foreach: Actions">
                                        <!-- ko ifnot:Action.toLowerCase() === "movecolumn" -->
                                        <a data-bind="attr: {href: Href, title: Title, 'class': Action.toLowerCase()}, html: icon, click: fireAction">
                                                    
                                        </a>
                                        <!-- /ko -->
                                    </span>
                                </h3>
                            </div>
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
