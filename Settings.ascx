<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Settings.ascx.cs" Inherits="InnovAction.Modules.InnovActionAdvanceSQL.Settings" %>
<%@ Register TagName="label" TagPrefix="dnn" Src="~/controls/labelcontrol.ascx" %>

<style type="text/css">
    .auto-style2 {
        width: 120px;
    }
    .auto-style3 {
        width: 120px;
        height: 103px;
    }
    .auto-style4 {
        height: 103px;
    }
</style>

<h2 id="dnnSitePanel-BasicSettings" class="dnnFormSectionHead"><a href="" class="dnnSectionExpanded">Settings</a></h2>
<p class="dnnFormSectionHead"><span class="Head">Reemplazo de Tokens</span><br />
<span class="normal">
    TODO ES CASE SENSITIVE <br />
    
    [Query:Variable]<br />
    [Query:Variable:ValorDefault]<br />                  
    
    [DNN:UserID]<br />
    [DNN:TabID]<br />
    [DNN:ModuleID]<br />
    [DNN:PortalID]<br />
    [DNN:Username]<br />
    [DNN:DisplayName]<br />
    [DNN:Email]<br />
    [DNN:LastName]<br />
    [DNN:FirstName]<br />

    </span></p>



	 <p>
         <table class="Normal">
    <tr>
        <td class="auto-style3">
        <asp:Label ID="lblHeader" runat="server" Text="Query Header:"></asp:Label>
 
        </td>
        <td class="auto-style4">
 
       <asp:TextBox ID="Txt_HeaderQuery" runat="server" Height="90px" Width="609px" 
                TextMode="MultiLine" OnTextChanged="QuerySetting_TextChanged" BorderColor="Black"  BorderStyle="Solid" CssClass="NormalTextBox" />
        
        </td>
    </tr>
<tr>
    <td class="auto-style2">
         </td>
        <td>
        <asp:Label ID="Label1" runat="server"   Text="Este es el Repeater Body:"></asp:Label>
    </td>
    </tr>
     <tr>
        <td class="auto-style2">
        <asp:Label ID="lblBody" runat="server"   Text="Query   Body:"></asp:Label>
 
        </td>
        <td>
 
       <asp:TextBox ID="Txt_BodyQuery" runat="server" Height="90px" Width="609px" 
                TextMode="MultiLine" OnTextChanged="QuerySetting_TextChanged" BorderColor="Black" BorderStyle="Solid" CssClass="NormalTextBox" />
        
        </td>
    </tr>
                 <tr>
        <td class="auto-style2">
        <asp:Label ID="Label3" runat="server"   Text="Body Direction:"></asp:Label>
 
        </td>
        <td>
 
            <asp:RadioButtonList ID="RadioButtonDirection" runat="server" OnSelectedIndexChanged="RadioButtonDirection_SelectedIndexChanged" RepeatDirection="Horizontal">
                <asp:ListItem  Value="V">Vertical</asp:ListItem>
                <asp:ListItem Value="H">Horizontal</asp:ListItem>
            </asp:RadioButtonList>
        </td>
    </tr>
    <tr>
        <td class="auto-style2">
        <asp:Label ID="lblFooter" runat="server" Text="Query Footer:"></asp:Label>
 
        </td>
        <td>
 
       <asp:TextBox ID="Txt_FooterQuery" runat="server" Height="90px" Width="609px" 
                TextMode="MultiLine" OnTextChanged="QuerySetting_TextChanged" BorderColor="Black" BorderStyle="Solid" CssClass="NormalTextBox" />
        
        </td>
    </tr>
</table>
     <p>
         &nbsp;</p>
 
    <p>

        <asp:Label ID="Label2" runat="server" Text="Mostrar la Query"></asp:Label>
        <asp:CheckBox ID="CheckBoxMostrarQuery" runat="server" OnCheckedChanged="CheckBox1_CheckedChanged" />
&nbsp;
     </p>








