// ---- Auto-update footer year ----
const yearEl = document.getElementById('year');
if (yearEl) {
    yearEl.textContent = new Date().getFullYear();
}

// ---- Fade-in sections as they scroll into view ----
const revealElements = document.querySelectorAll('.reveal');

if ('IntersectionObserver' in window) {
    const revealOnScroll = new IntersectionObserver(
        (entries, observer) => {
            entries.forEach((entry) => {
                if (entry.isIntersecting) {
                    entry.target.classList.add('visible');
                    observer.unobserve(entry.target);
                }
            });
        },
        { threshold: 0.15 }
    );

    revealElements.forEach((section) => revealOnScroll.observe(section));
} else {
    // Fallback for older browsers without IntersectionObserver support
    revealElements.forEach((section) => section.classList.add('visible'));
}

// ---- Click-to-copy for email and WhatsApp number ----
const contactItems = document.querySelectorAll('#contacts li');

contactItems.forEach((item) => {
    item.style.cursor = 'pointer';
    item.title = 'Click to copy';

    item.addEventListener('click', () => {
        const text = item.textContent.split(':').slice(1).join(':').trim();

        navigator.clipboard.writeText(text).then(() => {
            const original = item.textContent;
            item.textContent = 'Copied!';
            setTimeout(() => {
                item.textContent = original;
            }, 1200);
        });
    });
});