
initializeOfficerLayout("profile", "My Profile");

const profileForm = document.getElementById("profile-form");
const saveButton = document.getElementById("save-profile");

async function loadProfile() {
    try {
        const officer = await apiRequest(
            "/api/agricultureofficers/me"
        );

        document.getElementById("officer-id").textContent =
            officer.officerId;

        document.getElementById("employee-number").textContent =
            officer.employeeNumber || "—";

        document.getElementById("full-name").value =
            officer.fullName || "";

        document.getElementById("specialization").value =
            officer.specialization || "";

        document.getElementById("district").value =
            officer.district || "";

        document.getElementById("office").value =
            officer.office || "";

        document.getElementById("contact-number").value =
            officer.contactNumber || "";

        saveButton.disabled = false;

    } catch (error) {
        showMessage(error.message, true);
    }
}

profileForm.addEventListener("submit", async function (event) {
    event.preventDefault();

    const data = {
        fullName: document.getElementById("full-name").value.trim(),
        specialization: document.getElementById(
            "specialization"
        ).value.trim() || null,
        district: document.getElementById("district").value.trim() || null,
        office: document.getElementById("office").value.trim() || null,
        contactNumber: document.getElementById(
            "contact-number"
        ).value.trim() || null
    };

    saveButton.disabled = true;

    try {
        await apiRequest("/api/agricultureofficers/me", {
            method: "PUT",
            body: JSON.stringify(data)
        });

        showMessage("Profile updated successfully.");

    } catch (error) {
        showMessage(error.message, true);
    } finally {
        saveButton.disabled = false;
    }
});

loadProfile();
