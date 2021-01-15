<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="wfRegister.aspx.cs" Inherits="BITCollegeSite.wfRegister" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <p>
        <br />
        <asp:Label ID="lblRegStudentName" runat="server" Text="StudentName" Font-Bold="True"></asp:Label>
    </p>
    <p>
        <asp:Label ID="lblCourseSelector" runat="server" Text="Course Selector: "></asp:Label>
        <asp:DropDownList ID="ddlCourse" runat="server" AutoPostBack="True" Height="22px" Width="148px">
        </asp:DropDownList>
    </p>
    <p>
        <asp:Label ID="lblNote" runat="server" Text="Registration Notes:"></asp:Label>
        <asp:TextBox ID="txtNote" runat="server" Width="230px"></asp:TextBox>
        <asp:RequiredFieldValidator ID="requiredValidationNote" runat="server" ControlToValidate="txtNote" ErrorMessage="Notes are required for Web Registrations"></asp:RequiredFieldValidator>
    </p>
    <p>
&nbsp;<asp:LinkButton ID="lnkBtnRegister" runat="server" Font-Underline="True" OnClick="lnkBtnRegister_Click">Register</asp:LinkButton>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:LinkButton ID="lnkBtnReturnRegistrationList" runat="server" CausesValidation="False" OnClick="lnkBtnReturnRegistrationList_Click">Return to Registration Listing</asp:LinkButton>
    </p>
    <p>
        &nbsp;</p>
    <p>
        <asp:Label ID="lblErrorRegister" runat="server" Text="lblErrorRegister" Visible="False" CssClass="alert-danger"></asp:Label>
    </p>
</asp:Content>
