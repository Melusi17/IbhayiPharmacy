document.getElementById('registerForm').addEventListener('submit', function (e) {
    e.preventDefault();

    // Clear previous errors
    document.querySelectorAll('.error-message').forEach(el => el.textContent = '');

    const name = document.getElementById('name').value.trim();
    const surname = document.getElementById('surname').value.trim();
    const idNumber = document.getElementById('idNumber').value.trim();
    const email = document.getElementById('email').value.trim();
    const cellphone = document.getElementById('cellphone').value.trim();
    const password = document.getElementById('password').value;
    const confirmPassword = document.getElementById('confirmPassword').value;

    let hasError = false;

    if (!name) {
        document.getElementById('nameError').textContent = 'Name is required.';
        hasError = true;
    }

    if (!surname) {
        document.getElementById('surnameError').textContent = 'Surname is required.';
        hasError = true;
    }

    if (!idNumber) {
        document.getElementById('idError').textContent = 'ID Number is required.';
        hasError = true;
    } else if (!/^\d{13}$/.test(idNumber)) {
        document.getElementById('idError').textContent = 'ID must be 13 digits.';
        hasError = true;
    } else {
        const yy = idNumber.substring(0, 2);
        const mm = idNumber.substring(2, 4);
        const dd = idNumber.substring(4, 6);
        const date = new Date(`20${yy}-${mm}-${dd}`);
        if (
            !(date.getFullYear() === parseInt(`20${yy}`) &&
                date.getMonth() + 1 === parseInt(mm) &&
                date.getDate() === parseInt(dd))
        ) {
            document.getElementById('idError').textContent = 'Invalid birthdate format in ID.';
            hasError = true;
        }
    }

    if (!email) {
        document.getElementById('emailError').textContent = 'Email is required.';
        hasError = true;
    } else if (!/^\S+@\S+\.\S+$/.test(email)) {
        document.getElementById('emailError').textContent = 'Enter a valid email address.';
        hasError = true;
    }

    if (!cellphone) {
        document.getElementById('cellphoneError').textContent = 'Cellphone is required.';
        hasError = true;
    } else if (!/^\d{10}$/.test(cellphone)) {
        document.getElementById('cellphoneError').textContent = 'Enter a 10-digit cellphone number.';
        hasError = true;
    }

    if (!password) {
        document.getElementById('passwordError').textContent = 'Password is required.';
        hasError = true;
    }

    if (!confirmPassword) {
        document.getElementById('confirmPasswordError').textContent = 'Please confirm password.';
        hasError = true;
    } else if (password !== confirmPassword) {
        document.getElementById('confirmPasswordError').textContent = 'Passwords do not match.';
        hasError = true;
    }

    if (hasError) return;

    // Save to localStorage
    const customer = {
        name,
        surname,
        idNumber,
        email,
        cellphone,
        password,
        allergies
    };

    const customers = JSON.parse(localStorage.getItem('customers') || '[]');
    customers.push(customer);
    localStorage.setItem('customers', JSON.stringify(customers));
    4
    // Show confirmation modal
    document.getElementById('confirmationModal').classList.remove('hidden');

});

/***** core data *****/
let allergies = [];          // current allergy cart
const allergySelect = document.getElementById('allergySelect');
const allergyCart = document.getElementById('allergyCart');

/***** add selected allergy *****/
document.getElementById('addAllergyBtn').addEventListener('click', () => {
    const chosen = allergySelect.value;
    if (!chosen || allergies.includes(chosen)) return;

    allergies.push(chosen);
    updateAllergyCart();
});

/***** remove allergy by index *****/
function removeAllergy(i) {
    allergies.splice(i, 1);
    updateAllergyCart();
}

/***** render allergy cart UI *****/
function updateAllergyCart() {
    allergyCart.innerHTML = '';
    allergies.forEach((a, i) => {
        const li = document.createElement('li');
        li.textContent = a;

        const btn = document.createElement('button');
        btn.textContent = '×';
        btn.className = 'remove-btn';
        btn.onclick = () => removeAllergy(i);

        li.appendChild(btn);
        allergyCart.appendChild(li);
    });
}

/***** Toggle password visibility *****/
function togglePassword(fieldId, icon) {
    const field = document.getElementById(fieldId);
    const iconSvg = icon.querySelector('svg');
    const isPass = field.type === 'password';

    field.type = isPass ? 'text' : 'password';
    iconSvg.classList.toggle('feather-eye-open', isPass);
}
