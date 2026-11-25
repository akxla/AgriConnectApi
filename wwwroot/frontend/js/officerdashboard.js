
initializeOfficerLayout("dashboard", "Officer Dashboard");

function formatDate(value) {
    if (!value) return "—";

    const date = new Date(value);

    if (Number.isNaN(date.getTime())) {
        return "—";
    }

    return date.toLocaleDateString();
}

async function loadDashboard() {
    const tableBody =
        document.getElementById("recent-recommendations");

    try {
        const dashboard = await apiRequest(
            "/api/agricultureofficers/dashboard"
        );

        document.getElementById("welcome").textContent =
            "Welcome, " + dashboard.fullName;

        const officerDetails = [
            dashboard.employeeNumber,
            dashboard.specialization,
            dashboard.district
        ].filter(Boolean);

        document.getElementById("officer-info").textContent =
            officerDetails.join(" | ") || "Officer Profile";

        document.getElementById(
            "total-recommendations"
        ).textContent = dashboard.totalRecommendations;

        document.getElementById(
            "published-recommendations"
        ).textContent = dashboard.publishedRecommendations;

        document.getElementById(
            "draft-recommendations"
        ).textContent = dashboard.draftRecommendations;

        document.getElementById(
            "archived-recommendations"
        ).textContent = dashboard.archivedRecommendations;

        tableBody.replaceChildren();

        const recent = dashboard.recentRecommendations || [];

        if (recent.length === 0) {
            const row = tableBody.insertRow();
            const cell = row.insertCell();
            cell.colSpan = 4;
            cell.textContent = "No recommendations found.";
            return;
        }

        recent.forEach(item => {
            const row = tableBody.insertRow();

            row.insertCell().textContent = item.title;
            row.insertCell().textContent = item.category || "—";
            row.insertCell().textContent = item.status;
            row.insertCell().textContent =
                formatDate(item.createdAt);
        });

    } catch (error) {
        tableBody.replaceChildren();

        const row = tableBody.insertRow();
        const cell = row.insertCell();
        cell.colSpan = 4;
        cell.textContent = "Dashboard data unavailable.";

        showMessage(error.message, true);
    }
}

loadDashboard();
