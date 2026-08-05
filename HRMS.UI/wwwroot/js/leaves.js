function LeaveViewModel() {
    var self = this;
    self.leaves = ko.observableArray([]);
    self.employees = ko.observableArray([]);
    self.currentLeave = {
        employeeId: ko.observable(null),
        startDate: ko.observable(new Date().toISOString().split('T')[0]),
        endDate: ko.observable(new Date().toISOString().split('T')[0]),
        leaveType: ko.observable('Sick Leave'),
        reason: ko.observable('')
    };

    var table;

    self.loadEmployees = function () {
        $.get('/Employees/GetAll', function (response) {
            self.employees(response.data);
        });
    };

    self.initDataTable = function () {
        table = $('#leavesTable').DataTable({
            ajax: '/Leave/GetAll',
            columns: [
                { data: 'employeeName' },
                { data: 'leaveType' },
                {
                    data: 'startDate',
                    render: function (data) {
                        return new Date(data).toLocaleDateString();
                    }
                },
                {
                    data: 'endDate',
                    render: function (data) {
                        return new Date(data).toLocaleDateString();
                    }
                },
                { data: 'status' },
                {
                    data: 'id',
                    render: function (data, type, row) {
                        var buttons = '';
                        if (row.status === 'Pending') {
                            buttons += '<button class="btn btn-sm btn-success me-2" onclick="updateLeaveStatus(' + data + ', \'Approved\')">Approve</button>';
                            buttons += '<button class="btn btn-sm btn-danger" onclick="updateLeaveStatus(' + data + ', \'Rejected\')">Reject</button>';
                        }
                        return buttons;
                    }
                }
            ]
        });
    };

    self.requestLeave = function () {
        var data = {
            employeeId: self.currentLeave.employeeId(),
            startDate: self.currentLeave.startDate(),
            endDate: self.currentLeave.endDate(),
            leaveType: self.currentLeave.leaveType(),
            reason: self.currentLeave.reason()
        };

        $.ajax({
            url: '/Leave/RequestLeave',
            type: 'POST',
            contentType: 'application/json',
            data: JSON.stringify(data),
            success: function (response) {
                if (response.success) {
                    $('#leaveModal').modal('hide');
                    table.ajax.reload();
                    self.resetForm();
                } else {
                    alert('Error submitting leave request');
                }
            }
        });
    };

    self.resetForm = function () {
        self.currentLeave.employeeId(null);
        self.currentLeave.startDate(new Date().toISOString().split('T')[0]);
        self.currentLeave.endDate(new Date().toISOString().split('T')[0]);
        self.currentLeave.leaveType('Sick Leave');
        self.currentLeave.reason('');
    };

    window.updateLeaveStatus = function (id, status) {
        $.post('/Leave/UpdateStatus', { id: id, status: status }, function (response) {
            if (response.success) {
                table.ajax.reload();
            } else {
                alert('Error updating status');
            }
        });
    };

    self.loadEmployees();
    self.initDataTable();
}

$(document).ready(function () {
    ko.applyBindings(new LeaveViewModel());
});