function validatePlayerRange() {
    const min = parseInt(document.getElementById("minPlayers").value);
    const max = parseInt(document.getElementById("maxPlayers").value);

    if (!isNaN(min) && !isNaN(max) && min > max) {
        alert("Minimum players cannot be greater than maximum players.");
        return false;
    }

    return true;
}
