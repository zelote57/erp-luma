<%@ Application Language="C#" %>

<script runat="server">

    void Application_Start(object sender, EventArgs e) 
    {
        // Code that runs on application startup

    }
    
    void Application_End(object sender, EventArgs e) 
    {
        //  Code that runs on application shutdown

    }
        
    void Application_Error(object sender, EventArgs e) 
    {
        try
        {
            if (Session != null)
            {
                // Code that runs when an unhandled error occurs
                Exception lastError = Server.GetLastError();

                if (lastError != null)
                {
                    lastError = lastError.InnerException;
                    if (lastError.Message.IndexOf("Cannot delete or update a parent row: a foreign key constraint fails") != -1)
                    {
                        Session["ErrorMessage"] = "This record is currently in use and cannot be deleted.";

                        Session["ErrorMessage"] = Session["ErrorMessage"].ToString() + "<br/></br>&nbsp;<b>Actual Error :</b>" + lastError.Message;
                    }
                    else
                    {
                        Session["ErrorMessage"] = lastError.Message;
                    }
                    Session["ErrorSource"] = lastError.Source;
                    Session["ErrorExceptionType"] = lastError.GetType().ToString();
                    Session["ErrorStackTrace"] = lastError.StackTrace;
                }

                Response.Redirect(AceSoft.RetailPlus.Constants.ROOT_DIRECTORY + "/GenericError.aspx");
            }
        }
        catch { }
    }

    void Session_Start(object sender, EventArgs e) 
    {
        // Code that runs when a new session is started

        Session["ErrorCurrentExecutionFilePath"] = Request.CurrentExecutionFilePath;
        Session["ErrMessage"] = null;
        Session["ErrorMessage"] = null;
        Session["ErrorSource"] = null;
        Session["ErrorExceptionType"] = null;
        Session["ErrorStackTrace"] = null;
    }

    void Session_End(object sender, EventArgs e) 
    {
        // Code that runs when a session ends. 
        // Note: The Session_End event is raised only when the sessionstate mode
        // is set to InProc in the Web.config file. If session mode is set to StateServer 
        // or SQLServer, the event is not raised.

    }
       
</script>
