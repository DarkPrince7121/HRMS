function EmployeeViewModel() {
    var self = this;
    self.departments = ko.observableArray([]);
    self.isEdit = ko.observable(false);
    self.currentEmployee = {
        id: ko.observable(0),
        firstName: ko.observable(''),
        lastName: ko.observable(''),
        email: ko.observable(''),
        departmentId: ko.observable(null),
        baseSalary: ko.observable(0)
    };

    // Search filters
    self.searchTerm = ko.observable('');
    self.selectedDepartmentId = ko.observable(null);

    var table;

    self.loadDepartments = function () {
        $.get('/Departments/GetAll', function (response) {
            self.departments(response.data);
        });
    };

    self.initDataTable = function () {
        table = $('#employeesTable').DataTable({
            dom: 'Bfrtip',
            buttons: [
                'copy', 'csv', 'excel', 'pdf', 'print'
            ],
            ajax: {
                url: '/Employees/GetAll',
                data: function (d) {
                    d.searchTerm = self.searchTerm();
                    d.departmentId = self.selectedDepartmentId();
                }
            },
            columns: [
                { data: 'firstName' },
                { data: 'lastName' },
                { data: 'email' },
                { data: 'departmentName' },
                {
                    data: 'id',
                    render: function (data) {
                        return '<button class="btn btn-sm btn-info me-2" onclick="editEmployee(' + data + ')">Edit</button>' +
                               '<button class="btn btn-sm btn-danger" onclick="deleteEmployee(' + data + ')">Delete</button>';
                    }
                }
            ]
        });
    };

    self.applyFilters = function() {
        table.ajax.reload();
    };

    self.resetFilters = function() {
        self.searchTerm('');
        self.selectedDepartmentId(null);
        table.ajax.reload();
    };

    self.resetForm = function () {
        self.isEdit(false);
        self.currentEmployee.id(0);
        self.currentEmployee.firstName('');
        self.currentEmployee.lastName('');
        self.currentEmployee.email('');
        self.currentEmployee.departmentId(null);
        self.currentEmployee.baseSalary(0);
    };

    self.saveEmployee = function () {
        var url = self.isEdit() ? '/Employees/Edit' : '/Employees/Create';
        var data = {
            id: self.currentEmployee.id(),
            firstName: self.currentEmployee.firstName(),
            lastName: self.currentEmployee.lastName(),
            email: self.currentEmployee.email(),
            departmentId: self.currentEmployee.departmentId(),
            baseSalary: self.currentEmployee.baseSalary()
        };

        $.ajax({
            url: url,
            type: 'POST',
            contentType: 'application/json',
            data: JSON.stringify(data),
            success: function (response) {
                if (response.success || response.data) {
                    $('#employeeModal').modal('hide');
                    table.ajax.reload();
                } else {
                    alert('Error saving employee');
                }
            }
        });
    };

    window.editEmployee = function (id) {
        var data = table.rows().data().toArray().find(x => x.id === id);
        if (data) {
            self.isEdit(true);
            self.currentEmployee.id(data.id);
            self.currentEmployee.firstName(data.firstName);
            self.currentEmployee.lastName(data.lastName);
            self.currentEmployee.email(data.email);
            self.currentEmployee.departmentId(data.departmentId);
            self.currentEmployee.baseSalary(data.baseSalary);
            $('#employeeModal').modal('show');
        }
    };

    window.deleteEmployee = function (id) {
        if (confirm('Are you sure you want to delete this employee?')) {
            $.ajax({
                url: '/Employees/Delete?id=' + id,
                type: 'POST',
                success: function (response) {
                    if (response.success) {
                        table.ajax.reload();
                    } else {
                        alert('Error deleting employee');
                    }
                }
            });
        }
    };

    self.loadDepartments();
    self.initDataTable();
}