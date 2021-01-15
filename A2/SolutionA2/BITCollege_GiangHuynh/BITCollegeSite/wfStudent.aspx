<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="wfStudent.aspx.cs" Inherits="BITCollegeSite.wfStudent" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <br />
    <br />
    <asp:Label ID="lblStudentName" runat="server" Font-Bold="True" Text="StudentName"></asp:Label>
    <br />
    <asp:GridView ID="gvCourses" runat="server" AutoGenerateColumns="False" AutoGenerateSelectButton="True" Height="115px" OnSelectedIndexChanged="gvCourses_SelectedIndexChanged" Width="587px">
        <Columns>
            <asp:BoundField DataField="CourseNumber" HeaderText="Course" />
            <asp:BoundField DataField="Title" HeaderText="Title" />
            <asp:BoundField DataField="CreditHours" HeaderText="Credit Hours">
            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
            </asp:BoundField>
            <asp:BoundField DataField="CourseType" HeaderText="Course Type" />
            <asp:BoundField DataField="TuitionAmount" DataFormatString="{0:c2}" HeaderText="Tuition" >
            <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" />
            </asp:BoundField>
        </Columns>
    </asp:GridView>
    <br />
    <asp:LinkButton ID="lnkBtnRegisterCourse" runat="server" OnClick="lnkBtnRegisterCourse_Click">Click Here to Register for a Course</asp:LinkButton>
    <br />
    <br />
    <br />
    <asp:Label ID="lblErrorStudent" runat="server" Text="lblErrorStudent" Visible="False"></asp:Label>
    <br />
    <br />
</asp:Content>
