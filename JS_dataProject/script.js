
let display = document.getElementById('dataDisplay');
let tip = document.getElementById('toolTip');
let editDisplay = document.getElementById("editBox");
let displayTable = document.createElement("table");
displayTable.id = "mainTable";
let tableCreated = false;
let hrOnShow = false;
let itOnShow = false;
let salesOnShow = false;
let chkHR = document.getElementById("HR");
let chkIT = document.getElementById("IT");
let chkSales = document.getElementById("Sales");

function showHideData() {
    if (tableCreated == false) {
        tRow = document.createElement("tr");
        tdNI = document.createElement("th");
        tdName = document.createElement("th");
        tdAddr = document.createElement("th");
        tdPhone = document.createElement("th");
        tdDept = document.createElement("th");

        tdNI.innerHTML = "NI Number";
        tdName.innerHTML = "Name";
        tdAddr.innerHTML = "Address";
        tdPhone.innerHTML = "Phone";
        tdDept.innerHTML = "Department";

        tRow.appendChild(tdNI);
        tRow.appendChild(tdName);
        tRow.appendChild(tdAddr);
        tRow.appendChild(tdPhone);
        tRow.appendChild(tdDept);

        displayTable.appendChild(tRow);
        tableCreated = true;
    }

    if (chkHR.checked == true && hrOnShow == false) {

        tableAdd("HR");
        hrOnShow = true;

    }
    else if (chkIT.checked == true && itOnShow == false) {

        tableAdd("IT");
        itOnShow = true;

    }
    else if (chkSales.checked == true && salesOnShow == false) {

        tableAdd("Sales");
        salesOnShow = true;

    }
    else if (chkHR.checked == false && hrOnShow == true) {

        tableDel("HR");
        hrOnShow = false;

    }
    else if (chkIT.checked == false && itOnShow == true) {

        tableDel("IT");
        itOnShow = false;

    }
    else if (chkSales.checked == false && salesOnShow == true) {

        tableDel("Sales");
        salesOnShow = false;
    }

}

function tableAdd(department) {
    for (let i = 0; i < QArecords.length; i++) {

        if (QArecords[i].department == department) {

            tRow = document.createElement("tr");
            tdNI = document.createElement("td");
            tdName = document.createElement("td");
            tdAddr = document.createElement("td");
            tdPhone = document.createElement("td");
            tdDept = document.createElement("td");

            tdNI.innerHTML = QArecords[i].ninumber;
            tdName.innerHTML = QArecords[i].fullname;
            tdAddr.innerHTML = QArecords[i].address;
            tdPhone.innerHTML = QArecords[i].phone;
            tdDept.innerHTML = QArecords[i].department;

            tRow.appendChild(tdNI);
            tRow.appendChild(tdName);
            tRow.appendChild(tdAddr);
            tRow.appendChild(tdPhone);
            tRow.appendChild(tdDept);

            displayTable.appendChild(tRow);

            display.appendChild(displayTable);
        }
    }
}

function tableDel(department) {
    let table = document.getElementById("mainTable");

    for (let i = 0; i < table.rows.length; i++) {

        if (table.rows[i].cells[4].innerHTML == department) {

            table.rows[i].remove("tr");
            i = -1;
        }
    }

}

function addDetails() {
    chkHR.disabled = true;
    chkIT.disabled = true;
    chkSales.disabled = true;

    tRow = document.createElement("tr");
    tdNI = document.createElement("td");
    tdName = document.createElement("td");
    tdAddr = document.createElement("td");
    tdPhone = document.createElement("td");
    tdDept = document.createElement("td");
    tdTest = document.createElement("td");

    tdNI.innerHTML = "<input type='text' id='newNI' maxlength='9'>";
    tdName.innerHTML = "<input type='text' id='newName'>";
    tdAddr.innerHTML = "<input type='text' id='newAddr'>";
    tdPhone.innerHTML = "<input type='text' id='newPhone'>";
    tdDept.innerHTML = "<select name='newdept' id='newDept'><option value='HR'>HR</option><option value='IT'>IT</option><option value='Sales'>Sales</option></select>";
    tdTest.innerHTML = "<input type='button' value='+' onclick='addUser()' class='addBTN'><input type='button' value='X' onclick='clearInput()' class='remBTN'>";

    tRow.appendChild(tdNI);
    tRow.appendChild(tdName);
    tRow.appendChild(tdAddr);
    tRow.appendChild(tdPhone);
    tRow.appendChild(tdDept);
    tRow.appendChild(tdTest);

    displayTable.appendChild(tRow);

    display.appendChild(displayTable);
}

function addUser(){
    let NI = document.getElementById("newNI").value;
    let Name = document.getElementById("newName").value;
    let Addr = document.getElementById("newAddr").value;
    let Phone = document.getElementById("newPhone").value;
    let Dept = document.getElementById("newDept").value;

    QArecords.push({ninumber: NI, fullname: Name, address: Addr, phone: Phone, department: Dept});
    clearInput();
    tableDel(Dept);
    tableAdd(Dept);

}

function clearInput() {
    let table = document.getElementById("mainTable");
    table.rows[(table.rows.length - 1)].remove("tr");

    chkHR.disabled = false;
    chkIT.disabled = false;
    chkSales.disabled = false;

}

function test() {
    mybox = document.getElementById("test");
    mybox.style.backgroundColor = "white";
    tip.innerHTML = "";

    if(mybox.value.length < 9){
        mybox.style.backgroundColor = "red";
    }
    else{
        for(let i=0; i<9; i++){

            if(i<2 && mybox.value.charCodeAt(i) >= 65 && mybox.value.charCodeAt(i) <= 90 || i<2 && mybox.value.charCodeAt(i) >= 97 && mybox.value.charCodeAt(i) <= 122){
                tip.innerHTML += "✓";
            }
            else if(i>=2 && i<=7 && mybox.value.charCodeAt(i) >= 48 && mybox.value.charCodeAt(i) <= 57){
                tip.innerHTML += "✓";
            }
            else if(i==8 && mybox.value.charCodeAt(i) >= 65 && mybox.value.charCodeAt(i) <= 90 || i==8 && mybox.value.charCodeAt(i) >= 97 && mybox.value.charCodeAt(i) <= 122){
                tip.innerHTML += "✓";
            }
            else {
                tip.innerHTML += "✖";
            }
        }
    }
    mybox.value = mybox.value.toUpperCase();
}