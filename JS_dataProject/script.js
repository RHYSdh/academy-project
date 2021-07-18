let display = document.getElementById('dataDisplay');
let displayTable = document.getElementById("mainTable");
let editDisplay = document.getElementById("editBox");
let editIndex = 0;
let editing = false;

function displayUpdate() {
    let choice = document.getElementById("deptChoice").value;

    if (choice == "All") {
        tableUpdate("All");
    }
    else if (choice == "HR") {
        tableUpdate("HR");
    }
    else if (choice == "IT") {
        tableUpdate("IT");
    }
    else if (choice == "Sales") {
        tableUpdate("Sales");
    }

}

function tableUpdate(department) {
    let rowCount = displayTable.rows.length - 1;
    for (let x = rowCount; x >= 1; x--) {
        displayTable.rows[x].remove("tr");
    }

    for (let i = 0; i < QArecords.length; i++) {

        if (QArecords[i].department == department || department == "All") {

            tRow = document.createElement("tr");
            tdNI = document.createElement("td");
            tdName = document.createElement("td");
            tdAddr = document.createElement("td");
            tdPhone = document.createElement("td");
            tdDept = document.createElement("td");
            tdControl = document.createElement("td");

            tdNI.innerHTML = QArecords[i].ninumber;
            tdName.innerHTML = QArecords[i].fullname;
            tdAddr.innerHTML = QArecords[i].address;
            tdPhone.innerHTML = QArecords[i].phone;
            tdDept.innerHTML = QArecords[i].department;
            let delBTN = document.createElement("input");
            delBTN.type = "button";
            delBTN.value = "✎";
            delBTN.className = "editBTN";
            delBTN.onclick = function () {
                editDetails(i);
            }

            tdControl.appendChild(delBTN);
            tdControl.style.background = "white";
            tdControl.style.border = "0px";
            tdControl.style.padding = "0px";
            //tdControl.style.width = 45;

            tRow.appendChild(tdNI);
            tRow.appendChild(tdName);
            tRow.appendChild(tdAddr);
            tRow.appendChild(tdPhone);
            tRow.appendChild(tdDept);
            tRow.appendChild(tdControl);

            displayTable.appendChild(tRow);
        }
    }
}

function removeDetails() {
    if (confirm("Are you sure?")) {
        QArecords.splice(editIndex, 1);
        displayUpdate();
        editDisplay.style.visibility = "hidden";
        document.getElementById("addDelete").style.visibility = "hidden";
    }
}

function editDetails(qaIndex) {
    document.getElementById("NIinput").value = QArecords[qaIndex].ninumber;
    document.getElementById("NAMEinput").value = QArecords[qaIndex].fullname;
    document.getElementById("ADDRinput").value = QArecords[qaIndex].address;
    document.getElementById("PHONEinput").value = QArecords[qaIndex].phone;
    document.getElementById("DEPTinput").value = QArecords[qaIndex].department;
    editIndex = qaIndex;
    editing = true;
    document.getElementById("addUpdate").value = "Update";
    document.getElementById("editHead").innerHTML = "Update record";
    document.getElementById("addDelete").style.visibility = "visible";
    editDisplay.style.visibility = "visible";
    validate("NI");
    validate("PHONE");
    validate("ADDR");
    validate("NAME");
}

function addDetails() {

    let newNI = document.getElementById("NIinput").value;
    let newName = document.getElementById("NAMEinput").value;
    let newAddr = document.getElementById("ADDRinput").value;
    let newPhone = document.getElementById("PHONEinput").value;
    let newDept = document.getElementById("DEPTinput").value;

    if (editing) {
        QArecords[editIndex].ninumber = newNI;
        QArecords[editIndex].fullname = newName;
        QArecords[editIndex].address = newAddr;
        QArecords[editIndex].phone = newPhone;
        QArecords[editIndex].department = newDept;
    }
    else {
        QArecords.push({ ninumber: newNI, fullname: newName, address: newAddr, phone: newPhone, department: newDept });
    }
    displayUpdate();
    editDisplay.style.visibility = "hidden";
    document.getElementById("addDelete").style.visibility = "hidden";

}

let t1 = false;
let t2 = false;
let t3 = false;
let t4 = false;

function validate(what) {

    let newNI = document.getElementById("NIinput").value;
    let niVal = document.getElementById("niVAL");
    let NIcount = 0;
    document.getElementById("addUpdate").disabled = true;

    if (what == "NI") {
        t1 = false;
        niVal.innerHTML = "";
        for (let i = 0; i < 9; i++) {

            if (i < 2 && newNI.charCodeAt(i) >= 65 && newNI.charCodeAt(i) <= 90 || i < 2 && newNI.charCodeAt(i) >= 97 && newNI.charCodeAt(i) <= 122) {
                niVal.innerHTML += "✓";
                NIcount++;
            }
            else if (i >= 2 && i <= 7 && newNI.charCodeAt(i) >= 48 && newNI.charCodeAt(i) <= 57) {
                niVal.innerHTML += "✓";
                NIcount++;
            }
            else if (i == 8 && newNI.charCodeAt(i) >= 65 && newNI.charCodeAt(i) <= 90 || i == 8 && newNI.charCodeAt(i) >= 97 && newNI.charCodeAt(i) <= 122) {
                niVal.innerHTML += "✓";
                NIcount++;
            }
            else {
                niVal.innerHTML += "✖";
            }
        }
    }
    if (NIcount == 9) {
        niVal.innerHTML = "OK";
        t1 = true;
    }

    let newPhone = document.getElementById("PHONEinput").value;
    let phoneVal = document.getElementById("phoneVAL");
    let Phonecount = 0;
    if (what == "PHONE") {
        t2 = false;
        phoneVal.innerHTML = "11 Digits";
        for (let i = 0; i < newPhone.length; i++)

            if (newPhone.charCodeAt(i) >= 48 && newPhone.charCodeAt(i) <= 57) {
                Phonecount++;
                if (Phonecount == 11) {
                    phoneVal.innerHTML = "OK";
                    if (!document.getElementById("PHONEinput").value.includes("-")) {
                        document.getElementById("PHONEinput").value = newPhone.substr(0, 5) + "-" + newPhone.substr(5, 6);
                    }
                    t2 = true;
                }
            }
            else {
                phoneVal.innerHTML = "Numbers only";
            }
    }

    let newAddr = document.getElementById("ADDRinput").value;
    let addrVal = document.getElementById("addrVAL");
    if (what == "ADDR") {
        t3 = false;
        addrVal.innerHTML = "";
        if (newAddr.length > 0) {
            addrVal.innerHTML = "OK";
            t3 = true;
        }
        else {
            addrVal.innerHTML = "At least one character";
        }
    }

    let newName = document.getElementById("NAMEinput").value;
    let nameVal = document.getElementById("nameVAL");
    if (what == "NAME") {
        t4 = false;
        nameVal.innerHTML = "";
        if (newName.length > 0) {
            nameVal.innerHTML = "OK";
            t4 = true;
        }
        else {
            nameVal.innerHTML = "At least one character";
        }
    }

    if (t1 == true && t2 == true && t3 == true && t4 == true) {
        document.getElementById("addUpdate").disabled = false;
    }
}

function popup() {
    editDisplay.style.visibility = "visible";
    document.getElementById("addUpdate").value = "Add";
    document.getElementById("addUpdate").disabled = true;
    document.getElementById("editHead").innerHTML = "Add new record";
    document.getElementById("addDelete").style.visibility = "hidden";
    document.getElementById("NIinput").value = "";
    document.getElementById("NAMEinput").value = "";
    document.getElementById("ADDRinput").value = "";
    document.getElementById("PHONEinput").value = "";
    validate("NI");
    validate("PHONE");
    validate("ADDR");
    validate("NAME");

}

setInterval(displayUpdate(), 250);