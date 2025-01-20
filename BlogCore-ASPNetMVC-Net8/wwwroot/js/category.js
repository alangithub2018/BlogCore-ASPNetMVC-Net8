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
            { "data": "name", "width": "40%" },
            { "data": "order", "width": "10%" },
            {
                "data": "id",
                "render": function (data)
                {
                    return `<div class="text-center">
                                <a href="/Admin/Category/Edit/${data}" class="btn btn-success text-white" style="cursor:pointer; width:140px;">
                                <i class="fa fa-pencil"></i>
                                </a>
                                &nbsp;
                                <a onclick=Delete("/Admin/Category/Delete/${data}") class="btn btn-danger text-white" style="cursor:pointer; width:140px;">
                                <i class="fa fa-trash-alt"></i>
                                </a>
                            </div>
                    `;
                }, "width": "40%"
            },
        ],
    })
}

function Delete(url) {
    swal({
        title: "Are you sure to delete?",
        text: "This operation cannot be undone!",
        type: "warning",
        showCancelButton: true,
        confirmButtonColor: "#DD6B55",
        confirmButtonText: "Yes, delete!",
        closeOnconfirm: true
    },
        function () {
            $.ajax({
                type: 'DELETE',
                url: url,
                success: function (data) {
                    if (data.success) {
                        toastr.success(data.message);
                        dataTable.ajax.reload();
                    } else {
                        toastr.error(data.message);
                    }
                }
            });
        }
    );
}