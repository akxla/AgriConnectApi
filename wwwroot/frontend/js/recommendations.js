
initializeOfficerLayout(
    "recommendations",
    "Recommendations"
);

const recommendationForm =
    document.getElementById("recommendation-form");

const recommendationsTable =
    document.getElementById("recommendations-list");

const saveRecommendationButton =
    document.getElementById("save-recommendation");

const cancelEditButton =
    document.getElementById("cancel-edit");

let editingRecommendationId = null;
let myRecommendations = [];

function clearRecommendationForm() {
    recommendationForm.reset();

    editingRecommendationId = null;

    document.getElementById("form-title").textContent =
        "Create Recommendation";

    saveRecommendationButton.textContent =
        "Save Recommendation";

    cancelEditButton.hidden = true;
}

function showEmptyRecommendations(message) {
    recommendationsTable.replaceChildren();

    const row = recommendationsTable.insertRow();
    const cell = row.insertCell();

    cell.colSpan = 5;
    cell.textContent = message;
}

function createActionButton(label, callback, className) {
    const button = document.createElement("button");

    button.type = "button";
    button.textContent = label;
    button.className = className;
    button.addEventListener("click", callback);

    return button;
}

function renderRecommendations() {
    recommendationsTable.replaceChildren();

    if (myRecommendations.length === 0) {
        showEmptyRecommendations(
            "You have no recommendations yet."
        );
        return;
    }

    myRecommendations.forEach(item => {
        const row = recommendationsTable.insertRow();

        row.insertCell().textContent =
            item.title || "—";

        row.insertCell().textContent =
            item.category || "—";

        row.insertCell().textContent =
            item.status || "—";

        const createdAt = item.createdAt
            ? new Date(item.createdAt).toLocaleDateString()
            : "—";

        row.insertCell().textContent = createdAt;

        const actionsCell = row.insertCell();
        const actions = document.createElement("div");
        actions.className = "actions";

        if (item.status !== "Archived") {
            actions.appendChild(
                createActionButton(
                    "Edit",
                    () => startEditing(item),
                    "btn btn-secondary"
                )
            );

            actions.appendChild(
                createActionButton(
                    "Archive",
                    () => archiveRecommendation(
                        item.recommendationId
                    ),
                    "btn btn-danger"
                )
            );
        }

        actionsCell.appendChild(actions);
    });
}

async function loadRecommendations() {
    try {
        const data = await apiRequest(
            "/api/recommendations/mine"
        );

        myRecommendations = Array.isArray(data)
            ? data
            : [];

        renderRecommendations();

    } catch (error) {
        showEmptyRecommendations(
            "Unable to load recommendations."
        );

        showMessage(error.message, true);
    }
}

function startEditing(item) {
    if (item.status === "Archived") {
        showMessage(
            "Archived recommendations cannot be edited.",
            true
        );
        return;
    }

    editingRecommendationId =
        item.recommendationId;

    document.getElementById("title").value =
        item.title || "";

    document.getElementById("description").value =
        item.description || "";

    document.getElementById("category").value =
        item.category || "";

    document.getElementById("status").value =
        item.status || "Draft";

    document.getElementById("form-title").textContent =
        "Edit Recommendation";

    saveRecommendationButton.textContent =
        "Update Recommendation";

    cancelEditButton.hidden = false;

    window.scrollTo({ top: 0, behavior: "smooth" });
}

recommendationForm.addEventListener(
    "submit",
    async function (event) {
        event.preventDefault();

        const data = {
            title: document.getElementById("title")
                .value.trim(),

            description: document.getElementById(
                "description"
            ).value.trim(),

            category: document.getElementById("category")
                .value.trim() || null,

            status: document.getElementById("status").value
        };

        const isEditing =
            editingRecommendationId !== null;

        const endpoint = isEditing
            ? `/api/recommendations/${editingRecommendationId}`
            : "/api/recommendations";

        const method = isEditing ? "PUT" : "POST";

        saveRecommendationButton.disabled = true;

        try {
            await apiRequest(endpoint, {
                method,
                body: JSON.stringify(data)
            });

            clearRecommendationForm();

            showMessage(
                isEditing
                    ? "Recommendation updated successfully."
                    : "Recommendation created successfully."
            );

            await loadRecommendations();

        } catch (error) {
            showMessage(error.message, true);

        } finally {
            saveRecommendationButton.disabled = false;
        }
    }
);

async function archiveRecommendation(id) {
    const confirmed = confirm(
        "Do you want to archive this recommendation?"
    );

    if (!confirmed) return;

    try {
        await apiRequest(`/api/recommendations/${id}`, {
            method: "DELETE"
        });

        showMessage(
            "Recommendation archived successfully."
        );

        await loadRecommendations();

    } catch (error) {
        showMessage(error.message, true);
    }
}

cancelEditButton.addEventListener(
    "click",
    clearRecommendationForm
);

document.getElementById(
    "refresh-recommendations"
).addEventListener("click", loadRecommendations);

loadRecommendations();
