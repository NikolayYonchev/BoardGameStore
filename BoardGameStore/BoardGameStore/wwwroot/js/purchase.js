document.addEventListener("DOMContentLoaded", () => {
    const quantity = document.getElementById("quantity");
    const totalAmount = document.getElementById("totalAmount");
    const price = document.getElementById("purchasePrice");

    function calculateTotal() {
        // Parse quantity and price
        const quantityValue = parseInt(quantity.value, 10) || 0; // Default to 0 if not valid
        const priceValue = parseFloat(price.textContent.replace(",", ".")) || 0.0; // Default to 0.0 if not valid

        // Calculate total
        const total = quantityValue * priceValue;

        // Update the DOM with the calculated total
        totalAmount.textContent = total.toFixed(2); // Format to 2 decimal places
    }
    calculateTotal();

    // Add change event listener to the dropdown
    quantity.addEventListener("change", calculateTotal);
});
