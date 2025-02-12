document.addEventListener("DOMContentLoaded", () => {
    const quantity = document.getElementById("quantity");
    const totalAmount = document.getElementById("totalAmount");
    const price = document.getElementById("purchasePrice");

    function calculateTotal() {
        const quantityValue = parseInt(quantity.value, 10) || 0; 
        const priceValue = parseFloat(price.textContent.replace(",", ".")) || 0.0; 
        
        const total = quantityValue * priceValue;
        
        totalAmount.textContent = total.toFixed(2); 
    }
    calculateTotal();

    quantity.addEventListener("change", calculateTotal);
});
