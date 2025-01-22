﻿var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $("#tblSlider").DataTable({
        "ajax": {
            "url": "/admin/slider/GetAll",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "id", "width": "5%" },
            { "data": "name", "width": "20%" },
            {
                "data": "state", "render": function (currentState) {
                    if (currentState == true) {
                        return "Active"
                    } else {
                        return "Inactive"
                    }
                }
            },
            {
                "data": "urlImage",
                "render": function (image) {
                    return `<img src="../${image}" width="120px">`
                }
            },
            {
                "data": "id",
                "render": function (data)
                {
                    return `<div class="text-center">
                                <a href="/Admin/Slider/Edit/${data}" class="btn btn-success text-white" style="cursor:pointer; width:140px;">
                                <i class="fa fa-pencil"></i> Edit
                                </a>
                                &nbsp;
                                <a onclick=Delete("/Admin/Slider/Delete/${data}") class="btn btn-danger text-white" style="cursor:pointer; width:140px;">
                                <i class="fa fa-trash-alt"></i> Delete
                                </a>
                            </div>
                    `;
                }, "width": "30%"
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