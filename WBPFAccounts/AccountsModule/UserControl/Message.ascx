<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Message.ascx.cs" Inherits="CollegeERP.UserControl.Message" %>
<div id="divsuccess" class="message-success" runat="server">
		<div class="image">
			<img src="/Images/success.png" alt="Success" height="28" />
		</div>
		<div class="text">
			<span id="SpanSuccessText" runat="server"></span>
		</div>
		
</div>

<div id="diverror" class="message-error" runat="server">
		<div class="image">
			<img src="/Images/error.png" alt="Error" height="28" />
		</div>
		<div class="text">
			<span id="SpanErrorText" runat="server"></span>
		</div>	
</div>