
/*
 * Shared API helper.
 * Assumes frontend and backend use the same origin.
 */

async function apiRequest(path, options = {}) {
    const headers = {
        Accept: "application/json",
        ...(options.headers || {})
    };

    if (options.body !== undefined) {
        headers["Content-Type"] = "application/json";
    }

    const response = await fetch(path, {
        ...options,
        headers
    });

    const contentType =
        response.headers.get("content-type") || "";

    let data = null;

    if (contentType.includes("application/json")) {
        data = await response.json();
    } else {
        data = await response.text();
    }

    if (!response.ok) {
        let message = "Request failed.";

        if (data && typeof data === "object") {
            message =
                data.message ||
                data.title ||
                data.detail ||
                message;
        }

        if (response.status === 401) {
            message = "Please sign in to access this feature.";
        } else if (response.status === 403) {
            message = "Your account is not authorized for this feature.";
        } else if (response.status === 501) {
            message = "This feature is awaiting backend integration.";
        }

        throw new Error(message);
    }

    return data;
}
