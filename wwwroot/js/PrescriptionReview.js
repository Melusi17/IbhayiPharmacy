// ==========================
// PrescriptionReview.js
// ==========================

let currentRejectionRow = null;

// Show rejection modal
function showRejectionModal(button) {
    currentRejectionRow = button.closest('tr');
    document.getElementById('rejectionModal').style.display = 'flex';
}

// Close modal
function closeModal() {
    document.getElementById('rejectionModal').style.display = 'none';
    document.querySelectorAll('input[name="rejectionReason"]').forEach(r => r.checked = false);
}

// Confirm rejection
function confirmRejection() {
    const reason = document.querySelector('input[name="rejectionReason"]:checked');
    if (!reason) {
        alert('Please select a rejection reason');
        return;
    }

    if (currentRejectionRow) {
        currentRejectionRow.classList.add('rejected-row');
        currentRejectionRow.querySelector('td:nth-child(7)').innerHTML = '<span class="badge bg-danger">Rejected</span>';

        const checkbox = currentRejectionRow.querySelector('.dispense-checkbox');
        const rejectBtn = currentRejectionRow.querySelector('.reject-btn');

        if (checkbox) checkbox.style.display = 'none';
        if (rejectBtn) rejectBtn.style.display = 'none';

        closeModal();
    }
}

// Remove medication row
function removeMedication(button) {
    if (confirm('Are you sure you want to remove this medication?')) {
        const row = button.closest('tr');
        const hadAllergy = row.classList.contains('has-allergy');
        const hadLowStock = row.classList.contains('low-stock');
        row.remove();

        if (hadAllergy && !document.querySelector('.has-allergy')) {
            document.querySelector('.allergy-alert').style.display = 'none';
        }
        if (hadLowStock && !document.querySelector('.low-stock')) {
            document.querySelector('.stock-alert').style.display = 'none';
        }
    }
}

// Mark selected medications as dispensed
function markAsDispensed() {
    const selected = document.querySelectorAll('.dispense-checkbox:checked');
    if (selected.length === 0) {
        alert('Please select at least one medication to mark as dispensed');
        return;
    }

    if (confirm(`Mark ${selected.length} medication(s) as dispensed?`)) {
        selected.forEach(checkbox => {
            const row = checkbox.closest('tr');
            row.classList.add('dispensed-row');
            row.querySelector('td:nth-child(7)').innerHTML = '<span class="badge bg-success">Dispensed</span>';
            checkbox.remove();
            row.querySelectorAll('.action-btn').forEach(btn => btn.remove());
        });
    }
}

// Add a new medication
function addNewMedication() {
    const medSelect = document.querySelector('.add-medication-form select:first-of-type');
    const qtyInput = document.querySelector('.add-medication-form input[type="number"]:first-of-type');
    const priceInput = document.querySelector('.add-medication-form input[type="number"]:last-of-type');
    const instrSelect = document.querySelector('.add-medication-form select:last-of-type');

    if (!medSelect.value) {
        alert('Please select a medication');
        return;
    }

    const tbody = document.getElementById('medsTableBody');
    const row = document.createElement('tr');
    const medText = medSelect.options[medSelect.selectedIndex].text;
    const qty = qtyInput.value || 1;
    const price = priceInput.value ? `R ${parseFloat(priceInput.value).toFixed(2)}` : 'R 0.00';
    const instructions = instrSelect.options[instrSelect.selectedIndex].text;

    row.innerHTML = `
        <td><input type="checkbox" class="dispense-checkbox"></td>
        <td>
            ${medText}
            <div class="med-details">
                <small><strong>Active:</strong> Active ingredient</small><br>
                <small><strong>Stock:</strong> 100 (Reorder: 30)</small>
            </div>
        </td>
        <td>Tablet</td>
        <td>${qty}</td>
        <td>${price}</td>
        <td>${instructions}</td>
        <td><span class="badge bg-warning">Pending</span></td>
        <td>
            <button class="action-btn reject-btn" onclick="showRejectionModal(this)"><i class="fas fa-ban"></i></button>
            <button class="action-btn remove-btn" onclick="removeMedication(this)"><i class="fas fa-trash"></i></button>
        </td>`;

    tbody.appendChild(row);
    medSelect.value = '';
    qtyInput.value = '1';
    priceInput.value = '';
    instrSelect.selectedIndex = 0;
}

// Initialize on DOM ready
window.addEventListener('DOMContentLoaded', () => {
    const allergyAlert = document.querySelector('.allergy-alert');
    const stockAlert = document.querySelector('.stock-alert');

    if (document.querySelector('.has-allergy')) allergyAlert.style.display = 'block';
    else allergyAlert.style.display = 'none';

    if (document.querySelector('.low-stock')) stockAlert.style.display = 'block';
    else stockAlert.style.display = 'none';

    // Button event bindings
    document.querySelectorAll('.reject-btn').forEach(btn => {
        btn.addEventListener('click', () => showRejectionModal(btn));
    });

    document.querySelectorAll('.remove-btn').forEach(btn => {
        btn.addEventListener('click', () => removeMedication(btn));
    });

    const markDispensedBtn = document.querySelector('.form-footer .btn-add');
    if (markDispensedBtn) markDispensedBtn.addEventListener('click', markAsDispensed);

    const addMedBtn = document.querySelector('.add-medication-form .btn');
    if (addMedBtn) addMedBtn.addEventListener('click', addNewMedication);

    const confirmRejectBtn = document.querySelector('#rejectionModal .btn-alert');
    if (confirmRejectBtn) confirmRejectBtn.addEventListener('click', confirmRejection);

    const closeModalBtn = document.querySelector('#rejectionModal .close-modal');
    if (closeModalBtn) closeModalBtn.addEventListener('click', closeModal);
});
