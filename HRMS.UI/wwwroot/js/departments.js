function DepartmentViewModel() {
    var self = this;
    self.departments = ko.observableArray([]);
    self.isEdit = ko.observable(false);
    self.currentDepartment = {
        id: ko.observable(0),
        name: ko.observable(''),
        description: ko.observable('')
    };

    self.searchTerm = ko.observable('');

    var table;

    self.initDataTable = function () {
        table = $('#departmentsTable').DataTable({
            dom: 'Bfrtip',
            buttons: [
                'copy', 'csv', 'excel', 'pdf', 'print'
            ],
            ajax: {
                url: '/Departments/GetAll',
                data: function (d) {
                    d.searchTerm = self.searchTerm();
                }
            },
            columns: [
                { data: 'name' },
                { data: 'description' },
                {
                    data: 'id',
                    render: function (data) {
                        return '<button class="btn btn-sm btn-info me-2" onclick="editDepartment(' + data + ')">Edit</button>' +
                               '<button class="btn btn-sm btn-danger" onclick="deleteDepartment(' + data + ')">Delete</button>';
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
        table.ajax.reload();
    };

    self.resetForm = function () {
        self.isEdit(false);
        self.currentDepartment.id(0);
        self.currentDepartment.name('');
        self.currentDepartment.description('');
    };

    self.saveDepartment = function () {
        var url = self.isEdit() ? '/Departments/Edit' : '/Departments/Create';
        var data = {
            id: self.currentDepartment.id(),
            name: self.currentDepartment.name(),
            description: self.currentDepartment.description()
        };

        $.ajax({
            url: url,
            type: 'POST',
            contentType: 'application/json',
            data: JSON.stringify(data),
            success: function (response) {
                if (response.success || response.data) {
                    $('#departmentModal').modal('hide');
                    table.ajax.reload();
                } else {
                    alert('Error saving department');
                }
            }
        });
    };

    window.editDepartment = function (id) {
        var data = table.rows().data().toArray().find(x => x.id === id);
        if (data) {
            self.isEdit(true);
            self.currentDepartment.id(data.id);
            self.currentDepartment.name(data.name);
            self.currentDepartment.description(data.description);
            $('#departmentModal').modal('show');
        }
    };

    window.deleteDepartment = function (id) {
        if (confirm('Are you sure you want to delete this department?')) {
            $.post('/Departments/Delete/' + id, function (response) {
                if (response.success) {
                    table.ajax.reload();
                } else {
                    alert('Error deleting department');
                }
            });
        }
    };

    self.initDataTable();
}

$(document).ready(function () {
    ko.applyBindings(new DepartmentViewModel());
});