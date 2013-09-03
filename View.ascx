<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="View.ascx.cs" Inherits="InnovAction.Modules.InnovActionAdvanceSQL.View" %>
<asp:Literal ID="LiteralHeader" runat="server"></asp:Literal>
<asp:Literal ID="LiteralBody" runat="server" ></asp:Literal>
<asp:DataList ID="DataListBody" runat="server" OnSelectedIndexChanged="DataListBody_SelectedIndexChanged">
<ItemTemplate>
<asp:Literal ID="LiteralRepeater" runat="server" text='<%#((DataRowView)Container.DataItem)[0] %>'  />
</ItemTemplate>
</asp:DataList>
<asp:Literal ID="LiteralFooter" runat="server"></asp:Literal>
<asp:Literal ID="LiteralQuery" runat="server" Visible="False"></asp:Literal>
<asp:Literal ID="LiteralException" runat="server" Visible="False"></asp:Literal>






