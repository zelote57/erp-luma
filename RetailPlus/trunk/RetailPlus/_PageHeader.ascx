<%@ Control Language="c#" Inherits="AceSoft.RetailPlus.__PageHeader" Codebehind="_PageHeader.ascx.cs" %>
<%--<script language="JavaScript" src="/RetailPlus/_Scripts/disablerightclick.js"></script>--%>
<script language="JavaScript" src="/RetailPlus/_Scripts/sExpCollapse.js"></script>
<script type='text/javascript' src='/RetailPlus/_Scripts/ajax/jquery-1.6.2.js'></script>
<script type="text/javascript">
//  $(document).ready(function(){
//    $(':input').keypress(function(evt){
//      if(evt.keyCode == 13){
//        var fields = $(this).parents('form:eq(0),body').find('button,input,textarea,select');
//        var index = fields.index( this );
//        if ( index > -1 && ( index + 1 ) < fields.length ) {
//           fields.eq( index + 1 ).focus();
//        }
//        else {
//           fields.eq( 1 ).focus();
//        }
//        return false;
//      }
//    });
//  });
  $(document).ready(function(){
    $("input").not( $(":button") ).keypress(function (evt) {
      if (evt.keyCode == 13) {
        iname = $(this).val();
        if (iname !== 'Sign In'){    
          var fields = $(this).parents('form:eq(0),body').find('button,input,textarea,select');
          var index = fields.index( this );
          if ( index > -1 && ( index + 1 ) < fields.length ) {
            fields.eq( index + 1 ).focus();
          }
          return false;
        }
      }
    });
  });
</script>
<head><link rel="shortcut icon" href="/RetailPlus/_layouts/images/rbs.ico" /></head>
<table width="100%" border="0" cellspacing="5" cellpadding="0">
	<tr>
		<td><table cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td vAlign="center" noWrap align="left" width="12%"><A title="Site Logo" href="/RetailPlus" target="_self">
							<IMG alt="Site Logo" src="/RetailPlus/_layouts/images/company_logo.gif" border="0">
						</A>
					</td>
					<td vAlign="center" noWrap align="right"><A title="RetailPlus Business Solutions Logo" href="http://www.myRetailPlus.com" target="_blank">
							<IMG alt="RetailPlus Business Solutions Logo" src="/RetailPlus/_layouts/images/rbs_logo.gif" border="0">
						</A>
					</td>
					<!--td width="88%" align="left" vAlign="top" background="/RetailPlus/_layouts/images/top4_bg.jpg">
						<div align="right"><img src="/RetailPlus/_layouts/images/top4_right.jpg"></div>
					</td-->
				</tr>
			</table>
		</td>
	</tr>
</table>
