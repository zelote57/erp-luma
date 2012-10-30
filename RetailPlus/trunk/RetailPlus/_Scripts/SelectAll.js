function SelectAll()
{			
	for (var i=0; i < document.forms[0].elements.length;i++)
	{							
		var e = document.forms[0].elements[i];						
		var stName = e.name;
		var iLen = stName.length;
		
		var stNewName = stName.substring(iLen-7,iLen);								
		if (stNewName == "chkList")
			if (e.disabled == false)				
				e.checked = document.forms[0].elements["selectall"].checked;
	}
}
function SelectAllRead()
{			
	for (var i=0; i < document.forms[0].elements.length;i++)
	{							
		var e = document.forms[0].elements[i];						
		var stName = e.name;
		var iLen = stName.length;
		
		var stNewName = stName.substring(iLen-7,iLen);								
		if (stNewName == "chkRead")						
			e.checked = document.forms[0].elements["selectallread"].checked;
	}
}
function SelectAllWrite()
{			
	for (var i=0; i < document.forms[0].elements.length;i++)
	{							
		var e = document.forms[0].elements[i];						
		var stName = e.name;
		var iLen = stName.length;
		
		var stNewName = stName.substring(iLen-8,iLen);								
		if (stNewName == "chkWrite")						
			e.checked = document.forms[0].elements["selectallwrite"].checked;
	}
}