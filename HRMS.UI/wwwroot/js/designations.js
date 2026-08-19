function DesignationsViewModel() {
    var self = this;
    self.designations = ko.observableArray([]);
    self.isEdit = ko.observable(false);
    self.currentDesignation = {
        id: ko.observable(0),
        name: ko.observable(''),
        description: ko.observable('')
    };

    self.searchTerm = ko.observable('');

    var table;

    self.initDataTable = function () {
        table = $('#designationsTable').DataTable({
            dom: 'Bfrtip',
            buttons: [
                'copy', 'csv', 'excel', 'pdf', 'print'
            ],
            ajax: {
                url: '/Designations/GetAll',
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
                        return '<button class="btn btn-sm btn-info me-2" onclick="editDesignation(' + data + ')">Edit</button>' +
                               '<button class="btn btn-sm btn-danger" onclick="deleteDesignation(' + data + ')">Delete</button>';
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
        self.currentDesignation.id(0);
        self.currentDesignation.name('');
        self.currentDesignation.description('');
    };

    self.saveDesignation = function () {
        var url = self.isEdit() ? '/Designations/Edit' : '/Designations/Create';
        var data = {
            id: self.currentDesignation.id(),
            name: self.currentDesignation.name(),
            description: self.currentDesignation.description()
        };

        $.ajax({
            url: url,
            type: 'POST',
            contentType: 'application/json',
            data: JSON.stringify(data),
            success: function (response) {
                if (response.success || response.data) {
                    $('#designationModal').modal('hide');
                    table.ajax.reload();
                } else {
                    alert('Error saving designation');
                }
            }
        });
    };

    window.editDesignation = function (id) {
        var data = table.rows().data().toArray().find(x => x.id === id);
        if (data) {
            self.isEdit(true);
            self.currentDesignation.id(data.id);
            self.currentDesignation.name(data.name);
            self.currentDesignation.description(data.description);
            $('#designationModal').modal('show');
        }
    };

    window.deleteDesignation = function (id) {
        if (confirm('Are you sure you want to delete this designation?')) {
            $.ajax({
                url: '/Designations/Delete?id=' + id,
                type: 'POST',
                success: function (response) {
                    if (response.success) {
                        table.ajax.reload();
                    } else {
                        alert('Error deleting designation');
                    }
                }
            });
        }
    };

    self.initDataTable();
}