function LoginViewModel() {
    var self = this;
    self.username = ko.observable('');
    self.password = ko.observable('');
    self.errorMessage = ko.observable('');

    self.login = function () {
        self.errorMessage('');
        var loginData = {
            username: self.username(),
            password: self.password()
        };

        fetch('/Account/Login', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(loginData)
        })
            .then(async response => {
                console.log(response);
            if (response.ok) {
                debugger;
                var result = await response.json();
                if (result.needsEmployeeRegistration) {
                    window.location.href = '/Account/RegisterEmployee';
                } else {
                    window.location.href = '/';
                }
            } else {
                var error = await response.json();
                self.errorMessage(error.message || 'Login failed');
            }
        })
        .catch(error => {
            console.error('Error:', error);
            self.errorMessage('An unexpected error occurred.');
        });
    };
}

function RegisterViewModel() {
    var self = this;
    self.username = ko.observable('');
    self.email = ko.observable('');
    self.password = ko.observable('');
    self.errorMessage = ko.observable('');
    self.successMessage = ko.observable('');

    self.register = function () {
        self.errorMessage('');
        self.successMessage('');
        
        var registerData = {
            username: self.username(),
            email: self.email(),
            password: self.password()
        };

        fetch('/Account/Register', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(registerData)
        })
        .then(async response => {
            if (response.ok) {
                self.successMessage('Registration successful! Redirecting to login...');
                setTimeout(function() {
                    window.location.href = '/Account/Login';
                }, 2000);
            } else {
                var error = await response.json();
                self.errorMessage(error.message || 'Registration failed');
            }
        })
        .catch(error => {
            console.error('Error:', error);
            self.errorMessage('An unexpected error occurred.');
        });
    };
}

function EmployeeRegistrationViewModel() {
    var self = this;
    self.firstName = ko.observable('');
    self.lastName = ko.observable('');
    self.departmentId = ko.observable(0);
    self.baseSalary = ko.observable(0);
    self.departments = ko.observableArray([]);
    self.isAdmin = ko.observable(false); // Hardcoded false for this view as per requirement
    self.errorMessage = ko.observable('');

    self.registerEmployee = function() {
        var data = {
            firstName: self.firstName(),
            lastName: self.lastName(),
            departmentId: self.departmentId(),
            baseSalary: self.baseSalary()
        };

        fetch('/Account/RegisterEmployee', {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(data)
        })
        .then(async response => {
            if (response.ok) {
                window.location.href = '/';
            } else {
                var error = await response.json();
                self.errorMessage(error.message || 'Failed to save profile');
            }
        })
        .catch(err => {
            self.errorMessage('An unexpected error occurred.');
        });
    };
}