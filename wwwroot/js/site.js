(function() {
    const hamburger = document.querySelector('.hamburger');
    const navMenu = document.querySelector('.nav-menu');

    if (hamburger && navMenu) {
        hamburger.addEventListener('click', () => {
            navMenu.classList.toggle('active');
            hamburger.classList.toggle('active');
        });
    }

    document.querySelectorAll('.nav-link, .user-menu a').forEach(link => {
        link.addEventListener('click', (e) => {
            if (link.getAttribute('href').startsWith('#')) {
                e.preventDefault();
                const target = document.querySelector(link.getAttribute('href'));
                if (target) {
                    target.scrollIntoView({
                        behavior: 'smooth'
                    });
                }
            }
            
            if (navMenu) {
                navMenu.classList.remove('active');
                hamburger.classList.remove('active');
            }
        });
    });

    window.addEventListener('scroll', () => {
        const navbar = document.querySelector('.navbar');
        if (navbar) {
            if (window.scrollY > 100) {
                navbar.style.background = 'rgba(0, 0, 0, 0.95)';
            } else {
                navbar.style.background = 'rgba(0, 0, 0, 0.8)';
            }
        }
    });

    function checkScroll() {
        const elements = document.querySelectorAll('.content-block, .contacts, .services-section, .code-section');
        
        elements.forEach(element => {
            const elementPosition = element.getBoundingClientRect().top;
            const screenPosition = window.innerHeight / 1.3;
            
            if (elementPosition < screenPosition) {
                element.classList.add('animate-on-scroll');
            }
        });
    }

    window.addEventListener('scroll', checkScroll);
    window.addEventListener('load', checkScroll);

    const scrollButton = document.createElement('div');
    scrollButton.className = 'scroll-to-top';
    scrollButton.innerHTML = '<span>&uarr;</span>';
    document.body.appendChild(scrollButton);

    scrollButton.addEventListener('click', () => {
        window.scrollTo({
            top: 0,
            behavior: 'smooth'
        });
    });

    window.addEventListener('scroll', () => {
        if (window.scrollY > 500) {
            scrollButton.classList.add('visible');
        } else {
            scrollButton.classList.remove('visible');
        }
    });

    function highlightActiveSection() {
        const sections = document.querySelectorAll('section');
        const navLinks = document.querySelectorAll('.nav-link');
        
        let currentSection = '';
        
        sections.forEach(section => {
            const sectionTop = section.offsetTop;
            const sectionHeight = section.clientHeight;
            
            if (window.scrollY >= sectionTop - 100) {
                currentSection = section.getAttribute('id');
            }
        });
        
        navLinks.forEach(link => {
            link.classList.remove('active');
            if (link.getAttribute('href') === `#${currentSection}`) {
                link.classList.add('active');
            }
        });
    }

    window.addEventListener('scroll', highlightActiveSection);

    function updateThemeLabels(isDark) {
        const labels = document.querySelectorAll('.theme-label');
        if (labels.length >= 2) {
            labels[0].textContent = isDark ? 'Светлая' : 'Светлая';
            labels[1].textContent = isDark ? 'Тёмная' : 'Тёмная';
            
            labels[0].style.opacity = isDark ? '0.7' : '1';
            labels[1].style.opacity = isDark ? '1' : '0.7';
        }
    }

    const themeToggle = document.getElementById('theme-toggle');
    if (themeToggle) {
        const savedTheme = localStorage.getItem('theme');

        if (savedTheme) {
            document.documentElement.setAttribute('data-theme', savedTheme);
            themeToggle.checked = savedTheme === 'dark';
            updateThemeLabels(savedTheme === 'dark');
        }

        themeToggle.addEventListener('change', function() {
            if (this.checked) {
                document.documentElement.setAttribute('data-theme', 'dark');
                localStorage.setItem('theme', 'dark');
                updateThemeLabels(true);
            } else {
                document.documentElement.removeAttribute('data-theme');
                localStorage.setItem('theme', 'light');
                updateThemeLabels(false);
            }
        });
        
        updateThemeLabels(savedTheme === 'dark');
    }

    window.addEventListener('load', function() {
        setTimeout(function() {
            const loader = document.querySelector('.loader');
            if (loader) {
                loader.classList.add('hidden');
            }
        }, 1000);
    });
})();

function copyCode() {
    const codeElement = document.querySelector('.code-content code');
    const textArea = document.createElement('textarea');
    textArea.value = codeElement.textContent;
    document.body.appendChild(textArea);
    textArea.select();
    document.execCommand('copy');
    document.body.removeChild(textArea);
    
    const copyButton = document.querySelector('.copy-btn');
    copyButton.classList.add('copied');
    
    setTimeout(() => {
        copyButton.classList.remove('copied');
    }, 2000);
}