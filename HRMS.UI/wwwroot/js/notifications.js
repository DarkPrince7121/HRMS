function NotificationViewModel() {
    var self = this;
    self.notifications = ko.observableArray([]);
    self.unreadCount = ko.computed(function () {
        return self.notifications().filter(n => !n.isRead).length;
    });

    self.loadNotifications = function () {
        $.getJSON('/api/notifications', function (data) {
            self.notifications(data);
        });
    };

    self.markAllRead = function () {
        $.post('/api/notifications/read-all', function () {
            self.loadNotifications();
        });
    };

    // Initial load and polling
    self.loadNotifications();
    setInterval(self.loadNotifications, 30000);
}

$(document).ready(function () {
    var notificationVm = new NotificationViewModel();
    ko.applyBindings(notificationVm, document.getElementById('notificationDropdown'));
});