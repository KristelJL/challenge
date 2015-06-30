<%@ Page Title="Winners" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="About.aspx.cs" Inherits="RockPaperScissors.About" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <hgroup class="title">
        <h1><%: Title %>.</h1>
        <asp:Button ID ="reset" OnClick="reset_click" runat="server" Text="Reset Winners Table"  />
    </hgroup>
    <table>
        <tr>
            <td>
                <asp:GridView runat="server" ID="gridWinners" AutoGenerateColumns="false">
                    <Columns>
                        <asp:BoundField DataField="WinnerName" HeaderText="Name" />
                        <asp:BoundField DataField="WinnerScore" HeaderText="Points" />
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
    </table>
</asp:Content>