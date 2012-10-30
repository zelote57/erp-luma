
function NewWindow() {
    document.forms[0].target = "_blank";
}

function OpenProductsDialog()
{
	var stAppName = navigator.appName.toLowerCase();	
	if (stAppName == "microsoft internet explorer")
	{
		window.open("/retailplus/inventory/productsdialog.aspx","Redirector","toolbar=0,location=0,status=0,menubar=0");
		window.opener = self;
	}
}

function AllNum()
{
	if (window.event.keyCode != 46)	//46 is for .
	{
		if ((window.event.keyCode < 48) || (window.event.keyCode > 57))
			window.event.keyCode = 0;
	}
}

function ValidSearch()
{
	if (window.event.keyCode == 38)	//38 is for &
	{
		window.event.keyCode = 0;
	}
}

    
//JavaScript code to be use for modalPopUp. 
//Reference of Processing.ascx
Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(beginReq); 
Sys.WebForms.PageRequestManager.getInstance().add_endRequest(endReq);    
var timeOut
function beginReq(sender, args) {
   openModal();
   // timeOut =  setTimeout('openModal()', 750);
}

function openModal() {
   $find(ModalProgress).show();
}

function endReq(sender, args) {
   //  shows the Popup
   // clearTimeout(timeOut);
   $find(ModalProgress).hide();
}