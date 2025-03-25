document.addEventListener("DOMContentLoaded", function () {
    let sectorDropdown = document.getElementById("SectorId");
    let departmentDropdown = document.getElementById("DepartmentId");

    if (!sectorDropdown || !departmentDropdown) return;

    function loadDepartments(sectorId) {
        if (sectorId) {
            fetch(`/Employee/GetDepartmentsBySector?sectorId=${sectorId}`)
                .then(response => response.json())
                .then(data => {
                    departmentDropdown.innerHTML = '<option value="" disabled selected>Select a department</option>';
                    if (data.length > 0) {
                        let department = data[0];
                        departmentDropdown.innerHTML += `<option value="${department.id}" selected>${department.name}</option>`;
                    }
                });
        } else {
            departmentDropdown.innerHTML = '<option value="" disabled selected>Select a department</option>';
        }
    }

    function loadSectors(departmentId) {
        if (departmentId) {
            fetch(`/Employee/GetSectorsByDepartment?departmentId=${departmentId}`)
                .then(response => response.json())
                .then(data => {
                    sectorDropdown.innerHTML = '<option value="" disabled selected>Select a sector</option>';
                    data.forEach(sector => {
                        sectorDropdown.innerHTML += `<option value="${sector.id}">${sector.name}</option>`;
                    });
                });
        } else {
            sectorDropdown.innerHTML = '<option value="" disabled selected>Select a sector</option>';
        }
    }

    sectorDropdown.addEventListener("change", function () {
        loadDepartments(this.value);
    });

    departmentDropdown.addEventListener("change", function () {
        loadSectors(this.value);
    });
});
