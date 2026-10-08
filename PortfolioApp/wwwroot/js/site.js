document.addEventListener('DOMContentLoaded', function () {

    // ---------- Loading screen ----------
    const loadingScreen = document.getElementById('loading-screen');
    window.addEventListener('load', () => {
        setTimeout(() => loadingScreen?.classList.add('loaded'), 350);
    });

    // ---------- AOS init ----------
    if (window.AOS) {
        AOS.init({ duration: 700, once: true, offset: 60 });
    }

    // ---------- Typed.js hero roles ----------
    const typedEl = document.getElementById('typed-roles');
    if (typedEl && window.Typed) {
        const strings = (document.body.dataset.typedRoles || '.NET Developer,Full Stack Developer,C# Programmer')
            .split(',').filter(Boolean);
        new Typed('#typed-roles', {
            strings: strings.length ? strings : ['.NET Developer', 'Full Stack Developer'],
            typeSpeed: 55,
            backSpeed: 30,
            backDelay: 1500,
            loop: true
        });
    }

    // ---------- Custom cursor ----------
    const cursor = document.querySelector('.custom-cursor');
    const cursorDot = document.querySelector('.custom-cursor-dot');
    if (cursor && cursorDot) {
        window.addEventListener('mousemove', (e) => {
            cursor.style.left = e.clientX + 'px';
            cursor.style.top = e.clientY + 'px';
            cursorDot.style.left = e.clientX + 'px';
            cursorDot.style.top = e.clientY + 'px';
        });
        document.querySelectorAll('a, button, .glass-card').forEach(el => {
            el.addEventListener('mouseenter', () => cursor.style.transform = 'translate(-50%, -50%) scale(1.6)');
            el.addEventListener('mouseleave', () => cursor.style.transform = 'translate(-50%, -50%) scale(1)');
        });
    }

    // ---------- Scroll progress bar + navbar shadow + back to top ----------
    const progressBar = document.querySelector('.scroll-progress-bar');
    const navbar = document.getElementById('mainNavbar');
    const backToTop = document.getElementById('backToTop');

    window.addEventListener('scroll', () => {
        const scrollTop = window.scrollY;
        const docHeight = document.documentElement.scrollHeight - window.innerHeight;
        const pct = docHeight > 0 ? (scrollTop / docHeight) * 100 : 0;
        if (progressBar) progressBar.style.width = pct + '%';

        if (navbar) navbar.classList.toggle('scrolled', scrollTop > 40);
        if (backToTop) backToTop.classList.toggle('show', scrollTop > 400);
    });

    backToTop?.addEventListener('click', () => window.scrollTo({ top: 0, behavior: 'smooth' }));

    // ---------- Theme toggle (dark / light) ----------
    const themeToggle = document.getElementById('themeToggle');
    const root = document.documentElement;
    const savedTheme = localStorage.getItem('portfolio-theme');
    if (savedTheme) root.setAttribute('data-theme', savedTheme);

    themeToggle?.addEventListener('click', () => {
        const current = root.getAttribute('data-theme') === 'light' ? 'dark' : 'light';
        root.setAttribute('data-theme', current);
        localStorage.setItem('portfolio-theme', current);
    });

    // ---------- Animated counters ----------
    const counters = document.querySelectorAll('.stat-number[data-count]');
    const counterObserver = new IntersectionObserver((entries) => {
        entries.forEach(entry => {
            if (entry.isIntersecting) {
                animateCounter(entry.target);
                counterObserver.unobserve(entry.target);
            }
        });
    }, { threshold: 0.5 });
    counters.forEach(c => counterObserver.observe(c));

    function animateCounter(el) {
        const target = parseInt(el.dataset.count, 10) || 0;
        const duration = 1200;
        const start = performance.now();
        function step(now) {
            const progress = Math.min((now - start) / duration, 1);
            el.textContent = Math.floor(progress * target);
            if (progress < 1) requestAnimationFrame(step);
            else el.textContent = target;
        }
        requestAnimationFrame(step);
    }

    // ---------- Skill progress bars ----------
    const fills = document.querySelectorAll('.progress-fill[data-level]');
    const fillObserver = new IntersectionObserver((entries) => {
        entries.forEach(entry => {
            if (entry.isIntersecting) {
                entry.target.style.width = entry.target.dataset.level + '%';
                fillObserver.unobserve(entry.target);
            }
        });
    }, { threshold: 0.4 });
    fills.forEach(f => fillObserver.observe(f));

    // ---------- Copy to clipboard (email / phone) ----------
    document.querySelectorAll('.copy-btn').forEach(btn => {
        btn.addEventListener('click', () => {
            const text = btn.dataset.copy;
            navigator.clipboard?.writeText(text).then(() => {
                if (window.Swal) {
                    Swal.fire({ toast: true, position: 'bottom-end', icon: 'success', title: 'Copied!', showConfirmButton: false, timer: 1500, background: '#101425', color: '#e6e9f5' });
                }
            });
        });
    });

    // ---------- Share portfolio ----------
    document.getElementById('sharePortfolio')?.addEventListener('click', async () => {
        const shareData = { title: document.title, url: window.location.href };
        if (navigator.share) {
            try { await navigator.share(shareData); } catch (e) { /* user cancelled */ }
        } else {
            navigator.clipboard?.writeText(window.location.href);
            if (window.Swal) {
                Swal.fire({ toast: true, position: 'bottom-end', icon: 'info', title: 'Link copied to clipboard', showConfirmButton: false, timer: 1800, background: '#101425', color: '#e6e9f5' });
            }
        }
    });
});
