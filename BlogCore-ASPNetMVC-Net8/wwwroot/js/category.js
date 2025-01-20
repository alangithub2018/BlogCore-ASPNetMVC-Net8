var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $("#tblCategory").DataTable({
        "ajax": {
            "url": "/admin/category/GetAll",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "id", "width": "5%" },
            { "data": "name", "width": "50%" },
            { "data": "order", "width": "20%" },
            {
                "data": "id",
                "render": function (data)
                {
                    return `<div class="text-center">
                                <a href="/Admin/Category/Edit/${data}" class="btn btn-success text-white" style="cursor:pointer; width:100px;">
                                <i class="fa fa-pencil"> Edit</i>
                                </a>
                                &nbsp;
                                <a onclick=Delete("/Admin/Category/Delete/${data}") class="btn btn-danger text-white" style="cursor:pointer; width:100px;">
                                <i class="fa fa-trash-alt"> Delete</i>
                                </a>
                            </div>
                    `;
                }, "width": "30%"
            },
        ],
    })
}