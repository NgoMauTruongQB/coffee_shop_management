function searchAcount() {
    var nameInput, filter, table, tr, td, i, txtValue;
    nameInput = document.getElementById("js-search-acount");
    filter = nameInput.value.toUpperCase();
    table = document.getElementById("js-data-table");
    tr = table.getElementsByTagName("tr");

    for (i = 0; i < tr.length; i++) {
        td = tr[i].getElementsByTagName("td")[1];
        if (td) {
            txtValue = td.textContent || td.innerText;
            if (txtValue.toUpperCase().indexOf(filter) > -1) {
                tr[i].style.display = "";
            } else {
                tr[i].style.display = "none";
            }
        }
    }
}

function searchRole() {
    var nameInput, filter, table, tr, td, i, txtValue;
    nameInput = document.getElementById("js-search-role");
    filter = nameInput.value.toUpperCase();
    table = document.getElementById("js-data-table");
    tr = table.getElementsByTagName("tr");

    for (i = 0; i < tr.length; i++) {
        td = tr[i].getElementsByTagName("td")[3];
        if (td) {
            txtValue = td.textContent || td.innerText;
            if (txtValue.toUpperCase().indexOf(filter) > -1) {
                tr[i].style.display = "";
            } else {
                tr[i].style.display = "none";
            }
        }
    }
}