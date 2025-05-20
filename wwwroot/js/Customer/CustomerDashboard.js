document.addEventListener("DOMContentLoaded", function () {
    const customerName = localStorage.getItem("loggedInCustomerName") || "Customer";
    document.getElementById("welcomeText").textContent = `Welcome, ${customerName}!`;
});

function logout() {
    localStorage.removeItem("loggedInCustomerName");
    window.location.href = "/Customer/Login.html";
}

function toggleProfileMenu() {
    const menu = document.getElementById("profileMenu");
    menu.classList.toggle("hidden");
}
let orders = []; // Array to store order data
function navigateTo(page) {
    const content = document.getElementById("mainContent");

    if (!content) {
        console.error("mainContent container not found!");
        return;
    }

    if (page === "uploadPrescription") {
        content.innerHTML = `
            <div class="upload-prescription-panel">
                <h2>📥 Upload Prescription</h2>
                <form id="prescriptionForm" enctype="multipart/form-data">
                    <input type="file" id="prescriptionFile" name="prescriptionFile" accept=".pdf, image/*" required>

                    <div class="inline-fields">
                        <input type="date" id="prescriptionDate" name="prescriptionDate" required placeholder="Prescription Date">
                        <input type="text" id="doctorName" name="doctorName" placeholder="Doctor's Name" required>
                    </div>

                    <label class="checkbox-label">
                        <input type="checkbox" id="dispenseCheckbox" name="dispenseCheckbox">
                        Dispense this prescription upon approval
                    </label>

                    <button type="submit" class="btn-upload">Upload</button>
                </form>
                <div id="uploadStatus"></div>
            </div>

    <div class="upload-prescription-panel">
    <h2>📋 Unprocessed Prescriptions</h2>
    <table class="prescription-table">
        <thead>
            <tr>
                <th>Date</th>
                <th>File</th>
                <th>Dispense</th>
                <th>Actions</th>
            </tr>
        </thead>
        <tbody id="unprocessedBody">
            <!-- Hardcoded sample record -->
            <tr>
                <td>2025-05-13</td>
                <td>Prescription_ABC.pdf</td>
                <td>Yes</td>
                <td>
                    <button class="btn-upload" onclick="alert('Edit clicked')">Edit</button>
                    <button class="btn-upload" onclick="this.parentElement.parentElement.remove()">Remove</button>
                </td>
            </tr>
        </tbody>
    </table>
</div>


<div class="upload-prescription-panel">
    <h2>✅ Processed Prescriptions</h2>
    <table class="prescription-table">
        <thead>
            <tr>
                <th>Date</th>
                <th>Doctor</th>
                <th>Prescription</th>
                <th>Order Medication</th>
            </tr>
        </thead>
        <tbody id="processedBody">
            <!-- Example hardcoded data -->
      <tr>
    <td>2025-05-13</td>
    <td>Dr. John Smith</td>
    <td>Prescription_XYZ.pdf</td>
    <td><button class="btn-upload" onclick="openOrderModal('2025-05-13', 'Dr. John Smith')">View</button></td>
</tr>
        </tbody>
    </table>
</div>
<!-- Medication Order Modal -->
<div id="orderModal" class="modal-overlay" style="display: none;">
    <div class="modal-content">
        <h2>🩺 Order Medication</h2>
        <div class="order-details">
            <p><strong>Date:</strong> <span id="modalDate">2025-05-13</span></p>
            <p><strong>Doctor:</strong> <span id="modalDoctor">Dr. John Smith</span></p>
        </div>

        <table class="prescription-table">
            <thead>
                <tr>
                    <th>Select</th>
                    <th>Medication</th>
                    <th>Qty</th>
                    <th>Instructions</th>
                    <th>Repeats</th>
                    <th>Price (R)</th>
                </tr>
            </thead>
            <tbody id="modalMedications">
                <tr>
                    <td><input type="checkbox" class="med-check" data-price="120" onchange="updateTotal()"></td>
                    <td>Amoxicillin</td>
                    <td>2</td>
                    <td>Twice a day</td>
                    <td>1</td>
                    <td>120</td>
                </tr>
                <tr>
                    <td><input type="checkbox" class="med-check" data-price="60" onchange="updateTotal()"></td>
                    <td>Ibuprofen</td>
                    <td>1</td>
                    <td>After meals</td>
                    <td>0</td>
                    <td>60</td>
                </tr>
            </tbody>
        </table>

        <div class="total-summary">
            <p><strong>Total (incl. VAT): R</strong><span id="totalDue">0.00</span></p>
        </div>

        <button class="btn-upload" onclick="placeMedicationOrder()">Place Order</button>
        <button class="btn-upload" style="background-color: grey;" onclick="closeModal()">Close</button>
    </div>
</div>

        `;
        renderUnprocessedTable();  // Show table on load
        attachUploadHandler();
    }
    else if (page === 'placeOrder') {
        mainContent.innerHTML = `
      <h2>Place Medication Order</h2>

    <div class="form-group">
        <label for="orderDate">Date:</label>
        <input type="date" id="orderDate">
    </div>

    <div class="form-group">
        <label for="doctorSelect">Select Doctor:</label>
        <select id="doctorSelect">
            <option value="">-- Choose a Doctor --</option>
            <option value="1">Dr. Smith</option>
            <option value="2">Dr. Johnson</option>
        </select>
        <button onclick="addMedications()">Add Medications</button>
    </div>

    <table id="medicationTable" style="display:none">
        <thead>
            <tr>
                <th>Select</th>
                <th>Doctor</th>
                <th>Medication</th>
                <th>Qty</th>
                <th>Repeats</th>
                <th>Instructions</th>
                <th>Price</th>
            </tr>
        </thead>
        <tbody id="medicationBody"></tbody>
    </table>

    <div class="summary">
        Subtotal: R<span id="subtotal">0.00</span><br>
        Tax (15%): R<span id="tax">0.00</span><br>
        Total Due: R<span id="total">0.00</span>
    </div>
        `;

        // Reset orders when navigating
        orders.length = 0;
        updateOrderList();
    }

    // Add other pages navigation here if needed

    else if (page === "trackOrders") {
        content.innerHTML = `
            <div class="track-order-panel upload-prescription-panel">
            <h2>📦 Track Orders</h2>
            <table class="prescription-table">
                <thead>
                    <tr>
                        <th>Date</th>
                        <th>Doctor</th>
                        <th>Medication</th>
                        <th>Qty</th>
                        <th>Instruction</th>
                        <th>Repeats</th>
                        <th>Price (R)</th>
                        <th>Status</th>
                    </tr>
                </thead>
                <tbody id="orderList">
                    <tr>
                        <td>2025-05-13</td>
                        <td>Dr. John Smith</td>
                        <td>Amoxicillin 250mg</td>
                        <td>20</td>
                        <td>1 capsule three times a day</td>
                        <td>1</td>
                        <td>150.00</td>
                        <td><span class="status ordered">Ordered</span></td>
                    </tr>
                    <tr>
                        <td>2025-05-13</td>
                        <td>Dr. John Smith</td>
                        <td>Ibuprofen 200mg</td>
                        <td>15</td>
                        <td>Take after meals</td>
                        <td>0</td>
                        <td>75.00</td>
                        <td><span class="status ordered">Ordered</span></td>
                    </tr>
                </tbody>
            </table>
        </div>
    `;
        populateOrderData();
        displayOrders();
    }
    else if (page === "manageRepeats") {
        content.innerHTML = `
            <div class="upload-prescription-panel">
                <h2>🔁 Manage Repeats</h2>
                <table class="prescription-table">
                    <thead>
                        <tr>
                            <th>Date</th>
                            <th>Doctor</th>
                            <th>Medication</th>
                            <th>Instruction</th>
                            <th>Qty</th>
                            <th>Repeats Left</th>
                            <th>Action</th>
                        </tr>
                    </thead>
                    <tbody id="repeatsList">
                        <tr>
                            <td>2025-05-13</td>
                            <td>Dr. John Smith</td>
                            <td>Amoxicillin 250mg</td>
                            <td>1 capsule three times a day</td>
                            <td>20</td>
                            <td id="repeatCount1">1</td>
                            <td>
                                <button class="btn-upload" onclick="requestRefill('repeatCount1', this)">Request Refill</button>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>
        `;
    }
    else if (page === "viewReports") {
        content.innerHTML = `
        <div class="upload-prescription-panel">
            <h2>📊 Medication Order Reports</h2>
            <table class="prescription-table">
                <thead>
                    <tr>
                        <th>Order Date</th>
                        <th>Doctor</th>
                        <th>Medication</th>
                        <th>Instruction</th>
                        <th>Qty</th>
                        <th>Repeats</th>
                        <th>Price (R)</th>
                        <th>Total (R)</th>
                    </tr>
                </thead>
                <tbody>
                    <tr>
                        <td>2025-05-13</td>
                        <td>Dr. John Smith</td>
                        <td>Amoxicillin 250mg</td>
                        <td>1 capsule three times a day</td>
                        <td>20</td>
                        <td>1</td>
                        <td>80.00</td>
                        <td>80.00</td>
                    </tr>
                    <tr>
                        <td>2025-05-13</td>
                        <td>Dr. John Smith</td>
                        <td>Ibuprofen 200mg</td>
                        <td>1 tablet twice a day</td>
                        <td>15</td>
                        <td>0</td>
                        <td>45.00</td>
                        <td>45.00</td>
                    </tr>
                </tbody>
                <tfoot>
                    <tr style="font-weight: bold;">
                        <td colspan="7" style="text-align: right;">Total Due:</td>
                        <td>125.00</td>
                    </tr>
                    <tr style="font-weight: bold;">
                        <td colspan="7" style="text-align: right;">VAT (15%):</td>
                        <td>18.75</td>
                    </tr>
                    <tr style="font-weight: bold; background-color: #e0f7fa;">
                        <td colspan="7" style="text-align: right;">Total with VAT:</td>
                        <td>143.75</td>
                    </tr>
                </tfoot>
            </table>
            
        </div>
         <div class="upload-prescription-panel">
            <h2>📄 Generate Medication Report</h2>
            <label for="reportDate">Select Date:</label>
            <input type="date" id="reportDate" style="margin-bottom: 15px;" />

            <button class="btn-upload" onclick="generatePDFReport()">🧾 Generate PDF</button>

            <div id="reportResults" style="margin-top: 20px;"></div>
        </div>
    `;
    }
}


function attachUploadHandler() {
    const form = document.getElementById("prescriptionForm");

    form.addEventListener("submit", function (e) {
        e.preventDefault();

        const fileInput = document.getElementById("prescriptionFile");
        const date = document.getElementById("prescriptionDate").value;
        const doctor = document.getElementById("doctorName").value;
        const shouldDispense = document.getElementById("dispenseCheckbox").checked;

        if (!fileInput.files.length) {
            alert("Please select a prescription file to upload.");
            return;
        }

        const fileName = fileInput.files[0].name;

        const newEntry = {
            date,
            doctor,
            fileName,
            dispense: shouldDispense ? "Yes" : "No"
        };

        // Store in localStorage
        const prescriptions = JSON.parse(localStorage.getItem("prescriptions") || "[]");
        prescriptions.push(newEntry);
        localStorage.setItem("prescriptions", JSON.stringify(prescriptions));

        // Reset and update
        form.reset();
        document.getElementById("uploadStatus").innerHTML = `
            <p style="color: green;">
                ✅ Prescription uploaded successfully!
            </p>
        `;
        renderUnprocessedTable();
    });
}

function renderUnprocessedTable() {
    const tbody = document.getElementById("unprocessedBody");
    if (!tbody) return;

    const prescriptions = JSON.parse(localStorage.getItem("prescriptions") || "[]");

    tbody.innerHTML = ""; // Clear old rows

    prescriptions.forEach((p, index) => {
        const row = document.createElement("tr");
        row.innerHTML = `
            <td>${p.date}</td>
            <td>${p.fileName}</td>
            <td>${p.dispense}</td>
            <td>
                <button onclick="editPrescription(${index})">Edit</button>
                <button onclick="deletePrescription(${index})">Remove</button>
            </td>
        `;
        tbody.appendChild(row);
    });
}

function deletePrescription(index) {
    const prescriptions = JSON.parse(localStorage.getItem("prescriptions") || "[]");
    prescriptions.splice(index, 1);
    localStorage.setItem("prescriptions", JSON.stringify(prescriptions));
    renderUnprocessedTable();
}

function editPrescription(index) {
    const prescriptions = JSON.parse(localStorage.getItem("prescriptions") || "[]");
    const p = prescriptions[index];

    document.getElementById("prescriptionDate").value = p.date;
    document.getElementById("doctorName").value = p.doctor;
    document.getElementById("dispenseCheckbox").checked = p.dispense === "Yes";

    prescriptions.splice(index, 1); // remove old record so edited will replace
    localStorage.setItem("prescriptions", JSON.stringify(prescriptions));
    renderUnprocessedTable();
}
function openOrderModal(date, doctor) {
    document.getElementById("modalDate").textContent = date;
    document.getElementById("modalDoctor").textContent = doctor;
    document.getElementById("orderModal").style.display = "flex";
}

function closeModal() {
    document.getElementById("orderModal").style.display = "none";
}

function updateTotal() {
    let total = 0;
    const checkboxes = document.querySelectorAll(".med-check");
    checkboxes.forEach(cb => {
        if (cb.checked) {
            total += parseFloat(cb.getAttribute("data-price"));
        }
    });

    const vat = total * 0.15; // 15% VAT
    const totalDue = (total + vat).toFixed(2);
    document.getElementById("totalDue").textContent = totalDue;
}

function placeMedicationOrder() {
    alert("Medication order has been placed successfully.");
    closeModal();
}
function attachUploadHandler() {
    const form = document.getElementById("prescriptionForm");
    form.addEventListener("submit", function (e) {
        e.preventDefault();

        const prescriptionDate = document.getElementById("prescriptionDate").value;
        const doctorName = document.getElementById("doctorName").value;
        const shouldDispense = document.getElementById("dispenseCheckbox").checked;

        if (!prescriptionDate || !doctorName) {
            alert("Please fill in all fields.");
            return;
        }

        // Simulate placing an order
        const order = {
            orderDate: prescriptionDate,
            doctor: doctorName,
            medication: "Medication XYZ", // Hardcoded medication for simplicity
            status: "Ready for pickup",
        };

        orders.push(order); // Store the order in the orders array

        alert("Order placed successfully!");

        // Navigate back to Track Orders to display the new order
        navigateTo("trackOrders");
    });
}

function displayOrders() {
    const orderList = document.getElementById("orderList");

    // Clear the current list before adding new rows
    orderList.innerHTML = "";

    // Loop through the orders and add them as rows in the table
    orders.forEach((order) => {
        const row = document.createElement("tr");

        const dateCell = document.createElement("td");
        dateCell.textContent = order.orderDate;
        row.appendChild(dateCell);

        const doctorCell = document.createElement("td");
        doctorCell.textContent = order.doctor;
        row.appendChild(doctorCell);

        const medicationCell = document.createElement("td");
        medicationCell.textContent = order.medication;
        row.appendChild(medicationCell);

        const statusCell = document.createElement("td");
        statusCell.textContent = order.status;
        row.appendChild(statusCell);

        orderList.appendChild(row);
    });

}
function requestRefill(repeatCountId, btn) {
    const countElem = document.getElementById(repeatCountId);
    let currentRepeats = parseInt(countElem.textContent);

    if (currentRepeats > 0) {
        currentRepeats -= 1;
        countElem.textContent = currentRepeats;
        alert("✅ Refill request placed!");

        if (currentRepeats === 0) {
            btn.disabled = true;
            btn.textContent = "No repeats left";
            btn.style.backgroundColor = "#ccc";
        }
    }
}
async function generatePDFReport() {
    const { jsPDF } = window.jspdf;
    const selectedDate = document.getElementById("reportDate").value;
    if (!selectedDate) {
        alert("Please select a date.");
        return;
    }

    // 🔹 Sample/mock data - replace with dynamic values if needed
    const dispensedData = [
        {
            patient: "Sarah Nkosi",
            doctor: "Dr. John Smith",
            medication: "Amoxicillin 250mg",
            qty: 20,
            instruction: "1 capsule 3x a day",
            repeats: 1,
            price: 80,
            date: "2025-05-13"
        },
        {
            patient: "Sarah Nkosi",
            doctor: "Dr. John Smith",
            medication: "Ibuprofen 200mg",
            qty: 15,
            instruction: "1 tablet twice a day",
            repeats: 0,
            price: 45,
            date: "2025-05-13"
        }
        // Add more if needed
    ];

    const filtered = dispensedData.filter(d => d.date === selectedDate);
    if (filtered.length === 0) {
        alert("No records found for selected date.");
        return;
    }

    const doc = new jsPDF();
    doc.setFontSize(16);
    doc.text(`Medication Report - ${selectedDate}`, 14, 20);
    doc.setFontSize(12);

    let y = 30;

    const grouped = {};
    filtered.forEach(entry => {
        const { patient, doctor, medication } = entry;
        if (!grouped[patient]) grouped[patient] = {};
        if (!grouped[patient][doctor]) grouped[patient][doctor] = [];
        grouped[patient][doctor].push(entry);
    });

    for (const patient in grouped) {
        doc.setFont(undefined, "bold");
        doc.text(`Patient: ${patient}`, 14, y);
        y += 8;

        for (const doctor in grouped[patient]) {
            doc.setFont(undefined, "normal");
            doc.text(`Doctor: ${doctor}`, 20, y);
            y += 8;

            grouped[patient][doctor].forEach(med => {
                doc.text(`• ${med.medication}`, 26, y);
                y += 6;
                doc.text(`  Qty: ${med.qty} | Repeats: ${med.repeats} | Instruction: ${med.instruction}`, 30, y);
                y += 6;
                doc.text(`  Price: R${med.price.toFixed(2)}`, 30, y);
                y += 10;

                if (y > 270) {
                    doc.addPage();
                    y = 20;
                }
            });
        }
    }

    doc.save(`Medication_Report_${selectedDate}.pdf`);
}
const placeOrder = [];

function addToOrder(doctorName, medId, qtyId) {
    const medication = document.getElementById(medId).value;
    const quantity = document.getElementById(qtyId).value;

    if (quantity < 1) {
        alert("Quantity must be at least 1.");
        return;
    }

    orders.push({
        doctor: doctorName,
        medication: medication,
        quantity: quantity
    });

    updateOrderList();
}

function updateOrderList() {
    const list = document.getElementById('order-list');
    if (!list) return; // If summary not loaded, skip

    list.innerHTML = '';

    orders.forEach((item, index) => {
        list.innerHTML += `
            <div class="order-item">
                ${index + 1}. <strong>${item.medication}</strong> (x${item.quantity}) from <em>${item.doctor}</em>
            </div>`;
    });
}

function submitOrder() {
    if (orders.length === 0) {
        alert("Please add at least one medication to your order.");
        return;
    }

    console.log("Submitting order:", orders);
    alert("Order submitted successfully!");

    orders.length = 0;
    updateOrderList();
}

function toggleProfileMenu() {
    const menu = document.getElementById('profileMenu');
    menu.classList.toggle('hidden');
}

function logout() {
    alert('Logging out...');
    // Add your logout logic here
}
document.getElementById('orderDate').valueAsDate = new Date();

/*Multiple medication order*/
// Sample medication data per doctor
const medicationData = {
    1: [
        { name: "Panado", price: 10.00, instructions: "Take 1 tab 3 times/day" },
        { name: "Amoxicillin", price: 50.00, instructions: "Take 1 tab 2 times/day" }
    ],
    2: [
        { name: "Ibuprofen", price: 20.00, instructions: "Take 1 tab after meals" },
        { name: "Loratadine", price: 25.00, instructions: "Take 1 tab daily" }
    ]
};

// Set date to today
window.onload = function () {
    document.getElementById("orderDate").valueAsDate = new Date();
};

function addMedications() {
    const doctorSelect = document.getElementById("doctorSelect");
    const doctorId = doctorSelect.value;
    const doctorName = doctorSelect.options[doctorSelect.selectedIndex].text;

    if (!doctorId || !medicationData[doctorId]) return;

    // Show the table
    const table = document.getElementById("medicationTable");
    table.style.display = 'table';

    const tbody = document.getElementById("medicationBody");

    // Remove previous rows from the same doctor (optional: or clear all)
    medicationData[doctorId].forEach(med => {
        const row = document.createElement("tr");
        row.innerHTML = `
                    <td><input type="checkbox" onchange="updateTotal()"></td>
                    <td>${doctorName}</td>
                    <td>${med.name}</td>
                    <td><input type="number" value="1" min="1" onchange="updateTotal()"></td>
                    <td><input type="number" value="0" min="0"></td>
                    <td>${med.instructions}</td>
                    <td class="price">${med.price.toFixed(2)}</td>
                `;
        tbody.appendChild(row);
    });
}

function updateTotal() {
    let subtotal = 0;
    const rows = document.querySelectorAll("#medicationBody tr");

    rows.forEach(row => {
        const checkbox = row.querySelector("input[type='checkbox']");
        const qtyInput = row.cells[3].querySelector("input");
        const price = parseFloat(row.querySelector(".price").textContent);

        if (checkbox.checked) {
            const qty = parseInt(qtyInput.value);
            subtotal += qty * price;
        }
    });

    const tax = subtotal * 0.15;
    const total = subtotal + tax;

    document.getElementById("subtotal").textContent = subtotal.toFixed(2);
    document.getElementById("tax").textContent = tax.toFixed(2);
    document.getElementById("total").textContent = total.toFixed(2);
}