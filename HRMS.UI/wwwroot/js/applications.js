function ApplicationsViewModel() {
    var self = this;

    // List view state
    self.applications = ko.observableArray([]);

    // Wizard state
    self.isWizardActive = ko.observable(false);
    self.currentStep = ko.observable(1);
    self.currentApplicationId = ko.observable(null);
    self.stepError = ko.observable('');
    self.isResuming = ko.observable(false);
    
    // Step 2 & 3 state
    self.employees = ko.observableArray([]);
    self.selectedEmployeeId = ko.observable(null);
    self.selectedEmployeeName = ko.observable('');
    self.selectedEmployeeEmail = ko.observable('');
    self.selectedEmployeeDepartment = ko.observable('');
    self.selectedEmployeeSalary = ko.observable(0.00);
    self.netBalance = ko.observable(0.00);
    self.enteredAmount = ko.observable(0.00);
    self.processedSalary = ko.observable(0.00);
    self.pendingDeductions = ko.observable(0.00);

    // Remaining balance computed property
    self.remainingBalance = ko.computed(function() {
        var net = parseFloat(self.netBalance()) || 0;
        var entered = parseFloat(self.enteredAmount()) || 0;
        return net - entered;
    });

    // Load existing withdrawal applications list
    self.loadApplications = function () {
        $.ajax({
            url: '/api/applications',
            type: 'GET',
            success: function (data) {
                if (data) {
                    self.applications(data);
                }
            },
            error: function () {
                console.error("Failed to load applications list.");
            }
        });
    };

    // Load active employees list
    self.loadEmployees = function (callback) {
        $.ajax({
            url: '/api/employees',
            type: 'GET',
            success: function (data) {
                if (data) {
                    self.employees(data);
                    if (callback) callback();
                }
            },
            error: function () {
                console.error("Failed to load employees.");
            }
        });
    };

    // React on selected employee changed to fetch real-time net balance
    self.selectedEmployeeId.subscribe(function (empId) {
        if (empId) {
            var foundEmp = self.employees().find(e => e.id == empId);
            if (foundEmp) {
                self.selectedEmployeeName(foundEmp.name);
                self.selectedEmployeeEmail(foundEmp.email || '');
                self.selectedEmployeeDepartment(foundEmp.department || 'N/A');
                self.selectedEmployeeSalary(foundEmp.baseSalary || 0.00);
            }
            // Fetch real-time balance
            $.ajax({
                url: '/api/employees/' + empId + '/balance',
                type: 'GET',
                success: function (balanceData) {
                    if (balanceData) {
                        self.netBalance(balanceData.netBalance || 0.00);
                        self.processedSalary(balanceData.amountBalance || 0.00);
                        self.pendingDeductions(balanceData.pendingWithdrawals || 0.00);
                    }
                },
                error: function () {
                    self.stepError("Could not retrieve real-time balance.");
                }
            });
        } else {
            self.selectedEmployeeName('');
            self.selectedEmployeeEmail('');
            self.selectedEmployeeDepartment('');
            self.selectedEmployeeSalary(0.00);
            self.netBalance(0.00);
            self.processedSalary(0.00);
            self.pendingDeductions(0.00);
        }
    });

    // Initiate/Resume wizard
    self.initiateWizard = function () {
        self.stepError('');
        $.ajax({
            url: '/api/applications/initiate',
            type: 'POST',
            success: function (data) {
                if (data) {
                    self.currentApplicationId(data.id);
                    self.enteredAmount(data.amount || 0);
                    
                    // Load employees first, then set selectedEmployeeId so that the subscription finds the employee!
                    self.loadEmployees(function() {
                        self.selectedEmployeeId(data.selectedEmployeeId);
                    });
                    
                    // Set wizard step based on resume status or default to 1
                    if (data.statusId && data.statusId < 4) {
                        self.currentStep(data.statusId);
                        self.isResuming(data.statusId > 1 || (data.selectedEmployeeId !== null) || (data.amount > 0));
                    } else {
                        self.currentStep(1);
                        self.isResuming(false);
                    }
                    
                    // Open wizard modal
                    self.isWizardActive(true);
                    var modalEl = document.getElementById('wizardModal');
                    if (modalEl) {
                        var modal = new bootstrap.Modal(modalEl);
                        modal.show();
                    }
                }
            },
            error: function () {
                alert("Failed to initiate withdrawal application.");
            }
        });
    };

    // Next step progression and validation
    self.nextStep = function () {
        self.stepError('');
        var step = self.currentStep();

        if (step === 1) {
            // Instructions Step - simply advance status to step 2 in DB and UI
            self.updateApplicationStatus(2, function() {
                self.currentStep(2);
            });
        } 
        else if (step === 2) {
            // Target Employee Selection
            var empId = self.selectedEmployeeId();
            if (!empId) {
                self.stepError("Please select a target employee to proceed.");
                return;
            }
            self.updateApplicationStatus(3, function() {
                self.currentStep(3);
            });
        } 
        else if (step === 3) {
            // Amount Entering
            var amount = parseFloat(self.enteredAmount());
            if (isNaN(amount) || amount <= 0) {
                self.stepError("Please enter a valid amount greater than 0.");
                return;
            }
            if (amount > self.netBalance()) {
                self.stepError("Entered amount exceeds the employee's available net salary balance of $" + self.netBalance().toFixed(2));
                return;
            }
            self.updateApplicationStatus(3, function() { // Stay on status 3 but save amount, then advance step UI to 4
                self.currentStep(4);
            });
        }
    };

    // Previous step regression
    self.prevStep = function () {
        self.stepError('');
        var step = self.currentStep();
        if (step > 1) {
            var targetStatus = step - 1;
            // Sync status to DB when moving back
            self.updateApplicationStatus(targetStatus, function() {
                self.currentStep(targetStatus);
            });
        }
    };

    // Save current status, employee, or amount changes to backend
    self.updateApplicationStatus = function (statusId, callback) {
        var payload = {
            statusId: statusId,
            amount: parseFloat(self.enteredAmount()) || 0,
            selectedEmployeeId: self.selectedEmployeeId() ? parseInt(self.selectedEmployeeId()) : null
        };

        $.ajax({
            url: '/api/applications/' + self.currentApplicationId() + '/status',
            type: 'PUT',
            contentType: 'application/json',
            data: JSON.stringify(payload),
            success: function () {
                if (callback) callback();
            },
            error: function () {
                self.stepError("Failed to synchronize application progress with server.");
            }
        });
    };

    // Complete / Final Submit application process
    self.completeApplication = function () {
        self.stepError('');
        var id = self.currentApplicationId();
        if (!id) return;

        $.ajax({
            url: '/api/applications/' + id + '/complete',
            type: 'POST',
            success: function (response) {
                if (response.success) {
                    // Hide modal
                    var modalEl = document.getElementById('wizardModal');
                    if (modalEl) {
                        var modal = bootstrap.Modal.getInstance(modalEl);
                        if (modal) modal.hide();
                    }
                    self.isWizardActive(false);
                    // Refresh applications list view
                    self.loadApplications();
                    alert("Withdrawal application successfully submitted and processed!");
                    window.location.href = "/";
                }
            },
            error: function (xhr) {
                var message = "Error completing withdrawal application.";
                if (xhr.responseJSON && xhr.responseJSON.message) {
                    message = xhr.responseJSON.message;
                }
                self.stepError(message);
            }
        });
    };

    // Initialization
    self.init = function() {
        self.loadApplications();
    };
}

// Single-page Navigation interception without page refresh
$(document).ready(function() {
    // Check if on /Applications or load it dynamically on Applications navbar click
    function setupApplicationsBinding() {
        var el = document.getElementById('applicationsPageContainer');
        if (el && !ko.dataFor(el)) {
            var vm = new ApplicationsViewModel();
            ko.applyBindings(vm, el);
            vm.init();
        }
    }

    // Intercept clicks on 'Applications' link in the layout header
    $(document).on('click', '#nav-applications-link', function(e) {
        e.preventDefault();
        
        // Update browser URL history without reloading
        window.history.pushState({}, '', '/Applications');
        
        // Fetch view from /Applications/Index and insert container
        $('#mainContent').load('/Applications/Index #applicationsPageContainer', function(response, status, xhr) {
            if (status === "error") {
                console.error("Error loading Applications view dynamically:", xhr.status, xhr.statusText);
            } else {
                setupApplicationsBinding();
            }
        });
    });

    // Handle initial direct load of /Applications or regular page load
    setupApplicationsBinding();
});
