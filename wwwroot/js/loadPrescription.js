// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
// ==========================
// loadPrescription.js
// ==========================

// Sample medication data
const medications = [
    {
        id: 1,
        name: "Amoxicillin 500mg",
        active: "Amoxicillin",
        stock: 142,
        reorder: 50,
        forms: ["Capsule"],
        schedule: "S4"
    },
    {
        id: 2,
        name: "Lisinopril 10mg",
        active: "Lisinopril",
        stock: 89,
        reorder: 30,
        forms: ["Tablet"],
        schedule: "S2"
    },
    {
        id: 3,
        name: "Panado Extra",
        active: "Paracetamol 500mg + Caffeine 65mg",
        stock: 256,
        reorder: 100,
        forms: ["Tablet", "Capsule"],
        schedule: "S0"
    },
    {
        id: 4,
        name: "Ventolin Inhaler",
        active: "Salbutamol 100mcg",
        stock: 75,
        reorder: 30,
        forms: ["Inhaler"],
        schedule: "S3"
    }
];

const commonInstructions = [
    "Take 1 tablet daily",
    "Take 1 tablet twice daily",
    "Take 2 tablets at bedtime",
    "Take as needed for pain",
    "Other (specify)"
];

// On DOM Ready
window.addEventListener('DOMContentLoaded', function () {
    addMedicationRow();

    document.getElementById('addPatientBtn').addEventListener('click', () => {
        document.getElementById('patientModal').style.display = 'flex';
    });

    document.getElementById('prescriptionUpload').addEventListener('change', function (e) {
        const file = e.target.files[0];
        const pdfViewer = document.getElementById('pdfViewer');

        if (file && file.type === 'application/pdf') {
            const reader = new FileReader();
            reader.onload = function (e) {
                pdfViewer.innerHTML = `<embed src="${e.target.result}" type="application/pdf" width="100%" height="100%">`;
            };
            reader.readAsDataURL(file);
        } else {
            pdfViewer.innerHTML = '<p class="placeholder">Please upload a valid PDF file.</p>';
        }
    });

    document.getElementById('customerSelect').addEventListener('change', function () {
        const patientId = this.value;
        const patients = {
            '1': { dob: '15/06/1990', allergies: 'Penicillin, Sulfa drugs', conditions: 'Hypertension' },
            '2': { dob: '22/02/1989', allergies: 'None recorded', conditions: 'Diabetes' },
            '3': { dob: '03/03/1988', allergies: 'Ibuprofen', conditions: 'None recorded' }
        };

        if (patients[patientId]) {
            document.getElementById('dob').textContent = patients[patientId].dob;
            document.getElementById('allergies').textContent = patients[patientId].allergies;
            document.getElementById('conditions').textContent = patients[patientId].conditions;
        }
    });

    document.getElementById('savePatientBtn').addEventListener('click', () => {
        const firstName = document.getElementById('patientFirstName').value;
        const lastName = document.getElementById('patientLastName').value;
        const idNumber = document.getElementById('patientIdNumber').value;

        if (firstName && lastName && idNumber) {
            const select = document.getElementById('customerSelect');
            const option = document.createElement('option');
            option.value = idNumber;
            option.textContent = `${firstName} ${lastName} (ID: ${idNumber})`;
            select.appendChild(option);
            select.value = idNumber;
            select.dispatchEvent(new Event('change'));

            closeModal('patientModal');

            document.getElementById('patientFirstName').value = '';
            document.getElementById('patientLastName').value = '';
            document.getElementById('patientIdNumber').value = '';
        } else {
            alert('Please fill in all required fields');
        }
    });

    document.getElementById('savePrescriptionBtn').addEventListener('click', () => {
        alert('Prescription saved successfully! (This would submit to server in a real app)');
    });

    document.getElementById('addMedBtn').addEventListener('click', addMedicationRow);
});

function addMedicationRow() {
    const tbody = document.getElementById('medsTableBody');
    const rowId = Date.now();

    let medOptions = '<option value="">Select medication...</option>';
    medications.forEach(med => {
        medOptions += `<option value="${med.id}" data-forms="${med.forms.join(',')}">${med.name} (${med.schedule})</option>`;
    });

    let instructionOptions = '';
    commonInstructions.forEach(inst => {
        instructionOptions += `<option value="${inst}">${inst}</option>`;
    });

    const row = document.createElement('tr');
    row.id = `row-${rowId}`;
    row.innerHTML = `
        <td>
            <div class="medication-select-container">
                <input type="text" class="medication-search" placeholder="Search medications..." id="med-search-${rowId}">
                <select class="medication-select" id="med-select-${rowId}" onchange="updateMedDetails(this, ${rowId})" style="display: none;">
                    ${medOptions}
                </select>
                <div class="medication-options" id="med-options-${rowId}"></div>
            </div>
        </td>
        <td>
            <select class="dosage-form-select" id="form-${rowId}">
                <option value="">Select form</option>
            </select>
        </td>
        <td><input type="number" min="1" value="1" class="qty-input"></td>
        <td>
            <select class="instruction-select" onchange="handleInstructionSelect(this, ${rowId})">
                ${instructionOptions}
            </select>
            <div id="custom-inst-${rowId}" style="display:none; margin-top:5px;">
                <textarea placeholder="Enter custom instructions..." rows="2"></textarea>
            </div>
        </td>
        <td><div class="med-details" id="details-${rowId}"></div></td>
        <td><button class="remove-btn" onclick="removeRow(${rowId})"><i class="fas fa-trash"></i></button></td>
    `;

    tbody.appendChild(row);
    initMedicationSearch(rowId);
}

function initMedicationSearch(rowId) {
    const searchInput = document.getElementById(`med-search-${rowId}`);
    const optionsContainer = document.getElementById(`med-options-${rowId}`);
    const select = document.getElementById(`med-select-${rowId}`);

    medications.forEach(med => {
        const option = document.createElement('div');
        option.className = 'medication-option';
        option.textContent = `${med.name} (${med.schedule})`;
        option.setAttribute('data-id', med.id);
        option.addEventListener('click', function () {
            select.value = med.id;
            searchInput.value = `${med.name} (${med.schedule})`;
            optionsContainer.style.display = 'none';
            updateMedDetails(select, rowId);
        });
        optionsContainer.appendChild(option);
    });

    searchInput.addEventListener('input', function () {
        const searchTerm = this.value.toLowerCase();
        const options = optionsContainer.querySelectorAll('.medication-option');
        options.forEach(option => {
            option.style.display = option.textContent.toLowerCase().includes(searchTerm) ? 'block' : 'none';
        });
    });

    searchInput.addEventListener('focus', () => optionsContainer.style.display = 'block');

    document.addEventListener('click', function (e) {
        if (!searchInput.contains(e.target)) {
            optionsContainer.style.display = 'none';
        }
    });
}

function updateMedDetails(select, rowId) {
    const medId = select.value;
    const detailsDiv = document.getElementById(`details-${rowId}`);
    const formSelect = document.getElementById(`form-${rowId}`);
    const allergyAlert = document.querySelector('.allergy-alert');
    const stockAlert = document.querySelector('.stock-alert');

    if (medId) {
        const med = medications.find(m => m.id == medId);

        if (med) {
            detailsDiv.innerHTML = `
                <small><strong>Active:</strong> ${med.active}</small><br>
                <small><strong>Stock:</strong> ${med.stock}</small><br>
                <small><strong>Reorder:</strong> ${med.reorder}</small>
            `;

            formSelect.innerHTML = '<option value="">Select form</option>';
            med.forms.forEach(form => {
                formSelect.innerHTML += `<option value="${form}">${form}</option>`;
            });

            const patientAllergies = document.getElementById('allergies').textContent;
            allergyAlert.style.display = (patientAllergies.includes('Penicillin') && med.name.includes('Amoxicillin')) ? 'block' : 'none';
            stockAlert.style.display = (med.stock < med.reorder * 1.5) ? 'block' : 'none';

            return;
        }
    }

    detailsDiv.innerHTML = '';
    formSelect.innerHTML = '<option value="">Select form</option>';
    allergyAlert.style.display = 'none';
    stockAlert.style.display = 'none';
}

function handleInstructionSelect(select, rowId) {
    const customDiv = document.getElementById(`custom-inst-${rowId}`);
    customDiv.style.display = select.value === "Other (specify)" ? 'block' : 'none';
}

function removeRow(rowId) {
    document.getElementById(`row-${rowId}`).remove();
}

function closeModal(id) {
    document.getElementById(id).style.display = 'none';
}

function saveCustomInstruction() {
    closeModal('instructionModal');
}
