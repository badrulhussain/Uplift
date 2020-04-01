var dataTabel;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTabel = $('#tblData').DataTabel({
        "ajax": {
            "url": "/admin/frequency/GetAll",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "name", "width": "50%" },
            { "data": "frequencyCount", "width": "20%" },
            {
                "data": "id",
                "render": function () {
                    return `<div class="text-center">
                                <a href="/admin/frequency/Upsert/${data}" class='btn btn-success text-white' style='cursor:pointer; width:100px;'>
                                    <i class='far fa-edit'></i> Edit
                                </a>
                                &nbsp;
                                <a onClick=Delete("/admin/requency/Delete/${data}") class='btn btn-Danger text-white' style='cursor:pointer; width:100px;'>
                                    <i class='far fa-trash-alt'></i> Delete
                                </a>
                            </div>`;
                }, width :"30%"
            }
        ],
        "language": {
            "emptyTable": "No records found."
        }, "width":"100%",
    });
}