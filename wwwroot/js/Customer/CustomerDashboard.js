// Global variable to store current notifications
let currentNotifications = {
    AvailableForOrderingCount: 0,
    AvailableForRefillsCount: 0,
    ReadyForCollectionCount: 0,
    RejectedMedicationsCount: 0,
    PendingOrdersCount: 0
};

// Simple Modal Functions
function toggleNotificationsModal() {
    console.log('💊 Medical card clicked!');
    const modal = document.getElementById('notificationsModal');
    modal.classList.toggle('hidden');

    // Refresh data when opening modal
    if (!modal.classList.contains('hidden')) {
        refreshNotificationData();
    }
}

function closeNotificationsModal() {
    const modal = document.getElementById('notificationsModal');
    modal.classList.add('hidden');
}

// Medical Card Notification Functions
function updateMedicalNotificationCard(notifications) {
    // Store notifications globally
    currentNotifications = { ...notifications };

    const card = document.getElementById('medicalNotificationCard');
    const content = document.getElementById('medicalCardContent');

    // Update stats
    document.getElementById('statReady').textContent = notifications.ReadyForCollectionCount;
    document.getElementById('statPending').textContent = notifications.PendingOrdersCount;
    document.getElementById('statRefills').textContent = notifications.AvailableForRefillsCount;
    document.getElementById('statAttention').textContent = notifications.RejectedMedicationsCount + notifications.AvailableForOrderingCount;

    const totalAlerts = notifications.AvailableForOrderingCount +
        notifications.AvailableForRefillsCount +
        notifications.ReadyForCollectionCount +
        notifications.RejectedMedicationsCount;

    if (totalAlerts > 0) {
        // Has notifications - Active state
        card.classList.remove('empty');
        card.classList.add('has-notifications');

        // Create dynamic message based on priority
        let message = '';
        if (notifications.ReadyForCollectionCount > 0) {
            message = `${notifications.ReadyForCollectionCount} medication(s) ready for pickup at pharmacy.`;
        } else if (notifications.RejectedMedicationsCount > 0) {
            message = `${notifications.RejectedMedicationsCount} medication(s) need your attention.`;
        } else if (notifications.AvailableForOrderingCount > 0) {
            message = `${notifications.AvailableForOrderingCount} prescription(s) ready to order.`;
        } else if (notifications.AvailableForRefillsCount > 0) {
            message = `${notifications.AvailableForRefillsCount} medication(s) available for refill.`;
        } else {
            message = `${notifications.PendingOrdersCount} order(s) being processed.`;
        }

        content.textContent = message;

    } else {
        // No notifications - Clear state
        content.textContent = 'Your medications are up to date and managed properly.';
        card.classList.add('empty');
        card.classList.remove('has-notifications');
    }

    console.log('💊 Medical card updated with:', notifications);
}

// Extract data from ViewComponent and update card
function extractDataFromViewComponent() {
    try {
        console.log('🔄 Extracting data from ViewComponent...');

        const notificationItems = document.querySelectorAll('.notification-item');
        const notifications = {
            AvailableForOrderingCount: 0,
            AvailableForRefillsCount: 0,
            ReadyForCollectionCount: 0,
            RejectedMedicationsCount: 0,
            PendingOrdersCount: 0
        };

        // Count notifications by their content
        notificationItems.forEach(item => {
            const title = item.querySelector('.notification-title')?.textContent || '';
            const icon = item.querySelector('.notification-icon')?.textContent || '';

            if (title.includes('Ready to Order') || icon.includes('📥')) {
                notifications.AvailableForOrderingCount++;
            } else if (title.includes('Available for Refill') || icon.includes('🔄')) {
                notifications.AvailableForRefillsCount++;
            } else if (title.includes('Ready for Collection') || icon.includes('✅')) {
                notifications.ReadyForCollectionCount++;
            } else if (title.includes('Need Attention') || title.includes('require attention') || icon.includes('❌')) {
                notifications.RejectedMedicationsCount++;
            } else if (title.includes('Being Processed') || icon.includes('⏳')) {
                notifications.PendingOrdersCount++;
            }
        });

        console.log('📊 Extracted notifications:', notifications);
        return notifications;

    } catch (error) {
        console.error('❌ Error extracting data from ViewComponent:', error);
        return currentNotifications;
    }
}

// Refresh notification data
function refreshNotificationData() {
    console.log('🔄 Refreshing notification data...');

    // Since the ViewComponent is already loaded, extract data from it
    const notifications = extractDataFromViewComponent();
    updateMedicalNotificationCard(notifications);
}

// Initialize when page loads
document.addEventListener('DOMContentLoaded', function () {
    console.log('🔔 Initializing notification system...');

    // Close modal when clicking outside
    const modal = document.getElementById('notificationsModal');
    if (modal) {
        modal.addEventListener('click', function (e) {
            if (e.target === this) {
                closeNotificationsModal();
            }
        });
    }

    // Close modal with Escape key
    document.addEventListener('keydown', function (e) {
        if (e.key === 'Escape') {
            closeNotificationsModal();
        }
    });

    // Load initial data after ViewComponent renders
    setTimeout(() => {
        refreshNotificationData();
    }, 1500);

    console.log('✅ Notification system initialized');
});

// Profile Menu Functions
function toggleProfileMenu() {
    console.log('👤 Profile icon clicked');
    const profileMenu = document.getElementById('profileMenu');
    if (profileMenu) {
        profileMenu.classList.toggle('hidden');
        console.log('👤 Profile menu visibility:', !profileMenu.classList.contains('hidden'));
    } else {
        console.log('❌ Profile menu element not found');
    }
}

// Close profile menu when clicking elsewhere
document.addEventListener('click', function (e) {
    const profileMenu = document.getElementById('profileMenu');
    const profileIcon = document.querySelector('.profile-icon');

    if (profileMenu && !profileMenu.classList.contains('hidden')) {
        // Check if click is outside profile menu and profile icon
        if (!profileMenu.contains(e.target) && !profileIcon.contains(e.target)) {
            profileMenu.classList.add('hidden');
            console.log('👤 Profile menu closed (clicked outside)');
        }
    }
});

// Close profile menu with Escape key
document.addEventListener('keydown', function (e) {
    if (e.key === 'Escape') {
        const profileMenu = document.getElementById('profileMenu');
        if (profileMenu && !profileMenu.classList.contains('hidden')) {
            profileMenu.classList.add('hidden');
            console.log('👤 Profile menu closed (Escape key)');
        }
    }
});

// Initialize profile menu
document.addEventListener('DOMContentLoaded', function () {
    console.log('🔔 Initializing profile menu...');

    // Make sure profile menu is hidden on page load
    const profileMenu = document.getElementById('profileMenu');
    if (profileMenu) {
        profileMenu.classList.add('hidden');
        console.log('👤 Profile menu hidden on page load');
    }

    // Add click event to profile icon
    const profileIcon = document.querySelector('.profile-icon');
    if (profileIcon) {
        profileIcon.addEventListener('click', function (e) {
            e.stopPropagation();
            toggleProfileMenu();
        });
        console.log('✅ Profile icon click event attached');
    }
});

// Make functions globally available
window.toggleNotificationsModal = toggleNotificationsModal;
window.closeNotificationsModal = closeNotificationsModal;
window.updateMedicalNotificationCard = updateMedicalNotificationCard;
window.refreshNotificationData = refreshNotificationData;
window.extractDataFromViewComponent = extractDataFromViewComponent;




// Debug function to check what's happening
function debugDataFlow() {
    console.log('=== DATA FLOW DEBUG ===');

    // Check if ViewComponent rendered
    const notificationItems = document.querySelectorAll('.notification-item');
    console.log('📋 Notification items found:', notificationItems.length);

    // Check each item
    notificationItems.forEach((item, index) => {
        const title = item.querySelector('.notification-title')?.textContent;
        const icon = item.querySelector('.notification-icon')?.textContent;
        console.log(`Item ${index + 1}:`, { title, icon });
    });

    // Check current state
    console.log('💊 Current notifications state:', currentNotifications);
    console.log('📊 Card stats - Ready:', document.getElementById('statReady')?.textContent);
    console.log('📊 Card stats - Pending:', document.getElementById('statPending')?.textContent);
    console.log('📊 Card stats - Refills:', document.getElementById('statRefills')?.textContent);
    console.log('📊 Card stats - Attention:', document.getElementById('statAttention')?.textContent);

    console.log('=== DEBUG END ===');
}

// Make it available globally
window.debugDataFlow = debugDataFlow;

// Call this after page loads to see what's happening
document.addEventListener('DOMContentLoaded', function () {
    setTimeout(debugDataFlow, 2000);
});
