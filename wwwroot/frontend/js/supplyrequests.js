
initializeOfficerLayout(
    "requests",
    "Supply Request Review"
);

const requestsTable =
    document.getElementById("requests-list");

const submitReviewButton =
    document.getElementById("submit-review");

const selectedRequestText =
    document.getElementById("selected-request");

const requestDetailsText =
    document.getElementById("request-details");

let selectedRequestId = null;

// Display messages inside the requests table
function showRequestTableMessage(message) {
    requestsTable.replaceChildren();

    const row = requestsTable.insertRow();
    const cell = row.insertCell();

    cell.colSpan = 4;
    cell.textContent = message;
}

// Retrieve pending Supply Requests
async function loadPendingRequests() {
    selectedRequestId = null;
    submitReviewButton.disabled = true;

    selectedRequestText.textContent =
        "Select a supply request from the table.";

    requestDetailsText.textContent =
        "Product and quantity details will appear " +
        "after backend integration.";

    showRequestTableMessage("Loading requests...");

    try {
        const requests = await apiRequest(
            "/api/agricultureofficers/supplyrequests/pending"
        );

        requestsTable.replaceChildren();

        if (!Array.isArray(requests) ||
            requests.length === 0) {
            showRequestTableMessage(
                "No pending supply requests found."
            );
            return;
        }

        requests.forEach(request => {
            const row = requestsTable.insertRow();

            row.insertCell().textContent =
                request.supplyRequestId;

            row.insertCell().textContent =
                request.farmerId;

            row.insertCell().textContent =
                request.requestStatus || "Pending";

            const actionCell = row.insertCell();

            const viewButton =
                document.createElement("button");

            viewButton.type = "button";
            viewButton.className =
                "btn btn-secondary";

            viewButton.textContent = "View";

            viewButton.addEventListener(
                "click",
                function () {
                    selectRequest(request.supplyRequestId);
                }
            );

            actionCell.appendChild(viewButton);
        });

    } catch (error) {
        showRequestTableMessage(
            "Supply Request information is unavailable."
        );

        showMessage(error.message, true);
    }
}

// Retrieve details of the selected request
async function selectRequest(requestId) {
    selectedRequestId = requestId;

    selectedRequestText.textContent =
        "Selected Supply Request ID: " + requestId;

    requestDetailsText.textContent =
        "Loading request details...";

    submitReviewButton.disabled = true;

    try {
        const request = await apiRequest(
            `/api/agricultureofficers/supplyrequests/${requestId}`
        );

        requestDetailsText.textContent =
            "Request ID: " + request.supplyRequestId +
            " | Farmer ID: " + request.farmerId +
            " | Status: " + request.requestStatus;

        // Keep submission disabled until integration.

    } catch (error) {
        requestDetailsText.textContent =
            "Unable to retrieve request details.";

        showMessage(error.message, true);
    }
}

// Prevent review submission until database integration
document.getElementById(
    "review-form"
).addEventListener(
    "submit",
    function (event) {
        event.preventDefault();

        showMessage(
            "Saving a Supply Request review is " +
            "not yet implemented.",
            true
        );
    }
);

// Refresh button
document.getElementById(
    "refresh-requests"
).addEventListener(
    "click",
    loadPendingRequests
);

// Load pending requests when the page opens
loadPendingRequests();
