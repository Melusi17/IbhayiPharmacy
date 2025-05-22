// Processed Orders JS - Fully Fixed Version
document.addEventListener('DOMContentLoaded', function () {
    // Initialize all event listeners
    initEventListeners();

    console.log('Processed Orders JS initialized successfully');
});

function initEventListeners() {
    // Toggle order details on card click (excluding buttons)
    document.querySelectorAll('.order-card').forEach(card => {
        card.addEventListener('click', function (e) {
            if (!e.target.closest('.action-btn')) {
                this.classList.toggle('expanded');
            }
        });
    });

    // Filter buttons - Add data-filter attribute to each button
    document.querySelectorAll('.filter-btn').forEach(btn => {
        // Set data-filter attribute based on button text
        const filterValue = btn.textContent.trim().toLowerCase()
            .replace(' ', '-')
            .replace('for-', ''); // Remove "for-" from "ready-for-collection"

        btn.dataset.filter = filterValue.includes('all-orders') ? 'all' :
            filterValue.includes('ready-collection') ? 'ready' :
                filterValue;

        btn.addEventListener('click', function () {
            filterOrders(this.dataset.filter);
        });
    });

    // Search button
    document.querySelector('.search-container button')?.addEventListener('click', searchOrders);

    // Search on Enter key
    document.getElementById('searchInput')?.addEventListener('keyup', function (e) {
        if (e.key === 'Enter') searchOrders();
    });

    // Event delegation for dynamic buttons
    document.addEventListener('click', function (e) {
        // Collect order button
        if (e.target.closest('.collect-btn')) {
            collectOrder(e.target.closest('.collect-btn'));
        }
        // Email order button
        if (e.target.closest('.email-btn')) {
            emailOrder(e.target.closest('.email-btn'));
        }
    });
}

// Filter orders by status
function filterOrders(status) {
    try {
        // Update active filter button
        document.querySelectorAll('.filter-btn').forEach(btn => {
            btn.classList.toggle('active', btn.dataset.filter === status);
        });

        // Filter cards
        const cards = document.querySelectorAll('.order-card');
        cards.forEach(card => {
            if (status === 'all') {
                card.style.display = 'block';
            } else {
                card.style.display = card.dataset.status === status ? 'block' : 'none';
            }
        });

        console.log(`Filtered orders by status: ${status}`);
    } catch (error) {
        console.error('Error in filterOrders:', error);
    }
}

// Collect order function
function collectOrder(button) {
    try {
        const card = button.closest('.order-card');
        if (!card) return;

        // Update status
        card.dataset.status = 'ready';
        const statusElement = card.querySelector('.order-status');
        if (statusElement) {
            statusElement.className = 'order-status status-ready';
            statusElement.textContent = 'Ready for Collection';
        }

        // Remove the collect button
        button.remove();

        console.log('Order marked as ready for collection');
    } catch (error) {
        console.error('Error in collectOrder:', error);
    }
}

// Email order function
function emailOrder(button) {
    try {
        const card = button.closest('.order-card');
        if (!card) return;

        const patient = card.querySelector('.order-patient')?.textContent || 'patient';
        alert(`Email sent to ${patient}`);
        console.log(`Email triggered for ${patient}`);

        // In a real app, this would trigger an email API
    } catch (error) {
        console.error('Error in emailOrder:', error);
    }
}

// Search orders
function searchOrders() {
    try {
        const searchTerm = document.getElementById('searchInput')?.value.toLowerCase() || '';
        const cards = document.querySelectorAll('.order-card');

        cards.forEach(card => {
            const patientText = card.querySelector('.order-patient')?.textContent.toLowerCase() || '';
            const orderNumber = card.querySelector('.order-meta strong')?.textContent.toLowerCase() || '';
            const searchContent = patientText + ' ' + orderNumber;

            card.style.display = searchContent.includes(searchTerm) ? 'block' : 'none';
        });

        console.log(`Searched orders for: ${searchTerm}`);
    } catch (error) {
        console.error('Error in searchOrders:', error);
    }
}