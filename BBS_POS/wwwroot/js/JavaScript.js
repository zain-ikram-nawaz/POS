function toggleSidebar() {
    const sidebar = document.querySelector('.sidebar');
    const main = document.querySelector('.main-content');
    const toggleBtn = document.querySelector('.sidebar-toggle');

    // If elements missing, stop
    if (!sidebar || !main || !toggleBtn) return;

    const isClosed = sidebar.style.width === '0px' || sidebar.style.width === '';

    if (isClosed) {
        sidebar.style.width = '220px';
        main.style.marginLeft = '220px';
        toggleBtn.style.left = '230px';
    } else {
        sidebar.style.width = '0px';
        main.style.marginLeft = '0px';
        toggleBtn.style.left = '10px';
    }
}

const addProductBtn = document.getElementById('posBtnAddProduct');
if (addProductBtn) {
    addProductBtn.addEventListener('click', function () {
        const productInput = document.getElementById('posTxtProduct');
        const qtyInput = document.getElementById('posTxtQty');

        const name = productInput.value.trim();
        const qty = parseInt(qtyInput.value);

        if (!name || !qty || qty <= 0) return;

        // Find the option element to get the price
        const option = Array.from(document.querySelectorAll('#productList option'))
            .find(o => o.value === name);

        if (!option) {
            alert('Product not found!');
            return;
        }

        const price = parseFloat(option.dataset.price);
        const total = price * qty;

        cart.push({ name, qty, price, total });
        renderCart('posCartBody', 'posLblTotal');

        productInput.value = '';
        qtyInput.value = '';
    });
}

$(document).ready(function () {
    $("table.table").DataTable({
        paging: true,
        searching: true,
        ordering: true,
        responsive: true
    });
});