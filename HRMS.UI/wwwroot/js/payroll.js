function PayrollViewModel() {
    var self = this;
    
    self.selectedMonth = ko.observable(new Date().toISOString().substring(0, 7)); // YYYY-MM
    self.isGenerating = ko.observable(false);
    
    var table;

    self.initDataTable = function () {
        if ($.fn.DataTable.isDataTable('#payrollTable')) {
            $('#payrollTable').DataTable().destroy();
        }

        table = $('#payrollTable').DataTable({
            ajax: '/Payroll/GetAll',
            order: [[1, 'desc']],
            columns: [
                { data: 'employeeName' },
                {
                    data: 'payDate',
                    render: function (data) {
                        return new Date(data).toLocaleDateString();
                    }
                },
                {
                    data: 'grossPay',
                    render: function (data) {
                        return '$' + data.toFixed(2);
                    }
                },
                {
                    data: 'deductions',
                    render: function (data) {
                        return '$' + data.toFixed(2);
                    }
                },
                {
                    data: 'netPay',
                    render: function (data) {
                        return '<strong>$' + data.toFixed(2) + '</strong>';
                    }
                },
                {
                    data: 'status',
                    render: function (data) {
                        var badgeClass = data === 'Processed' ? 'bg-success' : 'bg-warning';
                        return '<span class="badge ' + badgeClass + '">' + data + '</span>';
                    }
                }
            ]
        });
    };

    self.generatePayroll = function () {
        if (!self.selectedMonth()) {
            alert('Please select a month');
            return;
        }

        self.isGenerating(true);
        
        // Append day for DateTime parsing in C# (YYYY-MM-01)
        var payload = {
            month: self.selectedMonth() + "-01"
        };

        $.ajax({
            url: '/Payroll/Generate',
            type: 'POST',
            contentType: 'application/json',
            data: JSON.stringify(payload),
            success: function (response) {
                if (response.success) {
                    if (table) {
                        table.ajax.reload();
                    } else {
                        self.initDataTable();
                    }
                    alert('Payroll generated successfully');
                } else {
                    alert('Error generating payroll');
                }
            },
            error: function () {
                alert('Error connecting to server');
            },
            complete: function () {
                self.isGenerating(false);
            }
        });
    };
}