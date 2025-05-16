// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
// ==========================
// unprocessedScripts.js
// ==========================

window.addEventListener('DOMContentLoaded', function () {
    renderSampleScripts();

    document.getElementById('searchInput').addEventListener('keyup', function (event) {
        if (event.key === 'Enter') searchScripts();
    });
});

function renderSampleScripts() {
    const scripts = [
        {
            id: '1001',
            name: 'John Doe',
            idNumber: '9001011234081',
            primaryDoctor: 'Dr. Sarah Smith (PR1234567)',
            date: '2023-05-15',
            repeat: 'Repeat (2 remaining)',
            prescriptions: [
                { doctor: 'Dr. Sarah Smith', issued: '15 May 2023', pdf: 'script1001a.pdf' },
                { doctor: 'Dr. Michael Brown', issued: '10 May 2023', pdf: 'script1001b.pdf' }
            ]
        },
        {
            id: '1002',
            name: 'Amanda Johnson',
            idNumber: '8902021234082',
            primaryDoctor: 'Dr. Michael Brown (PR7654321)',
            date: '2023-05-14',
            repeat: '',
            prescriptions: [
                { doctor: 'Dr. Michael Brown', issued: '14 May 2023', pdf: 'script1002.pdf' }
            ]
        },
        {
            id: '1003',
            name: 'Lunga Dlamini',
            idNumber: '8803031234083',
            primaryDoctor: 'Dr. Thando Nkosi (PR9876543)',
            date: '2023-05-12',
            repeat: 'Repeat (FINAL)',
            prescriptions: [
                { doctor: 'Dr. Thando Nkosi', issued: '12 May 2023', pdf: 'script1003a.pdf' },
                { doctor: 'Dr. Sarah Smith', issued: '05 May 2023', pdf: 'script1003b.pdf' }
            ]
        }
    ];

    const container = document.getElementById('scriptsList');
    container.innerHTML = '';

    scripts.forEach(script => {
        const scriptCard = document.createElement('div');
        scriptCard.className = 'script-card';
        scriptCard.setAttribute('data-script-id', script.id);
        scriptCard.setAttribute('onclick', 'toggleScriptDetails(this)');

        let prescriptionBlocks = '';
        script.prescriptions.forEach((p, index) => {
            prescriptionBlocks += `
                <div class="prescription-option ${index === 0 ? 'selected' : ''}" onclick="selectPrescription(event, this)" data-pdf="${p.pdf}">
                    <strong>${p.doctor}</strong>
                    <div class="prescription-meta">Issued: ${p.issued}</div>
                    <div class="prescription-pdf">
                        <embed src="${p.pdf}" type="application/pdf" width="100%" height="100%">
                    </div>
                </div>
            `;
        });

        scriptCard.innerHTML = `
            <div class="script-header">
                <div>
                    <span class="script-patient">Patient: ${script.name} (ID: ${script.idNumber})</span>
                </div>
                <span class="script-status status-pending">Ready</span>
            </div>
            <div class="script-meta">
                <span>Primary Doctor: ${script.primaryDoctor}</span>
                <span>Date: ${script.date}</span>
                ${script.repeat ? `<span class="script-repeat">${script.repeat}</span>` : ''}
            </div>
            <div class="script-details">
                <h4>Available Prescription${script.prescriptions.length > 1 ? 's' : ''}:</h4>
                <div class="doctor-prescriptions">
                    ${prescriptionBlocks}
                </div>
            </div>
            <button class="email-btn" onclick="emailScript(event, '${script.id}')">
                <i class="fas fa-envelope"></i> Email
            </button>
            <button class="dispense-btn" onclick="dispenseScript(event, '${script.id}')">
                <i class="fas fa-pills"></i> Dispense Selected
            </button>
            <div style="clear:both"></div>
        `;

        container.appendChild(scriptCard);
    });
}

function toggleScriptDetails(card) {
    if (!event.target.classList.contains('dispense-btn') &&
        !event.target.classList.contains('email-btn') &&
        !event.target.classList.contains('prescription-option') &&
        !event.target.closest('.prescription-option')) {
        card.classList.toggle('expanded');
    }
}

function selectPrescription(event, element) {
    event.stopPropagation();
    const siblings = element.parentElement.querySelectorAll('.prescription-option');
    siblings.forEach(el => el.classList.remove('selected'));
    element.classList.add('selected');
}

function dispenseScript(event, scriptId) {
    event.stopPropagation();
    const card = document.querySelector(`.script-card[data-script-id="${scriptId}"]`);
    const selected = card.querySelector('.prescription-option.selected');

    if (selected) {
        const pdf = selected.getAttribute('data-pdf');
        const doctorName = selected.querySelector('strong').textContent;
        alert(`Dispensing prescription from ${doctorName}\nPDF: ${pdf}`);

        card.querySelector('.script-status').className = 'script-status status-processing';
        card.querySelector('.script-status').textContent = 'Processing';
        card.classList.remove('expanded');
    }
}

function emailScript(event, scriptId) {
    event.stopPropagation();
    const card = document.querySelector(`.script-card[data-script-id="${scriptId}"]`);
    const patient = card.querySelector('.script-patient').textContent;
    alert(`Email sent to: ${patient}`);
}

function searchScripts() {
    const searchTerm = document.getElementById('searchInput').value.toLowerCase();
    const cards = document.querySelectorAll('.script-card');
    const loading = document.getElementById('loading');
    const scriptsList = document.getElementById('scriptsList');

    loading.style.display = 'block';
    scriptsList.style.opacity = '0.5';

    setTimeout(() => {
        cards.forEach(card => {
            const text = card.querySelector('.script-patient').textContent.toLowerCase();
            card.style.display = text.includes(searchTerm) ? 'block' : 'none';
        });
        loading.style.display = 'none';
        scriptsList.style.opacity = '1';
    }, 800);
}

function closeModal(id) {
    document.getElementById(id).style.display = 'none';
}

function saveCustomInstruction() {
    closeModal('instructionModal');
}
