function SettingsViewModel() {
    var self = this;
    self.settings = ko.observableArray([]);

    self.loadSettings = function () {
        $.getJSON('/api/settings', function (data) {
            self.settings(data);
        });
    };

    self.updateSetting = function (setting) {
        $.ajax({
            url: '/api/settings',
            type: 'POST',
            contentType: 'application/json',
            data: JSON.stringify({ key: setting.key, value: setting.value }),
            success: function () {
                alert('Setting updated successfully');
            }
        });
    };

    self.loadSettings();
}