var dataTable;
$(document).ready(function () {
    loadDataTable();   
});

function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": {
            "url": '/admin/product/getall',  
            //"type": "GET",
            //"datatype": "json"
        },
        "columns": [
            { data: 'title', "width": "25%" },
            { data: 'isbn', "width": "15%" },
            { data: 'listPrice', "width": "10%" },
            { data: 'author', "width": "15%" },
            { data: 'category.name', "width": "10%" },
            {
                data: 'id',
                "render": function (data) {
                    return `<div class="w-75 btn-group" role="group">
                     <a href="/admin/product/upsert?id=${data}" class="btn btn-primary mx-2"> <i class="bi bi-pencil-square"></i> Edit</a>               
                     <a onClick="Delete('/admin/product/delete?id=${data}')" class="btn btn-danger mx-2"> <i class="bi bi-trash-fill"></i> Delete</a>
                        
                     </div>`
                },
                "width": "25%"
            }
        ]
    });
}

 


// Inject custom CSS for toastr error styling
const style = document.createElement('style');
style.innerHTML = `
.toast-error {
    background-color: #d9534f !important; /* Red background */
    color: #fff !important; /* White text */
}
`;
document.head.appendChild(style);



function Delete(url) {
    Swal.fire({
        title: 'Are you sure?',
        text: "You won't be able to revert this!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, delete it!'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: url,
                type: 'DELETE',
                success: function (data) {
                    if (data.success) {
                        dataTable.ajax.reload(); // Reload the DataTable

                        // Play success sound and show toastr notification
                        var audio = new Audio('/pixabay/error-126627.mp3');
                        audio.play();
                        toastr.success(data.message);
                    } else {
                        toastr.error('An error occurred while deleting.');
                    }
                }
            })
        }
    })
}