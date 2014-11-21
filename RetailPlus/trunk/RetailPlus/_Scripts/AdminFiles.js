function confirm_reset_password() {
    if (confirm("Are you sure you want to reset the password of the user?") == true)
    { return true; }
    else
    { return false; }
}
