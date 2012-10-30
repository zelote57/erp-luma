
function ComputeOpeningBalance()
{
	var openingbalance = 0;
	var debit = 0;
	var credit = 0;
	var currentbalance = true;
	
	openingbalance = document.getElementById("ctrlInsert_txtOpeningBalance").value;
	debit = document.getElementById("ctrlInsert_txtDebit").value;
	credit = document.getElementById("ctrlInsert_txtCredit").value;
	
	currentbalance = openingbalance + debit - credit
	
	document.getElementById("ctrlInsert_txtCurrentBalance").value = currentbalance.toFixed(3);
}
