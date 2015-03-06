
function InsertComputeMargin()
{
	var margin = 0;
	var purchaseprice = 0;
	var saleprice = 0;
	
	purchaseprice = document.getElementById("ctrlInsert_txtPurchasePrice").value;
	margin = document.getElementById("ctrlInsert_txtMargin").value;
	margin = margin / 100;
	margin = purchaseprice * margin;
	saleprice = +purchaseprice + +margin;
	document.getElementById("ctrlInsert_txtProductPrice").value = saleprice.toFixed(3);
    try {
	    purchaseprice = document.getElementById("ctrlInsert_txtPurchasePrice").value;
	    margin = document.getElementById("ctrlInsert_txtWSPriceMarkUp").value;
	    margin = margin / 100;
	    margin = purchaseprice * margin;
	    saleprice = +purchaseprice + +margin;
	    document.getElementById("ctrlInsert_txtWSPrice").value = saleprice.toFixed(3);
	}
	catch (e) { }
}

function InsertComputeMarginByPrice()
{
	var margin = 0;
	var purchaseprice = 0;
	var saleprice = 0;
	
	saleprice = document.getElementById("ctrlInsert_txtProductPrice").value;
	purchaseprice = document.getElementById("ctrlInsert_txtPurchasePrice").value;
	margin = saleprice - purchaseprice;
	margin = margin / purchaseprice;
	margin = margin * 100;
	document.getElementById("ctrlInsert_txtMargin").value = margin.toFixed(3);
	try {
	    saleprice = document.getElementById("ctrlInsert_txtWSPrice").value;
	    purchaseprice = document.getElementById("ctrlInsert_txtPurchasePrice").value;
	    margin = saleprice - purchaseprice;
	    margin = margin / purchaseprice;
	    margin = margin * 100;
	    document.getElementById("ctrlInsert_txtWSPriceMarkUp").value = margin.toFixed(3);
	}
	catch (e) { }
	
}

function UpdateComputeMargin()
{
	var margin = 0;
	var purchaseprice = 0;
	var saleprice = 0;
	
	purchaseprice = document.getElementById("ctrlUpdate_txtPurchasePrice").value;
	margin = document.getElementById("ctrlUpdate_txtMargin").value;
	margin = margin / 100;
	margin = purchaseprice * margin;
	saleprice = +purchaseprice + +margin;
	document.getElementById("ctrlUpdate_txtProductPrice").value = saleprice.toFixed(3);
	try {
	    purchaseprice = document.getElementById("ctrlUpdate_txtPurchasePrice").value;
	    margin = document.getElementById("ctrlUpdate_txtWSPriceMarkUp").value;
	    margin = margin / 100;
	    margin = purchaseprice * margin;
	    saleprice = +purchaseprice + +margin;
	    document.getElementById("ctrlUpdate_txtWSPrice").value = saleprice.toFixed(3);
	}
	catch (e) { }
}

function UpdateComputeMarginByPrice()
{
	var margin = 0;
	var purchaseprice = 0;
	var saleprice = 0;
	
	saleprice = document.getElementById("ctrlUpdate_txtProductPrice").value;
	purchaseprice = document.getElementById("ctrlUpdate_txtPurchasePrice").value;
	margin = saleprice - purchaseprice;
	margin = margin / purchaseprice;
	margin = margin * 100;
	document.getElementById("ctrlUpdate_txtMargin").value = margin.toFixed(3);
	try {
	    saleprice = document.getElementById("ctrlUpdate_txtWSPrice").value;
	    purchaseprice = document.getElementById("ctrlUpdate_txtPurchasePrice").value;
	    margin = saleprice - purchaseprice;
	    margin = margin / purchaseprice;
	    margin = margin * 100;
	    document.getElementById("ctrlUpdate_txtWSPriceMarkUp").value = margin.toFixed(3);
	}
	catch (e) { }
}

function ChangePriceComputeMarginPP(obj)
{
	var margin = 0; 
	var purchaseprice = 0; 
	var saleprice = 0;
	
	var txtSellingPrice;  
	var txtPurchasePrice; 
	var txtMargin;
	var txtWSPriceMarkUp;
	var txtWSPrice;
	
	var row = obj.parentNode.parentNode;
    var tb = row.getElementsByTagName("input");
	for (var i=0;i<tb.length;i++)
    {
        if (tb[i].type=="text")
        {
            // txtSellingPrice
            if (tb[i].id.indexOf("txtSellingPrice")!=-1)
            {   txtSellingPrice = tb[i];    }
            
            // txtPurchasePrice
            if (tb[i].id.indexOf("txtPurchasePrice")!=-1)
            {   txtPurchasePrice = tb[i];    }
            
            // txtPrice -- Purchase price for PO
            if (tb[i].id.indexOf("txtPrice")!=-1)
            {   txtPrice = tb[i]; }
            
            // txtMargin
            if (tb[i].id.indexOf("txtMargin")!=-1)
            {   txtMargin = tb[i];    }
            
            // txtWSPriceMarkUp
            if (tb[i].id.indexOf("txtWSPriceMarkUp")!=-1)
            {   txtWSPriceMarkUp = tb[i];    }
            
            // txtWSPrice
            if (tb[i].id.indexOf("txtWSPrice")!=-1)
            {   txtWSPrice = tb[i];    }
        }
    }

    purchaseprice = txtPurchasePrice.value;
	margin = txtMargin.value;
	margin = margin / 100;
	margin = purchaseprice * margin;
	saleprice = +purchaseprice + +margin;
	txtSellingPrice.value = saleprice.toFixed(2);
	
	purchaseprice = txtPurchasePrice.value;
	margin = txtWSPriceMarkUp.value;
	margin = margin / 100;
	margin = purchaseprice * margin;
	saleprice = +purchaseprice + +margin;
	txtWSPrice.value = saleprice.toFixed(2);
}

function ChangePriceComputeMarginByPricePP(obj)
{
	var margin = 0; 
	var purchaseprice = 0; 
	var saleprice = 0;

	var txtPurchasePrice;
	var txtMargin;
	var txtSellingPrice;  
	
	var txtWSPriceMarkUp;
	var txtWSPrice;

	var row = obj.parentNode.parentNode;
    var tb = row.getElementsByTagName("input");
	for (var i=0;i<tb.length;i++)
    {
        if (tb[i].type=="text")
        {
            // txtSellingPrice
            if (tb[i].id.indexOf("txtSellingPrice") != -1)
            { txtSellingPrice = tb[i]; }

            // txtPurchasePrice
            if (tb[i].id.indexOf("txtPurchasePrice") != -1)
            { txtPurchasePrice = tb[i]; }

            // txtPrice -- Purchase price for PO
            if (tb[i].id.indexOf("txtPrice") != -1)
            { txtPrice = tb[i]; }

            // txtMargin
            if (tb[i].id.indexOf("txtMargin") != -1)
            { txtMargin = tb[i]; }

            // txtWSPriceMarkUp
            if (tb[i].id.indexOf("txtWSPriceMarkUp") != -1)
            { txtWSPriceMarkUp = tb[i]; }

            // txtWSPrice
            if (tb[i].id.indexOf("txtWSPrice") != -1)
            { txtWSPrice = tb[i]; }
        }
    }
	
	saleprice = txtSellingPrice.value;
	purchaseprice = txtPurchasePrice.value;
	margin = saleprice - purchaseprice;
	margin = margin / purchaseprice;
	margin = margin * 100;
	txtMargin.value = margin.toFixed(3);

	saleprice = txtWSPrice.value;
	purchaseprice = txtPurchasePrice.value;
	margin = saleprice - purchaseprice;
	margin = margin / purchaseprice;
	margin = margin * 100;
	txtWSPriceMarkUp.value = margin.toFixed(3);
}

function ChangePriceComputeMarginMP(obj)
{
	var margin = 0; 
	var purchaseprice = 0; 
	var saleprice = 0;
	
	var txtSellingPrice;  
	var txtPurchasePrice; 
	var txtMargin;
	var txtWSPriceMarkUp;
	var txtWSPrice;
	
	var row = obj.parentNode.parentNode;
    var tb = row.getElementsByTagName("input");
	for (var i=0;i<tb.length;i++)
    {
        if (tb[i].type=="text")
        {
            // txtSellingPrice
            if (tb[i].id.indexOf("txtSellingPrice")!=-1)
            {   txtSellingPrice = tb[i];    }
            
            // txtPurchasePrice
            if (tb[i].id.indexOf("txtPurchasePrice")!=-1)
            {   txtPurchasePrice = tb[i];    
                purchaseprice = txtPurchasePrice.value;
            }
            
            // txtPrice -- Purchase price for PO
            if (tb[i].id.indexOf("txtPrice")!=-1)
            {   txtPrice = tb[i];  }
            
            // txtMargin
            if (tb[i].id.indexOf("txtMargin")!=-1)
            {   txtMargin = tb[i];    }
            
            // txtWSPriceMarkUp
            if (tb[i].id.indexOf("txtWSPriceMarkUp")!=-1)
            {   txtWSPriceMarkUp = tb[i];    }
            
            // txtWSPrice
            if (tb[i].id.indexOf("txtWSPrice")!=-1)
            {   txtWSPrice = tb[i];    }
        }
    }
	
	
	margin = txtMargin.value;
	margin = margin / 100;
	margin = purchaseprice * margin;
	saleprice = +purchaseprice + +margin;
	txtSellingPrice.value = saleprice.toFixed(3);
	
	margin = txtWSPriceMarkUp.value;
	margin = margin / 100;
	margin = purchaseprice * margin;
	saleprice = +purchaseprice + +margin;
	txtWSPrice.value = saleprice.toFixed(3);
}

function ChangePriceComputeMarginByPriceMP(obj)
{
	var margin = 0; 
	var purchaseprice = 0; 
	var saleprice = 0;
	
	var txtSellingPrice;  
	var txtPurchasePrice; 
	var txtMargin;
	var txtWSPriceMarkUp;
	var txtWSPrice;
	
	var row = obj.parentNode.parentNode;
    var tb = row.getElementsByTagName("input");
	for (var i=0;i<tb.length;i++)
    {
        if (tb[i].type=="text")
        {
            // txtSellingPrice
            if (tb[i].id.indexOf("txtSellingPrice")!=-1)
            {   txtSellingPrice = tb[i];    }
            
            // txtPurchasePrice
            if (tb[i].id.indexOf("txtPurchasePrice")!=-1)
            {   txtPurchasePrice = tb[i];    }
            
            // txtPrice -- Purchase price for PO
            if (tb[i].id.indexOf("txtPrice")!=-1)
            {   txtPrice = tb[i]; }
            
            // txtMargin
            if (tb[i].id.indexOf("txtMargin")!=-1)
            {   txtMargin = tb[i];    }
            
            // txtWSPriceMarkUp
            if (tb[i].id.indexOf("txtWSPriceMarkUp")!=-1)
            {   txtWSPriceMarkUp = tb[i];    }
            
            // txtWSPrice
            if (tb[i].id.indexOf("txtWSPrice")!=-1)
            {   txtWSPrice = tb[i];    }
        }
    }
    
	saleprice = txtSellingPrice.value;
	purchaseprice = txtPurchasePrice.value;
	margin = saleprice - purchaseprice;
	margin = margin / purchaseprice;
	margin = margin * 100;
	txtMargin.value = margin.toFixed(3);
	
	saleprice = txtWSPrice.value;
	purchaseprice = txtPurchasePrice.value;
	margin = saleprice - purchaseprice;
	margin = margin / purchaseprice;
	margin = margin * 100;
	txtWSPriceMarkUp.value = margin.toFixed(3);
}

function InvAdjustmentComputeByDiff()
{
	var intDifference = 0; 
	var intQuantityBefore = 0; 
	var intQuantityNow = 0;

	intQuantityBefore = document.getElementById("ctrlInvAdjustment_txtQuantityBefore").value;
	intDifference = document.getElementById("ctrlInvAdjustment_txtDifference").value;
	intDifference = +intQuantityBefore + +intDifference;
	document.getElementById("ctrlInvAdjustment_txtQuantityNow").value = intDifference.toFixed(3);
}

function InvAdjustmentComputeByQty()
{
	var intDifference = 0; 
	var intQuantityBefore = 0; 
	var intQuantityNow = 0;

	intQuantityBefore = document.getElementById("ctrlInvAdjustment_txtQuantityBefore").value;
	intQuantityNow = document.getElementById("ctrlInvAdjustment_txtQuantityNow").value;
	intDifference = +intQuantityNow - +intQuantityBefore;
	document.getElementById("ctrlInvAdjustment_txtDifference").value = intDifference.toFixed(3);
}

function InvAdjustmentComputeMatrixByDiff(obj)
{
	var intDifference = 0; 
	var intQuantityBefore = 0; 
	var intQuantityNow = 0;
	
	var txtQuantityBefore;  
	var txtDifference; 
	var txtQuantityNow;
	
	var row = obj.parentNode.parentNode;
    var tb = row.getElementsByTagName("input");
	for (var i=0;i<tb.length;i++)
    {
        if (tb[i].type=="text")
        {
            // txtQuantityBefore
            if (tb[i].id.indexOf("txtQuantityBefore")!=-1)
            {   txtQuantityBefore = tb[i];      }
            
            // txtDifference
            if (tb[i].id.indexOf("txtDifference")!=-1)
            {   txtDifference = tb[i];    }
            
            // txtQuantityNow
            if (tb[i].id.indexOf("txtQuantityNow")!=-1)
            {   txtQuantityNow = tb[i];    }
        }
    }
    
	intQuantityBefore = txtQuantityBefore.value;
	intDifference = txtDifference.value;
	intDifference = +intQuantityBefore + +intDifference;
	txtQuantityNow.value = intDifference.toFixed(3);
}

function InvAdjustmentComputeMatrixByQty(obj)
{
	var intDifference = 0; 
	var intQuantityBefore = 0; 
	var intQuantityNow = 0;
	
	var txtQuantityBefore;  
	var txtDifference; 
	var txtQuantityNow;
	
	var row = obj.parentNode.parentNode;
    var tb = row.getElementsByTagName("input");
	for (var i=0;i<tb.length;i++)
    {
        if (tb[i].type=="text")
        {
            // txtQuantityBefore
            if (tb[i].id.indexOf("txtQuantityBefore")!=-1)
            {   txtQuantityBefore = tb[i];     }
            
            // txtDifference
            if (tb[i].id.indexOf("txtDifference")!=-1)
            {   txtDifference = tb[i];    }
            
            // txtQuantityNow
            if (tb[i].id.indexOf("txtQuantityNow")!=-1)
            {   txtQuantityNow = tb[i];    }
        }
    }
    
	intQuantityBefore = txtQuantityBefore.value;
	intQuantityNow = txtQuantityNow.value;
	intDifference = +intQuantityNow - +intQuantityBefore;
	txtDifference.value = intDifference.toFixed(3);
}

function getVal(obj)
{
    var row = obj.parentNode.parentNode;

    var tb = row.getElementsByTagName("input");
    for (var i=0;i<tb.length;i++)
    {
        //TextBox
        if (tb[i].type=="text")
        {
            alert(tb[i].id);
        }
    }
}

/*
for (var i=0; i < document.forms[0].elements.length;i++)
	{							
		var e = document.forms[0].elements[i];						
		var stName = e.name;
		var iLen = stName.length;
		
		var stNewName = stName.substring(iLen-7,iLen);								
		alert("control: " + stName);	

	}
*/	




function ChangePriceComputeMarginPPPO(obj)
{
	var margin = 0; 
	var purchaseprice = 0; 
	var saleprice = 0;
	
	purchaseprice = document.getElementById("ctrlPost_txtPrice").value;
	margin = document.getElementById("ctrlPost_txtMargin").value;
	margin = margin / 100;
	margin = purchaseprice * margin;
	saleprice = +purchaseprice + +margin;
	document.getElementById("ctrlPost_txtSellingPrice").value = saleprice.toFixed(3);
}

function ChangePriceComputeMarginByPriceMPPO(obj)
{
	var margin = 0; 
	var purchaseprice = 0; 
	var saleprice = 0;
	
	saleprice = document.getElementById("ctrlPost_txtSellingPrice").value;
	purchaseprice = document.getElementById("ctrlPost_txtPrice").value;
	margin = saleprice - purchaseprice;
	margin = margin / purchaseprice;
	margin = margin * 100;
	document.getElementById("ctrlPost_txtMargin").value = margin.toFixed(3);
}

function ChangePriceComputeMarginMPPO(obj)
{
	var margin = 0; 
	var purchaseprice = 0; 
	var saleprice = 0;
	
	purchaseprice = document.getElementById("ctrlPost_txtPrice").value;
	margin = document.getElementById("ctrlPost_txtMargin").value;
	margin = margin / 100;
	margin = purchaseprice * margin;
	saleprice = +purchaseprice + +margin;
	document.getElementById("ctrlPost_txtSellingPrice").value = saleprice.toFixed(3);
	
	ComputeAmountPost();
}
	
