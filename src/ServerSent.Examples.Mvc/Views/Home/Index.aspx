<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Home Page
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: ViewBag.Message %></h2>
    <p>
        To learn more about ASP.NET MVC visit <a href="http://asp.net/mvc" title="ASP.NET MVC Website">http://asp.net/mvc</a>.
    </p>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="Scripts" runat="server">
<script>
    var source = new EventSource('/home/events');

    // new connection opened callback
    source.addEventListener('open', function(e) {
      console.log('connection opened');
    }, false);

    // subscribe to unnamed messages
    source.onmessage = function(e) {
      console.log(e);
      document.body.innerHTML += e.data + '<br />';
    };

    // listen for signup events
    source.addEventListener('signup', function(e) {
      console.log(e);
      document.body.innerHTML += e.data + '<br />';
    }, false);

    // connection closed callback
    source.addEventListener('error', function(e) {
      if (e.eventPhase == EventSource.CLOSED) {
        console.log('connection closed');
      }
    }, false);

  </script>
</asp:Content>
