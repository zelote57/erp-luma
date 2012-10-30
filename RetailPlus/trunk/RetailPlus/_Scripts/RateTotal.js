
function GetTotalRate()
{
	var total = 0;
	
	for (var i=0; i < document.forms[0].elements.length;i++)
		{							
			var e = document.forms[0].elements[i];						
			var stName = e.name;
			var iLen = stName.length;
			
			var stNewName = stName.substring(iLen-7,iLen);								
			if (stNewName=="txtBasicFreight")
			{
				total = total + document.forms[0].elements.value; 			
			}

		}
	document.txtTotal.defaultvalue = total;
}
	
//txtTotal.Text =	clsDetails.BasicFreight + clsDetails.VAS + clsDetails.Arbitrary + 
//							clsDetails.BAF_FAF + clsDetails.Inland + clsDetails.PSS + 
//							clsDetails.CurrencyAdjustment + clsDetails.GRI + clsDetails.Bunker + 
//							clsDetails.OtherCharges;
							
							