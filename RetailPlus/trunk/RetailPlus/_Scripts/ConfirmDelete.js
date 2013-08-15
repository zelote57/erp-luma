
function confirm_zeroout_inventory()
{
	if (confirm("Are you sure you want to update the quantity of all Products to ZERO (0)?")==true)
	{	return true;    }
	else
	{	return false;    }
}

function confirm_close_inventory()
{
	if (confirm("Please make sure that you save the actual quantity first. \n     Are you sure you want to Close the inventory?")==true)
	{	return true;    }
	else
	{	return false;    }
}

function confirm_item_delete()
{
	if (confirm("Are you sure you want to delete this item?")==true)
	{	return true;    }
	else
	{	return false;    }
}

function confirm_delete()
{
	if (WithChecked())
	{
		if (confirm("Are you sure you want to delete this item?")==true)
			return true;
		else
			return false;
	}
	else
	{
		alert("Please select at least one record to delete.");
		return false;
	}
}

function confirm_cancel()
{
	if (WithChecked())
	{
		if (confirm("Are you sure you want to cancel this item?")==true)
			return true;
		else
			return false;
	}
	else
	{
		alert("Please select at least one record to cancel.");
		return false;
	}
}

function confirm_select()
{
	if (!WithChecked())
	{
		alert("Please select at least one record.");
		return false;
	}
}

function WithChecked()
{
	for (var i=0; i < document.forms[0].elements.length;i++)
	{							
		var e = document.forms[0].elements[i];						
		var stName = e.name;
		var iLen = stName.length;
		
		var stNewName = stName.substring(iLen-7,iLen);								
		if (stNewName == "chkList")		
		{
			if (e.checked == true)
			{return true;}
		}
	}
	
	return false;
}