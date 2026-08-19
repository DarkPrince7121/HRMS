function AuditViewModel() {
    var self = this;
    self.logs = ko.observableArray([]);

    self.loadLogs = function() {
        $.getJSON("/api/audit", function(data) {
            self.logs(data);
        });
    };

    self.loadLogs();
}