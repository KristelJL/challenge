<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="RockPaperScissors._Default" %>

<asp:Content runat="server" ID="FeaturedContent" ContentPlaceHolderID="FeaturedContent">
    <section class="featured">
        <div class="content-wrapper">
            <hgroup class="title">
                <h1><%: Title %>.</h1>
            </hgroup>
            <p>
                To start playing upload the text file containing the list of players and their strategies
            </p>
        </div>
    </section>
</asp:Content>
<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <h3>Upload file</h3>
    <table>
        <tr>
            <td>
                <asp:FileUpload AllowMultiple="false" runat="server" ID="fileuploader"/>
            </td>
            <td>
                <asp:Button runat="server" ID="buttonOk" Text="Go" />
            </td>
        </tr>
    </table>
</asp:Content>
