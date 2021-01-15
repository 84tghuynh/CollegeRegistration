<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="wfDrop.aspx.cs" Inherits="BITCollegeSite.wfDrop" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <p>
        &nbsp;</p>
    <p>
        &nbsp;</p>
    <p>
        <asp:DetailsView ID="dvRegistration" runat="server" AllowPaging="True" AutoGenerateRows="False" CellPadding="4" ForeColor="#333333" GridLines="None" Height="49px" OnPageIndexChanging="dvRegistration_PageIndexChanging" Width="375px">
            <AlternatingRowStyle BackColor="White" />
            <CommandRowStyle BackColor="#D1DDF1" Font-Bold="True" />
            <EditRowStyle BackColor="#2461BF" />
            <FieldHeaderStyle BackColor="#DEE8F5" Font-Bold="True" />
            <Fields>
                <asp:BoundField DataField="RegistrationNumber" HeaderText="Registration" />
                <asp:BoundField DataField="student.FullName" HeaderText="Student" />
                <asp:BoundField DataField="Course.Title" HeaderText="Course" />
                <asp:BoundField DataField="RegistrationDate" DataFormatString="{0:yyyy-MM-dd}" HeaderText="Date" />
                <asp:BoundField DataField="Grade" DataFormatString="{0:p2}" HeaderText="Grade" />
            </Fields>
            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
            <RowStyle BackColor="#EFF3FB" />
        </asp:DetailsView>
    </p>
    <p>
        &nbsp;</p>
    <p>
        <asp:LinkButton ID="lnkBtnDrop" runat="server" OnClick="lnkBtnDrop_Click">Drop Course</asp:LinkButton>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:LinkButton ID="lnkBtnRegistrationListing" runat="server" OnClick="lnkBtnRegistrationListing_Click">Return to Registration Listing</asp:LinkButton>
    </p>
    <p>
        &nbsp;</p>
    <p>
        <asp:Label ID="lblErrorDrop" runat="server" Text="lblErrorDrop" Visible="False" CssClass="alert-danger" Font-Italic="True" ForeColor="#FF3300"></asp:Label>
    </p>
    <p>
    </p>
</asp:Content>
