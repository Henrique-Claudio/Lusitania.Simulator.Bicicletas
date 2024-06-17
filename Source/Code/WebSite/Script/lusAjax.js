//Lusitania Ajax. (Simple and Clear)
var xmlHttp=null;

//Obter um objecto XMLHTTP
function GetXmlHttpObject()
{
	var _xmlHttp=null;
	try
	{
		// Internet Explorer
		_xmlHttp=new ActiveXObject("Msxml2.XMLHTTP");		
	}
	catch (e)
	{
		// Internet Explorer (versões antigas)
		try
		{
			_xmlHttp=new ActiveXObject("Microsoft.XMLHTTP");
		}
		catch (e)
		{
			// Firefox, Opera 8.0+, Safari
			_xmlHttp=new XMLHttpRequest();
		}
	}
	return _xmlHttp;
}

function ExecutarXMLHttp(sURL, fnChamarDepois)
{
	xmlHttp=GetXmlHttpObject();
		
	if (xmlHttp==null)
	{
		alert ("O seu browser não suporta AJAX!");
		return;
	} 
	
	xmlHttp.onreadystatechange = function(){
        if (xmlHttp.readyState==4) //OK
        {   // if "OK"
			if (xmlHttp.status==200)
			{
				fnChamarDepois(xmlHttp); //Chama uma função definida no argumento de entrada
			}
        }
    };
    
    xmlHttp.open("POST", sURL, true); //true, continua o script, false para o script e aguarda a resposta
	xmlHttp.send(null);
}

function ExecutarXMLHttpForm(sURL, fnChamarDepois, params)
{
	xmlHttp=GetXmlHttpObject();
		
	if (xmlHttp==null)
	{
		alert ("O seu browser não suporta AJAX!");
		return;
	} 
	
	xmlHttp.onreadystatechange = function(){
        if (xmlHttp.readyState==4) //OK
        {   // if "OK"
			if (xmlHttp.status==200)
			{
				fnChamarDepois(xmlHttp); //Chama uma função definida no argumento de entrada
			}
        }
    };
    
    xmlHttp.open("POST", sURL, true); //true, continua o script, false para o script e aguarda a resposta
    
    //Para o post
    xmlHttp.setRequestHeader("Content-Type","application/x-www-form-urlencoded");
	xmlHttp.send(params);
}


function ExecutarXMLHttpAguardaResposta(sURL, fnChamarDepois)
{
	xmlHttp=GetXmlHttpObject();
		
	if (xmlHttp==null)
	{
		alert ("O seu browser não suporta AJAX!");
		return;
	} 
	
	xmlHttp.onreadystatechange = function(){
        if (xmlHttp.readyState==4) //OK
        {   // if "OK"
			if (xmlHttp.status==200)
			{
				fnChamarDepois(xmlHttp); //Chama uma função definida no argumento de entrada
			}
        }
    };
    
    xmlHttp.open("POST",sURL,false); //true, continua o script, false para o script e aguarda a resposta
    
    //Para o post
    //xmlHttp.setRequestHeader("Method", "POST "+sURL+" HTTP/1.1");
    //xmlHttp.setRequestHeader("Content-Type","application/x-www-form-urlencoded");
	//xmlHttp.send("&teste=1");
	
	xmlHttp.send(null);
}

//Funções para preenchimento de objectos

function PreencherDropDown(sTagID, sTextoJSON){
	//o texto deve estar no seguinte formato:
	//'1|Lisboa:2|Porto:3|Vila Nova Milfontes:4|Faro'	
	var _oCombo = document.getElementById(sTagID);

	//criar um array dos elementos
	var _oArray = sTextoJSON.split(":");

	//saber o tamanho do array
	var _oArray_Length = _oArray.length;

	// Remover todos os elementos da combo
	_oCombo.options.length = 0;
	
	//for (var count = _oCombo.options.length-1; count >-1; count--)
	//{
	//	_oCombo.options[count] = null;
	//}

	var _oTempArray;
	var _newValue;
	var _newText;
	
	// adicionar novas options
	for(var i=0; i < _oArray_Length; i++) {
		// dividir o texto para ficar com o valor e o texto
		_oTempArray = _oArray[i].split("|");
		_newValue = _oTempArray[0];
		_newText = _oTempArray[1];
		_oCombo.options[i] = new Option(_newText,_newValue,false,false);
	}
}