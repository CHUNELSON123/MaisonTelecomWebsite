// Function callable from C#
window.scrollToTop = () => {
    window.scrollTo({ top: 0, left: 0, behavior: 'instant' });
};

// Listener for Blazor Enhanced Navigation (Static SSR)
// This ensures scroll resets even when not using InteractiveServer mode
if (typeof Blazor !== 'undefined') {
    Blazor.addEventListener('enhancedload', () => {
        window.scrollTo({ top: 0, left: 0, behavior: 'instant' });
    });
}