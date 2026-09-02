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
            if (response && response.data) {
                self.employees(response.data);
            }
        });
    };

    self.initDataTable = function () {
        var tableElement = $('#leavesTable');
        if (tableElement.length && !$.fn.DataTable.isDataTable('#leavesTable')) {
            table = tableElement.DataTable({
                ajax: '/Leave/GetAll',
                columns: [
                    { data: 'employeeName' },
                    { data: 'leaveType' },
                    {
                        data: null,
                        render: function (data, type, row) {
                            var start = row.startDate ? new Date(row.startDate).toLocaleDateString() : '';
                            var end = row.endDate ? new Date(row.endDate).toLocaleDateString() : '';
                            return start + ' - ' + end;
                        }
                    },
                    {
                        data: 'status',
                        render: function (data) {
                            var badgeClass = 'bg-secondary';
                            if (data === 'Approved') badgeClass = 'bg-success';
                            if (data === 'Rejected') badgeClass = 'bg-danger';
                            if (data === 'Pending') badgeClass = 'bg-warning text-dark';
                            return '<span class="badge ' + badgeClass + '">' + data + '</span>';
                        }
                    },
                    {
                        data: 'id',
                        className: 'text-end',
                        render: function (data, type, row) {
                            var buttons = '';
                            if (row.status === 'Pending') {
                                buttons += '<button class="btn btn-sm btn-outline-success me-2" onclick="updateLeaveStatus(' + data + ', \'Approved\')"><i class="fas fa-check"></i></button>';
                                buttons += '<button class="btn btn-sm btn-outline-danger" onclick="updateLeaveStatus(' + data + ', \'Rejected\')"><i class="fas fa-times"></i></button>';
                            }
                            return buttons;
                        }
                    }
                ]
            });
        }
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
                    var modalEl = document.getElementById('leaveModal');
                    var modal = bootstrap.Modal.getInstance(modalEl);
                    if (modal) modal.hide();
                    
                    if (table) table.ajax.reload();
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
                if (table) table.ajax.reload();
            } else {
                alert('Error updating status');
            }
        });
    };

    self.loadEmployees();
    // Remove $(document).ready from within the ViewModel to avoid timing issues with external init
}

// Initialize when DOM is ready
$(document).ready(function() {
    var vm = new LeaveViewModel();
    var el = document.getElementById('pageContent');
    if (el && !ko.dataFor(el)) {
        ko.applyBindings(vm, el);
        vm.initDataTable();
    }
});