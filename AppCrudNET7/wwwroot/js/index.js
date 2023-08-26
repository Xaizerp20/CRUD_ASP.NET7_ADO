
const _modelEmployee = {

    idEmpleoyee: 0,
    fullName: "",
    idDepartment: "",
    salary: 0,
    dateContract: ""
}

function ShowEmployees() {
    fetch("/Home/listEmployees")
        .then(response => {
            return response.ok ? response.json() : Promise.reject(response);
        })
        .then(responseJson => {
            if (responseJson.length > 0) {
                $("#tableEmployees tbody").html("");
                responseJson.forEach((employee) => {
                    $("#tableEmployees tbody").append(
                        $("<tr>").append(
                            $("<td>").text(employee.fullName),
                            $("<td>").text(employee.refDepartment.nameDepartment),
                            $("<td>").text(employee.salary),
                            $("<td>").text(employee.dateContract),
                            $("<td>").append(
                                $("<button>").addClass("btn btn-primary btn-sm btn-edit-employee").text("Edit").data("dataEmployee", employee),
                                $("<button>").addClass("btn btn-danger btn-sm ms-2 btn-delete-employee").text("Delete").data("dataEmployee", employee)
                            )
                        )
                    );
                });
            }
        });
}

document.addEventListener("DOMContentLoaded", function () {

    ShowEmployees()

    fetch("/Home/listDepartments")
        .then(response => {
            return response.ok ? response.json() : Promise.reject(response);
        })
        .then(responseJson => {
            if (responseJson.length > 0) {
                responseJson.forEach((item) => {

                    $("#cboDepartment").append(
                        $("<option>").val(item.idDepartment).text(item.nameDepartment)
                    );

                })
            }
        });

    $("#txtDateContract").datepicker({

        format: "dd/mm/yyyy",
        autoclose: true,
        todayHighlight: true
    })

}, false)


function ShowModal() {



    $("#txtFullName").val(_modelEmployee.fullName);
    $("#cboDepartment").val(_modelEmployee.idDepartment == 0 ? $("#cboDepartment option:first").val() : _modelEmployee.idDepartment);
    $("#txtSalary").val(_modelEmployee.salary);
    $("#txtDateContract").val(_modelEmployee.dateContract);

    $("#modalEmployee").modal("show");

}




$(document).on("click", ".btn-new-employee", function () {

    _modelEmployee.idEmpleoyee = 0;
   
    _modelEmployee.fullName = "";
    _modelEmployee.idDepartment = 0;
    _modelEmployee.salary = 0;
    _modelEmployee.dateContract = "",

        ShowModal();
})


$(document).on("click", ".btn-edit-employee", function () {

    const _employee = $(this).data("dataEmployee")

    _modelEmployee.idEmpleoyee = _employee.idEmployee;
  
    _modelEmployee.fullName = _employee.fullName;
    _modelEmployee.idDepartment = _employee.refDepartment.idDepartment;
    _modelEmployee.salary = _employee.salary;
    _modelEmployee.dateContract = _employee.dateContract; 

        ShowModal();
})



$(document).on("click", ".btn-save-changes", function () {

    const model = {

        idEmpleoyee: _modelEmployee.idEmpleoyee,
        fullName: $("#txtFullName").val(),
        refDepartment: {
            idDepartment: $("#cboDepartment").val()
        },
        salary: $("#txtSalary").val(),
        dateContract: $("#txtDateContract").val()
    }
    

    if (_modelEmployee.idEmpleoyee == 0) {

        fetch("/Home/saveEmployee", {

            method: "POST",
            headers: { "Content-Type": "application/json; charset=utf-8" },
            body: JSON.stringify(model)
        })
            .then(response => {
                return response.ok ? response.json() : Promise.reject(response);
            })
            .then(responseJson => {

                if (responseJson.value) {
                    $("#modalEmployee").modal("hide");
                    Swal.fire("Reade!", "Employee was Created", "success")
                    ShowEmployees()
                }
                else {
                    Swal.fire("Sorry!", "Employee wasn't Created", "error")
                }
            })

    }
    else {

        console.log(model)

        fetch("/Home/editEmployee", {
            method: "PUT",
            headers: { "Content-Type": "application/json; charset=utf-8" },
            body: JSON.stringify(model)
        })
            .then(response => {
                return response.ok ? response.json() : Promise.reject(response);
            })
            .then(responseJson => {

                if (responseJson.value == true) {
                    $("#modalEmployee").modal("hide");
                    Swal.fire("Reade!", "Employee was Update", "success")
                    ShowEmployees()
                }
                else {
                    Swal.fire("Sorry!", "Employee wasn't Update", "error")
                }
            })


    }

})

$(document).on("click", ".btn-delete-employee", function () {
    const _employee = $(this).data("dataEmployee");

    Swal.fire({
        title: "Are you Sure?",
        text: `Delete employee "${_employee.fullName}"`,
        icon: "warning",
        showCancelButton: true,
        cancelButtonColor: "#d33",
        confirmButtonText: "Yes, Delete",
        cancelButtonText: "No, return"
    }).then((result) => {


        fetch(`/Home/deleteEmployee?idEmployee=${_employee.idEmployee}`, {
            method: "DELETE"
        })
            .then(response => {
                return response.ok ? response.json() : Promise.reject(response);
            })
            .then(responseJson => {

                if (responseJson.value == true) {
                    Swal.fire("Reade!", "Employee was Delete", "success")
                    ShowEmployees()
                }
                else {
                    Swal.fire("Sorry!", "Employee wasn't Delete", "error")
                }
            })


    });
});