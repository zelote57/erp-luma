function confirm_print_billing() {
    if (WithChecked()) {
        if (confirm("Please make sure that you have a default printer & adobe reader installed. \n     Are you sure you want to print the latest bill for the selected customers?") == true)
            return true;
        else
            return false;
    }
    else {
        alert("Please select at least one record to print the billing.");
        return false;
    }
}

function confirm_changecreditcardtype() {
    if (confirm("Are you sure you want to change the credit card type?") == true)
    { return true; }
    else
    { return false; }
}


function confirm_changeguarantor() {
    if (confirm("Are you sure you want to change the Guarantor?") == true)
    { return true; }
    else
    { return false; }
}