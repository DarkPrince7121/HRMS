function HolidaysViewModel() {
    var self = this;
    self.holidays = ko.observableArray([]);
    self.newName = ko.observable("");
    self.newDate = ko.observable("");
    self.newIsRecurring = ko.observable(false);

    self.loadHolidays = function() {
        $.getJSON("/api/holidays", function(data) {
            self.holidays(data);
        });
    };

    self.addHoliday = function() {
        var data = {
            name: self.newName(),
            date: self.newDate(),
            isRecurring: self.newIsRecurring()
        };
        $.ajax({
            url: "/api/holidays",
            type: "POST",
            contentType: "application/json",
            data: JSON.stringify(data),
            success: function() {
                self.newName("");
                self.newDate("");
                self.newIsRecurring(false);
                self.loadHolidays();
            }
        });
    };

    self.deleteHoliday = function(holiday) {
        $.ajax({
            url: "/api/holidays/" + holiday.id,
            type: "DELETE",
            success: function() {
                self.loadHolidays();
            }
        });
    };

    self.loadHolidays();
}