var datatable;

$(document).ready(function(){
    loadDataTable();
});

function loadDataTable() {
    datatable = $('#tblData').DataTable({
        "ajax": {
            "url": "/Empleado/ObtenerTodos"
        },
        "columns": [
            {"data": "nombre","width":"10%"},
            {"data": "paterno","width":"10%"},
            {"data": "materno","width":"10%"},
            {"data": "titulo","width":"10%"},
            {"data": "puesto","width":"10%"},
            {"data": "observaciones","width":"10%"},
        ]
    });
}