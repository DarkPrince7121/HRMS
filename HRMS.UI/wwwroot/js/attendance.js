function AttendanceViewModel() {
    var self = this;
    self.records = ko.observableArray([]);
    self.employees = ko.observableArray([]);
    self.currentAttendance = {
        employeeId: ko.observable(null),
        date: ko.observable(new Date().toISOString().split('T')[0]),
        status: ko.observable('Present'),
        remarks: ko.observable('')
    };

    var table;

    self.loadEmployees = function () {
        $.get('/Employees/GetAll', function (response) {
            self.employees(response.data);
        });
    };

    self.initDataTable = function () {
        table = $('#attendanceTable').DataTable({
            ajax: '/Attendance/GetAll',
            columns: [
                { data: 'employeeName' },
                {
                    data: 'date',
                    render: function (data) {
                        return new Date(data).toLocaleDateString();
                    }
                },
                { data: 'status' },
                { data: 'remarks' }
            ]
        });
    };

    self.markAttendance = function () {
        var data = {
            employeeId: self.currentAttendance.employeeId(),
            date: self.currentAttendance.date(),
            status: self.currentAttendance.status(),
            remarks: self.currentAttendance.remarks()
        };

        $.ajax({
            url: '/Attendance/Mark',
            type: 'POST',
            contentType: 'application/json',
            data: JSON.stringify(data),
            success: function (response) {
                if (response.success) {
                    $('#attendanceModal').modal('hide');
                    table.ajax.reload();
                    self.resetForm();
                } else {
                    alert('Error marking attendance');
                }
            }
        });
    };

    self.resetForm = function () {
        self.currentAttendance.employeeId(null);
        self.currentAttendance.date(new Date().toISOString().split('T')[0]);
        self.currentAttendance.status('Present');
        self.currentAttendance.remarks('');
    };

    self.loadEmployees();
    self.initDataTable();
}