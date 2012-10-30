// This is the specific "sExpCollapse.js" file that should be renamed before being used in the builds of 

var popupWin;
function openWindow(url, example){	if (typeof(popupWin) != "object" || null == popupWin) 		popupWin = window.open(url, example, "width=452,height=572,top=0,left=0,alwaysRaised=yes,toolbar=0,directories=0,menubar=0,status=1,resizable=yes,location=0,scrollbars=1,copyhistory=0");	else		{		if (!popupWin.closed) 			popupWin.location.href = url;		else 			popupWin = window.open(url, example, "width=452,height=572,top=0,left=0,alwaysRaised=yes,toolbar=0,directories=0,menubar=0,status=1,resizable=yes,location=0,scrollbars=1,copyhistory=0");		}	  	popupWin.focus();}

function ExpandDiv(theDivName)
{
	InitializeGlobalData();

	if (null == theDivName || typeof(theDivName) == "undefined") return; var theDiv = allDivs[theDivName]; if (null == theDiv || typeof(theDiv) == "undefined") return;
	theDiv.style.display = "block";

	var thePic = allImages[theDivName + "_img"];
	if (null != thePic && typeof(thePic) != "undefined")
	{
		thePic.src = "/RetailPlus/_layouts/images/DLMIN.gif";
		thePic.alt = strHide;
	}
}

function CollapseDiv(theDivName)
{
	InitializeGlobalData();

	if (null == theDivName || typeof(theDivName) == "undefined") return; var theDiv = allDivs[theDivName]; if (null == theDiv || typeof(theDiv) == "undefined") return;
	theDiv.style.display = "none";

	var thePic = allImages[theDivName + "_img"];
	if (null != thePic && typeof(thePic) != "undefined")
	{
		thePic.src = "/RetailPlus/_layouts/images/DLMAX.gif";
		thePic.alt = strShow;
	}
}

function ToggleDiv(theDivName)
{
	InitializeGlobalData();

	if (null == theDivName || typeof(theDivName) == "undefined") return; var theDiv = allDivs[theDivName]; if (null == theDiv || typeof(theDiv) == "undefined") return;

	if (theDiv.style.display.toUpperCase() != "BLOCK")
		ExpandDiv(theDivName);
	else
		CollapseDiv(theDivName);
}

function AlterAllDivs(displayStyle)
{
	InitializeGlobalData();

	if (null == allDivs || typeof(allDivs) == "undefined")
		return;

	if (typeof(allDivs["divShowAll"]) != "undefined" &&
		typeof(allDivs["divHideAll"]) != "undefined")
		{
		if (displayStyle == "block")
			{
			allDivs["divShowAll"].style.display = "none";
			allDivs["divHideAll"].style.display = "block";
			}
		else
			{
			allDivs["divShowAll"].style.display = "block";
			allDivs["divHideAll"].style.display = "none";
			}
		}

	for (i=0; i < allDivs.length; i++)
		{
		if (0 == allDivs[i].id.indexOf("divExpCollAsst_")) 
			if (displayStyle == "block")
				ExpandDiv(allDivs[i].id);
			else
				CollapseDiv(allDivs[i].id);

		if (0 == allDivs[i].id.indexOf("divInlineDef_")) 
			if (displayStyle == "block")
				allDivs[i].style.display = "inline";
			else
				allDivs[i].style.display = "none";
		}
}

function ToggleAllDivs()
{
	InitializeGlobalData();

	if (fExpanded)
		AlterAllDivs("none");
	else
		AlterAllDivs("block");

	fExpanded = !fExpanded;
}

function ToggleAll()
{
	InitializeGlobalData();
	ToggleAllDivs();
}

function InitializeGlobalData()
{
	allDivs = document.body.getElementsByTagName("DIV");
	allImages = document.body.getElementsByTagName("IMG");
}

var allDivs = null;
var allImages = null;
var fExpanded = false;
var strShow = 'Show';
var strHide = 'Hide';