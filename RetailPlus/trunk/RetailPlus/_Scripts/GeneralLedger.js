
function ComputeOpeningBalance()
{
	var openingbalance = 0;
	var debit = 0;
	var credit = 0;
	var currentbalance = true;
	
	openingbalance = document.getElementById("ctrlInsert_txtOpeningBalance").value.replace(/\,/g, '');
	debit = document.getElementById("ctrlInsert_txtDebit").value.replace(/\,/g, '');
	credit = document.getElementById("ctrlInsert_txtCredit").value.replace(/\,/g, '');
	
	currentbalance = openingbalance + debit - credit
	
	document.getElementById("ctrlInsert_txtCurrentBalance").value = currentbalance.toFixed(3);
}
