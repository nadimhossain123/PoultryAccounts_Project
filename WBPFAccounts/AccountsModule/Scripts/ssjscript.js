function NumbersOnly(evt) {
    var charCode = (window.event) ? window.event.keyCode : evt.which;
    if ((charCode == 8 || charCode == 0)) return true; 
    if (charCode >= 48 && charCode <= 57)
        return true;
    return false;
}

 function AmountOnly(myfield, e)
    {
    
	    var key;
	    var keychar;
    	
	    if (window.event)
		       key = window.event.keyCode;
	    else if (e)
		       key = e.which;
	    else
		       return true;
	    keychar = String.fromCharCode(key);
    	
	    // control keys
	    if ((key==null) || (key==0) || (key==8) || (key==9) || (key==13) || (key==27) )
	    {
		    return true;
	    }
	    // numbers
	    else if ((("0123456789").indexOf(keychar) > -1))
	    {
		    return true;
	    }
	    // decimal point jump
	    else if ((keychar == "."))
	    {
		    num =	myfield.value;
		    index = num.indexOf(".");
		    if( index > 0 )
		    {	// there is already a '.' present
			    return false;
		    }			
		    return true;
	    }
	    // ctrl+c and ctrl+v
	    else if(((key==99) || (key==118)) && window.event.ctrlKey )
	    {
		    return true;
	    }
	    else
	    {
		    return false;
	    }
    }

function EmailOnly(evt) {
    var keyCode = (window.event) ? window.event.keyCode : evt.which;
    if ((keyCode == 8 || keyCode == 0)) return true; 
    if (isCharCodeAlphaDigit(keyCode) == true || keyCode == 46 || keyCode == 45 || keyCode == 95 || keyCode == 64 || keyCode == 44)
        return true;
    return false;
}

function isCharCodeAlphaDigit(keyCode) {
    if ((keyCode >= 48 && keyCode <= 57)) return true;
    if ((keyCode >= 65 && keyCode <= 90)) return true;
    if ((keyCode >= 97 && keyCode <= 122)) return true;
    return false;
}

function checkforvaliddata(obj, name, type) {
    // Parameters:
    //		obj - form element
    //		name - name of the form element for error message to be displayed
    //		type - type of error checking
    var msg = "";
    var temp;
    if ((type == 0) && (obj.type == "select-one" || obj.type == "select-multiple")) {

        if (obj.length > 0) {
            if (obj.selectedIndex < 0) obj.selectedIndex = 0
            temp = obj[obj.selectedIndex].value
        }
        else {
            temp = "";
        }
    }
    else { temp = obj.value; }

    temp = stringReplace(temp, " ", "");

    //  DROPDOWN
    //	Check for valid entry in a drop down listbox	
    if (type == 0 && temp == 0) {
        //alert(temp);
        genmsg(name, 1, 0, 0);
        if (!obj.disabled) {
            obj.focus();
        } return false; 
      }

    //	STRING
    //	Check for empty string
    if (type == 1 && temp == "") { genmsg(name, 1, 0, 0); obj.focus(); return false; }
    //	Check for valid name
    if (type == 11 && !isName(temp)) { genmsg(name, 13, 0, 0); obj.focus(); return false; }

    // DATE

    //	Check for valid date 
    if (type == 2 && temp != "" && !isDate(temp)) { genmsg(name, 2, 0, 0); obj.focus(); return false; }
    //	Check for valid date and empty string
    if (type == 21 && (temp == "" || !isDate(temp))) { genmsg(name, 2, 0, 0); obj.focus(); return false; }

    // DATE -- dd/mm/yyyy
    if (type == 22 && (temp == "" || !isDate_ddmmyyyy(temp))) { genmsg(name, 16, 0, 0); obj.focus(); return false; }

    // NUMERIC 
    //	Check for valid numeric value 
    if (type == 3 && temp != "" && !isNumeric(temp)) { genmsg(name, 3, 0, 0); obj.focus(); return false; }
    //	Check for valid unsigned numeric value 
    if (type == 31 && temp != "" && !isuNumeric(temp)) { genmsg(name, 4, 0, 0); obj.focus(); return false; }
    //	Check for valid numeric value and empty string
    if (type == 32 && (temp == "" || !isNumeric(temp))) { genmsg(name, 3, 0, 0); obj.focus(); return false; }
    //	Check for valid unsigned numeric value and empty string
    if (type == 33 && (temp == "" || !isuNumeric(temp))) { genmsg(name, 4, 0, 0); obj.focus(); return false; }


    // MONEY
    //	Check for valid money value 
    if (type == 4 && temp != "" && !isMoney(temp)) { genmsg(name, 5, 0, 0); obj.focus(); return false; }
    //	Check for valid unsigned money value 
    if (type == 41 && temp != "" && !isuMoney(temp)) { genmsg(name, 6, 0, 0); obj.focus(); return false; }
    //	Check for valid money value and empty string
    if (type == 42 && (temp == "" || !isMoney(temp))) { genmsg(name, 5, 0, 0); obj.focus(); return false; }
    //	Check for valid unsigned money value and empty string
    if (type == 43 && (temp == "" || !isuMoney(temp))) { genmsg(name, 6, 0, 0); obj.focus(); return false; }
    //	Check for valid money value and integer with two decimal
    if (type == 44 && temp != "" && !isMoney(temp)) { genmsg(name, 14, 0, 0); obj.focus(); return false; }


    // INTEGER
    //	Check for valid integer value 
    if (type == 5 && temp != "" && !isInteger(temp)) { genmsg(name, 7, 0, 0); obj.focus(); return false; }
    //	Check for valid unsigned integer value 
    if (type == 51 && temp != "" && !isuInteger(temp)) { genmsg(name, 8, 0, 0); obj.focus(); return false; }
    //	Check for valid integer value  and empty string
    if (type == 52 && (temp == "" || !isInteger(temp))) { genmsg(name, 7, 0, 0); obj.focus(); return false; }
    //	Check for valid unsigned integer value  and empty string
    if (type == 53 && (temp == "" || !isuInteger(temp))) { genmsg(name, 8, 0, 0); obj.focus(); return false; }

    // ALPHANUMERIC
    //	Check for valid alphanuemirc charectors
    if (type == 6 && temp != "" && !isAlphaNumeric(temp)) { genmsg(name, 15, 0, 0); obj.focus(); return false; }

    //Email
    if (type == 7 && temp != "" && !IsValidEmail(temp)) {genmsg(name, 1, 0, 0); obj.focus(); return false; }
    return true;
}

    function IsValidEmail(s)
       {
         var filter =/^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$/;
         if (!filter.test(s)) {
            return false;
           }
         else
           {return true;}
       } 

function genmsg(name, type, x, y) {
    // Parameters:
    //		name - name of the form element for error message to be displayed
    //		type  - type of value being checked
    //		x - additional info if needed
    //		y - additional info if needed

    var msg = "Please enter/select a valid '" + name + "'.";
    var msg_1 = "\r\nNon-numeric characters are not allowed.";
    var msg_2 = "\r\n'+' or '-' sign is not allowed.";
    var msg_3 = "\r\nDate Format:mm/dd/yyyy";
    var msg_4 = "\r\nFormat:Number with decimals.";
    var msg_5 = "\r\nFormat:Number with maximum two digits after decimal.";
    var msg_6 = "\r\nFormat:Number with no decimals.";
    var msg_7 = "Please enter valid Unit of Measure for " + name + ".";
    var msg_8 = "'" + name + "' cannot be greater than " + x + " characters.";
    var msg_9 = "Minimum cannot be greater than or equal to Maximum value for '" + name + "'";
    var msg_10 = "Please enter at least " + x + " characters for '" + name + "' to perform a valid search.";
    var msg_11 = "'" + name + "' is not valid ( illegal characters ).";
    var msg_12 = "\r\nOnly alpha or numeric characters are allowed.";
    var msg_13 = "\r\nDate Format:dd/mm/yyyy";
    
    switch (type) {
        case 1: msg = msg; break; 						// Empty string
        case 2: msg = msg + msg_3; break; 				// Date check
        case 3: msg = msg + msg_1; break; 				// Numeric
        case 4: msg = msg + msg_4 + msg_1 + msg_2; break; // Numeric ( unsigned )
        case 5: msg = msg + msg_1; break; 				// Money
        case 6: msg = msg + msg_5 + msg_1 + msg_2; break; // Money ( unsigned )
        case 7: msg = msg + msg_6 + msg_1; break; 		// Integer
        case 8: msg = msg + msg_6 + msg_1 + msg_2; break; // Integer ( unsigned )
        case 9: msg = msg_7; break; 						// Unit of measure 
        case 10: msg = msg_8; break; 						// Length check
        case 11: msg = msg_9; break; 						// Min Max check
        case 12: msg = msg_10; break; 						// Min no of characters for a search
        case 13: msg = msg_11; break; 						// Name Validation
        case 14: msg = msg + msg_5; break; 		// Money
        case 15: msg = msg + msg_12; break; 	//Alphanumeric
        case 16: msg = msg + msg_13; break; 	// Date check - dd/mm/yyyy
        default: return;
    }
    alert(msg); return;
}

// Check for valid Date
function isDate(strDate) {
    strDate = ConvertToUSDate(strDate); //vb 09212005
    var validcharacters = "0123456789/"
    var j;

    for (var i = 0; i < strDate.length; i++)			// check for valid characters
    {
        j = "" + strDate.substring(i, i + 1);
        if (validcharacters.indexOf(j) == "-1") { return false; }
    }

    if (strDate.length > 10 || strDate.length < 8) { return false; }  // check for validate length of string

    var fs = strDate.indexOf("/")					// check for valid separators
    var ls = strDate.lastIndexOf("/")
    if (fs == -1 || ls == -1 || fs == ls) { return false; }

    var m = strDate.substring(0, fs)					// find the date parts - month
    var d = strDate.substring(fs + 1, ls)			// day
    var y = strDate.substring(ls + 1, strDate.length)// year
    if (!isuInteger(m) || !isuInteger(d) || !isuInteger(y)) { return false; }

    if (m < 1 || m > 12) { return false; } 			// check validity of month
    if (d < 1 || d > 31) { return false; } 		// check validity of the day of the month ( primary check )
    if (y < 1900 || y > 9999) { return false; } 	// check validity of year

    if ((m == 2 || m == 4 || m == 6 || m == 9 || m == 11) && (d == 31)) { return false; } // check validity of the day of the month ( secondary check )

    if (m == 2)									// check validity of the day of the month ( leap year check )
    {
        var g = parseInt(y / 4)
        if (isNaN(g)) { return false; }
        if (d > 29) { return false; }
        if (d == 29 && ((y / 4) != parseInt(y / 4))) { return false; }
    }
    return true;
}

///dd/mm/yyyy
function isDate_ddmmyyyy(strDate) {
    strDate = ConvertToUSDate_ddmmyyyy(strDate); //vb 09212005
    var validcharacters = "0123456789/"
    var j;

    for (var i = 0; i < strDate.length; i++)			// check for valid characters
    {
        j = "" + strDate.substring(i, i + 1);
        if (validcharacters.indexOf(j) == "-1") { return false; }
    }

    if (strDate.length > 10 || strDate.length < 8) { return false; }  // check for validate length of string

    var fs = strDate.indexOf("/")					// check for valid separators
    var ls = strDate.lastIndexOf("/")
    if (fs == -1 || ls == -1 || fs == ls) { return false; }

    var d = strDate.substring(0, fs)					// find the date parts - month
    var m = strDate.substring(fs + 1, ls)			// day
    var y = strDate.substring(ls + 1, strDate.length)// year
    if (!isuInteger(d) || !isuInteger(m) || !isuInteger(y)) { return false; }

    if (d < 1 || d > 31) { return false; } 		// check validity of the day of the month ( primary check )
    if (m < 1 || m > 12) { return false; } 			// check validity of month

    if (y < 1900 || y > 9999) { return false; } 	// check validity of year

    if ((m == 2 || m == 4 || m == 6 || m == 9 || m == 11) && (d == 31)) { return false; } // check validity of the day of the month ( secondary check )

    if (m == 2)									// check validity of the day of the month ( leap year check )
    {
        var g = parseInt(y / 4)
        if (isNaN(g)) { return false; }
        if (d > 29) { return false; }
        if (d == 29 && ((y / 4) != parseInt(y / 4))) { return false; }
    }
    return true;
}

function isNumeric(s) {
    var i, j = 0;
    if (isEmpty(s)) { return false; } // check for empty string

    for (i = 0; i < s.length; i++)  // check for valid digit or operator( first position only ) and
    {								// multiple decimal points
        var c = s.charAt(i);
        if ((c == "-" || c == "+") && i == 0) { continue; }
        if (!isDigit(c) && c != ".") { return false; }
        if (c == ".") { j++; }
        if (j > 1) { return false; }
    }
    return true;
}

function isMoney(s) {
    var i, j = 0, k = 0;

    if (!isNumeric(s)) { return false; } // check for numeric

    for (i = 0; i < s.length; i++) // check for more than three digits after decimal point
    {
        var c = s.charAt(i);
        if (c == ".") { j++; }
        if (j > 0) { k++; }
        if (k > 3) { return false; }
    }
    return true;
}

function isInteger(s) {
    var i;
    if (isEmpty(s)) { return false; } // check for empty string

    for (i = 0; i < s.length; i++) // check for valid digit or operator( first position only )
    {
        var c = s.charAt(i);
        if ((c == "-" || c == "+") && i == 0) continue;
        if (!isDigit(c)) { return false; }
    }
    return true;
}

function isAlphaNumeric(s) {
    for (var i = 0; i < s.length; i++) {
        var c = s.charAt(i);
        if (!isDigit(c) && !isAlphabet(c))
            return false;
    }
    return true;
}

function isName(s) {
    for (var i = 0; i < s.length; i++) // check for valid alphanumeric characters
    {
        var c = s.charAt(i);
        if (c != "," && c != "." && c != "'" && !isAlphabet(c) && c != "-") { return false; }
    }
    return true;
}

function stringReplace(originalString, findText, replaceText) {
    return replaceString(originalString, findText, replaceText);
}

function isuNumeric(s) {
    if (!isNumeric(s)) { return false; } // check for numeric
    if (s.charAt(0) == "+" || s.charAt(0) == "-") { return false; } // check for operator at the first position
    return true;
}

function isuMoney(s) {
    var i, j = 0, k = 0;

    if (!isMoney(s)) { return false; } // check for money
    if (s.charAt(0) == "+" || s.charAt(0) == "-") { return false; } // check for operator at the first position
    return true;
}

function isuInteger(s) {
    if (!isInteger(s)) { return false; } // check for integer	    	
    if (s.charAt(0) == "+" || s.charAt(0) == "-") { return false; } // check for operator at the first position
    return true;
}

function replaceString(x, y, z) {
    var pos = 0;
    var len = y.length;
    pos = x.indexOf(y)
    while (pos != -1) {
        var pre = x.substring(0, pos);
        var post = x.substring(pos + len, x.length);
        x = pre + z + post;
        pos = x.indexOf(y);
    }
    return x;
}

// Convert Date to dd/mm/yyyy format
function ConvertToUSDate_ddmmyyyy(dt) {
    if (dt == undefined || dt == '') return ''

    if (isObject(dt)) {
        newdate = formatDateStr(dt, 2);
    }
    else {
        newdate = dt;
    }

    var ary_date = newdate.split('/');
    var str2;
    if (getDateFormatID() == 1) {
        str2 = newdate; // Date is already in mm/dd/yyyy format
    }
    else {
        var month = new String(ary_date[1]);
        var day = new String(ary_date[0]);

        if (month.length < 2)
            month = "0" + month;
        if (day.length < 2)
            day = "0" + day;
        str2 = day + "/" + month + "/" + ary_date[2];
    }
    return (str2);
}

// Format Date - Expects date object and converts in string based on user date format;
// timeInd -> 1 = attach time in hh:mm:ss format to the date; 2 = Do not attach time
function formatDateStr(x, timeInd) {
    var actualday = x.getDate(); // Get the Actual Day (1-31) from the computed date
    if (actualday < 10) { actualday = "0" + actualday };

    var actualmonth = x.getMonth() + 1; // Get the Actual Month (1-12) from the computed date
    if (actualmonth < 10) { actualmonth = "0" + actualmonth };

    var actualyear = x.getYear(); // Get the Actual Year from the computed date
    if (actualyear < 2000) { actualyear += 1900; }

    var rDay = /\%d/gi;
    var rMonth = /\%m/gi;
    var rYear = /\%Y/gi;
    str = new String(getDateFormat()).replace(rDay, new String(actualday));
    str1 = str.replace(rMonth, new String(actualmonth));
    str2 = str1.replace(rYear, new String(actualyear));

    if (timeInd == 1) {
        var hour = x.getHours();
        var minute = x.getMinutes();
        var second = x.getSeconds();

        if (hour < 10) { hour = "0" + hour; }
        if (minute < 10) { minute = "0" + minute; }
        if (second < 10) { second = "0" + second; }

        str2 = str2 + " " + hour + ":" + minute + ":" + second
    }

    return str2;
}

function isObject(a) {
    return (typeof a == 'object' && !!a);
}

function getDateFormatID() {
    //var objParent;
    //objParent = getFrameMenuObject() ;
    //return (objParent.dateformat_id);	
    return 1;

}
// Check if a string is empty
function isEmpty(s) {
    return ((s == null) || (s.length == 0));
}

// Check if parameter is a valid digit
function isDigit(c) {
    return ((c >= "0") && (c <= "9"));
}