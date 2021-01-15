<%@ Page Title="Contact" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Contact.aspx.cs" Inherits="BITCollegeSite.Contact" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: Title %>.</h2>
    <h3>Red River College.</h3>
    <address>
        BIT Program<br />
        160, Princess, Winnipeg, MB<br />
        <abbr title="Phone">P:</abbr>
        425.555.0100
    </address>

    <address>
        <strong>Support:</strong>   <a href="mailto:Students@rrc.ca">Students@rrc.ca</a><br />
        <strong>Marketing:</strong> <a href="mailto:Marketing@example.com">Marketing@rrc.ca</a>
    </address>
</asp:Content>
