// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

document.addEventListener("DOMContentLoaded", () => {
    const rentalDataElement = document.getElementById("rentalData");
    //const rentalPricePerDay = parseFloat(rentalDataElement.getAttribute("data-price"))
    const rentalEndDate = document.getElementById("rentalEndDate");
    const totalAmount = document.getElementById("totalAmount");

    function calculateTotal() {
        const today = new Date(); // Current date
        const endDate = new Date(rentalEndDate.value); // User-selected end date

        if (rentalEndDate.value && endDate > today) {
            const timeDiff = endDate - today; // Difference in milliseconds
            const days = Math.ceil(timeDiff / (1000 * 60 * 60 * 24)); // Convert to days
            const total = days * rentalPricePerDay;
            totalAmount.textContent = total.toFixed(2); // Display total with 2 decimal places
        }
        else {
            totalAmount.textContent = "0";
        }
    }
    console.log("JavaScript loaded successfully!");

    rentalEndDate.addEventListener("change", calculateTotal); // Trigger calculation on end date change
});
