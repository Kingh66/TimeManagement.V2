// This function will be called when the document is ready
document.addEventListener("DOMContentLoaded", function () {
    console.log("site.js loaded!");

    // Add a click event to an element with an ID of "addModuleLink"
    var addModuleLink = document.getElementById("addModuleLink");

    if (addModuleLink) {
        addModuleLink.addEventListener("click", function (event) {
            // Prevent the default behavior of the link
            event.preventDefault();

            // Redirect to the Create action of the Module controller
            console.log("Redirecting to /Module/Create");
            window.location.href = "/Module/Create";
        });
    }
});
