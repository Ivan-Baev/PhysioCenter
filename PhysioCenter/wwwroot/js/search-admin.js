function searchFunctionAdmin(e) {
    var filter, table, tr, td, i, txtValue;

    filter = e.target.value.toUpperCase();


    table = document.getElementById("adminTable");

    tr = table.getElementsByTagName("tr");

    for (i = 1; i < tr.length; i++) {
        td = tr[i];
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

document.querySelector('#searchAdmin').addEventListener('keyup', searchFunctionAdmin, false);