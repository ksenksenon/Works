<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="CulturesWebForms._Default" %>

<asp:Content runat="server" ID="FeaturedContent" ContentPlaceHolderID="FeaturedContent">
    <section class="featured">
        <div class="content-wrapper">
            <hgroup class="title">
                <h1><%: Title %>.</h1>
                <h2>Modify this template to jump-start your ASP.NET application.</h2>
            </hgroup>
            <p>
                To learn more about ASP.NET, visit <a href="http://asp.net" title="ASP.NET Website">http://asp.net</a>.
                The page features <mark>videos, tutorials, and samples</mark> to help you get the most from ASP.NET.
                If you have any questions about ASP.NET visit
                <a href="http://forums.asp.net/18.aspx" title="ASP.NET Forum">our forums</a>.
            </p>
        </div>
    </section>
</asp:Content>
<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <asp:ListBox runat="server" ID="CulturesListBox" Width="500px" Height="300px"/>
    <div style="float: right;">
        <p><span>Date</span></p>
        <p><input type="text" ID="DateTextBox" value="" /></p>
        <p><span>Size</span></p>
        <p><input type="text" ID="SizeTextBox" /> </p>
    </div>
    <h3>We suggest the following:</h3>
    <ol class="round">
        <li class="one">
            <h5>Getting Started</h5>
            ASP.NET Web Forms lets you build dynamic websites using a familiar drag-and-drop, event-driven model.
            A design surface and hundreds of controls and components let you rapidly build sophisticated, powerful UI-driven sites with data access.
            <a href="http://go.microsoft.com/fwlink/?LinkId=245146">Learn more…</a>
        </li>
        <li class="two">
            <h5>Add NuGet packages and jump-start your coding</h5>
            NuGet makes it easy to install and update free libraries and tools.
            <a href="http://go.microsoft.com/fwlink/?LinkId=245147">Learn more…</a>
        </li>
        <li class="three">
            <h5>Find Web Hosting</h5>
            You can easily find a web hosting company that offers the right mix of features and price for your applications.
            <a href="http://go.microsoft.com/fwlink/?LinkId=245143">Learn more…</a>
        </li>
    </ol>
    <script type="text/javascript">
        document.addEventListener("DOMContentLoaded", function () {
            var listBox = document.getElementById("MainContent_CulturesListBox");
            listBox.addEventListener("change", function () {
                var element = listBox.item(listBox.selectedIndex);

                // 1. Создаём новый объект XMLHttpRequest
                var xhr = getXmlHttp();

                // 2. Конфигурируем его: GET-запрос на URL 'phones.json'
                xhr.open("GET", "/worker.axd?culture=" + element.value, true);

                // 3. Отсылаем запрос
                xhr.send();

                xhr.onreadystatechange = function () { // (3)
                    if (xhr.readyState != 4)
                        return;
                    if (xhr.status != 200) {
                        alert(xhr.status + ': ' + xhr.statusText);
                    } else {
                        var result = JSON.parse(xhr.responseText);
                        var dateTextBox = document.getElementById("DateTextBox");
                        dateTextBox.value = result.date;
                        var sizeTextBox = document.getElementById("SizeTextBox");
                        sizeTextBox.value = result.size;
                    }
                }
            });
        });
        function getXmlHttp() {
            var xmlhttp;
            try {
                xmlhttp = new ActiveXObject("Msxml2.XMLHTTP");
            } catch (e) {
                try {
                    xmlhttp = new ActiveXObject("Microsoft.XMLHTTP");
                } catch (E) {
                    xmlhttp = false;
                }
            }
            if (!xmlhttp && typeof XMLHttpRequest != 'undefined') {
                xmlhttp = new XMLHttpRequest();
            }
            return xmlhttp;
        }
    </script>
</asp:Content>
