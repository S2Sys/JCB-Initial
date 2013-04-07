
function PrintMe(ctl) {
    
    var PGrid = document.getElementById(ctl);
    
    PGrid.border = 0;
    var Pwin = window.open('', 'PrintMe', 'letf=0,top=0,toolbar=0,scrollbars=0,status=0,width=0,height=0');
    //window.open('', 'PrintGrid', 'left=100,top=100,width=1024,height=768,tollbar=0,scrollbars=1,status=0,resizable=1');
    Pwin.document.write("<link href='Print.css' rel='stylesheet' type='text/css' />");
    Pwin.document.write("<div style='width:400px;'>");
    Pwin.document.write(PGrid.outerHTML);
    Pwin.document.write("</div>");
    Pwin.document.close();
    Pwin.focus();
    Pwin.print();
    Pwin.close();
}


function LoadContent(url,ctl) {


    var Pwin = window.open(url, '', 'letf=0,top=0,toolbar=0,scrollbars=0,status=0,width=0,height=0');
    //setTimeout('location.href=' + url, 15000);
   
}

function PrintContent(ctl) {
    //var PGrid = document.getElementById(ctl);
     
    //PGrid.border = 0;
    var bodyContent =  document.getElementsByTagName('body');

    bodyContent[0].innerHTML = '';
    //window.open('', 'PrintGrid', 'left=100,top=100,width=1024,height=768,tollbar=0,scrollbars=1,status=0,resizable=1');
    window.document.write("<link href='Print.css' rel='stylesheet' type='text/css' />");
    window.document.write("<div style='width:800px;'>");
    window.document.write(ctl);
    window.document.write("</div>");
    window.document.close();
    window.focus();
    window.print();
    window.close();
}
 