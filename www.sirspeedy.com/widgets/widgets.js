function GadgetEscapeHTML(id)
{
    var tbData = $ektron("#"+id);
    var result;
    if(tbData.length) {  
    result = tbData.val();
        // less-thans (<)
	    tbData.val(result);
    }    
  return true;
}



if( typeof Sys != "undefined" ){
   Sys.Application.notifyScriptLoaded();
}