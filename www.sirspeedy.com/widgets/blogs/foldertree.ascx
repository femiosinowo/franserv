<%@ Control Language="C#" AutoEventWireup="true" CodeFile="foldertree.ascx.cs" Inherits="Blogs_Widgets_pagebuilder_foldertree" %>
<div class="LSfoldercontainer" >
    <span class="folder" data-ektron-folid="0"><%=m_refMsg.GetMessage ("lbl root")%></span>
    <ul class="EktronFolderTree EktronFiletree" data-ektron-folid="0"></ul>
</div>
