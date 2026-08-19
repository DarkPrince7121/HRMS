function DashboardViewModel() {
    var self = this;
    
    self.employeeCount = ko.observable(0);
    self.departmentCount = ko.observable(0);
    self.activeLeaveCount = ko.observable(0);
    self.isLoading = ko.observable(true);
    self.error = ko.observable("");

    self.loadStats = function() {
        self.isLoading(true);
        $.getJSON("/api/dashboard/stats")
            .done(function(data) {
                self.employeeCount(data.employeeCount);
                self.departmentCount(data.departmentCount);
                self.activeLeaveCount(data.activeLeaveCount);
                self.error("");
            })
            .fail(function() {
                self.error("Failed to load dashboard statistics.");
            })
            .always(function() {
                self.isLoading(false);
            });
    };

    self.loadStats();
}