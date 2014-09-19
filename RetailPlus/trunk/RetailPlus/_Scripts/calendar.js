function ontime(name)
{
	window.dateField =name;
	var w = window.screen.width / 2 - 160;
	var h = window.screen.height / 2 - 120;
	calendar = window.showModalDialog("/retailplus/_scripts/calendar.htm", this, "dialogWidth:340px;dialogHeight:275px;dialogLeft:200;dialogTop:120;status:0;location:0;toolbar:0;menubar:0;resizable:0;scrollbars:no");
}
