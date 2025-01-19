// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

document.addEventListener("DOMContentLoaded", () => {
    const rentalDataElement = document.getElementById("rentalPricePerDay");
    const rentalPricePerDay = parseFloat(rentalDataElement.getAttribute("data-price").replace(",", "."))
    const rentalEndDate = document.getElementById("rentalEndDate");
    const totalAmount = document.getElementById("totalAmount");

    function calculateTotal() {
        const today = new Date(); // Current date
        const endDate = new Date(rentalEndDate.value); // User-selected end date

        if (rentalEndDate.value && endDate > today) {
            const timeDiff = endDate - today; // Difference in milliseconds
            const days = Math.ceil(timeDiff / (1000 * 60 * 60 * 24)); // Convert to days
            const total = days * rentalPricePerDay;
            console.log("Total: " + total);
            totalAmount.textContent = total.toFixed(2); // Display total with 2 decimal places
        }
        else {
            totalAmount.textContent = "0";
        }
    }

    console.log("JavaScript loaded successfully!");

    rentalEndDate.addEventListener("change", calculateTotal); // Trigger calculation on end date change

    var today = new Date();
    var dd = String(today.getDate()).padStart(2, '0');
    var mm = String(today.getMonth() + 1).padStart(2, '0'); //January is 0!
    var yyyy = today.getFullYear();

    today = yyyy + '-' + mm + '-' + dd;
    rentalEndDate.setAttribute("min", today);

});
