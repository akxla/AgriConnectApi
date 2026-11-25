
function initializeOfficerLayout(activePage, pageTitle) {
    const links = [
        { id: "dashboard", text: "Dashboard", href: "dashboard.html" },
        { id: "profile", text: "My Profile", href: "profile.html" },
        { id: "recommendations", text: "Recommendations", href: "recommendations.html" },
        { id: "quotations", text: "Quotation Verification", href: "quotations.html" },
        { id: "requests", text: "Supply Request Review", href: "requests.html" }
    ];

    const sidebar = document.getElementById("sidebar");

    if (sidebar) {
        const logo = document.createElement("div");
        logo.className = "logo";
        logo.textContent = "AgriConnect";
        sidebar.appendChild(logo);

        links.forEach(link => {
            const a = document.createElement("a");
            a.href = link.href;
            a.textContent = link.text;

            if (link.id === activePage) {
                a.classList.add("active");
                a.setAttribute("aria-current", "page");
            }

            sidebar.appendChild(a);
        });
    }

    const heading = document.getElementById("page-title");

    if (heading) {
        heading.textContent = pageTitle;
    }
}

function showMessage(message, isError = false) {
    const element = document.getElementById("message");

    if (!element) return;

    element.textContent = message;
    element.className = isError
        ? "message error"
        : "message";
    element.hidden = false;
}
