function submitForm(element) {
    // NOTE: This is a hack as the "Script" variable in our REPL does not update when focus isn't changed from the text area when typing inside of it
    // Forcing the focus away from the TextArea forces the script variable to update and will have the correct value when clicked.
    // We then restore the focus back to the previous active element for a cleaner user experience.
    const previousFocus = document.activeElement;
    element.focus();
    element.click();
    previousFocus.focus();
}